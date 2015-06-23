using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Map
{
	using Physics;
	using BaseClasses;
	using Drawing;

	public class MapPlatform//:GameObject
	{
		private AABB boundingBox;
		private TextureNode textureNode;

		private List<MapBlock> blocks;
		private int size;// Vector2 size;

		// or give position, MapBlock and use the same block, size
		public MapPlatform(Vector2 position, int size, TextureNode textureNode)
		{
			this.boundingBox = new AABB(position, new Vector2(MapBlock.DefaultBlockSize.X * size, MapBlock.DefaultBlockSize.Y), CollisionObjectFlags.Kinematic);
			this.blocks = new List<MapBlock>();

			this.TextureNode = textureNode;

			this.Size = size;
		}

		public IList<MapBlock> Blocks
		{
			get
			{
				return this.blocks;
			}
		}

		public CollisionShape CollisionHull
		{
			get
			{
				return this.boundingBox;
			}
		}

		public TextureNode TextureNode
		{
			get
			{
				return this.textureNode;
			}
			set
			{
				foreach (var block in this.blocks)
				{
					block.TextureNode = value;
				}
				this.textureNode = value;
			}
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
					int lastMapBlockIndex = 0;
					var offset = this.Position;
					var offsetStep = new Vector2(MapBlock.DefaultBlockSize.X, 0.0f);

					if (this.blocks.Count > 0)
					{
						lastMapBlockIndex = this.blocks.Count - 1;
						var lastMapBlock = this.blocks[lastMapBlockIndex];
						offset = lastMapBlock.Position;
					}

					for (int i = 0; i < value - lastMapBlockIndex; ++i)
					{
						this.blocks.Add(new MapBlock(
							offset,
							this.TextureNode,
							CollisionObjectFlags.Kinematic,
							lastMapBlockIndex + i));

						offset += offsetStep;
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
	}
}
