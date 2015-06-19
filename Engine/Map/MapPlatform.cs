using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Map
{
	using Physics;
	using BaseClasses;

	public class MapPlatform//:GameObject
	{
		private static readonly Vector2 DefaultSize = new Vector2(1,1);

		private AABB boundingBox;

		private List<MapBlock> blocks;
		private int size;// Vector2 size;

		// or give position, MapBlock and use the same block, size
		public MapPlatform(Vector2 position, int size)
		{
			this.boundingBox = new AABB(position, new Vector2(DefaultSize.X * size, DefaultSize.Y), CollisionObjectFlags.Kinematic);
			this.blocks = new List<MapBlock>();
			this.Size = size;
		}

		public int Size
		{
			get { return size; }
			set
			{
				// TODO: ADD VALUE VALIDATION !!!!
				if (value < size)
				{
					this.blocks.RemoveRange(value, size - value);
				}
				else
				{
					int lastMapBlockIndex = this.blocks.Count - 1;
					var lastMapBlock = this.blocks[lastMapBlockIndex];

					for (int i = 0; i < value - lastMapBlockIndex; ++i)
					{
						this.blocks.Add(new MapBlock(
							new Vector2( lastMapBlock.Position.X + i*DefaultSize.X, lastMapBlock.Position.Y),
							CollisionObjectFlags.Kinematic,
							lastMapBlockIndex + i));
					}
				}

				size = value;
			}
		}

		public Vector2 Position
		{
			get
			{
				return this.boundingBox.Position;
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

		public void Draw()
		{
			foreach (var block in this.blocks)
			{
				block.Draw();
			}
		}
	}
}
