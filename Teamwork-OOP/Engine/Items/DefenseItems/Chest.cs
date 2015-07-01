using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Chest : DefenseItem
    {
         //baseStats
        public const int DefaultStrengthChest = 25;
        public const int DefaultDexterityChest = 12;
        public const int DefaultVitalityChest = 5;
        public const int DefaultIntelligenceChest = 8;
        //secondaryStats
        public const int DefaultManaPointsChest = 18;
        public const int DefaultHealthPointsChest = 22;
        public const int DefaultMagicResistanceChest = 16;
        public const int DefaultArmorChest = 18;

        public Chest(Vector2 position, int id, float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange, DefaultStrengthChest, DefaultDexterityChest, DefaultIntelligenceChest, DefaultVitalityChest, criticalDamage, DefaultManaPointsChest, DefaultHealthPointsChest, DefaultMagicResistanceChest, DefaultArmorChest)
        {
        }
    }
}
