using System;
using System.Collections.Generic;
using System.Text;
using Core.Debug;

#if UseDouble
using Float = System.Double;
using Math = System.Math;
#else
using Float = System.Single;
using Math = System.MathF;
#endif
using Image = Core.LightStrongImage;

namespace Core.Cameras {
	public class TestCamera {
		Vector3 _origin;

		Float _horRotation = ConstValues.Zero, _verRotation = 90.0f;
		/// <summary>
		/// 水平视野，角度
		/// </summary>
		Float _horField = 124;

		/// <summary>
		/// 相机原点
		/// </summary>
		public Vector3 Origin { get => _origin; set => _origin = value; }
		/// <summary>
		/// 水平旋转，角度，xoz面与z轴正半轴顺时针夹角[0, 360)
		/// </summary>
		public Float HorRotation {
			get => _horRotation;
			set {
				value = value % 360;
				if (value < 0) {
					value = (value + 360) % 360;
				}
				_horRotation = value;
			}
		}
		/// <summary>
		/// 垂直旋转，角度，与y轴正半轴夹角[0, 180]
		/// </summary>
		public Float VerRotation {
			get => _verRotation;
			set {
				//if (value < 0 || value > 180) {
				//	throw new Exception("值不在范围内");
				//}
				//_verRotation = value;
				value = value % 360;
				if (value < 0) {
					value = (value + 360) % 360;
				}
				_verRotation = value;
			}
		}

		/// <summary>
		/// 水平视野，角度
		/// </summary>
		public Float HorField {
			get => _horField;
			set {
				if (value < 5 || value > 360) {
					throw new Exception("角度超出合法范围");
				}
				_horField = value;
			}
		}


		public TestCamera(Vector3 origin) {
			Origin = origin;
		}
		public TestCamera(Vector3 origin, Float horRotation, Float verRotation) {
			Origin = origin;
			HorRotation = horRotation;
			VerRotation = verRotation;
		}

		public void Render(Image image, Scene scene) {

			int imgw = image.Width, imgh = image.Height;
			Float verField = _horField * image.Height / image.Width;

			if (verField < 5 || verField > 360) {
				throw new Exception("根据图像比例计算得出：垂直方向视野角度超出范围[5, 360]");
			}
			Float verStart = _verRotation - verField / 2;
			Float verEnd = _verRotation + verField / 2;

			Float horStart = _horRotation - _horField / 2;
			for (int t = 0; t < imgh; t++) {
				Float nrt = Tools.Radian(verStart + verField * t / imgh);
				Float sina = Math.Sin(nrt);
				Float cosa = Math.Cos(nrt);
				for (int l = 0; l < imgw; l++) {
					Float nrl = Tools.Radian(horStart + _horField * l / imgw);
					Float sinb = Math.Sin(nrl);
					Float cosb = Math.Cos(nrl);
					Vector3 d = new Vector3(sina * sinb, cosa, sina * cosb);

					Ray r = new Ray(_origin, d);
					LightStrong color = scene.Render(r);
					image[t, l] = color;
				}
			}
		}
	}
}
