using System;
using System.Collections.Generic;
using System.Text;

namespace Core {
	public struct Light {
		public float R, G, B;
		public Light(float r, float g, float b) {
			R = r;
			G = g;
			B = b;
		}


		public float Strong () {
			return 0.299f * R + 0.587f * G + 0.114f * B;
		}

		#region Functions
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public Light Lerp(Light r, float lp) {
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
		public static Light operator +(Light l, Light r) {
			return Add(l, r);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light operator -(Light l, Light r) {
			return Sub(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light operator *(Light l, float r) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light operator *(float r, Light l) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light operator /(Light l, float r) {
			return Div(l, r);
		}
		/// <summary>
		/// 坐标对应数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light operator *(Light l, Light r) {
			return Mul(l, r);
		}
		/// <summary>
		/// 坐标对应数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light operator /(Light l, Light r) {
			return NumDiv(l, r);
		}

		public static bool operator ==(Light l, Light r) {
			return Equals(l, r);
		}
		public static bool operator !=(Light l, Light r) {
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
		public static Light Add(Light l, Light r) {
			return new Light(l.R + r.R, l.G + r.G, l.B + r.B);
		}
		/// <summary>
		/// 强度减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light Sub(Light l, Light r) {
			return new Light(l.R - r.R, l.G - r.G, l.B - r.B);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light Mut(Light l, float r) {
			return new Light(l.R * r, l.G * r, l.B * r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light Div(Light l, float r) {
			return new Light(l.R / r, l.G / r, l.B / r);
		}
		/// <summary>
		/// 强度除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light NumDiv(Light l, Light r) {
			return new Light(l.R / r.R, l.G / r.G, l.B / r.B);
		}
		/// <summary>
		/// 强度乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Light Mul(Light l, Light r) {
			return new Light(l.R * r.R, l.G * r.G, l.B * r.B);
		}
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public static Light Lerp(Light l, Light r, float lp) {
			return (l * lp) + (r * (1 - lp));
		}

		public static bool Equals(Light l, Light r) {
			return (l.R == r.R) && (l.G == r.G) && (l.B == r.B);
		}
		#endregion

		#region Const Values
		public static readonly Light Dark = new Light(0.0f, 0.0f, 0.0f);
		public static readonly Light White = new Light(1.0f, 1.0f, 1.0f);

		public static readonly Light Red = new Light(1.0f, 0.0f, 0.0f);
		public static readonly Light Green = new Light(0.0f, 1.0f, 0.0f);
		public static readonly Light Blue = new Light(0.0f, 0.0f, 1.0f);

		public static readonly Light Yellow = new Light(1.0f, 1.0f, 0.0f);
		public static readonly Light Magenta = new Light(1.0f, 0.0f, 1.0f);
		public static readonly Light Cyan = new Light(0.0f, 1.0f, 1.0f);
		#endregion
	}
}
