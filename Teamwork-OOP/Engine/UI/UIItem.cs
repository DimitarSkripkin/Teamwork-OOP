using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.UI
{
	using Physics;

	public abstract class UIItem
	{
		private AABB collisionBox;

		public UIItem(Vector2 position, Vector2 size)
		{
			this.collisionBox = new AABB(position, size, CollisionObjectFlags.Kinematic);
		}
	}
}
