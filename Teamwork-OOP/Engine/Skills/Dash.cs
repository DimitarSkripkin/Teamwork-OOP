using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
    public class Dash : TargetSkill
    {
        private const float DashCooldown = 8.0f;
        private const float DashMaxActiveTime = 3.0f;

        public Dash(Entity usedFrom)
            : base(usedFrom, DashCooldown, DashMaxActiveTime)
        {
        }

        public override void ApplySkillEffect(Entity target)
        {
            // TODO:
        }

       
    }
}
