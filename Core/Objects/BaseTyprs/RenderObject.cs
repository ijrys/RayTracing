#if UseDouble
using Float = System.Double;
#else
using Core.Materials;
using System;
using Float = System.Single;
#endif

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
		public abstract (Float, Vector3, Vector3) IntersectDeep(Ray ray);
		/// <summary>
		/// 内相交信息，用于计算折射光线的出射点
		/// </summary>
		/// <param name="ray"></param>
		/// <returns>距离，焦点，焦平面法线</returns>
		public abstract (Float, Vector3, Vector3) InterIntersect(Ray ray);

		/// <summary>
		/// 相交点辐射光
		/// </summary>
		/// <param name="point"></param>
		/// <param name="normal"></param>
		/// <param name="deep"></param>
		/// <returns></returns>
		public virtual LightStrong IntersectLight(Vector3 point, Vector3 dir, Vector3 normal, int deep) {
			// 发光体返回发光颜色
			if (Material.LightAble) {
				return Material.LightColor;
			}
			// 递归深度极限
			if (deep <= 1) {
				return Material.BaseColor * 0.4f;
			}


			#region 计算反射光
			LightStrong reflectl = default; // 反射光
			{
				int smapL = RenderConfiguration.Configurations.ReflectSmapingLevel - RenderConfiguration.Configurations.RayTraceDeep + deep;
				if (smapL < 1) smapL = 1;
				smapL = smapL * 3 - 2;

				int raycount = 1;
				smapL = (int)(smapL * (Material.AMetalDegree));
				if (smapL < 1) smapL = 1;

				raycount = smapL;
				Vector3 spO;
				{
					Vector3 spRO = Tools.Reflect(dir, normal);
					spO = normal * (1.0f - Material.MetalDegree) + spRO * Material.MetalDegree;
				}
				for (int nsmap = 0; nsmap < smapL; nsmap++) {
					Vector3 tp = Tools.RandomPointInSphere() * Material.AMetalDegree + spO;
					Vector3 raydir = tp;
					while (raydir.LengthSquared() < 0.1) {
						tp = Tools.RandomPointInSphere() + spO;
						raydir = tp;
					}
					Ray r = new Ray(point, raydir);

					LightStrong c = Scene.Light(r, deep - 1, this);
					reflectl += c;
				}
				reflectl /= raycount;
				reflectl *= (0.06f * Material.MetalDegree + 0.93f) * Material.BaseColor;
			}
			#endregion

			#region 计算折射光
			LightStrong refractl = default; // 折射光
			float refractPower = 0.0f; // 折射光强度
			if (Material.IsTransparent) {
				//Vector3 outwardNormal;
				//float ni_over_nt;
				//float cosine;
				//float innormaldot = dir.Dot( normal);
				//if (innormaldot < 0) {
				//	outwardNormal = -normal;
				//	ni_over_nt = Material.RefractiveIndices;
				//	cosine = Material.RefractiveIndices * dir.Dot(normal) / dir.Length();
				//} else {
				//	outwardNormal = normal;
				//	ni_over_nt = 1.0f / Material.RefractiveIndices;
				//	cosine = -dir.Dot(normal) / dir.Length();
				//}

				(float pow, Vector3 rdir) = Tools.Refract(dir, normal, Material.RefractiveIndices);
				if (pow < 0) {
					goto endRefract;
					rdir = Tools.Reflect(dir, normal);
					pow = -pow;
				}
				refractPower = MathF.Sqrt(pow);
				refractl = Scene.Light(new Ray(point, rdir), deep - 1, this) * Material.BaseColor;

				//(Float rdis, Vector3 opoint, Vector3 onormal) = InterIntersect(new Ray(point, rdir));
				//(float opow, Vector3 odir) = Tools.Refract(rdir, -onormal, 1.0f / Material.RefractiveIndices);
				//if (opow < 0.0f) { // 内部发生全反射，多一次计算
				//	// 反射计算


				//	(rdis, opoint, onormal) = InterIntersect(new Ray(opoint, odir));
				//	(opow, odir) = Tools.Refract(rdir, -onormal, 1.0f / Material.RefractiveIndices);




				//	float reflectpower = (0.299f * reflectl.R + 0.587f * reflectl.G + 0.114f * reflectl.B) / 3.0f;
				//	refractl = new LightStrong(reflectpower, reflectpower, reflectpower);
				//	goto endRefract;
				//}
				//refractl = Scene.Light(new Ray(opoint, odir), deep - 1, this);

			}

		endRefract:
			#endregion

			return refractl * refractPower + reflectl * (1.0f - refractPower);
		}

		public RenderObject() { }
		public RenderObject(Vector3 position) : base(position) { }
	}
}
