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
using Color = Core.RGBColor8;

namespace Core.Objects {
	public class Ground : RenderObject {
		LightStrong _color;

		public override Vector3 Position {
			get => _position;
			set => _position = new Vector3(ConstValues.Zero, value.Y, ConstValues.Zero);
		}

		public Ground() {
			_color = new LightStrong(0.25f, 0.75f, 0.25f) * 2;
		}
		public Ground(Float y) : base(new Vector3(ConstValues.Zero, y, ConstValues.Zero)) {
			_color = new LightStrong(0.25f, 0.75f, 0.25f) * 2;
		}
		public Ground(Materials.Material material) {
			Material = material;
		}
		public Ground(Float y, Materials.Material material) : base(new Vector3(ConstValues.Zero, y, ConstValues.Zero)) {
			Material = material;
		}

		//public override LightStrong IntersectColor(Vector3 point, Vector3 normal, int deep) {
		//	return Material.Color(point);
		//}

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
	}
}
