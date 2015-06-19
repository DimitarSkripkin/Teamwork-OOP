using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	public class Fireball : ProjectileSkill, ISkillEffects
	{
		private const float FireballRadius = 5.0f;

		private const float FireballCooldown = 3.0f;
		private const float FireballMaxActiveTime = 10.0f;

		public Fireball(Entity usedFrom)
			: base(usedFrom, FireballCooldown, FireballMaxActiveTime)
		{
		}

		public float SpellDamage
		{
			get
			{
				return 10.0f * UsedFrom.Intelligence;
			}
		}
	}
}
