using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using Math = System.MathF;

namespace Core.Objects {
	public class Sphere : RenderObject {
		Vector3 O;
		float R;

		public Sphere(Vector3 o, float r) {
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
			float a = Vector3.Dot(B, B);
			float b = Vector3.Dot(B, (A - C)) * 2.0f;
			float c = (A - C).LengthSquared() - R * R;


			float drt = b * b - 4 * a * c;
			if (drt < 0) {
				return (float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
			}
			drt = Math.Sqrt(drt);
			float x1 = (-b + drt) / a / 2;
			float x2 = (-b - drt) / a / 2;
			if (x1 < 0 && x2 < 0) {
				return (float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
			}

			float d;
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
			float distance = (ray.Direction * d).Length();
			Vector3 normal = Vector3.Normalize(point - O);
			return (d, point, normal);
		}

		/// <summary>
		/// 相交深度测试
		/// </summary>
		/// <param name="ray"></param>
		/// <returns>距离， 相交点，法线</returns>
		public override (float, Vector3, Vector3) IntersectDeep(Ray ray) {
			Vector3 A = ray.Origin, B = ray.Direction, C = O;
			float a = Vector3.Dot(B, B);
			float b = Vector3.Dot(B, (A - C)) * 2.0f;
			float c = (A - C).LengthSquared() - R * R;


			float drt = b * b - 4 * a * c;
			if (drt < 0) {
				return (float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
			}
			drt = Math.Sqrt(drt);
			float x1 = (-b + drt) / a / 2;
			float x2 = (-b - drt) / a / 2;
			if (x1 < 0 && x2 < 0) {
				return (float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
			}

			float d;
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
			float distance = (ray.Direction * d).Length();
			Vector3 normal = Vector3.Normalize(point - O);
			return (d, point, normal);
		}
	}
}
