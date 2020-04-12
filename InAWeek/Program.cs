//#define RayDebugger

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

using Image = Core.LightStrongImage;
using ImageTool = Core.ImageTools.ImageTools;
using Core.Materials;
using Core.Debug;

namespace InAWeek {
	class Program {

		static RenderConfiguration OutputConfig = new RenderConfiguration() {
			RayTraceDeep = 8,
			SmapingLevel = 4,
			ReflectSmapingLevel = 5,
		};
		static RenderConfiguration CheckConfig = new RenderConfiguration() {
			RayTraceDeep = 2,
			SmapingLevel = 2,
			ReflectSmapingLevel = 1,
		};
		static RenderConfiguration ReviewConfig = new RenderConfiguration() {
			RayTraceDeep = 6,
			SmapingLevel = 4,
			ReflectSmapingLevel = 4,
		};

		static Scene GetScene() {
			Scene scene = new Scene();
#if RayDebugger
			scene.debugger = new SceneDebug();
#endif
			scene.AppendObject(new SkyBox() { Name = "sky" });
			scene.AppendObject(new Ground(-0.2f) { Name = "ground" });
			return scene;
		}

		static Scene SceneAppendMatelTest(Scene scene) {
			Sphere sphere;

			sphere = new Sphere(new Vector3(-40, 30, 65), 10) { Name = "sp1", Material = new Material(new LightStrong(0.3f, 1.0f, 1.0f)) };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(0, 30, 65), 10) { Name = "sp2", Material = new Material(new LightStrong(1.0f, 0.3f, 1.0f)) };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(40, 30, 65), 10) { Name = "sp3", Material = new Material(new LightStrong(1.0f, 1.0f, 0.3f)) };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(0.0f, 10.0f, -120.0f), 10) { Name = "sp4", Material = new Material(new LightStrong(0.3f, 0.3f, 0.3f)) };
			scene.AppendObject(sphere);

			sphere = new Sphere(new Vector3(-40, 10, 70), 10) { Name = "sp5", Material = new Material(new LightStrong(1.0f, 1.0f, 1.0f)) { MetalDegree = 0.0f } };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(0, 10, 70), 10) { Name = "sp6", Material = new Material(new LightStrong(1.0f, 1.0f, 1.0f)) { MetalDegree = 0.5f } };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(40, 10, 70), 10) { Name = "sp7", Material = new Material(new LightStrong(1.0f, 1.0f, 1.0f)) { MetalDegree = 1.0f } };
			scene.AppendObject(sphere);

			return scene;
		}

		static Scene SceneAppendTransparent(Scene scene) {
			Sphere sphere;
			sphere = new Sphere(new Vector3(-15, 6, 30), 6) { Name = "spt1", Material = new Material(new LightStrong(0.8f, 1.0f, 0.8f)) { MetalDegree = 0.5f, IsTransparent = true, RefractiveIndices = 1.5f, TransparentIndex = 0.5f } };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(0, 6, 30), 6) { Name = "spt2", Material = new Material(new LightStrong(0.8f, 1.0f, 0.8f)) { MetalDegree = 0.75f, IsTransparent = true, RefractiveIndices = 1.5f } };
			scene.AppendObject(sphere);
			sphere = new Sphere(new Vector3(15, 6, 30), 6) { Name = "spt3", Material = new Material(new LightStrong(0.8f, 1.0f, 0.8f)) { MetalDegree = 1.0f, IsTransparent = true, RefractiveIndices = 1.5f } };
			scene.AppendObject(sphere);
			return scene;
		}

		static void Main(string[] args) {
			//Test();
			//return;

			DateTime bgTime = DateTime.Now;
			DateTime nTime;

			Scene scene = GetScene();
			SceneAppendMatelTest(scene);
			SceneAppendTransparent(scene);


			//Image image = new Image(72, 48);
			//Image image = new Image(144, 96);
			//Image image = new Image(384, 256);
			Image image = new Image(768, 512);
			//Image image = new Image(1536, 1024);

#if RayDebugger
			LaserCamera camera = new LaserCamera(new Vector3(0.0f, 15.0f, -100.0f), 1.0f, 1.5f);
			camera.Points.Add(new LaserCamera.PointPair(524, 296));
			//TraditionalTestCamera camera = new TraditionalTestCamera(new Vector3(0.0f, 15.0f, -100.0f), 1.0f, 1.5f);
#else
			TraditionalTestCamera camera = new TraditionalTestCamera(new Vector3(0.0f, 15.0f, -100.0f), 1.0f, 1.5f);
#endif
			RenderConfiguration.Configurations = OutputConfig;

			Console.WriteLine((DateTime.Now - bgTime).ToString("hh\\:mm\\:ss") + " begin render");
			camera.Render(image, scene);
			Console.WriteLine((DateTime.Now - bgTime).ToString("hh\\:mm\\:ss") + " end render");

			string fname = $"A:\\img\\{FileName()}";
#if RayDebugger
			Console.WriteLine("save debug file");
			scene.debugger.SaveToFile(fname + ".obj");
			Console.WriteLine("debug file saved");
#endif

			Console.WriteLine("save to " + fname + ".png");
			ImageTool.SaveImageToFile(image, fname + ".png", 2);

			Console.WriteLine((DateTime.Now - bgTime).ToString("hh\\:mm\\:ss") + "finish");
		}

		static void Test() {
			SceneDebug dbg = new SceneDebug();
			dbg.BeginBranch(new Vector3(1, 1, 1));
			dbg.BeginBranch(new Vector3(2, 2, 2));
			dbg.BeginBranch(new Vector3(3, 3, 3));
			dbg.EndBranch();
			dbg.BeginBranch(new Vector3(4, 4, 4));
			dbg.EndBranch();

			string fname = $"A:\\img\\{FileName()}";
			dbg.SaveToFile(fname + ".obj");
		}
		static string FileName() {
			DateTime time = DateTime.Now;
			string re = time.ToString("yyyyMMdd_HHmmss");
			return re;
		}
	}
}
