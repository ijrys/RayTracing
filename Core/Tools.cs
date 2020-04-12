using System;
using System.Collections.Generic;
using System.Text;

using Vector3 = System.Numerics.Vector3;

namespace Core {
	public static class Tools {
		public static Random random = new Random();

		public static float Clamp(float o, float min, float max) {
			if (o < min) return min;
			if (o > max) return max;
			return o;
		}
		/// <summary>
		/// 角度转为弧度
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static float Radian(float angle) {
			return MathF.PI / 180 * angle;
		}

		public static float Length(float x, float y) {
			return MathF.Sqrt(x * x + y * y);
		}
		public static float Length(float x, float y, float z) {
			return MathF.Sqrt(x * x + y * y + z * z);
		}

		public static Vector3 RandomPointInSphere() {
			double x = random.NextDouble() * 2.0 - 1.0, y = random.NextDouble() * 2.0 - 1.0, z = random.NextDouble() * 2.0 - 1.0;
			while (x * x + y * y + z * z > 1.0f) {
				x = random.NextDouble() * 2.0 - 1.0;
				y = random.NextDouble() * 2.0 - 1.0;
				z = random.NextDouble() * 2.0 - 1.0;
			}
			Vector3 re = new Vector3((float)x, (float)y, (float)z);
			return re;
		}

		public static float LightStrongByDistance(float distance) {
			if (distance < 0) return 1.0f;
			if (distance > 1000) { distance = 1000; };
			distance = MathF.Log10(distance + 1.2f) + 1.1f;
			distance = 1 / distance;
			return distance;
		}

		public static float RandomIn(float min, float max) {
			float d = (float)random.NextDouble();
			return (float)(min * d + max * (1.0f - d));
		}

		/// <summary>
		/// 获取入射方向在法线n的反射
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		public static Vector3 Reflect(Vector3 dir, Vector3 n) {
			return dir - Vector3.Dot(dir, n) * 2.0f * n;
		}

		/// <summary>
		/// 返回折射光方向和能量强度
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="niOverNt"></param>
		/// <returns></returns>
		public static (float, Vector3) Refract(Vector3 dir, Vector3 normal, float niOverNt) {
			dir = -Vector3.Normalize(dir);
			float cosAlpha = Vector3.Dot(dir, normal);
			float discriminant = 1.0f - niOverNt * niOverNt * (1.0f - cosAlpha * cosAlpha);
			if (discriminant > 0) {
				float cosGamma = MathF.Sqrt(discriminant);
				Vector3 re = ((normal * cosAlpha) - dir) * niOverNt - normal * cosGamma;
				return (1.0f - Schlick(cosAlpha, niOverNt), re);
			}
			else {
				return (float.NegativeInfinity, Vector3.Zero);
			}
		}

		/// <summary>
		/// 折射光线强度
		/// https://en.wikipedia.org/wiki/Schlick%27s_approximation
		/// </summary>
		/// <param name="cosine"></param>
		/// <param name="ref_idx"></param>
		/// <returns></returns>
		public static float Schlick(float cosine, float ref_idx) {
			float r0 = (1.0f - ref_idx) / (1.0f + ref_idx);
			r0 = r0 * r0;
			float tmp = (1 - cosine);
			float re = tmp * tmp;
			re = re * re * tmp;
			return r0 + (1 - r0) * re;
		}
	}
}
