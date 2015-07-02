using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Map
{
	//using Physics;
	//using BaseClasses;
	using Drawing;

	public class MapPlatform : MapItem
	{
		public MapPlatform(Vector2 position, Vector2 endPosition, TextureNode textureNode)
			: base(position, textureNode)
		{
			this.EndPoint = endPosition;
		}

		public Vector2 EndPoint { get; set; }

		public override void AddToWorld(World physicsWorld)
		{
			base.AddToWorld(physicsWorld);
			this.CollisionHull.BodyType = BodyType.Kinematic;
			StartMovement();
		}

		private void StartMovement()
		{
			this.CollisionHull.LinearVelocity = this.EndPoint - this.Position;
		}

		public void InternalUpdateMovement()
		{
			if ((this.CollisionHull.Position - this.Position).LengthSquared() < 1.0f)
			{
				var speed = this.EndPoint - this.Position;
				speed.Normalize();
				this.CollisionHull.LinearVelocity = speed;
			}
			else if ((this.CollisionHull.Position - this.EndPoint).LengthSquared() < 1.0f)
			{
				var speed = this.Position - this.EndPoint;
				speed.Normalize();
				this.CollisionHull.LinearVelocity = speed;
			}
		}

		// TODO:
		public void Move(Vector2 direction, Vector2 speed)
		{
		}

		// TODO:
		public void MovePath(Vector2 pointA, Vector2 pointB, float speed)
		{
		}
	}
}
