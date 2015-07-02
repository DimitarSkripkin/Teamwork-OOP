using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Sword : MeleeWeapon
    {
        public const int DefaultStrenghtSword = 30;
        public const int DefaultDexteritySword = 0;
        public const int DefaultVitalitySword = 8;

        public const float DefaultBaseStatRange = 0.5f;
        public const float DefaultSecontaryStatRange = 0.1f;
        
        public const float DefaultCriticalDamageSword = 1.5f;
        private const int DefaultAttackDamageSword = 4;
        public Sword(Vector2 position,
            float baseStatRange = DefaultBaseStatRange,
            float secondaryStatRange = DefaultSecontaryStatRange, 
           
            //...
            float criticalDamage = DefaultCriticalDamageSword)
            : base(position,
            baseStatRange, 
            secondaryStatRange,
            DefaultStrenghtSword,
            DefaultDexteritySword,
            DefaultVitalitySword,
            criticalDamage, DefaultAttackDamageSword)
        {

        }
    }
}
