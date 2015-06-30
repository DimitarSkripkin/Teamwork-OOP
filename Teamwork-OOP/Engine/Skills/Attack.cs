using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Interfaces;

namespace Teamwork_OOP.Engine.Skills
{
    public class Attack : Skill, ISecondaryStats
    {
        private const float Damage = 2.0f;
        private const float AttackCooldown = 0.5f;
        private const float AtackMaxActiveTime = 1.0f;

        public Attack(Entity usedFrom, float cooldownTime, float maxActiveTime)
            : base(usedFrom, AttackCooldown, AtackMaxActiveTime)
        {
        }

        public override void ApplySkillEffect(Entity target)
        {
            //TODO
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
