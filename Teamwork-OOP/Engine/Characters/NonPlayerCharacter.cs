using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Characters
{
	using BaseClasses;
	using Map;

	public abstract class NonPlayerCharacter : Entity
	{
		protected NonPlayerCharacter(int strength, int dexterity, int intelligence, int vitality,
			int attackDamage, int spellDamage, int armor, int magicResistance,
			float attackSpeed, float spellCastingSpeed, float movementSpeed, int healthPoints, int manaPoints, float attackRange,
			float criticalHitChance, float criticalDamage)
			: base(strength, dexterity, intelligence, vitality, attackDamage, spellDamage, armor, magicResistance,
			attackSpeed, spellCastingSpeed, movementSpeed, healthPoints, manaPoints,
			attackRange, criticalHitChance, criticalDamage)
		{
		}

		public int ItemDropChance { get; set; }

		public MapSpawnPoint FromSpawnPoint { get; set; }
	}
}
