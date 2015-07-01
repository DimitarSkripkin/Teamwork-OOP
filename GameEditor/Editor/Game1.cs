using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Contacts;

using Teamwork_OOP.Engine;
using Teamwork_OOP.Engine.Drawing;
using Teamwork_OOP.Engine.Characters.CharacterClasses;
using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Factories;

using FarseerPhysics.DebugView;

namespace MonogameTestProject.Editor
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		private const float DisplayUnitToSimUnitRatio = 32.0f;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private DebugViewXNA debugDraw;

		private Editor editor;
		private SceneManager sceneManager;
		private TextureManager textureManager;
		private SpriteFont spriteFont;

		private bool editorMode, holdF5;

		Entity temp = new Warrior();

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.editor = new Editor();
			this.sceneManager = new SceneManager();
			this.textureManager = new TextureManager();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			ConvertUnits.SetDisplayUnitToSimUnitRatio(DisplayUnitToSimUnitRatio);

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			this.textureManager.Init(this.Content);

			this.sceneManager.Init(this.textureManager, this.spriteBatch);
			this.sceneManager.LoadLevel("map.txt");

			// init editor
			this.editor.Init(this.sceneManager.PhysicsWorld, this.textureManager, this.spriteBatch);
			this.editor.Map = this.sceneManager.MapManager;

			this.spriteFont = Content.Load<SpriteFont>("Font/Font");


			this.editor.ResizeWindow(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
			this.sceneManager.ResizeWindow(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
			// TODO: use this.Content to load your game content here

			EntityFactory.LoadCharacterAnimations((Warrior)temp, this.textureManager, "Characters");
			this.temp.AddToWorld(this.sceneManager.PhysicsWorld);

			this.temp.CollisionHull.Position = this.sceneManager.MapManager.Triggers[0].Position;
			this.sceneManager.CameraAttachedTo = temp;

			//Debug View
			debugDraw = new DebugViewXNA(this.sceneManager.PhysicsWorld);

			debugDraw.AppendFlags(DebugViewFlags.Shape);
			debugDraw.AppendFlags(DebugViewFlags.DebugPanel);

			debugDraw.DefaultShapeColor = Color.White;
			debugDraw.SleepingShapeColor = Color.LightGray;

			debugDraw.LoadContent(GraphicsDevice, this.Content);
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here

			this.textureManager.Dispose();
			debugDraw.Dispose();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			float deltaTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);
			
			if (this.IsActive)
			{
				KeyboardState keyboardState = Keyboard.GetState();
				MouseState mouseState = Mouse.GetState();

				if (keyboardState.IsKeyDown(Keys.F5) && !this.holdF5)
				{
					this.editorMode = !this.editorMode;
				}
				this.holdF5 = keyboardState.IsKeyDown(Keys.F5);

				if (keyboardState.IsKeyDown(Keys.F6) && this.sceneManager.MapManager.Triggers.Count > 0)
				{
					temp.CollisionHull.Position = this.sceneManager.MapManager.Triggers[0].Position;
					temp.CollisionHull.LinearVelocity = Vector2.Zero;
				}

				if (editorMode)
				{
					this.editor.ProcessInput(keyboardState, mouseState);
				}
				else
				{
					this.sceneManager.ProcessInput(keyboardState, mouseState);
				}
			}

			this.sceneManager.Update(deltaTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Vector2 cameraPosistion;
			var screenCenter = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) / 2.0f;

			if (this.editorMode)
			{
				screenCenter = Vector2.Zero;
				this.editor.Draw();
				cameraPosistion = ConvertUnits.ToSimUnits(-this.editor.CameraPosition);
			}
			else
			{
				this.sceneManager.Draw();
				cameraPosistion = -this.sceneManager.CameraAttachedTo.CollisionHull.Position;
			}

			PhysicsDebugDraw(ref cameraPosistion, ref screenCenter);

			this.spriteBatch.Begin();
			this.spriteBatch.DrawString(spriteFont, new StringBuilder(string.Format("Blocks: {0}\nCharacter position: {1}", this.editor.PhysicsWorld.BodyList.Count, temp.CollisionHull.Position)), Vector2.Zero, Color.WhiteSmoke, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
			this.spriteBatch.End();

			base.Draw(gameTime);
		}

		private void PhysicsDebugDraw(ref Vector2 cameraPosistion, ref Vector2 screenCenter)
		{
			Matrix projection = Matrix.CreateOrthographicOffCenter(0.0f, graphics.GraphicsDevice.Viewport.Width / DisplayUnitToSimUnitRatio,
																  graphics.GraphicsDevice.Viewport.Height / DisplayUnitToSimUnitRatio, 0.0f,
																  0.0f, 1.0f);

			Matrix view = Matrix.CreateTranslation(new Vector3(cameraPosistion, 0.0f)) * Matrix.CreateTranslation(new Vector3((screenCenter / DisplayUnitToSimUnitRatio), 0.0f));

			debugDraw.BeginCustomDraw(projection, view);

			foreach (var body in this.sceneManager.PhysicsWorld.BodyList)
			{
				Transform charTransform;
				body.GetTransform(out charTransform);
				foreach (var fixture in body.FixtureList)
				{
					debugDraw.DrawShape(fixture, charTransform, Color.Gray);
				}
			}

			debugDraw.EndCustomDraw();
		}
	}
}
