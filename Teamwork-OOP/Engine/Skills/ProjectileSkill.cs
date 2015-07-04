using System;
using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;
	using Interfaces;

	public abstract class ProjectileSkill : Skill
	{
		private const float DefaultActiveTime = 0.5f;

		protected ProjectileSkill(Entity usedFrom, float cooldownTime, float maxActiveTime)
			: base(usedFrom, cooldownTime, maxActiveTime)
		{
		}

		public virtual void ActivateInDirection(World physicsWorld, Vector2 direction)
		{
			if (base.IsActive)//base.Activate())
			{
				// launch projectile
				// hit by projectile will be implemented in Entity class if it hits other Entity ?
				direction.Normalize();
				var velocity = 15.0f;

				var projectile = new Projectile(this.UsedFrom, direction * velocity, DefaultActiveTime);

				projectile.UserData = this;

				projectile.AddToWorld(physicsWorld);
			}
		}
	}
}
