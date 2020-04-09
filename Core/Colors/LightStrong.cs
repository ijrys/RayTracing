using System;
using System.Collections.Generic;
using System.Text;

namespace Core {
	public struct LightStrong {
		float R, G, B;
		public LightStrong(float r, float g, float b) {
			R = r;
			G = g;
			B = b;
		}


		public float Strong () {
			return 0.299f * R + 0.587f * G + 0.114f * B;
		}
		public RGBColor8 ToRGBColor8() {
			RGBColor8 color = new RGBColor8(
			(byte)(Tools.Clamp(R, 0.0f, 1.0f) * 255),
			(byte)(Tools.Clamp(G, 0.0f, 1.0f) * 255),
			(byte)(Tools.Clamp(B, 0.0f, 1.0f) * 255));
			return color;
		}
		public RGBColor8 ToRGBColor8(float min, float max) {
			if (min > max) {
				float t = min;
				min = max;
				max = t;
			}
			float offset = max - min;
			float r = Tools.Clamp(R - min, 0.0f, max) / offset;
			float g = Tools.Clamp(R - min, 0.0f, max) / offset;
			float b = Tools.Clamp(R - min, 0.0f, max) / offset;
			RGBColor8 color = new RGBColor8(
			(byte)(r * 255),
			(byte)(g * 255),
			(byte)(b * 255));
			return color;
		}

		#region Functions
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public LightStrong Lerp(LightStrong r, float lp) {
			return Lerp(this, r, lp);
		}
		#endregion

		#region Operators
		/// <summary>
		/// 矢量加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator +(LightStrong l, LightStrong r) {
			return Add(l, r);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator -(LightStrong l, LightStrong r) {
			return Sub(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator *(LightStrong l, float r) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator *(float r, LightStrong l) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator /(LightStrong l, float r) {
			return Div(l, r);
		}
		/// <summary>
		/// 坐标对应数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator *(LightStrong l, LightStrong r) {
			return Mul(l, r);
		}
		/// <summary>
		/// 坐标对应数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong operator /(LightStrong l, LightStrong r) {
			return NumDiv(l, r);
		}

		public static bool operator ==(LightStrong l, LightStrong r) {
			return Equals(l, r);
		}
		public static bool operator !=(LightStrong l, LightStrong r) {
			return !Equals(l, r);
		}
		#endregion

		#region Static Functions
		/// <summary>
		/// 强度加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong Add(LightStrong l, LightStrong r) {
			return new LightStrong(l.R + r.R, l.G + r.G, l.B + r.B);
		}
		/// <summary>
		/// 强度减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong Sub(LightStrong l, LightStrong r) {
			return new LightStrong(l.R - r.R, l.G - r.G, l.B - r.B);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong Mut(LightStrong l, float r) {
			return new LightStrong(l.R * r, l.G * r, l.B * r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong Div(LightStrong l, float r) {
			return new LightStrong(l.R / r, l.G / r, l.B / r);
		}
		/// <summary>
		/// 强度除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong NumDiv(LightStrong l, LightStrong r) {
			return new LightStrong(l.R / r.R, l.G / r.G, l.B / r.B);
		}
		/// <summary>
		/// 强度乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static LightStrong Mul(LightStrong l, LightStrong r) {
			return new LightStrong(l.R * r.R, l.G * r.G, l.B * r.B);
		}
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public static LightStrong Lerp(LightStrong l, LightStrong r, float lp) {
			return (l * lp) + (r * (1 - lp));
		}

		public static bool Equals(LightStrong l, LightStrong r) {
			return (l.R == r.R) && (l.G == r.G) && (l.B == r.B);
		}
		#endregion

		#region Const Values
		public static readonly LightStrong Dark = new LightStrong(0.0f, 0.0f, 0.0f);
		public static readonly LightStrong White = new LightStrong(1.0f, 1.0f, 1.0f);

		public static readonly LightStrong Red = new LightStrong(1.0f, 0.0f, 0.0f);
		public static readonly LightStrong Green = new LightStrong(0.0f, 1.0f, 0.0f);
		public static readonly LightStrong Blue = new LightStrong(0.0f, 0.0f, 1.0f);

		public static readonly LightStrong Yellow = new LightStrong(1.0f, 1.0f, 0.0f);
		public static readonly LightStrong Magenta = new LightStrong(1.0f, 0.0f, 1.0f);
		public static readonly LightStrong Cyan = new LightStrong(0.0f, 1.0f, 1.0f);
		#endregion
	}
}
