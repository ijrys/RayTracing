using T = System.UInt16;
using rgbt = Core.RGBColor16;
using rgbat = Core.RGBAColor16;
using System;

#if UseDouble
using Float = System.Double;
using Math = System.Math;
#else
using Float = System.Single;
using Math = System.MathF;
#endif

namespace Core {
	public struct RGBColor16 {
		public const int PixelSize = 3;

		public T R, G, B;
		public RGBColor16(T r, T g, T b) {
			R = r;
			G = g;
			B = b;
		}

		public rgbt Lerp(RGBColor16 r, Float lp) {
			return Lerp(this, r, lp);
		}

		public static rgbt Lerp(RGBColor16 l, RGBColor16 r, Float lp) {
			Float rp = 1 - lp;
			byte cr = (Byte)(Tools.Clamp(l.R * lp + r.R * rp, 0, 255));
			byte cg = (Byte)(Tools.Clamp(l.G * lp + r.G * rp, 0, 255));
			byte cb = (Byte)(Tools.Clamp(l.B * lp + r.B * rp, 0, 255));
			return new rgbt(cr, cg, cb);
		}

		#region ConstValues
		public static readonly rgbt Black = new rgbt(0, 0, 0);
		public static readonly rgbt White = new rgbt(T.MaxValue, T.MaxValue, T.MaxValue);

		public static readonly rgbt Red = new rgbt(T.MaxValue, 0, 0);
		public static readonly rgbt Green = new rgbt(0, T.MaxValue, 0);
		public static readonly rgbt Blue = new rgbt(0, 0, T.MaxValue);

		public static readonly rgbt Yellow = new rgbt(T.MaxValue, T.MaxValue, 0);
		public static readonly rgbt Magenta = new rgbt(T.MaxValue, 0, T.MaxValue);
		public static readonly rgbt Cyan = new rgbt(0, T.MaxValue, T.MaxValue);
		#endregion
	}
	public struct RGBAColor16 {
		public const int PixelSize = 4;

		public T R, G, B, A;
		public RGBAColor16(T r, T g, T b) {
			R = r;
			G = g;
			B = b;
			A = T.MaxValue;
		}
		public RGBAColor16(T r, T g, T b, T a) {
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public static implicit operator rgbat(rgbt c) {
			return new rgbat(c.R, c.G, c.B, T.MaxValue);
		}
		public static explicit operator rgbt(rgbat c) {
			return new rgbt(c.R, c.G, c.B);
		}

		#region ConstValues
		public static readonly rgbt Black = new rgbt(0, 0, 0);
		public static readonly rgbt White = new rgbt(T.MaxValue, T.MaxValue, T.MaxValue);

		public static readonly rgbt Red = new rgbt(T.MaxValue, 0, 0);
		public static readonly rgbt Green = new rgbt(0, T.MaxValue, 0);
		public static readonly rgbt Blue = new rgbt(0, 0, T.MaxValue);

		public static readonly rgbt Yellow = new rgbt(T.MaxValue, T.MaxValue, 0);
		public static readonly rgbt Magenta = new rgbt(T.MaxValue, 0, T.MaxValue);
		public static readonly rgbt Cyan = new rgbt(0, T.MaxValue, T.MaxValue);
		#endregion
	}
}
