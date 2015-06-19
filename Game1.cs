using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Teamwork_OOP
{
	using Engine;
	using Engine.Physics;

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Texture2D logo;
		private Texture2D runAnimation;
		private AnimationSprite animation;

		// vvvv remove vvvv
		private Texture2D box, circle;
		CollisionShape shapeA, shapeB;
		// ^^^^^^^^^^^^^^^^

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			animation = new AnimationSprite(10, 1);

			// WINDOW SETTINGS
			graphics.PreferredBackBufferWidth = 640;
			graphics.PreferredBackBufferHeight = 480;
			//graphics.ToggleFullScreen();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			logo = this.Content.Load<Texture2D>("Textures/logo");
			runAnimation = this.Content.Load<Texture2D>("Textures/Run");

			// vvvv remove vvvv
			box = this.Content.Load<Texture2D>("CollisionTestShapes/box");
			circle = this.Content.Load<Texture2D>("CollisionTestShapes/circle");
			shapeA = new AABB(new Vector2(100, 100), new Vector2(64, 64), CollisionObjectFlags.Kinematic);
			//shapeB = new AABB(new Vector2(100, 100), new Vector2(-32), new Vector2(32), CollisionObjectFlags.Kinematic);
			shapeB = new Circle(32, new Vector2(30, 30), CollisionObjectFlags.Kinematic);
			// ^^^^^^^^^^^^^^^^
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			//this.Content.Unload();
			logo.Dispose();
			runAnimation.Dispose();

			// vvvv remove vvvv
			box.Dispose();
			circle.Dispose();
			// ^^^^^^^^^^^^^^^^
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

			animation.UpdateAnimation(deltaTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			// vvvv remove vvvv
			Matrix matrix = Matrix.Identity;
			//matrix = Matrix.CreateRotationZ((float)(-45.0f * (Math.PI / 180.0)));

			//spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			//spriteBatch.Draw(logo, new Vector2(10, 10), Color.White);
			//spriteBatch.End();
			// ^^^^^^^^^^^^^^^^

			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null,null,null,null, matrix);
			//spriteBatch.Draw(logo, new Vector2(10, 10), Color.White);

			// TODO: ???
			//animation.DrawSprite();

			// vvvv remove vvvv
			shapeB.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
			Color color = Color.White;
			if (CollisionChecker.CheckForCollision(shapeA, shapeB))
			{
				color = Color.Blue;
			}
			spriteBatch.Draw(box, shapeA.Position, null, color, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
			spriteBatch.Draw(circle, shapeB.Position, null, color, 0.0f, new Vector2(circle.Width/2), 0.5f, SpriteEffects.None, 0);
			// ^^^^^^^^^^^^^^^^

			// draw animated test sprite
			//spriteBatch.Draw(runAnimation, new Vector2(Mouse.GetState().X, Mouse.GetState().Y),// Vector2.Zero,
			//	new Rectangle(animation.GetCurrentFrame() * 64, 0, 64, 64),
			//	Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
