using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	public class PhysicsEngine
	{
		private Vector2 gravity;
		private List<CollisionShape> collisionItems;

		public PhysicsEngine(Vector2 gravity)
		{
			this.collisionItems = new List<CollisionShape>();
			this.Gravity = gravity;
		}

		public Vector2 Gravity
		{
			get { return gravity; }
			set { gravity = value; }
		}

		public void AddItem(CollisionShape item)
		{
			this.collisionItems.Add(item);
		}

		public void RemoveItem(CollisionShape item)
		{
			this.collisionItems.Remove(item);
		}

		public virtual void Update(float deltaTime)
		{
			foreach (var item in collisionItems)
			{
				switch (item.ObjectFlags)
				{
					case CollisionObjectFlags.Static:
						break;
					case CollisionObjectFlags.Dynamic:
						item.Velocity += this.Gravity;
						item.Position += item.Velocity * deltaTime;
						break;
					case CollisionObjectFlags.Kinematic:
						item.Position += item.Velocity * deltaTime;
						break;
					default:
						throw new NotImplementedException();
				}
			}

			ProcessCollisions();
		}

		public virtual void ProcessCollisions()
		{
			for (int i = 0; i < this.collisionItems.Count; ++i)
			{
				var currentItem = this.collisionItems[i];
				for (int j = i; j < this.collisionItems.Count; ++j)
				{
					var checkWith = this.collisionItems[j];

					if ((currentItem.CollisionMask & checkWith.CollisionMask) > 0
						&& CollisionChecker.CheckForCollision(currentItem, checkWith))
					{
						currentItem.RaiseCollision(new CollisionEventArgs(checkWith));
						//currentItem.CollidesWith(checkWith);
						// TODO: dispatch collision

						// TODO: FIX
						currentItem.Velocity = Vector2.Zero;
						checkWith.Velocity = Vector2.Zero;
					}
				}
			}
		}
	}
}
