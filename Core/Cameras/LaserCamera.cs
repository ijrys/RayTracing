using System;
using System.Collections.Generic;
using System.Text;
using Core.Debug;

using Math = System.MathF;
using Image = Core.LightStrongImage;

using Vector3 = System.Numerics.Vector3;

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
		float _zOffset = 1.0f;
		/// <summary>
		/// 水平视野，角度
		/// </summary>
		float _horLength = 3.464f;

		/// <summary>
		/// 相机原点
		/// </summary>
		public Vector3 Origin { get => _origin; set => _origin = value; }

		/// <summary>
		/// 胶片水平宽度
		/// </summary>
		public float HorLength {
			get => _horLength;
			set {
				_horLength = value;
			}
		}
		/// <summary>
		/// 胶片距汇点位置
		/// </summary>
		public float ZOffset {
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
		public LaserCamera(Vector3 origin, float horLength, float zOffset) {
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
			//float verLen = imgh * HorLength / imgw;
			float lenPerPixel = HorLength / imgw;
			float lp = HorLength / 2, tp = lenPerPixel * imgh / 2;

			foreach (var point in Points) {
				int t = point.y, l = point.x;
				//float ntp = tp - verLen / imgh * t;
				//float ntpnext = tp - verLen / imgh * (t + 1);
				//float nlp = _horLength / imgw * l - lp;
				//float nlpnext = _horLength / imgw * (l + 1) - lp;
				float ntp = tp - lenPerPixel * t;
				float ntpnext = tp - lenPerPixel * (t + 1);
				float nlp = lenPerPixel * l - lp;
				float nlpnext = lenPerPixel * (l + 1) - lp;
				LightStrong color = default;
				float lptmp = (nlp + nlpnext) * 0.5f;
				float tptmp = (ntp + ntpnext) * 0.5f;
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
