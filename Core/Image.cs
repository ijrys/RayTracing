﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core {
	public class Image<ColorT> {
		int _width, _height;
		ColorT[,] _content;
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
		public ColorT[,] Content { get => _content; }

		public Image(int width, int height) {
			_width = width;
			_height = height;
			_content = new ColorT[height, width];
		}

		public void SetColor(int t, int l, ColorT color) {
			_content[t, l] = color;
		}
		public ColorT GetColor(int t, int l) {
			return _content[t, l];
		}

		public ColorT this[int t, int l] {
			get {
				return _content[t, l];
			}
			set {
				_content[t, l] = value;
			}
		}
	}
	public class LightStrongImage {
		int _width, _height;
		LightStrong[,] _content;
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
		public LightStrong[,] Content { get => _content; }

		public LightStrongImage(int width, int height) {
			_width = width;
			_height = height;
			_content = new LightStrong[height, width];
		}

		public void SetColor(int t, int l, LightStrong color) {
			_content[t, l] = color;
		}
		public LightStrong GetColor(int t, int l) {
			return _content[t, l];
		}

		public LightStrong this[int t, int l] {
			get {
				return _content[t, l];
			}
			set {
				_content[t, l] = value;
			}
		}
	}
}