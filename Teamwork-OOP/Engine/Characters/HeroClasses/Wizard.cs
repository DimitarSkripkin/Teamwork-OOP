using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Characters.CharacterClasses
{
	using Drawing;
	using BaseClasses;

	public class Wizard : PlayerCharacter
	{
		private const float BodyDensity = 10.0f;

		private const int StrengthBase = 5;
		private const int DexterityBase = 10;
		private const int IntelligenceBase = 15;
		private const int VitalityBase = 10;
		private const float AttackSpeedBase = 1.0f;
		private const float SpellCastingSpeedBase = 1.0f;
		private const float MovementSpeedBase = 1.0f;

		public Wizard(
			int strength = StrengthBase,
			int dexterity = DexterityBase,
			int intelligence = IntelligenceBase,
			int vitality = VitalityBase)
			: base(strength, dexterity, intelligence, vitality, 0, 0, 0, 0,
			AttackSpeedBase, SpellCastingSpeedBase, MovementSpeedBase, 0, 0,
			0, 0, 0)
		{
		}

		public override void AddToWorld(World physicsWorld)
		{
			this.CollisionHull = BodyFactory.CreateCapsule(physicsWorld, 0.8f, 0.5f, BodyDensity, this);

			base.AddToWorld(physicsWorld);
		}
	}
}