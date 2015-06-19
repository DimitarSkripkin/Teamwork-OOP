namespace Teamwork_OOP.Engine.BaseClasses
{
	using Physics;

	public abstract class CollidableObject : GameObject
	{
		private CollisionShape collisionHull;

		protected CollidableObject(CollisionShape collisionHull, int id)
			: base(id)
		{

			this.CollisionHull = collisionHull;
			this.CollisionHull.CollisionHandler += CollisionCallBack;
		}

		public CollisionShape CollisionHull
		{
			get
			{
				return collisionHull;
			}
			protected set
			{
				collisionHull = value;
			}
		}

		// TODO: check if with overide event handler will call the new CallBack function
		public virtual void CollisionCallBack(CollisionEventArgs eventArgs)
		{
			//eventArgs.CollidesWith
		}

	}
}
