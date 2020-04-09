using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Objects {
	public abstract class SceneObject : ISceneObjectAble {
		protected Scene _scene;
		protected Vector3 _position;

		public virtual Scene Scene { get => _scene; internal set => _scene = value; }
		
		public virtual Vector3 Position { get => _position; set =>_position = value; }

		public SceneObject () {
			Position = default;
		}
		public SceneObject(Vector3 position) {
			Position = position;
		}
	}
}
