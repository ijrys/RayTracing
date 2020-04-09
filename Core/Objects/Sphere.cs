using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
#if UseDouble
using Float = System.Double;
using Math = System.Math;
#else
using Float = System.Single;
using Math = System.MathF;
#endif

namespace Core.Objects {
	public class Sphere : RenderObject {
		Vector3 O;
		Float R;

		//public static LightStrong DefaultColor = new LightStrong(1.0f, 0.5f, 0);
		//public LightStrong Color { get; set; } = DefaultColor;

		public Sphere(Vector3 o, Float r) {
			O = o;
			R = r;
		}

		//public override LightStrong IntersectLight(Vector3 point, Vector3 normal, int deep) {
		//	return base.IntersectLight(point, normal, deep);
		//}

		//public override LightStrong IntersectColor(Vector3 p, Vector3 normal, int deep) {
		//	if (deep <= 1) {
		//		return Color * 0.1f;
		//	}
		//	int smapL = RenderConfiguration.Configurations.SmapingLevel - RenderConfiguration.Configurations.RayTraceDeep + deep;
		//	if (smapL < 1) smapL = 1;
		//	smapL = smapL * smapL;
		//	LightStrong l = default;
		//	for (int nsmap = 0; nsmap < smapL; nsmap++) {
		//		Vector3 spO = normal + p;
		//		Vector3 tp = Tools.RandomPointInSphere() + spO;
		//		Vector3 dir = tp - p;
		//		while (dir.LengthSquared() < 0.1) {
		//			tp = Tools.RandomPointInSphere() + spO;
		//			dir = tp - p;
		//		}
		//		Ray r = new Ray(p, tp - p);

		//		LightStrong c = Scene.Light(r, deep - 1, this);
		//		l += c;
		//	}
		//	l /= smapL;
		//	//Float lstrong = l.Strong();

		//	//return Color * lstrong * 0.01f + Color * l * lstrong * 0.9f;
		//	return Color * l;
		//}

		/// <summary>
		/// 相交深度测试
		/// </summary>
		/// <param name="ray"></param>
		/// <returns>距离， 相交点，法线</returns>
		public override (Float, Vector3, Vector3) IntersectDeep(Ray ray) {
			Vector3 A = ray.Origin, B = ray.Direction, C = O;
			Float a = B & B;
			Float b = 2 * B & (A - C);
			Float c = (A - C).LengthSquared() - R * R;


			Float drt = b * b - 4 * a * c;
			if (drt < 0) {
				return (Float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
			}
			drt = Math.Sqrt(drt);
			Float x1 = (-b + drt) / a / 2;
			Float x2 = (-b - drt) / a / 2;
			if (x1 < 0 && x2 < 0) {
				return (Float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
			}

			Float d;
			if (x1 > 0 && x2 > 0) {
				d = Math.Min(x1, x2);
			}
			else if (x1 > 0) {
				d = x1;
			}
			else {
				d = x2;
			}
			Vector3 point = ray.Origin + ray.Direction * d;
			Float distance = (ray.Direction * d).Length();
			Vector3 normal = (point - O).Normalize();
			return (d, point, normal);
		}
	}
}
