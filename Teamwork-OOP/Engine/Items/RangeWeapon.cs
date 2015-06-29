using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	public abstract class RangeWeapon : Item
	{
		protected RangeWeapon(Vector2 position, int id, float baseStatRange, float secondaryStatRange,
			int dexteriry, int intelligance,
			//...
			float criticalDamage)
			: base(position, id,
				baseStatRange, baseStatRange,
				0,// range weapons won't give strenght or somethin like that
				dexteriry, intelligance,
				0,// range weapons won't give vitality or somethin like that
				criticalDamage)
		{
		}
	}
}
