using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Boots : DefenseItem
    {
             // increase Movement Speed
           //baseStats
        public const int DefaultStrenghtBoots = 17;
        public const int DefaultDexterityBoots = 22;
        public const int DefaultVitalityBoots = 13;
        public const int DefaultIntelligenceBoots = 14;
           //secondaryStats
        public const int DefaultManaPointsBoots = 20;
        public const int DefaultHealthPointsBoots = 16;
        public const int DefaultMagicResistanceBoots = 14;
        public const int DefaultArmorBoots = 10;
        public const float DefaultMovementSpeedBoots = 35;

        public Boots(Vector2 position, int id, float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange, DefaultStrenghtBoots, DefaultDexterityBoots, DefaultIntelligenceBoots, DefaultVitalityBoots, criticalDamage, DefaultManaPointsBoots, DefaultHealthPointsBoots, DefaultMagicResistanceBoots, DefaultArmorBoots)
        {
            this.MovementSpeed = DefaultMovementSpeedBoots;
        }
    }
}
