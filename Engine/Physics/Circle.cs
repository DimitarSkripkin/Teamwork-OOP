using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	public class Circle : CollisionShape
	{
		private float radius;

		public Circle(float radius, CollisionObjectFlags objectFlags, int collisionMask)
			: this(radius, Vector2.Zero, objectFlags, collisionMask)
		{
		}

		public Circle(float radius, Vector2 position, CollisionObjectFlags objectFlags, int collisionMask = 1)
			: base(position, objectFlags, collisionMask)
		{
			this.Radius = radius;
		}

		public float Radius
		{
			get { return radius; }
			set { radius = value; }
		}
	}
}
