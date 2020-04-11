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

		public Sphere(Vector3 o, Float r) {
			O = o;
			R = r;
		}

		/// <summary>
		/// 内部相交
		/// </summary>
		/// <param name="ray"></param>
		/// <returns></returns>
		public override (float, Vector3, Vector3) InterIntersect(Ray ray) {
			Vector3 A = ray.Origin, B = ray.Direction, C = O;
			Float a = B * B;
			Float b = 2 * B * (A - C);
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
				d = Math.Max(x1, x2);
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

		/// <summary>
		/// 相交深度测试
		/// </summary>
		/// <param name="ray"></param>
		/// <returns>距离， 相交点，法线</returns>
		public override (Float, Vector3, Vector3) IntersectDeep(Ray ray) {
			Vector3 A = ray.Origin, B = ray.Direction, C = O;
			Float a = B * B;
			Float b = 2 * B * (A - C);
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
