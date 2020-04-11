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
	public class LaserCamera {
		public struct PointPair {
			public int x, y;
			public PointPair(int xx, int yy) {
				x = xx;
				y = yy;
			}
		}

		Vector3 _origin;

		/// <summary>
		/// 胶片z偏移
		/// </summary>
		Float _zOffset = 1.0f;
		/// <summary>
		/// 水平视野，角度
		/// </summary>
		Float _horLength = 3.464f;

		/// <summary>
		/// 相机原点
		/// </summary>
		public Vector3 Origin { get => _origin; set => _origin = value; }

		/// <summary>
		/// 胶片水平宽度
		/// </summary>
		public Float HorLength {
			get => _horLength;
			set {
				_horLength = value;
			}
		}
		/// <summary>
		/// 胶片距汇点位置
		/// </summary>
		public Float ZOffset {
			get => _zOffset;
			set {
				if (value < 0.5f) {
					value = 0.5f;
				}
				else if (value > 10) {
					value = 10.0f;
				}

				_zOffset = value;
			}
		}

		public LaserCamera(Vector3 origin) {
			Origin = origin;
		}
		public LaserCamera(Vector3 origin, Float horLength, Float zOffset) {
			Origin = origin;
			HorLength = horLength;
			ZOffset = zOffset;
		}

		public List<PointPair> Points = new List<PointPair>();

		public void Render(Image image, Scene scene) {
#if RayDebugger
			SceneDebug Debugger = scene.debugger;
			if (Debugger != null) {
				Debugger.BeginBranch(Origin);
			}
#endif

			int imgw = image.Width, imgh = image.Height;
			Float verLen = imgh * HorLength / imgw;
			Float lp = HorLength / 2, tp = verLen / 2;

			foreach (var point in Points) {
				int t = point.y, l = point.x;
				Float ntp = tp - verLen / imgh * t;
				Float ntpnext = tp - verLen / imgh * (t + 1);
				Float nlp = _horLength / imgw * l - lp;
				Float nlpnext = _horLength / imgw * (l + 1) - lp;
				LightStrong color = default;
				Float lptmp = (nlp + nlpnext) * 0.5f;
				Float tptmp = (ntp + ntpnext) * 0.5f;
				Vector3 d = new Vector3(lptmp, tptmp, _zOffset);
#if RayDebugger
				if (Debugger != null) {
					Debugger.AppendPoint(_origin + d);
				}
#endif
				Ray r = new Ray(_origin, d);
				color += scene.Render(r);
				image[t, l] = color;
			}
		}

	}
}
