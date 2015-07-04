namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;

	public class Slash : ProjectileSkill
	{
		private const float SlashCoolown = 1.0f;
		private const float SlashMaxActiveTime = 1.0f;

		public Slash(Entity usedFrom)
			: base(usedFrom, SlashCoolown, SlashMaxActiveTime)
		{
		}

		public int AttackDamage
		{
			get
			{
				return 2 * UsedFrom.Strength + UsedFrom.AttackDamage;
			}
		}

		public override void ApplySkillEffect(Entity target)
		{
			var skillEffect = target.Armor - this.AttackDamage;

			if (skillEffect < 0)
			{
				target.CurrentHealthPoints += skillEffect;
			}
		}
	}
}