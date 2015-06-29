using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Physics;

	// TODO: implement or remove this class
	public class Projectile : CollidableObject
	{
		protected Projectile(Vector2 position, Vector2 velocity, bool isBullet = true)
			: base()
		{
			this.CollisionHull.Position = position;
			this.CollisionHull.ApplyLinearImpulse(velocity);
			this.CollisionHull.IsBullet = isBullet;
		}

		public override void AddToWorld(FarseerPhysics.Dynamics.World physicsWorld)
		{
			throw new System.NotImplementedException();
		}
	}
}
