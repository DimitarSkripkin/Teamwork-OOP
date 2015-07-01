using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using FarseerPhysics.Dynamics;
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
            float criticalDamage, int attackDamage, int spellDamage, int armor, int magicResistance, float attackSpeed, float spellCastingSpeed, float movementSpeed, int healthPoints, int manaPoints, float attackRange, float criticalHitChance)
			: base()
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
                    case 4:
				        this.AttackDamage += GetRandomNumber(AttackDamage, (int) (baseStatRange * attackDamage));
				        break;
                    case 5:
                        this.SpellDamage += GetRandomNumber(SpellDamage, (int) (baseStatRange * spellDamage));
				        break;
                    case 6:
				        this.Armor += GetRandomNumber(Armor, (int) (baseStatRange * armor));
				        break;
                    case 7:
				        this.MagicResistance += GetRandomNumber(MagicResistance, (int) (baseStatRange * magicResistance));
				        break;
                    case 8:
                          this.AttackSpeed += GetRandomNumber(AttackSpeed, (baseStatRange * attackSpeed));
				        break;
                    case 9:
				        this.SpellCastingSpeed += GetRandomNumber(SpellCastingSpeed, (baseStatRange * spellCastingSpeed));
				        break;
                    case 10:
                        this.MovementSpeed += GetRandomNumber(MovementSpeed, (baseStatRange * movementSpeed));
				        break;
                    case 11:
                        this.HealthPoints += GetRandomNumber(HealthPoints, (int)(baseStatRange * healthPoints));
                        break;
                    case 12:
                        this.ManaPoints += GetRandomNumber(ManaPoints, (int)(baseStatRange * manaPoints));
                        break;
                    case 13:
                        this.AttackRange += GetRandomNumber(AttackRange, secondaryStatRange * attackRange);
                        break;
                    case 14:
                        this.CriticalHitChance += GetRandomNumber(criticalHitChance, secondaryStatRange * criticalHitChance);
                        break;
					//todo : implement case 4 - case 14
					case 15:
						CriticalDamage += GetRandomNumber(criticalDamage, secondaryStatRange * criticalDamage);
						break;
				}
			}


		}

		public override void AddToWorld(FarseerPhysics.Dynamics.World physicsWorld)
		{
			throw new NotImplementedException();
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
 
		public int AttackDamage { get; set; }
	

		public int SpellDamage { get; set; }
		
          
		public int Armor { get; set; }
		
       
		public int MagicResistance { get; set; }

	
		public float AttackSpeed { get; set; }

      
		public float SpellCastingSpeed { get; set; }
		

		public float MovementSpeed { get; set; }
		

		public int HealthPoints { get; set; }
		

		public int ManaPoints { get; set; }
	

		public float AttackRange { get; set; }
	

		public float CriticalDamage
		{
			get { return this.criticalDamage; }
			set { this.criticalDamage = value; }
		}

		public float CriticalHitChance { get; set; }
		
	}
}
