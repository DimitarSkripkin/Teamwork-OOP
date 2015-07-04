using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Interfaces;

	public abstract class CollidableObject : IDestructable
	{
		//private Body collisionHull;

		public abstract void AddToWorld(World physicsWorld);

		public bool ToDestroy { get; set; }

		public Body CollisionHull { get; set; }

		// TODO: check if with overide event handler will call the new CallBack function
		public virtual bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			return true;
		}
	}
}
