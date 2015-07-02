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

	public class Axe : MeleeWeapon
	{
		public const int DefaultStrenght = 20;
		public const int DefaultDexterity = 3;
		public const int DefaultVitality = 5;

		public const float DefaultBaseStatRange = 0.3f;
		public const float DefaultSecontaryStatRange = 0.1f;

		public const float DefaultCriticalDamage = 1.4f;
        public const int DefaultAttackDamageAxe = 9;
		public Axe(Vector2 position)
			: base(position,
				DefaultBaseStatRange, DefaultSecontaryStatRange,
				DefaultStrenght,
				DefaultDexterity,
				DefaultVitality,
                DefaultCriticalDamage, DefaultAttackDamageAxe)
		{
		}
	}
}