using System;

namespace Test {
	class Program {
		/// <summary>
		/// 角度转为弧度
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static double Radian(double angle) {
			return Math.PI / 180 * angle;
		}
		static string DeleteLastZeros(string s) {
			int count = 0;
			for (int i = s.Length - 1; i > 0; i--) {
				if (s[i] != '0') {
					if (s[i] == '.') count--;
					break;
				}
				count++;
			}
			s = s.Substring(0, s.Length - count);
			return s;
		}
		static void GeneratePoint(double horR, double verR) {
			horR = Radian(horR);
			verR = Radian(verR);
			double y = Math.Cos(verR);
			double xz = Math.Sqrt(1.0 - y * y);
			double x = Math.Cos(horR) * xz;
			double z = Math.Sin(horR) * xz;
			string formate = "0.000000000000000";
			string xstr = DeleteLastZeros(x.ToString(formate));
			string ystr = DeleteLastZeros(y.ToString(formate));
			string zstr = DeleteLastZeros(z.ToString(formate));
			Console.WriteLine($"new Vector3 ({xstr}f, {ystr}f, {zstr}f),");
		}
		static void GenerateLevel(int level) {
			Console.WriteLine($"new Vector3[] {{");
			int c = 90 / level;
			if (c % 5 != 0) {
				c = ((c / 5) + 1) * 5;
			}
			GeneratePoint(0, 0);
			for (int i = 1; i < level; i++) {
				Console.WriteLine();
				double horR = (i % 2 == 0 ? 60.0 : 0.0), verR = i * c;
				for (int j = 0; j < 3; j++) {
					GeneratePoint(horR + 120.0 * j, verR);
				}
			}
			Console.WriteLine("},");
		}

		static void Main(string[] args) {
			for (int i = 1; i <9; i ++) {
				GenerateLevel(i);
				Console.WriteLine();
			}
		}
	}
}
