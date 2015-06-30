using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
    public class Nova : ProjectileSkill
    {
        private const float NovaRadius = 3.0f;
        private const float NovaCooldown = 10.0f;

        // MaxActivateTime will be set to 2. Remove or change :
        private const float NovaMaxActiveTime = 2.0f;

        public Nova(Entity usedFrom)
            : base(usedFrom, NovaCooldown, NovaMaxActiveTime)
        {
        }

        public override void ApplySkillEffect(Entity target)
        {
            // TODO:
        }

        // TODO : DAMAGE
    }
}
