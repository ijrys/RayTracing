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
	public struct Ray {
		public Vector3 Origin, Direction;
		public Ray (Vector3 o, Vector3 d) {
			Origin = o;
			Direction = d;
		}
		public override string ToString() {
			return $"{{o: {Origin}, d: {Direction}}}";
		}

		public Vector3 this[Float t] {
			get {
				return Origin + Direction * t;
			}
		}
	}
}
