using System;
using System.Collections.Generic;
using System.Text;

#if UseDouble
using Float = System.Double;
using Math = System.Math;
#else
using Float = System.Single;
using Math = System.MathF;
#endif


namespace Core {
	public static class Tools {
		public static Random random = new Random();

		public static Float Clamp(Float o, Float min, Float max) {
			if (o < min) return min;
			if (o > max) return max;
			return o;
		}
		/// <summary>
		/// 角度转为弧度
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Float Radian(Float angle) {
			return Math.PI / 180 * angle;
		}

		public static Float Length(Float x, Float y) {
			return Math.Sqrt(x * x + y * y);
		}
		public static Float Length(Float x, Float y, Float z) {
			return Math.Sqrt(x * x + y * y + z * z);
		}

		public static Vector3 RandomPointInSphere() {
			double x = random.NextDouble() * 2.0 - 1.0, y = random.NextDouble() * 2.0 - 1.0, z = random.NextDouble() * 2.0 - 1.0;
			while (x * x + y * y + z * z > 1.0f) {
				x = random.NextDouble() * 2.0 - 1.0;
				y = random.NextDouble() * 2.0 - 1.0;
				z = random.NextDouble() * 2.0 - 1.0;
			}
			Vector3 re = new Vector3((Float)x, (Float)y, (Float)z);
			return re;
		}

