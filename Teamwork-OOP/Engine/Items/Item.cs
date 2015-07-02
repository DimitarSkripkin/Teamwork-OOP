using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Teamwork_OOP.Engine.Factories;

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
		protected Item(Vector2 position,
			float baseStatRange, float secondaryStatRange,
			int strength, int dexterity, int intelligance, int vitality,
            float criticalDamage, int attackDamage, int spellDamage, int armor, int magicResistance, float attackSpeed,
			float spellCastingSpeed, float movementSpeed, int healthPoints, int manaPoints, float attackRange, float criticalHitChance)
			
		{
			int statID = 0;
			int addStatCount = 4;
			for (int i = 0; i < addStatCount; i++)
			{
				statID = ItemFactory.GetRandomNumber(8, 8);  
				switch (statID)
				{
					case 0:
						this.Strength += ItemFactory.GetRandomNumber(strength, (int)(baseStatRange * strength));
						break;
					case 1:
						this.Dexterity += ItemFactory.GetRandomNumber(dexterity, (int)(baseStatRange * dexterity));
						break;
					case 2:
						this.Intelligence += ItemFactory.GetRandomNumber(intelligance, (int)(baseStatRange * intelligance));
						break;
					case 3:
						this.Vitality += ItemFactory.GetRandomNumber(vitality, (int)(baseStatRange * vitality));
						break;
                    case 4:
						this.AttackDamage += ItemFactory.GetRandomNumber(attackDamage, (int)(baseStatRange * attackDamage));
				        break;
                    case 5:
						this.SpellDamage += ItemFactory.GetRandomNumber(spellDamage, (int)(baseStatRange * spellDamage));
				        break;
                    case 6:
						this.Armor += ItemFactory.GetRandomNumber(armor, (int)(baseStatRange * armor));
				        break;
                    case 7:
						this.MagicResistance += ItemFactory.GetRandomNumber(magicResistance, (int)(baseStatRange * magicResistance));
				        break;
                    case 8:
						this.AttackSpeed += ItemFactory.GetRandomNumber(attackSpeed, (baseStatRange * attackSpeed));
				        break;
                    case 9:
						this.SpellCastingSpeed += ItemFactory.GetRandomNumber(spellCastingSpeed, (baseStatRange * spellCastingSpeed));
				        break;
                    case 10:
						this.MovementSpeed += ItemFactory.GetRandomNumber(movementSpeed, (baseStatRange * movementSpeed));
				        break;
                    case 11:
						this.HealthPoints += ItemFactory.GetRandomNumber(healthPoints, (int)(baseStatRange * healthPoints));
                        break;
                    case 12:
						this.ManaPoints += ItemFactory.GetRandomNumber(manaPoints, (int)(baseStatRange * manaPoints));
                        break;
                    case 13:
						this.AttackRange += ItemFactory.GetRandomNumber(attackRange, secondaryStatRange * attackRange);
                        break;
                    case 14:
						this.CriticalHitChance += ItemFactory.GetRandomNumber(criticalHitChance, secondaryStatRange * criticalHitChance);
                        break;
					case 15:
						CriticalDamage += ItemFactory.GetRandomNumber(criticalDamage, secondaryStatRange * criticalDamage);
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
			get;
			set;
		}

		public int Dexterity
		{
			get;
			set;
		}

		public int Intelligence
		{
			get;
			set;
		}

		public int Vitality
		{
			get;
			set;
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
			get;
			set;
		}

		public float CriticalHitChance { get; set; }

		public bool ToDestroy { get; set; }
		
	}
}
