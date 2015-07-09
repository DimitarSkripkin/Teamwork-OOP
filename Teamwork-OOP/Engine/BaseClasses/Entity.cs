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

	public abstract class Entity : CollidableObject, IBaseStats, ISecondaryStats, ITimeUpdateable//, IMoveable
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

		private Dictionary<string, AnimationSprite> animations;

		private AnimationSprite currentAnimation;
		private Frame lastFrame;

		//private bool isAlive;
		private bool isAttacking;
		private bool inTheAir;

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

			this.animations = new Dictionary<string, AnimationSprite>();

			this.CurrentHealthPoints = this.HealthPoints;
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
				return this.CurrentHealthPoints > 0;
			}
		}

		public bool IsControlledByHuman { get; set; }

		public AnimationSprite CurrentAnimation
		{
			get
			{
				return this.currentAnimation;
			}
		}
		
		public IDictionary<string, AnimationSprite> Animations
		{
			get
			{
				return this.animations;
			}
		}


		public MapCheckPoint CheckPoint { get; set; }

		public Skill BasicAttack { get; set; }

		public Skill SpecialSkill_0 { get; set; }

		public Skill SpecialSkill_1 { get; set; }

		public override void AddToWorld(World physicsWorld)
		{
			if (this.CollisionHull != null)
			{
				this.CollisionHull.Friction = 4.0f;
				this.CollisionHull.BodyType = BodyType.Dynamic;
				this.CollisionHull.FixedRotation = true;

				SetAnimation("IDLE");

				this.CollisionHull.OnCollision += OnCollision;
			}
		}

		// TODO: check if with overide event handler will call the new CallBack function
		public override bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			if (fixtureB.UserData is MapBlock || fixtureB.UserData is MapCheckPoint)
			{
				this.jump = 0;
				this.inTheAir = false;
			}
			else if (fixtureB.UserData is MapFlagBlock)
			{
				return false;
			}
			//else if (fixtureB.UserData is Skill)
			//{
			//}
			else if (fixtureB.UserData is MapCheckPoint)
			{
				this.CheckPoint = (MapCheckPoint)fixtureB.UserData;
			}
			else if (fixtureB.UserData is Projectile)
			{
				var projectile = (Projectile)fixtureB.UserData;

				if (projectile.UsedFrom == this)
				{
					return false;
				}

			  ((ProjectileSkill)projectile.UserData).ApplySkillEffect(this);
			}
			// apply skill effects
			return true;
		}

		public void SetAnimation(string animationType)
		{
			if (this.animations.ContainsKey(animationType) && this.currentAnimation != this.animations[animationType])
			{
				this.currentAnimation = this.animations[animationType];
				this.currentAnimation.Reset();
			}
		}

		public virtual void AttackTarget(Entity attackTarget)
		{
			// TODO: use skill instead of 
			if (this.BasicAttack != null && this.BasicAttack.Activate())
			{
				this.BasicAttack.ApplySkillEffect(attackTarget);

				SetAnimation("ATTACK");
				this.isAttacking = true;
			}
			else
			{
				if (this.currentAnimation.Ended)
				{
					SetAnimation("IDLE");
					this.isAttacking = false;
				}
				else
				{
					if (attackTarget.CollisionHull.Position.X < this.CollisionHull.Position.X)
					{
						this.currentAnimation.Effects = SpriteEffects.FlipHorizontally;
					}
					else
					{
						this.currentAnimation.Effects = SpriteEffects.None;
					}
				}
			}
		}

		public virtual void StopAttack()
		{
			this.isAttacking = false;
		}

		public virtual void UpdateCooldowns(float deltaTime)
		{
			if (this.BasicAttack != null)
			{
				this.BasicAttack.Update(deltaTime);
			}
			if (this.SpecialSkill_0 != null)
			{
				this.SpecialSkill_0.Update(deltaTime);
			}
			if (this.SpecialSkill_1 != null)
			{
				this.SpecialSkill_1.Update(deltaTime);
			}
		}

		public void Update(float deltaTime)
		{
			this.currentAnimation.UpdateAnimation(deltaTime);

			if (this.IsAlive)
			{
				if (this.inTheAir)
				{
					SetAnimation("JUMP");
				}
				else
				{
					if (!this.isAttacking)
					{
						if (this.CollisionHull.LinearVelocity.Length() > 0.1f)
						{
							SetAnimation("RUN");
						}
						else
						{
							SetAnimation("IDLE");
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
			else
			{
				SetAnimation("DEATH");

				if (this.currentAnimation.Ended)
				{
					this.ToDestroy = true;
				}
			}

			UpdateCooldowns(deltaTime);
		}

		public void Move(Vector2 impulse)
		{
			this.CollisionHull.ApplyLinearImpulse(impulse);

			if (!this.inTheAir)
			{
				SetAnimation("RUN");
			}
		}

		public void Jump(Vector2 impulse)
		{
			if (jump < MaxJumps)
			{
				this.CollisionHull.ApplyLinearImpulse(impulse);

				++this.jump;

				this.inTheAir = true;
			}
		}
	}
}
