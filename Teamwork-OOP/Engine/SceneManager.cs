using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;

namespace Teamwork_OOP.Engine
{
	using Drawing;
	using Map;
	using BaseClasses;
	using Factories;
	using Characters;
	using Characters.CharacterClasses;

	// TODO: RENAME WITH SOMETHING MORE PROPER !!!
	public class SceneManager
	{
		private const float DrawRadius = 1000.0f;
		private static readonly Vector2 DefaultGravityDirection = new Vector2(0.0f, 9.82f);

		private Vector2 windowHalfSize;
		private MapManager mapManager;
		private World physicsWorld;

		private Vector2 cameraPosition;
		private Vector2 mouseInSimUnitsPosition;
		private bool jumped;
		// here or in MapManager class ????
		//private List<Items> items;

		private IEnumerable<Body> inDrawRange;

		public SceneManager()
		{
			this.physicsWorld = new World(DefaultGravityDirection);

			this.MapManager = new MapManager();
		}

		public SceneManager(TextureManager textureManager, SpriteBatch spriteBatch)
			: this()
		{
			Init(textureManager, spriteBatch);
		}

		public bool LevelFinished { get; set; }

		public Entity CameraAttachedTo { get; set; }

		SpriteBatch SpriteBatch { get; set; }

		public MapManager MapManager
		{
			get
			{
				return this.mapManager;
			}
			set
			{
				this.mapManager = value;
			}
		}
		public TextureManager TextureManager { get; set; }
		public World PhysicsWorld
		{
			get
			{
				return this.physicsWorld;
			}
		}

		public void Init(TextureManager textureManager, SpriteBatch spriteBatch)
		{
			this.TextureManager = textureManager;
			this.SpriteBatch = spriteBatch;
		}

		public void LoadLevel(string filePath)
		{
			this.mapManager = MapFactory.MapLoad(this.TextureManager, filePath);
			this.mapManager.InitPhysics(this.PhysicsWorld);

			if (this.mapManager.CheckPoints.Count > 0)
			{
				var hero = new Warrior();
				EntityFactory.LoadCharacterAnimations(hero, this.TextureManager, "Characters");
				hero.AddToWorld(this.PhysicsWorld);
				hero.CollisionHull.Position = this.mapManager.CheckPoints[0].Position;
				this.CameraAttachedTo = hero;
			}
		}

		public void ResizeWindow(Vector2 newSize)
		{
			this.windowHalfSize = newSize / 2.0f;
		}

		public void ProcessInput(KeyboardState keyboardState, MouseState mouseState)
		{
			if (this.CameraAttachedTo != null)
			{
				var entity = this.CameraAttachedTo;

				if (keyboardState.IsKeyDown(Keys.A))
				{
					entity.Move(new Vector2(-10.0f, 0.0f));
				}

				if (keyboardState.IsKeyDown(Keys.D))
				{
					entity.Move(new Vector2(10.0f, 0.0f));
				}

				if (keyboardState.IsKeyDown(Keys.Space) && !jumped)
				{
					entity.Jump(new Vector2(0.0f, -250.0f));
				}
				jumped = keyboardState.IsKeyDown(Keys.Space);

				if (mouseState.LeftButton == ButtonState.Pressed)
				{
				}
			}

			this.mouseInSimUnitsPosition = ConvertUnits.ToSimUnits(new Vector2(mouseState.X, mouseState.Y) - this.windowHalfSize) + this.cameraPosition;
		}
		
		public void Update(float deltaTime)
		{
			if (this.MapManager.EndOfLevel != null)
			{
				this.LevelFinished = this.MapManager.EndOfLevel.LevelFinished;

				if (this.LevelFinished)
				{
					// TODO: show victory screen or go to next level
				}
			}
			// update and draw only things that are in certain radius
			if (this.CameraAttachedTo != null)
			{
				this.cameraPosition = this.CameraAttachedTo.CollisionHull.Position;
			}

			inDrawRange = this.physicsWorld.BodyList.Where(b => (b.Position - this.cameraPosition).Length() < DrawRadius);

			foreach (var body in inDrawRange)
			{
				if (body.UserData is Entity)
				{
					var entity = ((Entity)body.UserData);

					if (entity.IsAlive)
					{
						entity.Update(deltaTime);
					}
					else
					{
						if (!entity.ToDestroy)
						{
							entity.PlayDeathAnimation(deltaTime);
						}
						else
						{
							if (entity.IsControlledByHuman)
							{
								// TODO: show defeat screen
							}

							this.MapManager.RemoveEntity(entity);
							this.PhysicsWorld.RemoveBody(entity.CollisionHull);
						}
					}
				}
			}

			this.MapManager.Update();

			this.physicsWorld.Step(deltaTime);
		}

		public void Draw()
		{
			if (this.mapManager.Background != null)
			{
				this.SpriteBatch.Begin();

				// TODO: move the background with the camera
				// var mapSize = this.Map.GetMapSize();
				//var backgroundOffset = this.physicsWorld.
				this.SpriteBatch.Draw(this.mapManager.Background, Vector2.Zero, Color.White);

				this.SpriteBatch.End();
			}

			// BEGIN DRAW
			this.SpriteBatch.Begin(SpriteSortMode.BackToFront);

			var cPos = ConvertUnits.ToDisplayUnits(this.cameraPosition) - windowHalfSize;

			foreach (var body in inDrawRange)
			{
				DrawManager.Draw(this.SpriteBatch, body, cPos);
			}

			// END DRAW
			this.SpriteBatch.End();
		}

		public void Clear()
		{
			this.LevelFinished = false;
			this.CameraAttachedTo = null;

			this.PhysicsWorld.Clear();
			this.MapManager.Clear();
		}
	}
}
