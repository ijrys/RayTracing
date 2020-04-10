using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Materials {

	public class Material {

		public LightStrong BaseColor { get; set; } = new LightStrong(0.25f, 0.25f, 0.25f);

		public bool LightAble { get; set; }
		public LightStrong LightColor { get; set; } = new LightStrong(0.8f, 0.8f, 0.8f);

		private float _metalDegree = 0.0f;
		/// <summary>
		/// 金属程度
		/// </summary>
		public float MetalDegree {
			get => _metalDegree;
			set {
				if (value < 0) value = 0;
				else if (value > 1) value = 1;
				_metalDegree = value;
				AMetalDegree = 1.0f - 0.95f * value;
			}
		}
		/// <summary>
		/// 反金属度
		/// </summary>
		public float AMetalDegree = 1.0f;
		/// <summary>
		/// 是否为透明物体
		/// </summary>
		public bool IsTransparent = false;
		/// <summary>
		/// 折射率
		/// </summary>
		public float RefractiveIndices = 1.0f;

		public Material() { }
		public Material (LightStrong baseColor) {
			BaseColor = baseColor;
		}
	}
}
