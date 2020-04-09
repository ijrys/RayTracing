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

		public override LightStrong IntersectLight(Vector3 point, Vector3 dir, Vector3 normal, int deep) {
			if (dir.Y > 0.94f) {
				return LightStrong.White;
			}
			Float y = 0.5f * (dir.Y + 1.0f);
			return (1.0f - y) * LightStrong.White + y * new LightStrong(0.51f, 0.68f, 0.95f);
		}

		public override (Float, Vector3, Vector3) IntersectDeep(Ray ray) {
			return (Float.PositiveInfinity, ray.Direction, ray.Direction);
		}
	}
}
