using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
	class WarriorAttack : Attack
	{
		public WarriorAttack(Entity usedFrom)
			: base(usedFrom)
		{
		}

		public override void ApplySkillEffect(Entity target)
		{
			base.ApplySkillEffect(target);
		}
	}
}
