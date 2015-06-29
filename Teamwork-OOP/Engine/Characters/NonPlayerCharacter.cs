using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Physics;

namespace Teamwork_OOP.Engine.Characters
{
	public abstract class NonPlayerCharacter: Entity
	{
		protected NonPlayerCharacter(int strength, int dexterity, int intelligence, int vitality,
			int attackDamage, int spellDamage, int armor, int magicResistance,
			float attackSpeed, float spellCastingSpeed, float movementSpeed, int healthPoints, int manaPoints, float attackRange,
			float criticalHitChance, float criticalDamage, CollisionShape collisionHull, int id) 
			: base(strength, dexterity, intelligence, vitality, attackDamage, spellDamage, armor, magicResistance,
			attackSpeed, spellCastingSpeed, movementSpeed, healthPoints, manaPoints,
			attackRange, criticalHitChance, criticalDamage, collisionHull, id)
		{
		}

		public int ItemDropChance { get; set; }
	}
}
