using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
    public class Kick : TargetSkill
    {
        private const float KickCoolown = 1.0f;
        private const float KickMaxActiveTime = 1.0f;

        public Kick(Entity usedFrom)
            : base(usedFrom, KickCoolown, KickMaxActiveTime)
        {
        }

        public override void ApplySkillEffect(BaseClasses.Entity target)
        {
            // TODO:
        }

        // TODO : DAMAGE
        
    }
}
