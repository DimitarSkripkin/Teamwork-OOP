using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Teamwork_OOP.Engine.Map
{
	using Drawing;

	public class MapFlagBlock : MapBlock
	{
		public MapFlagBlock(Vector2 position, Point size, TextureNode textureNode)
			: base(position, size, textureNode)
		{
		}

		public override void AddToWorld(World physicsWorld)
		{
			base.AddToWorld(physicsWorld);

			this.CollisionHull.IsSensor = true;
		}
	}
}
