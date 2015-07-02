using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace Teamwork_OOP.Engine.Map
{
	using Drawing;
	using Characters;
	using Characters.CharacterClasses;

	public class MapEndOfLevel : MapItem
	{
		public MapEndOfLevel(Vector2 position, TextureNode textureNode)
			: base(position, textureNode)
		{
		}

		public override void AddToWorld(World physicsWorld)
		{
			var size = ConvertUnits.ToSimUnits(new Vector2(this.TextureNode.SourceRectangle.Width, this.TextureNode.SourceRectangle.Height));
			this.CollisionHull = BodyFactory.CreateRectangle(physicsWorld,
						size.X,
						size.Y,
						10.0f,
						this.Position + size / 2.0f,
						this);

			foreach (var fixture in this.CollisionHull.FixtureList)
			{
				fixture.UserData = this;
			}

			this.CollisionHull.OnCollision += OnCollision;
		}

		public bool LevelFinished { get; set; }

		public override bool OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
		{
			if (fixtureB.UserData is PlayerCharacter)
			{
				this.LevelFinished = true;
			}
			return true;
		}
	}
}
