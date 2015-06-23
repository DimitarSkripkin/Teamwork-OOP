using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	/* IBaseStats, ISecondaryStats will be abstract here and
	 * implement them in derivative class ?
	 * 
	 * will we add usable items like potions ??
	 */
	public abstract class Item : CollidableObject, IBaseStats, ISecondaryStats
	{
		// both GetRandomNumber methods needs to be moved to static class
		float GetRandomNumber(float baseValue, float epsilonRange)
		{
			double minValue = baseValue - epsilonRange;
			Random random = new Random();
			return (float)(random.NextDouble() * (epsilonRange * 2) + minValue);
		}

		int GetRandomNumber(int baseValue, int epsilonRange)
		{
			Random random = new Random();
			return random.Next((baseValue - epsilonRange), (baseValue + epsilonRange));
		}
		
		/* collisionHull shoud be Circle or AABB
		 * and is the shape that will be visible
		 * when the item drops on the ground
		 */
		private int strength;
		private int dexterity;
		private int intelligence;
		private int vitality;

		private float criticalDamage;
		//
		protected Item(Vector2 position, int id,
			float baseStatRange, float secondaryStatRange,
			int strength, int dexterity, int intelligance, int vitality,
			float criticalDamage)
			: base(new AABB(position, new Vector2(0.5f), CollisionObjectFlags.Dynamic), id)
		{
			int statID = 0;
			int addStatCount = 0;
			for (int i = 0; i < addStatCount; i++)
			{
				statID = GetRandomNumber(8, 8);//The values are a placeholder
				switch (statID)
				{
					case 0:
						this.Strength += GetRandomNumber(strength, (int)(baseStatRange * strength));
						break;
					case 1:
						this.Dexterity += GetRandomNumber(dexterity, (int)(baseStatRange * dexterity));
						break;
					case 2:
						this.Intelligence += GetRandomNumber(intelligance, (int)(baseStatRange * intelligance));
						break;
					case 3:
						this.Vitality += GetRandomNumber(vitality, (int)(baseStatRange * vitality));
						break;
					//todo : implement case 4 - case 14
					case 15:
						CriticalDamage += GetRandomNumber(criticalDamage, secondaryStatRange * criticalDamage);
						break;
				}
			}


		}
		public int Strength
		{
			get { return this.strength; }
			set { this.strength = value; }
		}

		public int Dexterity
		{
			get { return this.dexterity; }
			set { this.dexterity = value; }
		}

		public int Intelligence
		{
			get { return intelligence; }
			set { intelligence = value; }
		}

		public int Vitality
		{
			get { return vitality; }
			set { vitality = value; }
		}

		public int AttackDamage
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int SpellDamage
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int Armor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int MagicResistance
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float AttackSpeed
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float SpellCastingSpeed
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float MovementSpeed
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int HealthPoints
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int ManaPoints
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float AttackRange
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float CriticalHit
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public float CriticalDamage
		{
			get { return this.criticalDamage; }
			set { this.criticalDamage = value; }
		}
	}
}
