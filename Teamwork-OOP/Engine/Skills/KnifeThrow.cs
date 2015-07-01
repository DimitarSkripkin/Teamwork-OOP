using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
    public class KnifeThrow : ProjectileSkill
    {
        private const float ThrowCooldown = 6.0f;
        // Check for max active time
        private const float ThrowMaxActiveTime = 1.0f;

        public KnifeThrow(Entity usedFrom)
            : base(usedFrom, ThrowCooldown, ThrowMaxActiveTime)
        {
        }

        public float ThrowDamage
        {
            get
            {
                return 5 * UsedFrom.Dexterity;
            }
        }

        public override void ApplySkillEffect(Entity target)
        {
            // TODO:
        }
    }
}
