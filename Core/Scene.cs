using Core.Objects;
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

using Core.Debug;

namespace Core {
	public class Scene {
#if RayDebugger
		public SceneDebug debugger;
#endif

		List<RenderObject> Objects = new List<RenderObject>();
		RenderObject[] objects;
		public static int ZXJHL = 0;

		public void AppendObject(RenderObject renderobj) {
			renderobj.Scene = this;
			Objects.Add(renderobj);
		}

		public void ReadyToRender () {
			objects = Objects.ToArray();
		}

		/// <summary>
		/// 获取光线追踪辐照度
		/// </summary>
		/// <param name="ray">光线</param>
		/// <param name="deep">当前追踪深度</param>
		/// <returns>颜色，追踪距离</returns>
		public LightStrong Render(Ray ray) {
			(LightStrong c, Float _) = Light(ray, RenderConfiguration.Configurations.RayTraceDeep, null);
			return c;
		}

		/// <summary>
		/// 获取光线追踪辐照度
		/// </summary>
		/// <param name="ray">光线</param>
		/// <param name="deep">当前追踪深度</param>
		/// <returns>颜色，追踪距离</returns>
		public (LightStrong, Float) Light(Ray ray, int deep, RenderObject callerObj, RenderObject ignore = null) {
			if (deep < 0 || Objects == null || Objects.Count == 0) return (LightStrong.Dark, 0.0f);
			ray = new Ray(ray.Origin, ray.Direction.Normalize());
			Float minDistance = Float.NaN;
			Vector3 point = default, normal = default;
			RenderObject minobj = null;
			foreach (RenderObject obj in objects) {
				if (obj == ignore) continue;
				(Float d, Vector3 p, Vector3 n) = obj.IntersectDeep(ray);
				if (d > 0 && (Float.IsNaN(minDistance) || minDistance > d)) {
					if (obj == callerObj && d <= 0.001f) {
						//ZXJHL++;
						continue;
					}
					minobj = obj;
					minDistance = d;
					point = p;
					normal = n;
				}
			}

			LightStrong c;
			if (minobj != null) {
				//Console.WriteLine($"rt: {minobj.Name} \t point:{point} \t dir:{ray.Direction} \t caller:{callerObj?.Name}");
				c = minobj.IntersectLight(point, ray.Direction, normal, deep);
			}
			else {
				c = new LightStrong(0.25f, 0.25f, 0.25f);
			}
			return (c, minDistance);
		}
	}
}
