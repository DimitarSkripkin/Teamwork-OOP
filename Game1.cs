using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Teamwork_OOP
{
	using Engine;
	using Engine.Physics;
	using Engine.Drawing;
	using Engine.Map;

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		private const int WindowWidth = 640;
		private const int WindowHeight = 480;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Matrix projection;

		private TextureManager textureManager;
		private DrawManager drawManager;
		private SceneManager sceneManager;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.textureManager = new TextureManager();
			this.drawManager = new DrawManager();
			this.sceneManager = new SceneManager(this.textureManager, this.drawManager);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// WINDOW SETTINGS
			this.graphics.PreferredBackBufferWidth = WindowWidth;
			this.graphics.PreferredBackBufferHeight = WindowHeight;
			//this.graphics.ToggleFullScreen();

			// to make bottom left (0, 0) instead of top left
			this.projection = new Matrix(
				1, 0, 0, 0,
				0, -1, 0, 0,
				0, 0, 1, 0,
				0, WindowHeight, 0, 1
			);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			this.spriteBatch = new SpriteBatch(GraphicsDevice);

			this.drawManager.Init(this.spriteBatch);
			this.textureManager.Init(this.Content);

			this.sceneManager.LoadTestLevel();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			this.textureManager.Dispose();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			// check for input and dispatch as event
			MouseState mouse = Mouse.GetState();
			if (mouse.LeftButton == ButtonState.Pressed)
			{
				//Exit();
			}

			// TODO: add update logic here
			TimeSpan timeSpan = gameTime.ElapsedGameTime;
			float deltaTime = (float)(timeSpan.TotalMilliseconds / 1000.0f);

			this.sceneManager.Update(deltaTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// BEGIN DRAW
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, RasterizerState.CullClockwise, null, projection);

			this.sceneManager.Draw();

			// END DRAW
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}