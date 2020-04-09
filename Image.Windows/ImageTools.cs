using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Core.ImageTools {
	public class ImageTools {
		struct RGBColor8 {
			public byte R, G, B;
			public RGBColor8 (byte r, byte g, byte b) {
				R = r;
				G = g;
				B = b;
			}
		}

		private static RGBColor8 TransToColorGamma1(LightStrong light, float min = 0.0f, float max = 1.0f) {
			byte r, g, b;
			float drt = (max - min) / 256.0f;

			float tmp = (light.R - min) / drt;
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.9f; }
			r = (byte)tmp;

			tmp = (light.G - min) / drt;
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			g = (byte)tmp;

			tmp = (light.B - min) / drt;
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			b = (byte)tmp;

			return new RGBColor8(r, g, b);
		}
		private static RGBColor8 TransToColorGamma2(LightStrong light, float min = 0.0f, float max = 1.0f) {
			byte r, g, b;
			float drt = (max - min) / 256.0f;

			float tmp = (MathF.Sqrt(light.R - min) / drt);
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			r = (byte)tmp;

			tmp = (MathF.Sqrt(light.G - min) / drt);
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			g = (byte)tmp;

			tmp = (MathF.Sqrt(light.B - min) / drt);
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			b = (byte)tmp;

			return new RGBColor8(r, g, b);
		}
		private static RGBColor8 TransToColor(LightStrong light, float min, float max, int gamma) {
			byte r, g, b;
			float drt = (max - min);
			float ga = 1.0f / gamma;

			float tmp = MathF.Pow((light.R - min) / drt, ga) * 256.0f;
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.9f; }
			r = (byte)tmp;

			tmp = MathF.Pow((light.G - min) / drt, ga) * 256.0f;
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			g = (byte)tmp;

			tmp = MathF.Pow((light.B - min) / drt, ga) * 256.0f;
			if (tmp < 0.0f) { tmp = 0.0f; }
			if (tmp > 255.9f) { tmp = 255.0f; }
			b = (byte)tmp;

			return new RGBColor8(r, g, b);
		}

		private static WriteableBitmap GetBitmap(LightStrongImage image, float min, float max, int gamma) {
			int imgw = image.Width, imgh = image.Height;
			WriteableBitmap bitmap = new WriteableBitmap(imgw, imgh, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);
			int byteperpixel = bitmap.Format.BitsPerPixel / 8;
			byte[] colordata = new byte[imgw * imgh * byteperpixel];
			int pos = 0;

			for (int t = 0; t < imgh; t++) {
				for (int l = 0; l < imgw; l++) {
					int rt, rl;
					if (image.HorMirror) {
						rl = imgw - 1 - l;
					}
					else {
						rl = l;
					}
					if (image.VerMirror) {
						rt = imgh - 1 - t;
					}
					else {
						rt = t;
					}
					RGBColor8 color = default ;
					if (gamma == 1) color = TransToColorGamma1(image.Content[rt, rl], min, max);
					else if (gamma == 2) color = TransToColorGamma2(image.Content[rt, rl], min, max);
					else color = TransToColor(image.Content[rt, rl], min, max, gamma);
					colordata[pos] = color.B; pos++;
					colordata[pos] = color.G; pos++;
					colordata[pos] = color.R; pos++;
				}
			}
			Int32Rect rect = new Int32Rect(0, 0, image.Width, image.Height);
			bitmap.WritePixels(rect, colordata, image.Width * byteperpixel, 0);
			return bitmap;
		}


		public static void SaveImageToFile(LightStrongImage image, string path, int gamma = 1, float min = 0.0f, float max = 1.0f) {
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create(GetBitmap(image, min, max, gamma)));
			pngBitmapEncoder.Save(fs);
			fs.Flush();
			fs.Close();
		}
	}
}
