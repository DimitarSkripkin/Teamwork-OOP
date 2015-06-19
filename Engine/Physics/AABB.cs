using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	public class AABB : CollisionShape
	{
		private Vector2 min;
		private Vector2 max;

		public AABB(Vector2 position, Vector2 size, CollisionObjectFlags objectFlags, int collisionMask = 1)
			: this(position, Vector2.Zero, size, objectFlags, collisionMask)
		{
		}

		public AABB(Vector2 position, Vector2 min, Vector2 max, CollisionObjectFlags objectFlags, int collisionMask = 1)
			: base(position, objectFlags, collisionMask)
		{
			//this.Min = min;
			//this.Max = max;

			SetMinMax(min, max);
		}

		public void SetMinMax(Vector2 min, Vector2 max)
		{
			this.min = new Vector2(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y));
			this.max = new Vector2(Math.Max(min.X, max.X), Math.Max(min.Y, max.Y));
		}

		public Vector2 Min
		{
			get
			{
				return min + Position;
			}
			set
			{
				SetMinMax(value, this.max);
			}
		}
		public Vector2 Max
		{
			get
			{
				return max + Position;
			}
			set
			{
				SetMinMax(this.min, value);
			}
		}
	}
}
