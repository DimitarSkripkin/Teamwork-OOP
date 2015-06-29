using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Contacts;
using System.Text;

namespace MonogameTestProject.Editor
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		World world;

		Editor editor;

		private SpriteFont spriteFont;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.world = new World(new Vector2(0f, 9.82f));
			this.editor = new Editor();
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

			this.editor.Init(this.world, this.Content, this.spriteBatch);

			this.spriteFont = Content.Load<SpriteFont>("Font/Font");

			float scale = 32.0f;//block.Height;
			ConvertUnits.SetDisplayUnitToSimUnitRatio(scale);

			this.editor.ResizeWindow(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

			// TODO: use this.Content to load your game content here
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here

			this.editor.Dispose();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			// TODO: Add your update logic here
			float deltaTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);

			KeyboardState keyState = Keyboard.GetState();
			MouseState mouseState = Mouse.GetState();

			this.editor.UpdateInput(keyState, mouseState, this.IsActive);

			this.world.Step(deltaTime);
			//this.world.Step(0.01f);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			this.editor.Draw();

			this.spriteBatch.Begin();
			this.spriteBatch.DrawString(spriteFont, new StringBuilder(string.Format("Blocks: {0}", this.editor.PhysicsWorld.BodyList.Count)), Vector2.Zero, Color.WhiteSmoke, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
			this.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
