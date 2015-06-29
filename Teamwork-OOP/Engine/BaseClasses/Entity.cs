using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Contacts;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Skills;
	using Interfaces;
	using Items;
	using Map;
	using Drawing;

	public abstract class Entity : CollidableObject, IBaseStats, ISecondaryStats//IMoveable
	{
		private const int MaxJumps = 2;
		private int jump;

		//Base Stats
		private int strength;
		private int dexterity;
		private int intelligence;
		private int vitality;

		//Secondary Stats
		private int attackDamage;
		private int spellDamage;
		private int armor;
		private int magicResistance;
		//private float attackSpeed;
		//private float spellCastingSpeed;
		//private float movementSpeed;
		private int healthPoints;
		private int manaPoints;
		//private float attackRange;
		private float criticalHitChance;
		private float criticalDamage;

		private AnimationSprite currentAnimation;
		private Frame lastFrame;

		//private bool isAlive;

		protected Entity(int strength, int dexterity, int intelligence, int vitality,
			int attackDamage, int spellDamage,
			int armor, int magicResistance,
			float attackSpeed, float spellCastingSpeed, float movementSpeed,
			int healthPoints, int manaPoints,
			float attackRange,
			float criticalHitChance, float criticalDamage)
			: base()
		{
			this.Strength = strength;
			this.Dexterity = dexterity;
			this.Intelligence = intelligence;
			this.Vitality = vitality;

			this.AttackDamage = attackDamage;
			this.SpellDamage = spellDamage;
			this.Armor = armor;
 			this.MagicResistance = magicResistance;
			this.AttackSpeed = attackSpeed;
			this.SpellCastingSpeed = spellCastingSpeed;
			this.MovementSpeed = movementSpeed;
			this.HealthPoints = healthPoints;
			this.ManaPoints = manaPoints;
			this.AttackRange = attackRange;
			this.CriticalHitChance = criticalHitChance;
			this.CriticalDamage = criticalDamage;

			this.currentAnimation = new AnimationSprite();
			//this.CollisionHull.CollisionHandler += CollisionCallBack;
		}

		public override void AddToWorld(World physicsWorld)
		{
			this.CollisionHull = BodyFactory.CreateCapsule(physicsWorld, 2.0f, 1.0f, 10.0f, this);
			this.CollisionHull.OnCollision += OnCollision;
		}

		// TODO: check if with overide event handler will call the new CallBack function
		public override bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			throw new NotImplementedException();
			//return true;

			//if (fixtureA is MapBlock ||
			if (fixtureB.UserData is MapBlock)
			{
				if (contact.Manifold.LocalNormal.Y > 0.0f)
				{
					this.jump = 0;
				}
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
		//todo Current HP and MP
		public virtual int AttackDamage
		{
			get
			{
				return this.Strength;

			}
			set
			{
				this.attackDamage = value;

			}
		}

		public virtual int SpellDamage
		{
			get
			{
				return this.Intelligence;
			}
			set
			{
				this.spellDamage = value;

			}
		}

		public virtual int Armor
		{
			get
			{
				return this.Strength + this.Vitality+ this.armor;
			}
			set
			{
				if (value > 70)
				{
					this.armor = 70;
				}
				else
				{
					this.armor = value;
				}
			}
		} //Damage reduction from armor shouldnt exceed 70%.
		public virtual int MagicResistance
		{
			get
			{
				return this.Intelligence + this.Vitality + this.magicResistance;
			}
			set
			{
				if (value > 70)
				{
					this.magicResistance = 70;
				}
				else
				{
					this.magicResistance = value;
				}
			}
		}//Same as armor.
		// TODO: EnergyShield prop ?

		public virtual float AttackSpeed { get; set; }
		public virtual float SpellCastingSpeed { get; set; }
		public virtual float MovementSpeed { get; set; }

		public virtual int HealthPoints
		{
			get
			{
				return this.Vitality * 10;
			}
			set
			{
				this.healthPoints = value;
			}
		}
		public int CurrentHealthPoints { get; set; }

		public virtual int ManaPoints
		{
			get
			{
				return this.Intelligence * 10;
			}
			set
			{
				this.manaPoints = value;
			}
		}
		public int CurrentManaPoints { get; set; }

		public virtual float AttackRange { get; set; }

		public virtual float CriticalHitChance
		{
			get
			{
				return this.Dexterity;
			}
			set
			{
				this.criticalHitChance = value;
			}
		}
		public virtual float CriticalDamage
		{
			get
			{
				return (this.Strength + this.Intelligence) * 0.025f + 2.0f;
			}
			set
			{
				this.criticalDamage = value;
			}
		}

		//public bool Jumped { get; set; }

		public bool IsAlive { get { return false; } }

		public bool IsControlledByHuman { get; set; }

		public bool TryToAttack(Entity attackTarget)
		{
			if (true)
			{

			}
			return false;
		}

		public virtual void CheckCooldown()
		{
			//todo
		}

		public virtual void Update()
		{
			//todo
			if (this.lastFrame != this.currentAnimation.CurrentFrame)
			{
				this.lastFrame = this.currentAnimation.CurrentFrame;
				//var hull = this.CollisionHull;

				//foreach (var fixture in hull.FixtureList)
				//{
				//	hull.DestroyFixture(fixture);
				//}
				//hull.FixtureList = new 
				
			}
		}

		public void Move(Vector2 impulse)
		{
			this.CollisionHull.ApplyLinearImpulse(impulse);
		}

		public void Jump(Vector2 impulse)
		{
			if (jump < MaxJumps)
			{
				this.CollisionHull.ApplyLinearImpulse(impulse);
				++this.jump;
			}
		}
	}
}
