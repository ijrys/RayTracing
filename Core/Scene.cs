#if DEBUG
//#define RayDebugger
#endif

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

using Color = Core.RGBColor8;
using Image = Core.Image<Core.RGBColor8>;
using Core.Debug;

namespace Core {
	public class Scene {
		List<RenderObject> Objects = new List<RenderObject>();
#if RayDebugger
		public SceneDebug Debugger = default;
#endif

		public void AppendObject(RenderObject renderobj) {
			renderobj.Scene = this;
			Objects.Add(renderobj);
		}

		/// <summary>
		/// 获取光线追踪辐照度
		/// </summary>
		/// <param name="ray">光线</param>
		/// <param name="deep">当前追踪深度</param>
		/// <returns>颜色，追踪距离</returns>
		public LightStrong Render(Ray ray) {
			return Light(ray, RenderConfiguration.Configurations.RayTraceDeep);
		}

		public LightStrong Color(Ray ray, int deep, IRenderAble ignore = null) {
			if (deep < 0 || Objects == null || Objects.Count == 0) return LightStrong.Dark;
			ray = new Ray(ray.Origin, ray.Direction.Normalize());
			Float minDistance = Float.NaN;
			Vector3 point = default, normal = default;
			IRenderAble minobj = null;
			foreach (IRenderAble obj in Objects) {
				(Float d, Vector3 p, Vector3 n) = obj.IntersectDeep(ray);
				if (d > 0 && (Float.IsNaN(minDistance) || minDistance > d)) {
					minobj = obj;
					minDistance = d;
					point = p;
					normal = n;
				}
			}
			LightStrong c;
			if (minobj != null) {
				c = minobj.IntersectColor(point, ray.Direction, normal, deep);
			}
			else {
				c = new LightStrong(0.25f, 0.25f, 0.25f);
			}
			return c;
		}

		/// <summary>
		/// 获取光线追踪辐照度
		/// </summary>
		/// <param name="ray">光线</param>
		/// <param name="deep">当前追踪深度</param>
		/// <returns>颜色，追踪距离</returns>
		public LightStrong Light(Ray ray, int deep, IRenderAble ignore = null) {
			if (deep < 0 || Objects == null || Objects.Count == 0) return LightStrong.Dark;
			ray = new Ray(ray.Origin, ray.Direction.Normalize());
			Float minDistance = Float.NaN;
			Vector3 point = default, normal = default;
			IRenderAble minobj = null;
			foreach (IRenderAble obj in Objects) {
				if (obj == ignore) continue;
				(Float d, Vector3 p, Vector3 n) = obj.IntersectDeep(ray);
				if (d > 0 && (Float.IsNaN(minDistance) || minDistance > d)) {
					minobj = obj;
					minDistance = d;
					point = p;
					normal = n;
				}
			}
			//LightStrong c = minobj.IntersectLight(point, ray.Direction, normal, deep);
#if RayDebugger
			if (Debugger != null) {
				if (Float.IsNaN(minDistance) || Float.IsInfinity(minDistance) || minobj is SkyBox) {
					Debugger.Ray.EndLine(ray.Direction);
				}
				else {
					Debugger.Ray.AppendPoint(point);
				}
			}
#endif
			//if (minobj is SkyBox) {
			//	return c;
			//}
			//else {
			//	return c * Tools.LightStrongByDistance(minDistance);
			//}
			LightStrong c;
			if (minobj != null) {
				c = minobj.IntersectLight(point, ray.Direction, normal, deep);
				//if (!(minobj is SkyBox)) {
				//	c *= Tools.LightStrongByDistance(minDistance);
				//}
			}
			else {
				c = new LightStrong(0.25f, 0.25f, 0.25f);
			}
			return c;
		}
	}
}
