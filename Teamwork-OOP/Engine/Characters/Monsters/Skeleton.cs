﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teamwork_OOP.Engine.Physics;

namespace Teamwork_OOP.Engine.Characters.Enemies
{
	public class Skeleton : NonPlayerCharacter
	{
		private const int StrengthBase = 10;
		private const int DexterityBase = 10;
		private const int IntelligenceBase = 10;
		private const int VitalityBase = 10;
		private const float AttackSpeedBase = 1.0f;
		private const float SpellCastingSpeedBase = 1.0f;
		private const float MovementSpeedBase = 1.0f;

		public Skeleton(CollisionShape collisionHull, int id,
			int strength = StrengthBase,
			int dexterity = DexterityBase,
			int intelligence = IntelligenceBase,
			int vitality = VitalityBase)
			: base(strength, dexterity, intelligence, vitality, 0, 0, 0, 0,
			AttackSpeedBase,SpellCastingSpeedBase,MovementSpeedBase,0,0,
			0, 0, 0, collisionHull, id)	
		{
		}
	}
}