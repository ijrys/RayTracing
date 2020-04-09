using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Core.ImageTools {
	public class ImageTools<ColorT> {
		private static WriteableBitmap GetBitmapFromRGB8(Image<RGBColor8> image) {
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
					} else {
						rl = l;
					}
					if (image.VerMirror) {
						rt = imgh - 1 - t;
					}
					else {
						rt = t;
					}
					RGBColor8 color = image.Content[rt, rl];
					colordata[pos] = color.B; pos++;
					colordata[pos] = color.G; pos++;
					colordata[pos] = color.R; pos++;
				}
			}
			Int32Rect rect = new Int32Rect(0, 0, image.Width, image.Height);
			bitmap.WritePixels(rect, colordata, image.Width * byteperpixel, 0);
			return bitmap;
		}

		private static WriteableBitmap GetBitmapFromRGBA8(Image<RGBAColor8> image) {
			WriteableBitmap bitmap = new WriteableBitmap(image.Width, image.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);
			int byteperpixel = bitmap.Format.BitsPerPixel / 8;
			byte[] colordata = new byte[image.Width * image.Height * byteperpixel];
			int pos = 0;
			for (int t = 0; t < image.Height; t++) {
				for (int l = 0; l < image.Width; l++) {
					RGBAColor8 color = image.Content[t, l];
					colordata[pos] = color.B; pos++;
					colordata[pos] = color.G; pos++;
					colordata[pos] = color.R; pos++;
					colordata[pos] = color.A; pos++;
				}
			}
			Int32Rect rect = new Int32Rect(0, 0, image.Width, image.Height);
			bitmap.WritePixels(rect, colordata, image.Width * byteperpixel, 0);
			return bitmap;
		}

		private static WriteableBitmap GetBitmap(Image<ColorT> image) {
			//System.Windows.Media.PixelFormat formate;
			if (typeof(ColorT) == typeof(RGBColor8)) {
				return GetBitmapFromRGB8(image as Image<RGBColor8>);
			}
			else if (typeof(ColorT) == typeof(RGBAColor8)) {
				return GetBitmapFromRGBA8(image as Image<RGBAColor8>);
			}
			return null;
		}

		public static void SaveImageToFile(Image<ColorT> image, string path) {
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create(GetBitmap(image)));
			pngBitmapEncoder.Save(fs);
			fs.Flush();
			fs.Close();
		}
	}
}
