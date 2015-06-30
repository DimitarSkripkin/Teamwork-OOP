using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Interfaces;

namespace Teamwork_OOP.Engine.Skills
{
    public class Kick : Skill, ISecondaryStats
    {
        private const float KickDamage = 3.0f;
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


        public int AttackDamage { get; set; }
        public int SpellDamage { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public float AttackSpeed { get; set; }
        public float SpellCastingSpeed { get; set; }
        public float MovementSpeed { get; set; }
        public int HealthPoints { get; set; }
        public int ManaPoints { get; set; }
        public float AttackRange { get; set; }
        public float CriticalHitChance { get; set; }
        public float CriticalDamage { get; set; }
    }
}
