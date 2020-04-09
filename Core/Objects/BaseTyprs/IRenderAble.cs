#if UseDouble
using Float = System.Double;
#else
using Core.Materials;
using Float = System.Single;
#endif

namespace Core.Objects {
	public interface IRenderAble {
		public Material Material { get; set; }

		/// <summary>
		/// 返回光追辐射值
		/// </summary>
		/// <param name="point"></param>
		/// <param name="normal"></param>
		/// <param name="deep"></param>
		/// <returns></returns>
		public LightStrong IntersectLight(Vector3 point, Vector3 dir, Vector3 normal, int deep);

		/// <summary>
		/// 返回光追颜色
		/// </summary>
		/// <param name="ray"></param>
		/// <returns></returns>
		public LightStrong IntersectColor(Vector3 point, Vector3 dir, Vector3 normal, int deep);

		/// <summary>
		/// 相交测试，返回相交距离和焦点。不相交返回负值
		/// </summary>
		/// <param name="ray"></param>
		/// <returns></returns>
		public (Float, Vector3, Vector3) IntersectDeep(Ray ray);
	}
}
