using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class TigerClaws : MeleeWeapon
    {
        public const int DefaultStrenghtTigerClaws = 15;
        public const int DefaultDexterityTigerClaws = 5;
        public const int DefaultVitalityTigerClaws = 2;

        public const float DefaultBaseStatRangeTigerClaws = 0.5f;
        public const float DefaultSecontaryStatRangeTigerClaws = 0.1f;


        public const float DefaultCriticalDamageTigerClaws = 1.5f;
        public TigerClaws(Vector2 position,float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, DefaultBaseStatRangeTigerClaws, DefaultSecontaryStatRangeTigerClaws, DefaultStrenghtTigerClaws, DefaultDexterityTigerClaws, DefaultVitalityTigerClaws, DefaultCriticalDamageTigerClaws, 0)
        {

        }
    }
}
