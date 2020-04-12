using Core.Debug;
using System;
using System.Collections.Generic;
using System.Text;


using Math = System.MathF;

using Image = Core.LightStrongImage;
using Vector3 = System.Numerics.Vector3;

namespace Core.Cameras {
	public class TraditionalTestCamera {
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

		public TraditionalTestCamera(Vector3 origin) {
			Origin = origin;
		}
		public TraditionalTestCamera(Vector3 origin, float horLength, float zOffset) {
			Origin = origin;
			HorLength = horLength;
			ZOffset = zOffset;
		}


		public void Render(Image image, Scene scene) {
#if RayDebugger
			SceneDebug Debugger = scene.debugger;
			if (Debugger != null) {
				Debugger.BeginBranch(Origin);
			}
#endif
			DateTime beginTime = DateTime.Now;
			scene.ReadyToRender();
			int mutiplySample = RenderConfiguration.Configurations.SmapingLevel;
			mutiplySample = mutiplySample * 3 - 2;

			int imgw = image.Width, imgh = image.Height;
			//float verLen = imgh * HorLength / imgw;
			//float lp = HorLength / 2, tp = verLen / 2;
			float lenPerPixel = HorLength / imgw;
			float lp = HorLength / 2, tp = lenPerPixel * imgh / 2;

			for (int t = 0; t < imgh; t++) {
				float ntp = tp - lenPerPixel * t;
				float ntpnext = tp - lenPerPixel * (t + 1);
				if (t % 16 == 0) {
					DateTime nowtime = DateTime.Now;
					Console.WriteLine($"{(nowtime - beginTime).ToString("hh\\:mm\\:ss")} : {t} / {imgh} : {t * 100.0 / imgh:0.0}%, ignore:{Scene.ZXJHL}");
				}
				for (int l = 0; l < imgw; l++) {
					float nlp = lenPerPixel * l - lp;
					float nlpnext = lenPerPixel * (l + 1) - lp;
					Light color = default;
					for (int nowsample = 0; nowsample < mutiplySample; nowsample++) {
						float lptmp = Tools.RandomIn(nlp, nlpnext);
						float tptmp = Tools.RandomIn(ntp, ntpnext);
						Vector3 d = new Vector3(lptmp, tptmp, _zOffset);
#if RayDebugger
						if (Debugger != null) {
							Debugger.AppendPoint(_origin + d);
						}
#endif
						Ray r = new Ray(_origin, d);
						color += scene.Render(r);
					}
					image[t, l] = color / mutiplySample;
				}
			}
		}
	}
}
