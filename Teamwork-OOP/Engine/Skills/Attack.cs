using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
    public class Attack : TargetSkill
    {
        private const float AttackCooldown = 0.5f;
        private const float AtackMaxActiveTime = 1.0f;

        public Attack(Entity usedFrom)
            : base(usedFrom, AttackCooldown, AtackMaxActiveTime)
        {
        }

        public float AttackDamage
        {
            get
            {
                return 1*UsedFrom.AttackDamage + 5*UsedFrom.Strength;
            }
        }

        public override void ApplySkillEffect(Entity target)
        {
            //TODO
        }
    }
}
