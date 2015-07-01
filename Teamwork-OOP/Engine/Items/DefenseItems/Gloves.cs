using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Gloves : DefenseItem
    {
        public const int DefaultStrengthGloves = 21;
        public const int DefaultDexterityGloves = 50;
        public const int DefaultVitalityGloves = 32;
        public const int DefaultIntelligenceGloves = 12;
         //secondaryStats
        public const int DefaultManaPointsGloves = 30;
        public const int DefaultHealthPointsGloves = 18;
        public const int DefaultMagicResistanceGloves = 42;
        public const int DefaultArmorGloves = 31;

        public const float DefaultSpellCastingSpeedGloves = 0.05f;
        public const float DefaultAttackSpeedGloves = 0.1f;

        //increase Spell Casting Speed i Attack Speed
        public Gloves(Vector2 position, int id, float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange, DefaultStrengthGloves, DefaultDexterityGloves, DefaultIntelligenceGloves, DefaultVitalityGloves, criticalDamage, DefaultManaPointsGloves, DefaultHealthPointsGloves, DefaultMagicResistanceGloves, DefaultArmorGloves, DefaultArmorGloves, DefaultMagicResistanceGloves)
        {
           
        }
    }
}
