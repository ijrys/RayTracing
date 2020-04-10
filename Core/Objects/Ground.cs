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

namespace Core.Objects {
	public class Ground : RenderObject {

		public override Vector3 Position {
			get => _position;
			set => _position = new Vector3(ConstValues.Zero, value.Y, ConstValues.Zero);
		}

		public Ground() {
		}
		public Ground(Float y) : base(new Vector3(ConstValues.Zero, y, ConstValues.Zero)) {
		}
		public Ground(Materials.Material material) {
			Material = material;
		}
		public Ground(Float y, Materials.Material material) : base(new Vector3(ConstValues.Zero, y, ConstValues.Zero)) {
			Material = material;
		}

		public override (Float, Vector3, Vector3) IntersectDeep(Ray ray) {
			Float _y = _position.Y;
			Float x = Tools.Length(ray.Direction.X, ray.Direction.Z);
			Float drty = _y - ray.Origin.Y;
			Float t = drty / ray.Direction.Y;
			if (Float.IsNaN(t) || Float.IsInfinity(t) || t < 0) {
				return (Float.NegativeInfinity, Vector3.Zero, new Vector3(0.0f, 1.0f, 0.0f));
			}
			Float distance = (t * ray.Direction).Length();
			return (distance, ray.Direction * t + ray.Origin, new Vector3(0.0f, 1.0f, 0.0f));
		}

		public override (Float, Vector3, Vector3) InterIntersect(Ray ray) {
			return (Float.NegativeInfinity, Vector3.Zero, Vector3.Zero);
		}
	}
}
