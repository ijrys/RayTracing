using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageCore {
	public class ImageTools {
		private static WriteableBitmap GetBitmap(MiRaIImage image) {
			WriteableBitmap bitmap = new WriteableBitmap(image.Width, image.Height, 96, 96, System.Windows.Media.PixelFormats.Bgra32, null);
			int byteperpixel = bitmap.Format.BitsPerPixel / 8;
			byte[] colordata = new byte[image.Width * image.Height * byteperpixel];
			int pos = 0;
			for (int t = 0; t < image.Height; t++) {
				for (int l = 0; l < image.Width; l++) {
					MiRaIColor color = image.Contents[t * image.Width + l];
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
		public static void SaveImageToFile(MiRaIImage image, string path) {
			FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create(GetBitmap(image)));
			pngBitmapEncoder.Save(fs);
			fs.Flush();
			fs.Close();
		}
	}
}
