using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Physics;

	public abstract class CollidableObject
	{
		//private Body collisionHull;

		public CollidableObject()
		{
		}

		public abstract void AddToWorld(World physicsWorld);

		public Body CollisionHull { get; set; }

		// TODO: check if with overide event handler will call the new CallBack function
		public virtual bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			return true;
		}

	}
}
