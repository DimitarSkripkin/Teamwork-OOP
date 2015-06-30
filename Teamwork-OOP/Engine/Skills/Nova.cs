using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Interfaces;

namespace Teamwork_OOP.Engine.Skills
{
    public class Nova : ProjectileSkill, ISecondaryStats
    {
        private const float NovaDamage = 10.0f;

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
