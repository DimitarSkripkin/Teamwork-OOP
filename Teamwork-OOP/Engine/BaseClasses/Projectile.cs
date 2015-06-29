using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Physics;

	// TODO: implement or remove this class
	public class Projectile : CollidableObject
	{
		protected Projectile(CollisionShape collisionHull, Vector2 position, Vector2 velocity, int id)
			: base(collisionHull, id)
		{
			this.CollisionHull.Position = position;
			this.CollisionHull.Velocity = velocity;
		}
	}
}
