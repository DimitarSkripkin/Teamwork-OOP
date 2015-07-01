using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using Microsoft.Xna.Framework;

    class Club : MeleeWeapon
    {
        public const int DefaultStrenghtClub = 30;
        public const int DefaultDexterityClub = 5;
        public const int DefaultVitalityClub = 4;

        public const float DefaultBaseStatRange = 0.5f;
        public const float DefaultSecontaryStatRange = 0.1f;
        public Club(Vector2 position, int id, float baseStatRange, float secondaryStatRange, float criticalDamage)
            : base(position, id, baseStatRange, secondaryStatRange, DefaultStrenghtClub, DefaultDexterityClub, DefaultVitalityClub, criticalDamage, 0)
        {
        }
    }
}
