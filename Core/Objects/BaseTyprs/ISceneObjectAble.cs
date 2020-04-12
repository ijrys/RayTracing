using System;
using System.Collections.Generic;
using System.Text;
using Vector3 = System.Numerics.Vector3;

namespace Core.Objects {
	public interface ISceneObjectAble {
		public Scene Scene { get; }

		Vector3 Position { get; set; }

		public string Name { get; set; }
		//Rotation Rotation { get; set; }
	}
}
