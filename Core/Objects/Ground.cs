using System;
using System.Collections.Generic;
using System.Text;


using Math = System.MathF;

using Vector3 = System.Numerics.Vector3;

namespace Core.Objects {
	public class Ground : RenderObject {

		public override Vector3 Position {
			get => _position;
			set => _position = new Vector3(0.0f, value.Y, 0.0f);
		}

		public Ground() {
		}
		public Ground(float y) : base(new Vector3(0.0f, y, 0.0f)) {
		}
		public Ground(Materials.Material material) {
			Material = material;
		}
		public Ground(float y, Materials.Material material) : base(new Vector3(0.0f, y, 0.0f)) {
			Material = material;
		}

		public override (float, Vector3, Vector3) IntersectDeep(Ray ray) {
			float _y = _position.Y;
			float x = Tools.Length(ray.Direction.X, ray.Direction.Z);
			float drty = _y - ray.Origin.Y;
			float t = drty / ray.Direction.Y;
			if (float.IsNaN(t) || t < 0 || float.IsInfinity(t)) {
				return (float.NegativeInfinity, Vector3.Zero, new Vector3(0.0f, 1.0f, 0.0f));
			}
			float distance = (t * ray.Direction).Length();
			return (distance, ray.Direction * t + ray.Origin, new Vector3(0.0f, 1.0f, 0.0f));
		}

		public override (float, Vector3, Vector3) InterIntersect(Ray ray) {
			return (float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
		}
	}
}
