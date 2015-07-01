using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Skills;
	using Interfaces;
	using Items;
	using Map;
	using Drawing;

	public abstract class Entity : CollidableObject, IBaseStats, ISecondaryStats//IMoveable
	{
		private const int CollisionEdges = 8;
		private const int MaxJumps = 2;
		private const float BodyDensity = 10.0f;

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

		private List<AnimationSprite> animations;

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

			this.animations = new List<AnimationSprite>();
			//this.animations = new Dictionary<string, AnimationSprite>();
			//this.CollisionHull.CollisionHandler += CollisionCallBack;
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
				return this.Strength + this.Vitality + this.armor;
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

		public bool IsAlive
		{
			get
			{
				return this.CurrentHealthPoints <= 0;
			}
		}

		public bool IsControlledByHuman { get; set; }

		public IList<AnimationSprite> Animations
		{
			get
			{
				return this.animations;
			}
		}

		public AnimationSprite CurrentAnimation
		{
			get
			{
				return this.currentAnimation;
			}
		}

		public override void AddToWorld(World physicsWorld)
		{
			//this.CollisionHull = BodyFactory.CreateEllipse(physicsWorld, 1.0f, 1.0f, CollisionEdges, BodyDensity, this);
			//this.CollisionHull.UserData = this;

			this.CollisionHull = BodyFactory.CreateCapsule(physicsWorld, 1.8f, 0.5f, BodyDensity, this);

			//this.CollisionHull = BodyFactory.CreateRectangle(physicsWorld, 1.0f, 1.8f, BodyDensity, this);

			this.CollisionHull.Friction = 4.0f;
			this.CollisionHull.BodyType = BodyType.Dynamic;
			this.CollisionHull.FixedRotation = true;

			if ((int)AnimationType.Idle < this.animations.Count && this.currentAnimation != this.animations[(int)AnimationType.Idle])
			{
				this.currentAnimation = this.animations[(int)AnimationType.Idle];
				this.currentAnimation.Reset();
			}

			this.CollisionHull.OnCollision += OnCollision;
		}

		// TODO: check if with overide event handler will call the new CallBack function
		public override bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			if (fixtureB.UserData is MapBlock)
			{
				this.jump = 0;
				this.inTheAir = false;
			}
			else if (fixtureB.UserData is MapTriggerBlock)
			{
				return false;
			}
			// apply skill effects
			return true;
		}

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

		bool inTheAir;

		public virtual void Update(float deltaTime)
		{
			this.currentAnimation.UpdateAnimation(deltaTime);

			//if (this.CollisionHull.LinearVelocity.Length() < 0.1f)
			//{
			//	if ((int)AnimationType.Idle < this.animations.Count
			//		&& this.currentAnimation != this.animations[(int)AnimationType.Idle])
			//	{
			//		this.currentAnimation = this.animations[(int)AnimationType.Idle];
			//		this.currentAnimation.Reset();
			//	}
			//}
			//else if (this.CollisionHull.LinearVelocity.X < 0)
			//{
			//	this.currentAnimation.Effects = SpriteEffects.FlipHorizontally;
			//}
			//else
			//{
			//	this.currentAnimation.Effects = SpriteEffects.None;
			//}

			if (this.inTheAir)
			{
				if ((int)AnimationType.Jump < this.animations.Count
					&& this.currentAnimation != this.animations[(int)AnimationType.Jump])
				{
					this.currentAnimation = this.animations[(int)AnimationType.Jump];
					this.currentAnimation.Reset();
				}
			}
			else
			{
				if (this.CollisionHull.LinearVelocity.Length() > 0.1f)
				{
					if ((int)AnimationType.Run < this.animations.Count
						&& this.currentAnimation != this.animations[(int)AnimationType.Run])
					{
						this.currentAnimation = this.animations[(int)AnimationType.Run];
						this.currentAnimation.Reset();
					}
				}
				else
				{
					if ((int)AnimationType.Idle < this.animations.Count
						&& this.currentAnimation != this.animations[(int)AnimationType.Idle])
					{
						this.currentAnimation = this.animations[(int)AnimationType.Idle];
						this.currentAnimation.Reset();
					}
				}
			}

			if (this.CollisionHull.LinearVelocity.X < 0)
			{
				this.currentAnimation.Effects = SpriteEffects.FlipHorizontally;
			}
			else
			{
				this.currentAnimation.Effects = SpriteEffects.None;
			}

			this.inTheAir = this.CollisionHull.ContactList == null;
		}

		public void Move(Vector2 impulse)
		{
			this.CollisionHull.ApplyLinearImpulse(impulse);

			if (!this.inTheAir
				&& (int)AnimationType.Run < this.animations.Count
				&& this.currentAnimation != this.animations[(int)AnimationType.Run])
			{
				this.currentAnimation = this.animations[(int)AnimationType.Run];
				this.currentAnimation.Reset();
			}
		}

		public void Jump(Vector2 impulse)
		{
			if (jump < MaxJumps)
			{
				this.CollisionHull.ApplyLinearImpulse(impulse);

				//if (this.jump == 0 && (int)AnimationType.Jump < this.animations.Count)
				//{
				//	this.currentAnimation = this.animations[(int)AnimationType.Jump];
				//	this.currentAnimation.Reset();
				//}

				++this.jump;

				this.inTheAir = true;
			}
		}
	}
}
