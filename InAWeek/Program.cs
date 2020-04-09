#if DEBUG
//#define RayDebugger
#endif

using System;
using Core;
using Core.Objects;
using Core.Cameras;


#if UseDouble
using Float = System.Double;
using Math = System.Math;
#else
using Float = System.Single;
using Math = System.MathF;
#endif

using Color = Core.RGBColor8;
using Image = Core.Image<Core.RGBColor8>;
using ImageTool = Core.ImageTools.ImageTools<Core.RGBColor8>;
using Core.Materials;

namespace InAWeek {
	class Program {
		static void Main(string[] args) {
			Scene scene = new Scene();
#if RayDebugger
			scene.Debugger = new Core.Debug.SceneDebug();
#endif
			scene.AppendObject(new SkyBox());
			scene.AppendObject(new Ground(-0.2f));

			Sphere sphere = new Sphere(new Vector3(0, 30, 100), 30) { Material = new Material(new LightStrong(0.9f, 0.2f, 0.2f)) };
			//scene.AppendObject(sphere);

			sphere = new Sphere(new Vector3(-40, 30, 65), 10) { Material = new Material(new LightStrong(1.0f, 0.3f, 1.0f)) };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(0, 30, 65), 10) { Material = new Material(new LightStrong(1.0f, 0.3f, 1.0f)) };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(40, 30, 65), 10) { Material = new Material(new LightStrong(1.0f, 0.3f, 1.0f)) };
			scene.AppendObject(sphere);

			sphere = new Sphere(new Vector3(-40, 10, 70), 10) { Material = new Material(new LightStrong(1.0f, 1.0f, 1.0f)) { MetalDegree = 0.0f } };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(0, 10, 70), 10) { Material = new Material(new LightStrong(1.0f, 1.0f, 1.0f)) { MetalDegree = 0.5f } };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(40, 10, 70), 10) { Material = new Material(new LightStrong(1.0f, 1.0f, 1.0f)) { MetalDegree = 1.0f } };
			scene.AppendObject(sphere);

			//Image image = new Image(72, 48);
			//Image image = new Image(144, 96);
			Image image = new Image(768, 512);
			//Image image = new Image(1536, 1024);

			//TestCamera camera = new TestCamera(Vector3.Zero, 0.0f, 30.0f);
			TraditionalTestCamera camera = new TraditionalTestCamera(new Vector3(0.0f, 15.0f, -50.0f), 2.0f, 1.0f);
			//RenderConfiguration.Configurations.RayTraceDeep = 1;
			RenderConfiguration.Configurations.RayTraceDeep = 6;
			RenderConfiguration.Configurations.SmapingLevel = 4;

			camera.Render(image, scene);

			string fname = $"A:\\img\\{FileName()}";
#if RayDebugger
			Console.WriteLine("save debug file");
			scene.Debugger.SaveToFile(fname + ".obj");
			Console.WriteLine("debug file saved");
#endif

			Console.WriteLine("save to " + fname + ".png");
			ImageTool.SaveImageToFile(image, fname + ".png");

			//Test();
			Console.WriteLine("finish");
		}

		static void Test() {
			Sphere sphere = new Sphere(new Vector3(0, 0, 100), 50);
			Ray ray = new Ray(Vector3.Zero, new Vector3(0, 0, 1));
			(Float deep, Vector3 point, _) = sphere.IntersectDeep(ray);

			Console.WriteLine(deep);
		}
		static string FileName() {
			DateTime time = DateTime.Now;
			string re = time.ToString("yyyyMMdd_HHmmss");
			return re;
		}
	}
}
