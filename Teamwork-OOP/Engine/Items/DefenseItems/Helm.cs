using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Helm : DefenseItem
    {
         //baseStats
        public const int DefaultStrengthHelm = 19;
        public const int DefaultDexterityHelm = 32;
        public const int DefaultVitalityHelm = 9;
        public const int DefaultIntelligenceHelm = 15;
        //secondaryStats
        public const int DefaultManaPointsHelm = 15;
        public const int DefaultHealthPointsHelm = 20;
        public const int DefaultMagicResistanceHelm = 12;
        public const int DefaultArmorHelm = 18;
       
         
        public Helm(Vector2 position, int id, float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange, DefaultStrengthHelm, DefaultDexterityHelm, DefaultIntelligenceHelm, DefaultVitalityHelm, criticalDamage, DefaultManaPointsHelm, DefaultHealthPointsHelm, DefaultMagicResistanceHelm, DefaultArmorHelm)
        {
        }
    }
}
