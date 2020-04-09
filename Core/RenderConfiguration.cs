using System;
using System.Collections.Generic;
using System.Text;

namespace Core {
	public class RenderConfiguration {
		public int RayTraceDeep = 2;
		public int SmapingLevel = 2;
		public int ReflectSmapingLevel = 4;

		public static RenderConfiguration Configurations { get; set; } = new RenderConfiguration();
	}
}
