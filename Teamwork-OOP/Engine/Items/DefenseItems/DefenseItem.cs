using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamwork_OOP.Engine.Items
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

    public abstract class DefenseItem : Item
    {

        protected DefenseItem(Vector2 position, float baseStatRange, float secondaryStatRange,
			int strength, int dexterity, int intelligance, int vitality, float criticalDamage,
			int manaPoints, int healthPoints, int magicResistance, int armor, float spellCastingSpeed = 0, float attackSpeed = 0 )
            : base(position, baseStatRange, secondaryStatRange, strength, dexterity, intelligance, vitality,
			criticalDamage, 0,0 , armor, magicResistance, attackSpeed, spellCastingSpeed, 0f, 0, 0, 0,0)
       {
         
       }
    }
}
