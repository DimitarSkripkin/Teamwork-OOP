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
	}
}
