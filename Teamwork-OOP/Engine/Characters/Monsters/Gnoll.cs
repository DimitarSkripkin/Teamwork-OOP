﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teamwork_OOP.Engine.Physics;

namespace Teamwork_OOP.Engine.Characters.Enemies
{
	public class Gnoll : NonPlayerCharacter
	{
		private const int StrengthBase = 15;
		private const int DexterityBase = 0;
		private const int IntelligenceBase = 0;
		private const int VitalityBase = 9;
		private const float AttackSpeedBase = 1.0f;
		private const float SpellCastingSpeedBase = 1.0f;
		private const float MovementSpeedBase = 1.0f;

		public Gnoll(CollisionShape collisionHull, int id,
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
