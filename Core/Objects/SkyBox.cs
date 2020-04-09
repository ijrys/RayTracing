using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

#if UseDouble
using Float = System.Double;
using Math = System.Math;
#else
using Float = System.Single;
using Math = System.MathF;
#endif

namespace Core.Objects {
	public class SkyBox : RenderObject {
		private static LightStrong Sun = new LightStrong(1.0f, 1.0f, 1.0f);
		private static LightStrong B1 = new LightStrong(0.3f, 0.75f, 1.0f);
		private static LightStrong W1 = new LightStrong(0.95f, 0.95f, 0.95f);
		private static LightStrong G1 = new LightStrong(0.7f, 0.7f, 0.7f);
		private static LightStrong G2 = new LightStrong(0.2f, 0.2f, 0.2f);

		public override LightStrong IntersectLight(Vector3 point, Vector3 dir, Vector3 normal, int deep) {
			//Float y = point.Y;
			//if (y > 0.3f) {
			//	return LightStrong.White;
			//}
			//if (y > -0.1) {
			//	return LightStrong.White.Lerp(new LightStrong(0.5f, 0.5f, 0.5f), (y + 0.1f) / 0.4f);
			//}
			//return new LightStrong(0.3f, 0.3f, 0.3f);
			if (dir.Y > 0.94f) {
				return LightStrong.White;
			}
			Float y = 0.5f * (dir.Y + 1.0f);
			return (1.0f - y) * LightStrong.White + y * new LightStrong(0.51f, 0.68f, 0.95f);
		}

		public override LightStrong IntersectColor(Vector3 point, Vector3 dir, Vector3 normal, int deep) {
			//Float y = point.Y + 1.0f;
			//if (y > 1.99f) {
			//	return Sun;
			//}
			//if (y > 1.9f) {
			//	return Sun.Lerp(B1, (y - 1.9f) / 0.09f);
			//}
			//if (y > 1.1f) {
			//	return B1.Lerp(W1, (y - 1.1f) / 0.8f);
			//}
			//if (y > 0.8f) {
			//	return W1.Lerp(G1, (y - 0.8f) / 0.3f);
			//}
			//if (y > 0.5f) {
			//	return G1.Lerp(G2, (y - 0.5f) / 0.3f);
			//}
			//return G2;
			Float y = 0.5f * (dir.Y + 1.0f);
			return (1.0f - y) * LightStrong.White + y * new LightStrong(0.5f, 0.7f, 1.0f);
		}

		public override (Float, Vector3, Vector3) IntersectDeep(Ray ray) {
			return (Float.PositiveInfinity, ray.Direction, ray.Direction);
		}
	}
}
