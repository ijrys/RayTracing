using Core.Debug;
using Core.Materials;
using System;

using Math = System.MathF;

using Vector3 = System.Numerics.Vector3;

namespace Core.Objects {
	public abstract class RenderObject : SceneObject { //, IRenderAble {
		private Material _material;
		public Material Material {
			get {
				if (_material == null) {
					_material = new Material();
				}
				return _material;
			}
			set {
				if (value == null) {
					value = new Material();
				}
				_material = value;
			}
		}

		/// <summary>
		/// 相交点信息
		/// </summary>
		/// <param name="ray"></param>
		/// <returns>距离，焦点，焦平面法线</returns>
		public abstract (float, Vector3, Vector3) IntersectDeep(Ray ray);
		/// <summary>
		/// 内相交信息，用于计算折射光线的出射点
		/// </summary>
		/// <param name="ray"></param>
		/// <returns>距离，焦点，焦平面法线</returns>
		public abstract (float, Vector3, Vector3) InterIntersect(Ray ray);

		/// <summary>
		/// 相交点辐射光
		/// </summary>
		/// <param name="point"></param>
		/// <param name="normal"></param>
		/// <param name="deep"></param>
		/// <returns></returns>
		public virtual LightStrong IntersectLight(Vector3 point, Vector3 dir, Vector3 normal, int deep) {
#if RayDebugger
			SceneDebug Debugger = Scene.debugger;
			if (Debugger != null) {
				Debugger.BeginBranch(point);
			}
#endif

			LightStrong returnlight = default;
			// 发光体返回发光颜色
			if (Material.LightAble) {
				returnlight = Material.LightColor;
				goto returnPoint;
			}
			// 递归深度极限
			if (deep <= 1) {
				returnlight = Material.BaseColor * 0.3f;
				goto returnPoint;
			}

			dir = Vector3.Normalize(dir);
			bool IsBackFace = false;
			if (Vector3.Dot(dir, normal) > 0) { // 背面
				IsBackFace = true;
				normal = -normal;
			}

			#region 计算最大光强
			//{
			//	float maxLight = Material.LightColor.R;
			//	maxLight = Math.Max(maxLight, Material.LightColor.G);
			//	maxLight = Math.Max(maxLight, Material.LightColor.B);
			//	maxLightStrong *= maxLight;
			//} 
			#endregion

			// 计算追踪光线总数
			int traceRayNum = RenderConfiguration.Configurations.ReflectSmapingLevel - RenderConfiguration.Configurations.RayTraceDeep + deep;
			traceRayNum = (int)(traceRayNum * Material.AMetalDegree);
			if (traceRayNum < 1) traceRayNum = 1;
			traceRayNum = traceRayNum * 3 - 2;

			#region 计算折射光
			LightStrong refractl = default; // 折射光
			float refractPower = 0.0f; // 折射光强度
			if (Material.IsTransparent) {
				float riindex = Material.RefractiveIndices;
				if (!IsBackFace) riindex = 1.0f / riindex;
				// 计算折射光线
				(float pow, Vector3 rdir) = Tools.Refract(dir, normal, riindex);
				if (pow < 0) {
					goto endRefract;
				}

				refractPower = pow * Material.TransparentIndex;

				int raycount = (int)(traceRayNum * refractPower);
				traceRayNum -= raycount;

				float randomScale = Material.AMetalDegree * Material.AMetalDegree * 0.5f;
				raycount = (int)(traceRayNum * randomScale);
				if (raycount < 1 && refractPower > 0.00001) raycount = 1;

				if (raycount == 0) {
					refractl = Material.BaseColor;
					goto endRefract;
				}

				rdir = normal * randomScale + rdir * (1.0f - randomScale);

				for (int nsmap = 0; nsmap < raycount; nsmap++) {
					Vector3 raydir = Tools.RandomPointInSphere() * randomScale + rdir;

					Ray r = new Ray(point, raydir);
					//Console.WriteLine('\t' + this.Name + " [refract] : " + r);
					(LightStrong c, float distance) = Scene.Light(r, deep - 1, this);

					if (IsBackFace) { //内部光线，进行吸收计算
						float xsl = Math.Log(distance + 1.0f) + 1.0f;
						refractl *= Material.BaseColor / xsl;
					}
					refractl += c;
				}
				refractl /= raycount;
			}

		endRefract:
			#endregion

			#region 计算反射光
			LightStrong reflectl = default; // 反射光
			{
				int raycount = traceRayNum;
				if (raycount < 1 && refractPower < 0.99f) {
					raycount = 1;
				}
				Vector3 spO;
				{
					Vector3 spRO = Tools.Reflect(dir, normal);
					spO = normal * (1.0f - Material.MetalDegree) + spRO * Material.MetalDegree;
				}
				for (int nsmap = 0; nsmap < raycount; nsmap++) {
					Vector3 tp = Tools.RandomPointInSphere() * Material.AMetalDegree + spO;
					Vector3 raydir = tp;
					while (raydir.LengthSquared() < 0.1) {
						tp = Tools.RandomPointInSphere() + spO;
						raydir = tp;
					}

					Ray r = new Ray(point, raydir);
					//Console.WriteLine('\t' + this.Name + " [reflact] : " + r);
					(LightStrong c, float _) = Scene.Light(r, deep - 1, this); //, this);
					reflectl += c;
				}
				reflectl /= raycount;
				reflectl *= (0.06f * Material.MetalDegree + 0.93f) * Material.BaseColor;
			}
			#endregion

			returnlight = refractl * refractPower + reflectl * (1.0f - refractPower);

		returnPoint:
#if RayDebugger
			if (Debugger != null) {
				Debugger.EndBranch();
			}
#endif
			return returnlight;
		}

		public RenderObject() { }
		public RenderObject(Vector3 position) : base(position) { }
	}
}
