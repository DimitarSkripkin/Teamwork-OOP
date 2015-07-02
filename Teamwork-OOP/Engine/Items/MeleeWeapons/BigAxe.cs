using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	public class BigAxe : MeleeWeapon
	{
		public const int DefaultStrenght = 30;
		public const int DefaultDexterity = 0;
		public const int DefaultVitality = 8;

		public const float DefaultBaseStatRange = 0.5f;
		public const float DefaultSecontaryStatRange = 0.1f;
	    private const int DefaultAttackDamage = 8;
		public const float DefaultCriticalDamage = 1.5f;

		public BigAxe(Vector2 position,
			float baseStatRange = DefaultBaseStatRange,
			float secondaryStatRange = DefaultSecontaryStatRange,

			int strenght = DefaultStrenght,
			int dexteriry = DefaultDexterity,
			int vitality = DefaultVitality,
			//...
			float criticalDamage = DefaultCriticalDamage,
            int attackDamage = 0)
			: base(position,
				baseStatRange, baseStatRange,

				strenght,
				dexteriry,
				vitality,
                criticalDamage, DefaultAttackDamage)
		{
		}
	}
}