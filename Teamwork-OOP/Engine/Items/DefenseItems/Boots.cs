using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

     public class Boots : DefenseItem
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
	
        public Boots(Vector2 position, float baseStatRange, float secondaryStatRange , int manaPoints = DefaultManaPointsBoots , int healthPoints = DefaultHealthPointsBoots , int armorPoints = DefaultArmorBoots)
            : base(position, baseStatRange, secondaryStatRange, DefaultStrenghtBoots, DefaultDexterityBoots,
			DefaultIntelligenceBoots, DefaultVitalityBoots, 0,
			manaPoints, healthPoints, armorPoints , 0)
        {
           
        }
    }
}
