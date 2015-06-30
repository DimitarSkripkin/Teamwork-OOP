using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine
{
	using Drawing;
	using Map;
	using BaseClasses;
	using Factories;

	// TODO: RENAME WITH SOMETHING MORE PROPER !!!
	public class SceneManager
	{
		private const float DrawRadius = 1000.0f;
		private static readonly Vector2 DefaultGravityDirection = new Vector2(0.0f, 9.82f);
		//private TextureManager textureManager;
		//private DrawManager drawManager;
		private MapManager mapManager;
		private World physicsWorld;

		Vector2 cameraPosition;

		// here or in MapManager class ????
		//private List<Items> items;

		private IEnumerable<Body> inDrawRange;

		public Entity CameraAttachedTo { get; set; }

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
		}

		public void Update(float deltaTime)
		{
			// update and draw only things that are in certain radius
			if (this.CameraAttachedTo != null)
			{
				cameraPosition = this.CameraAttachedTo.CollisionHull.Position;
			}

			inDrawRange = this.physicsWorld.BodyList.Where(b => (b.Position - this.cameraPosition).Length() < DrawRadius);

			foreach (var body in inDrawRange)
			{
				if (body.UserData is Entity)
				{
					((Entity)body.UserData).Update(deltaTime);
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
				this.SpriteBatch.Draw(this.mapManager.Background, Vector2.Zero, Color.White);

				this.SpriteBatch.End();
			}

			// BEGIN DRAW
			this.SpriteBatch.Begin(SpriteSortMode.BackToFront);

			foreach (var body in inDrawRange)
			{
				DrawManager.Draw(this.SpriteBatch, body, cameraPosition);
			}

			// END DRAW
			this.SpriteBatch.End();
		}
	}
}
