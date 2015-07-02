using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Map
{
	using Drawing;
	using Characters;
	using Characters.Enemies;

	public class MapSpawnPoint : MapItem
	{
		private int MaxSpawnedMonstersCount = 1;
		private float SpawnMonsterTime = 5.0f;

		private List<NonPlayerCharacter> monsters;

		public MapSpawnPoint(Vector2 position, TextureNode textureNode)
			: base(position, textureNode)
		{
			this.monsters = new List<NonPlayerCharacter>();
		}

		public IList<NonPlayerCharacter> Monsters
		{
			get
			{
				return this.monsters;
			}
		}

		public float TimeToNextSpawn { get; set; }

		public bool Spawn { get; private set; }

		public void Update(float deltaTime)
		{
			this.Spawn = false;

			this.TimeToNextSpawn += deltaTime;

			if (this.TimeToNextSpawn > SpawnMonsterTime)
			{
				if (this.monsters.Count < MaxSpawnedMonstersCount)
				{
					this.Spawn = true;

					//this.monsters.Add();
				}

				this.TimeToNextSpawn = 0.0f;
			}
		}
	}
}
