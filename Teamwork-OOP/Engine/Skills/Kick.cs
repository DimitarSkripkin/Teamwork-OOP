namespace Teamwork_OOP.Engine.Skills
{
	using BaseClasses;

    public class Kick : TargetSkill
    {
        private const float KickCoolown = 4.0f;
        private const float KickMaxActiveTime = 1.0f;

        public Kick(Entity usedFrom)
            : base(usedFrom, KickCoolown, KickMaxActiveTime)
        {
        }

        public float AttackDamage
        {
            get
            {
                return 2*UsedFrom.Strength + 1*UsedFrom.AttackDamage;
            }
        }

        public override void ApplySkillEffect(Entity target)
        {
            // TODO:
        }
    }
}
