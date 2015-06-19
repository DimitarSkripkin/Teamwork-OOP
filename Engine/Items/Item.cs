using System;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Items
{
	using BaseClasses;
	using Interfaces;
	using Physics;

	/* IBaseStats, ISecondaryStats will be abstract here and
	 * implement them in derivative class ?
	 * 
	 * will we add usable items like potions ??
	 */
	public abstract class Item : CollidableObject//, IBaseStats, ISecondaryStats
	{
		/* collisionHull shoud be Circle or AABB
		 * and is the shape that will be visible
		 * when the item dropson the ground
		 */

		protected Item(CollisionShape collisionHull, int id)
			: base(collisionHull, id)
		{
		}
	}
}
