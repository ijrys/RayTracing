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
	public struct Vector3 {
		public Float X, Y, Z;

		public Vector3(Float f) {
			X = f;
			Y = f;
			Z = f;
		}

		public Vector3(Float x, Float y, Float z) {
			X = x;
			Y = y;
			Z = z;
		}

		#region Functions
		/// <summary>
		/// 矢量加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector3 Add(Vector3 r) {
			return Add(this, r);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector3 Sub(Vector3 r) {
			return Sub(this, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector3 Mut(Float r) {
			return Mut(this, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector3 Div(Float r) {
			return Div(this, r);
		}
		/// <summary>
		/// 点积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Float Dot(Vector3 r) {
			return Dot(this, r);
		}
		/// <summary>
		/// 叉积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public Vector3 Cross(Vector3 r) {
			return Cross(this, r);
		}
		/// <summary>
		/// 反向量
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public Vector3 Negative() {
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
		public Vector3 Normalize() {
			return Normalize(this);
		}
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public Vector3 Lerp (Vector3 r, float lp) {
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
		public static Vector3 operator +(Vector3 l, Vector3 r) {
			return Add(l, r);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator -(Vector3 l, Vector3 r) {
			return Sub(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator *(Vector3 l, Float r) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator *(Float r, Vector3 l) {
			return Mut(l, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator /(Vector3 l, Float r) {
			return Div(l, r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator /(Float r, Vector3 l) {
			return Div(l, r);
		}
		/// <summary>
		/// 坐标对应数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator *(Vector3 l, Vector3 r) {
			return NumMut(l, r);
		}
		/// <summary>
		/// 坐标对应数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator /(Vector3 l, Vector3 r) {
			return NumDiv(l, r);
		}
		/// <summary>
		/// 点积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Float operator &(Vector3 l, Vector3 r) {
			return Dot(l, r);
		}
		/// <summary>
		/// 叉积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 operator ^(Vector3 l, Vector3 r) {
			return Cross(l, r);
		}

		public static bool operator == (Vector3 l, Vector3 r) {
			return Equals(l, r);
		}
		public static bool operator !=(Vector3 l, Vector3 r) {
			return !Equals(l, r);
		}

		public Float this[int index] {
			get {
				switch (index) {
					case 0: return X;
					case 1: return Y;
					case 2: return Z;
					default: throw new IndexOutOfRangeException();
				}
			}
			set {
				switch (index) {
					case 0: X = value; break;
					case 1: Y = value; break;
					case 2: Z = value; break;
					default: throw new IndexOutOfRangeException();
				}
			}
		}


		public RGBColor8 ToRGB8 () {
			RGBColor8 color = new RGBColor8(
				(byte)(Tools.Clamp(X, 0.0f, 1.0f) * 255),
				(byte)(Tools.Clamp(Y, 0.0f, 1.0f) * 255),
				(byte)(Tools.Clamp(Z, 0.0f, 1.0f) * 255));
			return color;
		}
		#endregion

		#region Static functions
		/// <summary>
		/// 矢量加
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 Add(Vector3 l, Vector3 r) {
			return new Vector3(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
		}
		/// <summary>
		/// 矢量减
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 Sub(Vector3 l, Vector3 r) {
			return new Vector3(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
		}
		/// <summary>
		/// 数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 Mut(Vector3 l, Float r) {
			return new Vector3(l.X * r, l.Y * r, l.Z * r);
		}
		/// <summary>
		/// 数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 Div(Vector3 l, Float r) {
			return new Vector3(l.X / r, l.Y / r, l.Z / r);
		}
		/// <summary>
		/// 坐标对应数乘
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 NumMut(Vector3 l, Vector3 r) {
			return new Vector3(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
		}
		/// <summary>
		/// 坐标对应数除
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 NumDiv(Vector3 l, Vector3 r) {
			return new Vector3(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
		}
		/// <summary>
		/// 点积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Float Dot(Vector3 l, Vector3 r) {
			return l.X * r.X + l.Y * r.Y + l.Z * r.Z;
		}
		/// <summary>
		/// 叉积
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector3 Cross(Vector3 l, Vector3 r) {
			return new Vector3(
				(l.Y * r.Z) - (l.Z * r.Y),
				(l.Z * r.X) - (l.X * r.Z),
				(l.X * r.Y) - (l.Y * r.X));
		}
		/// <summary>
		/// 反向量
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector3 Negative(Vector3 v) {
			return new Vector3(-v.X, -v.Y, -v.Z);
		}
		/// <summary>
		/// 长度平方
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Float LengthSquared(Vector3 v) {
			return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
		}
		/// <summary>
		/// 长度
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Float Length(Vector3 v) {
			return Math.Sqrt(LengthSquared(v));
		}
		/// <summary>
		/// 长度单位化
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector3 Normalize(Vector3 v) {
			return Div(v, Length(v));
		}
		/// <summary>
		/// 带权中点
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <param name="lp"></param>
		/// <returns></returns>
		public static Vector3 Lerp (Vector3 l, Vector3 r, Float lp) {
			return (l * lp) + (r * (1 - lp));
		}

		public static bool Equals (Vector3 l, Vector3 r) {
			return (l.X == r.X) && (l.Y == r.Y) && (l.Z == r.Z);
		}

		#endregion

		#region Const Values
		public static readonly Vector3 Zero = new Vector3(ConstValues.Zero);
		public static readonly Vector3 One = new Vector3(ConstValues.One);
		#endregion
	}
}
