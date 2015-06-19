using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	using BaseClasses;

	public enum CollisionObjectFlags
	{
		Static,
		Dynamic,
		Kinematic
	}

	public abstract class CollisionShape
	{
		private Vector2 position;
		private Vector2 velocity;

		protected CollisionShape(Vector2 position, CollisionObjectFlags objectFlags, int collisionMask = 1)
		{
			this.Position = position;
			this.ObjectFlags = objectFlags;
			this.CollisionMask = collisionMask;
		}

		public event CollisionEventHandler CollisionHandler;

		public CollisionObjectFlags ObjectFlags { get; set; }

		public int CollisionMask { get; set; }

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Vector2 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		public void RaiseCollision(CollisionEventArgs eventArgs)
		{
			if (CollisionHandler != null)
			{
				CollisionHandler(eventArgs);
			}
		}
	}
}
