using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Skills
{
    public class Block : TargetSkill
    {
        // increasing Entity's Armor, MagicResistance  ?

        private const float BlockCooldown = 20.0f;
        private const float BlockMaxActiveTime = 5.0f;

        public Block(Entity usedFrom)
            : base(usedFrom, BlockCooldown, BlockMaxActiveTime)
        {
        }

        public override void ApplySkillEffect(Entity target)
        {
            // TODO:
        }

        
    }
}
