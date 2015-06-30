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

		private Editor editor;
		private SceneManager sceneManager;

		private TextureManager textureManager;

		private SpriteFont spriteFont;

		private bool editorMode, holdF5;

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
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			this.textureManager.Init(this.Content);

			this.sceneManager.Init(this.textureManager, this.spriteBatch);
			this.sceneManager.LoadLevel("map.txt");

			// init editor
			this.editor.Init(this.sceneManager.PhysicsWorld, this.textureManager, this.spriteBatch);
			this.editor.Map = this.sceneManager.MapManager;

			this.spriteFont = Content.Load<SpriteFont>("Font/Font");

			ConvertUnits.SetDisplayUnitToSimUnitRatio(DisplayUnitToSimUnitRatio);

			this.editor.ResizeWindow(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

			// TODO: use this.Content to load your game content here
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here

			this.textureManager.Dispose();
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
				KeyboardState keyState = Keyboard.GetState();
				MouseState mouseState = Mouse.GetState();

				if (keyState.IsKeyDown(Keys.F5) && !this.holdF5)
				{
					this.editorMode = !this.editorMode;
				}
				this.holdF5 = keyState.IsKeyDown(Keys.F5);

				if (editorMode)
				{
					this.editor.UpdateInput(keyState, mouseState);
				}

			}

			this.sceneManager.Update(deltaTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			if (this.editorMode)
			{
				this.editor.Draw();
			}
			else
			{
				this.sceneManager.Draw();
			}

			this.spriteBatch.Begin();
			this.spriteBatch.DrawString(spriteFont, new StringBuilder(string.Format("Blocks: {0}", this.editor.PhysicsWorld.BodyList.Count)), Vector2.Zero, Color.WhiteSmoke, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
			this.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
