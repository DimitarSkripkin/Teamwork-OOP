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
			this.CollisionHull = BodyFactory.CreateRectangle(physicsWorld,
						ConvertUnits.ToSimUnits(this.TextureNode.Texture.Width * this.Size.X),
						ConvertUnits.ToSimUnits(this.TextureNode.Texture.Height * this.Size.Y),
						10.0f,
						this.Position,
						this);
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

		public Body CollisionHull { get; private set; }
	}
}
