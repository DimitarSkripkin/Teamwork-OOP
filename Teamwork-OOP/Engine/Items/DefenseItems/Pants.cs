using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Pants : DefenseItem
    {
         
           //baseStats
        public const int DefaultStrenghtPants = 12;
        public const int DefaultDexterityPants = 16;
        public const int DefaultVitalityPants = 10;
        public const int DefaultIntelligencePants = 8; 
           //secondaryStats
        public const int DefaultManaPointsPants = 15;
        public const int DefaultHealthPointsPants = 20;
        public const int DefaultMagicResistancePants = 12;
        public const int DefaultArmorPants = 18;

        public Pants(Vector2 position, int id, float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange, DefaultStrenghtPants, DefaultDexterityPants, DefaultIntelligencePants, DefaultVitalityPants, criticalDamage, DefaultManaPointsPants, DefaultHealthPointsPants, DefaultMagicResistancePants, DefaultArmorPants)
        {

        }
    }
}
