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

	public class MapBlock
	{
		private Point size;

		// Point poneje e int razmera
		public MapBlock(Vector2 position, Point size, TextureNode textureNode)
		{
			this.TextureNode = textureNode;
			this.Position = position;
			this.Size = size;
		}

		public virtual void AddToWorld(World physicsWorld)
		{
			var size = ConvertUnits.ToSimUnits(new Vector2(this.TextureNode.SourceRectangle.Width * this.Size.X, this.TextureNode.SourceRectangle.Height * this.Size.Y));
			this.CollisionHull = BodyFactory.CreateRectangle(physicsWorld,
						size.X,
						size.Y,
						10.0f,
						this.Position + size/2.0f,
						this);

			foreach (var fixture in this.CollisionHull.FixtureList)
			{
				fixture.UserData = this;
			}
		}

		public Point Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (value.X <= 0 || value.Y <= 0)
				{
					throw new ArgumentException();
				}
				this.size = value;
			}
		}

		public Vector2 Position { get; set; }

		public TextureNode TextureNode { get; set; }

		public Body CollisionHull { get; protected set; }
	}
}
