#if UseDouble
using Float = System.Double;
#else
using Core.Materials;
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
		/// <returns></returns>
		public abstract (Float, Vector3, Vector3) IntersectDeep(Ray ray);

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

			int smapL = RenderConfiguration.Configurations.ReflectSmapingLevel - RenderConfiguration.Configurations.RayTraceDeep + deep;
			if (smapL < 1) smapL = 1;
			smapL = smapL * 3 - 2;
			LightStrong l = default;
			#region 仅计算Color
			{
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
					l += c;
				}
				l /= raycount;
				l *= (0.06f * Material.MetalDegree + 0.93f);
			}
#endregion

			return Material.BaseColor * l;
		}

		public RenderObject() { }
		public RenderObject(Vector3 position) : base(position) { }
	}
}
