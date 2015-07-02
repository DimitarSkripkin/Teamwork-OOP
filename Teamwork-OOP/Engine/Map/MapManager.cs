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
		private List<MapSpawnPoint> spawnPoints;
		private List<MapCheckPoint> checkPoints;
		private List<MapFlagBlock> flags;
		private List<MapPlatform> platforms;
		private List<Entity> entities;

		public MapManager()
		{
			this.blocks = new List<MapBlock>();
			this.spawnPoints = new List<MapSpawnPoint>();
			this.checkPoints = new List<MapCheckPoint>();
			this.flags = new List<MapFlagBlock>();
			this.platforms = new List<MapPlatform>();
			this.entities = new List<Entity>();
		}

		public MapEndOfLevel EndOfLevel { get; set; }

		public Texture2D Background { get; set; }

		public List<MapBlock> Blocks
		{
			get
			{
				return this.blocks;
			}
		}

		public List<MapCheckPoint> CheckPoints
		{
			get
			{
				return this.checkPoints;
			}
		}

		public List<MapSpawnPoint> SpawnPoints
		{
			get
			{
				return this.spawnPoints;
			}
		}
		
		// only for editor ?
		public List<MapFlagBlock> Flags
		{
			get
			{
				return this.flags;
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

			foreach (var flag in this.flags)
			{
				flag.AddToWorld(physicsWorld);
			}

			foreach (var platform in this.platforms)
			{
				platform.AddToWorld(physicsWorld);
			}

			foreach (var entity in this.entities)
			{
				entity.AddToWorld(physicsWorld);
			}

			foreach (var spawnPoint in this.spawnPoints)
			{
				spawnPoint.AddToWorld(physicsWorld);
			}

			foreach (var checkPoint in this.checkPoints)
			{
				checkPoint.AddToWorld(physicsWorld);
			}

			if (this.EndOfLevel != null)
			{
				this.EndOfLevel.AddToWorld(physicsWorld);
			}
		}

		public void AddBlock(MapBlock block)
		{
			this.blocks.Add(block);
		}

		public void AddCheckPoint(MapCheckPoint checkPoint)
		{
			this.checkPoints.Add(checkPoint);
		}

		public void AddSpawnPoint(MapSpawnPoint spawnPoint)
		{
			this.spawnPoints.Add(spawnPoint);
		}

		public void AddMapFlag(MapFlagBlock flagBlock)
		{
			this.flags.Add(flagBlock);
		}

		public void AddPlatform(MapPlatform platform)
		{
			this.platforms.Add(platform);
		}

		public void AddEntity(Entity entity)
		{
			this.entities.Add(entity);
		}

		public void RemoveBlock(MapBlock block)
		{
			this.blocks.Remove(block);
		}

		public void RemoveCheckPoint(MapCheckPoint checkPoint)
		{
			this.checkPoints.Remove(checkPoint);
		}

		public void RemoveSpawnPoint(MapSpawnPoint spawnPoint)
		{
			this.spawnPoints.Remove(spawnPoint);
		}

		public void RemoveFlag(MapFlagBlock triggerBlock)
		{
			this.flags.Remove(triggerBlock);
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

		internal void Clear()
		{
			this.EndOfLevel = null;
			this.Background = null;
			this.Blocks.Clear();
			this.CheckPoints.Clear();
			this.Entities.Clear();
			this.Flags.Clear();
			this.Platforms.Clear();
			this.SpawnPoints.Clear();
		}
	}
}
