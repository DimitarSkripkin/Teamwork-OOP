using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

   public class DoubleAxe : MeleeWeapon
    {
        public const int DefaultStrenght = 80;
        public const int DefaultDexterity = 0;
        public const int DefaultVitality = 8;

        public const float DefaultBaseStatRange = 0.5f;
        public const float DefaultSecontaryStatRange = 0.1f;

        public const float DefaultCriticalDamage = 1.5f;
       private const int DefaultAttackDamageDoubleAxe = 7;
        public DoubleAxe(Vector2 position, int id, 
            float baseStatRange = DefaultBaseStatRange,
            float secondaryStatRange = DefaultSecontaryStatRange, 
            int strenght = DefaultStrenght, 
            int dexteriry = DefaultDexterity,
            int vitality = DefaultVitality,
            //...
            float criticalDamage = DefaultCriticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange,
            
            strenght, 
            dexteriry, 
            vitality,
            criticalDamage, DefaultAttackDamageDoubleAxe)
        {
        }
    }
}
