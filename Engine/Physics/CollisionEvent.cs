using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Physics
{
	public delegate void CollisionEventHandler(CollisionEventArgs eventArgs );

	public class CollisionEventArgs : EventArgs
	{
		public CollisionEventArgs(CollisionShape collidesWith)
		{
			this.CollidesWith = collidesWith;
		}

		public CollisionShape CollidesWith { get; set; }
	}
}
