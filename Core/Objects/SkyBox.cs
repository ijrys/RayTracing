using Core.Debug;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


using Math = System.MathF;

using Vector3 = System.Numerics.Vector3;

namespace Core.Objects {
	public class SkyBox : RenderObject {

		public override Light IntersectLight(Vector3 point, Vector3 dir, Vector3 normal, int deep) {
#if RayDebugger
			SceneDebug Debugger = Scene.debugger;
			if (Debugger != null) {
				Debugger.BeginBranch(point);
				Debugger.EndBranch();
			}
#endif
			if (dir.Y > 0.94f) {
				return Light.White;
			}
			float y = 0.5f * (dir.Y + 1.0f);
			return (1.0f - y) * Light.White + y * new Light(0.51f, 0.68f, 0.95f);
		}

		public override (float, Vector3, Vector3) IntersectDeep(Ray ray) {
			return (float.PositiveInfinity, ray.Origin + ray.Direction, ray.Direction);
		}

		public override (float, Vector3, Vector3) InterIntersect(Ray ray) {
			return (float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
		}
	}
}
