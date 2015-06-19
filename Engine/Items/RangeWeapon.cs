using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	public abstract class RangeWeapon : Item
	{
		protected RangeWeapon(CollisionShape collisionHull, int id)
			: base(collisionHull, id)
		{
		}
	}
}
