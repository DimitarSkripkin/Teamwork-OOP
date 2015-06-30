using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;

	public class Fireball : ProjectileSkill
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

		public override void ApplySkillEffect(Entity target)
		{
			this.UsedFrom.HealthPoints -= target.Strength * 10;
			target.CollisionHull.ApplyLinearImpulse(new Vector2(1.0f, 0));
		}
	}
}
