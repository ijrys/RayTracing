using System;
using System.Collections.Generic;
using System.Text;

using Math = System.MathF;

using Vector3 = System.Numerics.Vector3;

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

		//public Vector3 this[float t] {
		//	get {
		//		return Origin + Direction * t;
		//	}
		//}
	}
}
