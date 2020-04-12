using System;
using System.Collections.Generic;
using System.Text;

namespace Core {

	public class LightStrongImage {
		int _width, _height;
		Light[,] _content;
		/// <summary>
		/// 水平镜像
		/// </summary>
		public bool HorMirror { get; set; } = false;
		/// <summary>
		/// 垂直镜像
		/// </summary>
		public bool VerMirror { get; set; } = false;

		public int Width { get => _width; }
		public int Height { get => _height; }
		public Light[,] Content { get => _content; }

		public LightStrongImage(int width, int height) {
			_width = width;
			_height = height;
			_content = new Light[height, width];
		}

		public void SetColor(int t, int l, Light color) {
			_content[t, l] = color;
		}
		public Light GetColor(int t, int l) {
			return _content[t, l];
		}

		public Light this[int t, int l] {
			get {
				return _content[t, l];
			}
			set {
				_content[t, l] = value;
			}
		}
	}
}
