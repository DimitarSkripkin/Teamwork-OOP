using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace Teamwork_OOP.Engine.Map
{
	using BaseClasses;
	using Drawing;

	public abstract class MapItem : CollidableObject
	{
		protected MapItem(Vector2 position, TextureNode textureNode)
		{
			this.Position = position;
			this.TextureNode = textureNode;
		}

		public Vector2 Position { get; set; }

		public TextureNode TextureNode { get; set; }

		public override void AddToWorld(World physicsWorld)
		{
			var size = ConvertUnits.ToSimUnits(new Vector2(this.TextureNode.SourceRectangle.Width, this.TextureNode.SourceRectangle.Height));
			this.CollisionHull = BodyFactory.CreateRectangle(physicsWorld,
						size.X,
						size.Y,
						10.0f,
						this.Position + size / 2.0f,
						this);

			foreach (var fixture in this.CollisionHull.FixtureList)
			{
				fixture.UserData = this;
			}
		}
	}
}
