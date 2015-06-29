using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;
	using Interfaces;

	public abstract class ProjectileSkill : Skill
	{
		protected ProjectileSkill(Entity usedFrom, float cooldownTime, float maxActiveTime)
			: base(usedFrom, cooldownTime, maxActiveTime)
		{
		}

		public virtual void ActivateInDirection(Vector2 position, Vector2 direction)
		{
			if (base.Activate())
			{
				// launch projectile
				// hit by projectile will be implemented in Entity class if it hits other Entity ?
			}
		}
	}
}
