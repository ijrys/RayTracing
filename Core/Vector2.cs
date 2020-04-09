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
	public struct Vector2 {
		public Float X, Y;

		public Vector2(Float f) {
			X = f;
			Y = f;
		}

		public Vector2(Float x, Float y) {
			X = x;
			Y = y;
		}

		#region Functions
		/// <summary>
		/// 矢量加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector2 Add(Vector2 r) {
			return Add(this, r);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector2 Sub(Vector2 r) {
			return Sub(this, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector2 Mut(Float r) {
			return Mut(this, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector2 Div(Float r) {
			return Div(this, r);
		}
		/// <summary>
		/// 点积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Float Dot(Vector2 r) {
			return Dot(this, r);
		}
		/// <summary>
		/// 反向量
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public Vector2 Negative() {
			return Negative(this);
		}
		/// <summary>
		/// 长度平方
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public Float LengthSquared() {
			return LengthSquared(this);
		}
		/// <summary>
		/// 长度
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public Float Length() {
			return Length(this);
		}
		/// <summary>
		/// 长度单位化
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public Vector2 Normalize() {
			return Normalize(this);
		}
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public Vector2 Lerp(Vector2 r, float lp) {
			return Lerp(this, r, lp);
		}
		#endregion

		#region Operator
		/// <summary>
		/// 矢量加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator +(Vector2 l, Vector2 r) {
			return Add(l, r);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator -(Vector2 l, Vector2 r) {
			return Sub(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator *(Vector2 l, Float r) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator *(Float r, Vector2 l) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator /(Vector2 l, Float r) {
			return Div(l, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator /(Float r, Vector2 l) {
			return Div(l, r);
		}
		/// <summary>
		/// 坐标对应数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator *(Vector2 l, Vector2 r) {
			return NumMut(l, r);
		}
		/// <summary>
		/// 坐标对应数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 operator /(Vector2 l, Vector2 r) {
			return NumDiv(l, r);
		}
		/// <summary>
		/// 点积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Float operator &(Vector2 l, Vector2 r) {
			return Dot(l, r);
		}

		public static bool operator ==(Vector2 l, Vector2 r) {
			return Equals(l, r);
		}
		public static bool operator !=(Vector2 l, Vector2 r) {
			return !Equals(l, r);
		}

		public Float this[int index] {
			get {
				switch (index) {
					case 0: return X;
					case 1: return Y;
					default: throw new IndexOutOfRangeException();
				}
			}
			set {
				switch (index) {
					case 0: X = value; break;
					case 1: Y = value; break;
					default: throw new IndexOutOfRangeException();
				}
			}
		}

		#endregion

		#region Static functions
		/// <summary>
		/// 矢量加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 Add(Vector2 l, Vector2 r) {
			return new Vector2(l.X + r.X, l.Y + r.Y);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 Sub(Vector2 l, Vector2 r) {
			return new Vector2(l.X - r.X, l.Y - r.Y);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 Mut(Vector2 l, Float r) {
			return new Vector2(l.X * r, l.Y * r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 Div(Vector2 l, Float r) {
			return new Vector2(l.X / r, l.Y / r);
		}
		/// <summary>
		/// 坐标对应数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 NumMut(Vector2 l, Vector2 r) {
			return new Vector2(l.X * r.X, l.Y * r.Y);
		}
		/// <summary>
		/// 坐标对应数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2 NumDiv(Vector2 l, Vector2 r) {
			return new Vector2(l.X / r.X, l.Y / r.Y);
		}
		/// <summary>
		/// 点积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Float Dot(Vector2 l, Vector2 r) {
			return l.X * r.X + l.Y * r.Y;
		}

		/// <summary>
		/// 反向量
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector2 Negative(Vector2 v) {
			return new Vector2(-v.X, -v.Y);
		}
		/// <summary>
		/// 长度平方
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Float LengthSquared(Vector2 v) {
			return v.X * v.X + v.Y * v.Y;
		}
		/// <summary>
		/// 长度
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Float Length(Vector2 v) {
			return Math.Sqrt(LengthSquared(v));
		}
		/// <summary>
		/// 长度单位化
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector2 Normalize(Vector2 v) {
			return Div(v, Length(v));
		}
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public static Vector2 Lerp(Vector2 l, Vector2 r, Float lp) {
			return (l * lp) + (r * (1 - lp));
		}

		public static bool Equals(Vector2 l, Vector2 r) {
			return (l.X == r.X) && (l.Y == r.Y);
		}

		#endregion

		#region Const Values
		public static readonly Vector2 Zero = new Vector2(ConstValues.Zero);
		public static readonly Vector2 One = new Vector2(ConstValues.One);
		#endregion
	}
}
