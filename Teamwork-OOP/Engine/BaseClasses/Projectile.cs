using System.Collections.Generic;
using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Drawing;
	using Interfaces;

	public class Projectile : CollidableObject, ITimeUpdateable
	{
		private const float DefaultBulletDensity = 10.0f;
		private const float DefaultBulletRadius = 0.5f;

		private AnimationSprite _animationSpriteSprite;
		private float maxActiveTime;

		public Projectile(Entity usedFrom, Vector2 velocity, float maxActiveTime)//, bool isBullet = true)
			: base()
		{
			this.LaunchPosition = usedFrom.CollisionHull.Position;
			this.LaunchVelocity = velocity;
			//this.CollisionHull.IsBullet = isBullet;

			this.UsedFrom = usedFrom;

			this.maxActiveTime = maxActiveTime;
		}

		public override void AddToWorld(World physicsWorld)
		{
			this.CollisionHull = BodyFactory.CreateCircle(physicsWorld, DefaultBulletRadius, DefaultBulletDensity, this);
			this.CollisionHull.UserData = this;

			this.CollisionHull.BodyType = BodyType.Dynamic;
			this.CollisionHull.IsBullet = true;

			this.CollisionHull.Position = this.LaunchPosition;
			this.CollisionHull.LinearVelocity = this.LaunchVelocity;
			//this.CollisionHull.ApplyLinearImpulse(this.LaunchVelocity);
		}

		public Vector2 LaunchPosition { get; set; }
		public Vector2 LaunchVelocity { get; set; }
		//public Vector2 LaunchDirection { get; set; }

		public AnimationSprite AnimationSprite { get; set; }

		public Entity UsedFrom { get; set; }

		public float CurrentActiveTime { get; set; }

		public object UserData { get; set; }

		public void Update(float deltaTime)
		{
			if (this.CurrentActiveTime > this.maxActiveTime)
			{
				this.ToDestroy = true;
			}

			this.CurrentActiveTime += deltaTime;
		}
	}
}