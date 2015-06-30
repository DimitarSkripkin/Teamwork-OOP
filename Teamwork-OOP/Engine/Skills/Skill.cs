using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;
	using Interfaces;

	public abstract class Skill
	{
		private Entity usedFrom;
		private float cooldownTime;
		private float maxActiveTime;

		protected Skill(Entity usedFrom, float cooldownTime, float maxActiveTime)
		{
			this.CooldownTime = cooldownTime;
			this.MaxActiveTime = maxActiveTime;

			this.UsedFrom = usedFrom;
		}

		public float CurrentCooldownTime { get; private set; }
		public float CurrentActiveTime { get; private set; }

		public float CooldownTime
		{
			get
			{
				return cooldownTime;
			}
			private set
			{
				if (value <= 0)
				{
					throw new ArgumentException();
				}
				this.cooldownTime = value;
			}
		}
		public float MaxActiveTime
		{
			get
			{
				return maxActiveTime;
			}
			private set
			{
				if (value <= 0)
				{
					throw new ArgumentException();
				}
				this.maxActiveTime = value;
			}
		}

		public bool IsActive { get; private set; }

		public Entity UsedFrom
		{
			get
			{
				return this.usedFrom;
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentException();
				}
				this.usedFrom = value;
			}
		}

		public void InternalUpdate(float deltaTime)
		{
			if (this.IsActive)
			{
				if (this.CurrentActiveTime >= this.maxActiveTime)
				{
					this.CurrentActiveTime = 0.0f;
					this.IsActive = false;
				}
				else
				{
					this.CurrentActiveTime += deltaTime;
				}
			}

			if (this.CurrentCooldownTime > 0.0f)
			{
				this.CurrentCooldownTime -= deltaTime;
			}
			else
			{
				this.CurrentCooldownTime = 0.0f;
			}
		}

		public virtual bool Activate()
		{
			if (this.CooldownTime <= 0.0f)
			{
				this.CurrentCooldownTime = this.CooldownTime;
				this.CurrentActiveTime = this.MaxActiveTime;

				this.IsActive = true;
				return true;
			}
			return false;
		}

		public abstract void ApplySkillEffect(Entity target);
	}
}
