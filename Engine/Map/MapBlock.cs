using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Map
{
	using BaseClasses;
	using Physics;
	using Drawing;

	public class MapBlock : CollidableObject
	{
		public static readonly Vector2 DefaultBlockSize = new Vector2( 32.0f );

		public MapBlock(Vector2 position, TextureNode textureNode, CollisionObjectFlags objectFlags, int id)
			: base(new AABB(position, DefaultBlockSize, objectFlags), id)
		{
			this.TextureNode = textureNode;
		}

		public Vector2 Position
		{
			get
			{
				return this.CollisionHull.Position;
			}
		}

		public TextureNode TextureNode { get; set; }
	}
}
