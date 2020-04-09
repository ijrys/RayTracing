using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Objects {
	public interface ISceneObjectAble {
		public Scene Scene { get; }

		Vector3 Position { get; set; }
		//Rotation Rotation { get; set; }
	}
}
