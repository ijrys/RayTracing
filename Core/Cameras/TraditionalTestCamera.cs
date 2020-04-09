using Core.Debug;
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
using Image = Core.Image<Core.RGBColor8>;

namespace Core.Cameras {
	public class TraditionalTestCamera {
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

		public TraditionalTestCamera(Vector3 origin) {
			Origin = origin;
		}
		public TraditionalTestCamera(Vector3 origin, Float horLength, Float zOffset) {
			Origin = origin;
			HorLength = horLength;
			ZOffset = zOffset;
		}


		public void Render(Image image, Scene scene) {
#if RayDebugger
			SceneDebug Debugger = scene.Debugger;
#endif
			int mutiplySample = RenderConfiguration.Configurations.SmapingLevel;
			mutiplySample = mutiplySample * mutiplySample;

			int imgw = image.Width, imgh = image.Height;
			Float verLen = imgh * HorLength / imgw;
			Float lp = HorLength / 2, tp = verLen / 2;

			for (int t = 0; t < imgh; t++) {
				Float ntp = tp - verLen / imgh * t;
				Float ntpnext = tp - verLen / imgh * (t + 1);
				if (t % 10 == 0) {
					Console.WriteLine($"{t} / {imgh} : {t * 100.0 / imgh:0.0}%");
				}
				for (int l = 0; l < imgw; l++) {
					Float nlp = _horLength / imgw * l - lp;
					Float nlpnext = _horLength / imgw * (l + 1) - lp;
					LightStrong color = default;
					for (int nowsample = 0; nowsample < mutiplySample; nowsample++) {
						Float lptmp = Tools.RandomIn(nlp, nlpnext);
						Float tptmp = Tools.RandomIn(ntp, ntpnext);
						Vector3 d = new Vector3(lptmp, tptmp, _zOffset);
#if RayDebugger
						if (Debugger != null) {
							Debugger.NewRayLine();
							Debugger.Ray.AppendPoint(1);
							Debugger.Ray.AppendPoint(d);
						}
#endif
						Ray r = new Ray(_origin, d);
						color += scene.Render(r);
					}
					image[t, l] = (color / mutiplySample).ToRGBColor8();
				}
			}
		}
	}
}
