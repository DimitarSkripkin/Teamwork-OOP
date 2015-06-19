using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;
	using Interfaces;

	public abstract class TargetSkill : Skill
	{
		protected TargetSkill(Entity usedFrom, float cooldownTime, float maxActiveTime)
			: base(usedFrom, cooldownTime, maxActiveTime)
		{
		}

		/* have to be
		 * 
		 * public void ActivateOnTarget(Entity target, SpellEffects spellEffects)
		 * 
		 * but first we hate to decide what spell effects to implement
		 * 
		 */
		public virtual void ActivateOnTarget(Entity target)
		{
			if (base.Activate())
			{
				// apply all spell effects on target
			}
		}
	}
}
