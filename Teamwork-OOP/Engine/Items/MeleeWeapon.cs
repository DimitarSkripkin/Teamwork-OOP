using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	public abstract class MeleeWeapon : Item
	{
		protected MeleeWeapon(Vector2 position, int id, float baseStatRange, float secondaryStatRange,
			int strenght, int dexteriry, int vitality,
			//...
			float criticalDamage)
			: base(position, id,
				baseStatRange, baseStatRange,
				strenght,
				dexteriry,
				0,// melee weapons won't give intelligance or somethin like that
				vitality,
				criticalDamage)
		{
		}
	}
}
