using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Physics
{
	public class PhysicsEngine
	{
		private static readonly Vector2 DefaultGravity = new Vector2(0.0f, -9.8f);

		private Vector2 gravity;
		private List<CollisionShape> collisionItems;

		public PhysicsEngine()
			: this(DefaultGravity)
		{
		}

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
			for (int i = 0; i < this.collisionItems.Count; ++i)
			{
				var currentItem = this.collisionItems[i];
				if (currentItem.ObjectFlags == CollisionObjectFlags.Static)
				{
					continue;
				}

				bool inCollision = false;

				for (int j = 0; j < this.collisionItems.Count; ++j)
				{
					if (i == j)
					{
						continue;
					}
					var checkWith = this.collisionItems[j];

					if ((currentItem.CollisionMask & checkWith.CollisionMask) > 0
						&& CollisionChecker.CheckForCollision(currentItem, checkWith))
					{
						currentItem.RaiseCollision(new CollisionEventArgs(checkWith));
						//currentItem.CollidesWith(checkWith);
						// TODO: dispatch collision

						// TODO: FIX
						currentItem.Velocity = Vector2.Zero;
						inCollision = true;
					}
				}

				switch (currentItem.ObjectFlags)
				{
					case CollisionObjectFlags.Static:
						break;
					case CollisionObjectFlags.Dynamic:
						if (!inCollision)
						{
							currentItem.Velocity += this.Gravity;
							currentItem.Position += currentItem.Velocity * deltaTime;
						}
						break;
					case CollisionObjectFlags.Kinematic:
						currentItem.Position += currentItem.Velocity * deltaTime;
						break;
					default:
						throw new NotImplementedException();
				}

			}
		}
	}
}
