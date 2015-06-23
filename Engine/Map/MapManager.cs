using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Map
{
	using Physics;
	using Drawing;

	public class MapManager
	{
		private List<MapPlatform> platforms;
		private List<MapBlock> blocks;
		private PhysicsEngine physicsEngine;

		public MapManager(PhysicsEngine physicsEngine)
		{
			this.blocks = new List<MapBlock>();
			this.platforms = new List<MapPlatform>();

			this.PhysicsEngine = physicsEngine;
		}

		public IList<MapPlatform> Platforms
		{
			get
			{
				return this.platforms;
			}
		}

		public IList<MapBlock> Blocks
		{
			get
			{
				return this.blocks;
			}
		}

		public PhysicsEngine PhysicsEngine
		{
			get { return physicsEngine; }
			set { physicsEngine = value; }
		}

		public void AddBlock(MapBlock block)
		{
			this.physicsEngine.AddItem(block.CollisionHull);
			this.blocks.Add(block);
		}

		public void AddBlocks(List<MapBlock> blocks)
		{
		}

		public void AddPlatform(MapPlatform platform)
		{
			this.physicsEngine.AddItem(platform.CollisionHull);
			this.platforms.Add(platform);
		}

		public void AddPlatforms(List<MapPlatform> platforms)
		{
		}

		public void AddEntity()
		{
		}

		public void AddItem()
		{
		}
	}
}
