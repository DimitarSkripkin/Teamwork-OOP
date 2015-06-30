using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Interfaces;

namespace Teamwork_OOP.Engine.Skills
{
    public class KnifeThrow : ProjectileSkill, ISecondaryStats
    {
        private const float ThrowDamage = 3.0f;
        private const float ThrowCooldown = 6.0f;
        // Check for max active time
        private const float ThrowMaxActiveTime = 1.0f;

        public KnifeThrow(Entity usedFrom)
            : base(usedFrom, ThrowCooldown, ThrowMaxActiveTime)
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
