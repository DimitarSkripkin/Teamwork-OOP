using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Map
{
	using BaseClasses;
	using Physics;

	public class MapBlock : CollidableObject
	{
		private static readonly Vector2 BlockSize = new Vector2( 1.0f, 1.0f );

		public MapBlock(Vector2 position, CollisionObjectFlags objectFlags, int id)
			: base(new AABB(position, BlockSize, objectFlags), id)
		{
		}

		public Vector2 Position
		{
			get
			{
				return this.CollisionHull.Position;
			}
		}

		public virtual void Draw()
		{
		}
	}
}
