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

namespace Core {
	public static class ConstValues {
		public const Float Zero = 0;
		public const Float One = 1;
	}
}
