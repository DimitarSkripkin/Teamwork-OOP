using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Map
{
	//using Physics;
	//using Drawing;
	using BaseClasses;

	public class MapManager
	{
		private List<MapBlock> blocks;
		private List<MapTriggerBlock> triggers;
		private List<MapPlatform> platforms;
		private List<Entity> entities;

		public MapManager()
		{
			this.blocks = new List<MapBlock>();
			this.triggers = new List<MapTriggerBlock>();
			this.platforms = new List<MapPlatform>();
		}

		public Texture2D Background { get; set; }

		public List<MapBlock> Blocks
		{
			get
			{
				return this.blocks;
			}
		}

		// only for editor ?
		public List<MapTriggerBlock> Triggers
		{
			get
			{
				return this.triggers;
			}
		}

		public List<MapPlatform> Platforms
		{
			get
			{
				return this.platforms;
			}
		}

		public List<Entity> Entities
		{
			get
			{
				return this.entities;
			}
		}

		public void InitPhysics(World physicsWorld)
		{
			foreach (var block in this.blocks)
			{
				block.AddToWorld(physicsWorld);
			}

			foreach (var trigger in this.triggers)
			{
				trigger.AddToWorld(physicsWorld);
			}

			foreach (var platform in this.platforms)
			{
				platform.AddToWorld(physicsWorld);
			}

			foreach (var entity in this.entities)
			{
				//entity.AddToWorld(physicsWorld);
			}
		}

		public void AddBlock(MapBlock block)
		{
			this.blocks.Add(block);
		}

		public void AddTrigger(MapTriggerBlock triggerBlock)
		{
			this.triggers.Add(triggerBlock);
		}

		public void AddPlatform(MapPlatform platform)
		{
			this.platforms.Add(platform);
		}

		public void RemoveBlock(MapBlock block)
		{
			this.blocks.Remove(block);
		}

		public void RemoveTrigger(MapTriggerBlock triggerBlock)
		{
			this.triggers.Remove(triggerBlock);
		}

		public void AddEntity(Entity entity)
		{
			this.entities.Add(entity);
		}

		public void RemoveEntity(Entity entity)
		{
			this.entities.Remove(entity);
		}

		public void Update()
		{
			foreach (var platform in this.platforms)
			{
				platform.InternalUpdateMovement();
			}
		}

		//public void AddItem()
		//{
		//}
	}
}
