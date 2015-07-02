using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Teamwork_OOP.Engine.Drawing;

namespace Teamwork_OOP.Engine.BaseClasses
{
	using Physics;
	public class Projectile : CollidableObject
	{
		private AnimationSprite _animationSpriteSprite;

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
		public AnimationSprite AnimationSprite
		{
			get
			{
				return this._animationSpriteSprite;
			}
			set { this._animationSpriteSprite = value; }
		}
	}
}
