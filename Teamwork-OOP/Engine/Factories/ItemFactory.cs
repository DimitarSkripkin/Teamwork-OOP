using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Items;

namespace Teamwork_OOP.Engine.Factories
{
	public static class ItemFactory
	{
		public static readonly Random random = new Random();
		public static float GetRandomNumber(float baseValue, float epsilonRange)
		{
			double minValue = baseValue - epsilonRange;
			Random random = new Random();
			return (float)(random.NextDouble() * (epsilonRange * 2) + minValue);
		}

		public static int GetRandomNumber(int baseValue, int epsilonRange)
		{
			Random random = new Random();
			return random.Next((baseValue - epsilonRange), (baseValue + epsilonRange));
		}
		
		public static Item GenerateItem(Entity monster)
		{
			Random rand = new Random();
			int randomNumber = rand.Next(0, 9);
			int baseStatRange = GetRandomNumber(5, 3); //placeholder values??
			int secondaryStatRange = GetRandomNumber(5, 3);
			switch (randomNumber)
			{
				case 0: return new Axe(monster.CollisionHull.Position);
					break;
				case 1: return new Sword(monster.CollisionHull.Position);
					break;
				case 2: return new BigAxe(monster.CollisionHull.Position);
					break;
				case 3: return new TigerClaws(monster.CollisionHull.Position,baseStatRange,secondaryStatRange,0);
					break;
				case 4: return new Helm(monster.CollisionHull.Position, baseStatRange, secondaryStatRange, 0);
					break;
				case 5: return new Chest(monster.CollisionHull.Position, baseStatRange, secondaryStatRange);
					break;
				case 6: return new Gloves(monster.CollisionHull.Position, baseStatRange, secondaryStatRange);
					break;
				case 7: return new Pants(monster.CollisionHull.Position, baseStatRange, secondaryStatRange);
					break;
				case 8: return new Boots(monster.CollisionHull.Position, baseStatRange, secondaryStatRange, 0);
					break;

			}
			return null;
		}

		public static void RemoveItemFromWorld(Vector2 position , Item item)
		{
			//if ()
			{
				
			}
		}
	}
}
