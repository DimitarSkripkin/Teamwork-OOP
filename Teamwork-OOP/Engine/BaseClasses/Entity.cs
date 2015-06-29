//using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Physics;
	using Skills;

	public abstract class Entity : CollidableObject//, IBaseStats//IMoveable
	{
		private int strength;
		private int dexterity;
		private int intelligence;
		private int vitality;

		//
		protected Entity(int strength, int dexterity, int intelligence, int vitality, CollisionShape collisionHull, int id)
			: base(collisionHull, id)
		{
			this.Strength = strength;
			this.Dexterity = dexterity;
			this.Intelligence = intelligence;
			this.Vitality = vitality;

			//this.CollisionHull.CollisionHandler += CollisionCallBack;
		}

		// TODO: check if with overide event handler will call the new CallBack function
		public override void CollisionCallBack(CollisionEventArgs eventArgs)
		{
			if (eventArgs.CollidesWith is Skill)
			{
			}
		}

		public int Strength
		{
			get { return strength; }
			set { strength = value; }
		}

		public int Dexterity
		{
			get { return dexterity; }
			set { dexterity = value; }
		}

		public int Intelligence
		{
			get { return intelligence; }
			set { intelligence = value; }
		}

		public int Vitality
		{
			get { return vitality; }
			set { vitality = value; }
		}
	}
}
