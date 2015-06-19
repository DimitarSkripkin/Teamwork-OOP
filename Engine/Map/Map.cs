using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Map
{
	public class Map
	{
		private const float DrawRadius = 10.0f;

		private List<MapPlatform> platforms;
		private List<MapBlock> blocks;

		public Map()
		{
			this.blocks = new List<MapBlock>();
			this.platforms = new List<MapPlatform>();
		}

		public void AddBlock(MapBlock block)
		{
		}

		public void AddBlocks(List<MapBlock> blocks)
		{
		}

		public void AddPlatform(MapPlatform platform)
		{
		}

		public void AddPlatforms(List<MapPlatform> platforms)
		{
		}

		public void Draw()
		{
			// draw only blocks and platforms that are in certain radius

			foreach (var block in this.blocks)
			{
				block.Draw();
			}

			foreach (var platform in this.platforms)
			{
				platform.Draw();
			}
		}
	}
}