		private readonly static Vector3[][] ReflectDirection = new Vector3[8][] {
			// 1
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),
			},
			// 2
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.707106781186547f, 0.707106781186548f, 0.0f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, 0.612372435695794f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, -0.612372435695794f),
			},
			// 3
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.5f, 0.866025403784439f, 0.0f),
				new Vector3 (-0.25f, 0.866025403784439f, 0.433012701892219f),
				new Vector3 (-0.25f, 0.866025403784439f, -0.433012701892219f),

				new Vector3 (0.433012701892219f, 0.5f, 0.75f),
				new Vector3 (-0.866025403784439f, 0.5f, 0.0f),
				new Vector3 (0.433012701892219f, 0.5f, -0.75f),
			},
			// 4
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.422618261740699f, 0.90630778703665f, 0.0f),
				new Vector3 (-0.21130913087035f, 0.90630778703665f, 0.365998150770667f),
				new Vector3 (-0.21130913087035f, 0.90630778703665f, -0.365998150770667f),

				new Vector3 (0.383022221559489f, 0.642787609686539f, 0.663413948168938f),
				new Vector3 (-0.766044443118978f, 0.642787609686539f, 0.0f),
				new Vector3 (0.383022221559489f, 0.642787609686539f, -0.663413948168938f),

				new Vector3 (0.965925826289068f, 0.258819045102521f, 0.0f),
				new Vector3 (-0.482962913144534f, 0.258819045102521f, 0.836516303737808f),
				new Vector3 (-0.482962913144535f, 0.258819045102521f, -0.836516303737808f),
			},
			// 5
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.342020143325669f, 0.939692620785908f, 0.0f),
				new Vector3 (-0.171010071662834f, 0.939692620785908f, 0.296198132726024f),
				new Vector3 (-0.171010071662834f, 0.939692620785908f, -0.296198132726024f),

				new Vector3 (0.32139380484327f, 0.766044443118978f, 0.556670399226419f),
				new Vector3 (-0.642787609686539f, 0.766044443118978f, 0.0f),
				new Vector3 (0.32139380484327f, 0.766044443118978f, -0.556670399226419f),

				new Vector3 (0.866025403784439f, 0.5f, 0.0f),
				new Vector3 (-0.433012701892219f, 0.5f, 0.75f),
				new Vector3 (-0.43301270189222f, 0.5f, -0.75f),

				new Vector3 (0.492403876506104f, 0.17364817766693f, 0.852868531952443f),
				new Vector3 (-0.984807753012208f, 0.17364817766693f, 0.0f),
				new Vector3 (0.492403876506104f, 0.17364817766693f, -0.852868531952443f),
			},
			// 6
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.258819045102521f, 0.965925826289068f, 0.0f),
				new Vector3 (-0.12940952255126f, 0.965925826289068f, 0.224143868042013f),
				new Vector3 (-0.12940952255126f, 0.965925826289068f, -0.224143868042013f),

				new Vector3 (0.25f, 0.866025403784439f, 0.433012701892219f),
				new Vector3 (-0.5f, 0.866025403784439f, 0.0f),
				new Vector3 (0.25f, 0.866025403784439f, -0.433012701892219f),

				new Vector3 (0.707106781186547f, 0.707106781186548f, 0.0f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, 0.612372435695794f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, -0.612372435695794f),

				new Vector3 (0.433012701892219f, 0.5f, 0.75f),
				new Vector3 (-0.866025403784439f, 0.5f, 0.0f),
				new Vector3 (0.433012701892219f, 0.5f, -0.75f),

				new Vector3 (0.965925826289068f, 0.258819045102521f, 0.0f),
				new Vector3 (-0.482962913144534f, 0.258819045102521f, 0.836516303737808f),
				new Vector3 (-0.482962913144535f, 0.258819045102521f, -0.836516303737808f),
			},
			// 7
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.258819045102521f, 0.965925826289068f, 0.0f),
				new Vector3 (-0.12940952255126f, 0.965925826289068f, 0.224143868042013f),
				new Vector3 (-0.12940952255126f, 0.965925826289068f, -0.224143868042013f),

				new Vector3 (0.25f, 0.866025403784439f, 0.433012701892219f),
				new Vector3 (-0.5f, 0.866025403784439f, 0.0f),
				new Vector3 (0.25f, 0.866025403784439f, -0.433012701892219f),

				new Vector3 (0.707106781186547f, 0.707106781186548f, 0.0f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, 0.612372435695794f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, -0.612372435695794f),

				new Vector3 (0.433012701892219f, 0.5f, 0.75f),
				new Vector3 (-0.866025403784439f, 0.5f, 0.0f),
				new Vector3 (0.433012701892219f, 0.5f, -0.75f),

				new Vector3 (0.965925826289068f, 0.258819045102521f, 0.0f),
				new Vector3 (-0.482962913144534f, 0.258819045102521f, 0.836516303737808f),
				new Vector3 (-0.482962913144535f, 0.258819045102521f, -0.836516303737808f),

				new Vector3 (0.5f, 0.0f, 0.866025403784439f),
				new Vector3 (-1.0f, 0.0f, 0.0f),
				new Vector3 (0.5f, 0.0f, -0.866025403784439f),
			},
			// 8
			new Vector3[] {
				new Vector3 (0.0f, 1.0f, 0.0f),

				new Vector3 (0.258819045102521f, 0.965925826289068f, 0.0f),
				new Vector3 (-0.12940952255126f, 0.965925826289068f, 0.224143868042013f),
				new Vector3 (-0.12940952255126f, 0.965925826289068f, -0.224143868042013f),

				new Vector3 (0.25f, 0.866025403784439f, 0.433012701892219f),
				new Vector3 (-0.5f, 0.866025403784439f, 0.0f),
				new Vector3 (0.25f, 0.866025403784439f, -0.433012701892219f),

				new Vector3 (0.707106781186547f, 0.707106781186548f, 0.0f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, 0.612372435695794f),
				new Vector3 (-0.353553390593274f, 0.707106781186548f, -0.612372435695794f),

				new Vector3 (0.433012701892219f, 0.5f, 0.75f),
				new Vector3 (-0.866025403784439f, 0.5f, 0.0f),
				new Vector3 (0.433012701892219f, 0.5f, -0.75f),

				new Vector3 (0.965925826289068f, 0.258819045102521f, 0.0f),
				new Vector3 (-0.482962913144534f, 0.258819045102521f, 0.836516303737808f),
				new Vector3 (-0.482962913144535f, 0.258819045102521f, -0.836516303737808f),

				new Vector3 (0.5f, 0.0f, 0.866025403784439f),
				new Vector3 (-1.0f, 0.0f, 0.0f),
				new Vector3 (0.5f, 0.0f, -0.866025403784439f),

				new Vector3 (0.965925826289068f, -0.258819045102521f, 0.0f),
				new Vector3 (-0.482962913144534f, -0.258819045102521f, 0.836516303737808f),
				new Vector3 (-0.482962913144535f, -0.258819045102521f, -0.836516303737808f),
			},
		};
		//public static Vector3[] RandomReflectDirection(Vector3 normal, int level) {
		//	if (level <= 1) return new Vector3[] { normal };
		//	Vector3[] source = null, re;

		//	if (level > 8) {
		//		level = 8;
		//	}
		//	source = ReflectDirection[level - 1];

		//	if (normal.Y >= 0.9999f) { //法线垂直向上
		//		return source;
		//	}
		//	re = new Vector3[source.Length];
			
		//	if (normal.Y <= -0.9999f) { //法线垂直向下
		//		for(int i = 0; i < source.Length; i++){
		//			re[i] = source[i] * -1.0f;
		//		}
		//	}
		//	else { //法线不垂直
		//		Vector3 vx = (normal ^ new Vector3(0.0f, 1.0f, 0.0f)).Normalize();
		//		Vector3 vz = (normal ^ vx).Normalize();
		//		for (int i = 0; i < source.Length; i++) {
		//			Vector3 pos = source[i];
		//			re[i] = pos.X * vx + pos.Y * normal + pos.Z * vz;
		//		}
		//	}
		//	return re;
		//}

		public static Float LightStrongByDistance(Float distance) {
			if (distance < 0) return 1.0f;
			if (distance > 1000) { distance = 1000; };
			distance = Math.Log10(distance + 1.2f) + 1.1f;
			distance = 1 / distance;
			return distance;
		}

		public static Float RandomIn(Float min, Float max) {
			double d = random.NextDouble();
			return (Float)(min * d + max * (1.0 - d));
		}

		/// <summary>
		/// 获取入射方向在法线n的反射
		/// </summary>
		/// <param name="dir"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		public static Vector3 Reflect(Vector3 dir, Vector3 n) {
			return dir - dir.Dot(n) * 2.0f * n;
		}
	}
}
