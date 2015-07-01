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

        protected DefenseItem(Vector2 position, int id, float baseStatRange, float secondaryStatRange, int strength, int dexterity, int intelligance, int vitality, float criticalDamage, int manaPoints, int healthPoints, int magicResistance, int armor, float spellCastingSpeed = 0, float AttackSpeed = 0)
            : base(position, id, baseStatRange, secondaryStatRange, strength, dexterity, intelligance, vitality, criticalDamage, manaPoints, healthPoints, magicResistance, armor, spellCastingSpeed, AttackSpeed, 0, 0, 0, 0, 0)
       {
         
       }
    }
}
