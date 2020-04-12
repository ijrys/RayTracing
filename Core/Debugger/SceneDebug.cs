using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Vector3 = System.Numerics.Vector3;

namespace Core.Debug {
	public class SceneDebug {
		/// <summary>
		/// 表示一条追踪光线
		/// </summary>
		public struct RayLine {
			int s, e;
			public RayLine(int ss, int ee) {
				s = ss;
				e = ee;
			}
			public override string ToString() {
				return $"l {s} {e}";
			}
		}
		//public class RayLine {
		//	public List<int> Line = new List<int>();
		//	/// <summary>
		//	/// 当前分支添加追踪点
		//	/// </summary>
		//	/// <param name="pointIndex"></param>
		//	public void AppendPoint(int pointIndex) {
		//		Line.Add(pointIndex);
		//	}
		//	public override string ToString() {
		//		StringBuilder sb = new StringBuilder("l");
		//		foreach (var item in Line) {
		//			sb.Append(' ');
		//			sb.Append(item);
		//		}
		//		return sb.ToString();
		//	}
		//}

		private List<Vector3> points = new List<Vector3>(); // { Vector3.Zero, Vector3.One };
		List<RayLine> Rays = new List<RayLine>();
		//public RayLine Ray;
		int[] bnodes = new int[128];
		int bnodesTop = 0;
		int last = -1;
		public int bbtimes = 0, ebtimes = 0, errtimes = 0;

		private bool lastEB = false;

		public int AppendPoint(Vector3 point) {
			points.Add(point);
			int index = points.Count;
			AppendPoint(index);
			return index;
		}
		public void AppendPoint(int point) {
			lastEB = false;
			//Console.WriteLine("AP " + point);
			if (last > 0) {
				Rays.Add(new RayLine(last, point));
			}
			last = point;
			//bnodes[bnodesTop] = point;
		}

		public void EndLine(Vector3 directionary) {
			lastEB = false;
			//Console.WriteLine("EL");
			//int nowindex = bnodes[bnodesTop];
			Vector3 p = points[last - 1] + directionary;
			AppendPoint(p);
		}

		public void BeginBranch(Vector3 point) {
			//Console.WriteLine("BB");
			int i = AppendPoint(point);
			BeginBranch(i);
		}
		public void BeginBranch(int point) {
			bbtimes++;
			lastEB = false;
			bnodes[bnodesTop] = point;
			bnodesTop++;
		}
		public void EndBranch() {
			ebtimes++;
			bnodesTop--;
			if (bnodesTop > 0) {
				last = bnodes[bnodesTop - 1];
			} else {
				errtimes++;
			}
		}

		public void SaveToFile(string path) {
			using (FileStream fs = new FileStream(path, FileMode.Create)) {
				using (StreamWriter sw = new StreamWriter(fs)) {
					foreach (var p in points) {
						sw.WriteLine($"v {p.X:0.000} {p.Y:0.000} {p.Z:0.000}");
					}

					sw.WriteLine("g RayDebug");
					foreach (var r in Rays) {
						sw.WriteLine(r.ToString());
					}
				}
			}
		}

		public SceneDebug() {
		}
	}
}
