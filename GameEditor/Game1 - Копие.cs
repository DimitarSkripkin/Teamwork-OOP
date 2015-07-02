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

namespace MonogameTestProject
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		World world;
		Texture2D boxTexture, platformTexture, block;
		Body box, platform;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			world = new World(new Vector2(0f, 9.82f));
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		private bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
		{
			//if (contact.Manifold.LocalNormal.Y < 0 && contact.Manifold.LocalPoint.Y < ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Height))
			//{
			//	return false;
			//}
			return true;
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			boxTexture = Content.Load<Texture2D>("testTextures/box");
			platformTexture = Content.Load<Texture2D>("testTextures/platform");
			block = Content.Load<Texture2D>("testTextures/0");

			float scale = block.Height;// 32.0f;
			ConvertUnits.SetDisplayUnitToSimUnitRatio(scale);

			float simulatedHeight = GraphicsDevice.Viewport.Height / scale;
			float simulatedWidth = GraphicsDevice.Viewport.Width / scale;
			BodyFactory.CreateEdge(this.world, new Vector2(0.0f, 0.0f), new Vector2(simulatedWidth, 0.0f));
			BodyFactory.CreateEdge(this.world, new Vector2(0.0f, simulatedHeight), new Vector2(simulatedWidth, simulatedHeight));
			BodyFactory.CreateEdge(this.world, new Vector2(0.0f, 0.0f), new Vector2(0.0f, simulatedHeight));
			BodyFactory.CreateEdge(this.world, new Vector2(simulatedWidth, 0.0f), new Vector2(simulatedWidth, simulatedHeight));

			box = BodyFactory.CreateRectangle(this.world,
				ConvertUnits.ToSimUnits(boxTexture.Width),
				ConvertUnits.ToSimUnits(boxTexture.Height),
				10.0f,
				ConvertUnits.ToSimUnits(new Vector2(350, 5)),
				boxTexture);//user data
			box.BodyType = BodyType.Dynamic;
			box.FixedRotation = true;
			box.OnCollision += OnCollision;

			//box.BodyType = BodyType.Kinematic;
			//box.ControllerFilter.ControllerFlags = FarseerPhysics.Controllers.ControllerType.GravityController;

			platform = BodyFactory.CreateRectangle(this.world,
				ConvertUnits.ToSimUnits(platformTexture.Width),
				ConvertUnits.ToSimUnits(platformTexture.Height),
				10.0f,
				ConvertUnits.ToSimUnits( new Vector2(250, 300)),
				platformTexture);//user data
			platform.BodyType = BodyType.Kinematic;
			platform.Friction = 5.0f;
			//platform.LinearVelocity = new Vector2(1, 0);

			// TODO: use this.Content to load your game content here
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			boxTexture.Dispose();
			platform.Dispose();
			block.Dispose();
		}

		bool jumped = false;
		bool tab = false;
		bool scaled = false;
		bool pressed = false;

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			float deltaTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);

			KeyboardState keyState = Keyboard.GetState();

			// box controlls
			if (keyState.IsKeyDown(Keys.A))
			{
				box.ApplyLinearImpulse(new Vector2(-15, 0));
			}

			if (keyState.IsKeyDown(Keys.D))
			{
				box.ApplyLinearImpulse(new Vector2(15, 0));
			}

			if (keyState.IsKeyDown(Keys.Space) && !jumped)
			{
				box.ApplyLinearImpulse(new Vector2(0, -150));
			}
			jumped = keyState.IsKeyDown(Keys.Space);

			// platform controlls
			if (keyState.IsKeyDown(Keys.Left))
			{
				//platform.ApplyForce(new Vector2(-100, 0));
				platform.LinearVelocity = new Vector2(-1, 0);
			}
			if (keyState.IsKeyDown(Keys.Right))
			{
				//platform.ApplyForce(new Vector2(100, 0));
				platform.LinearVelocity = new Vector2(1, 0);
			}

			if (platform.Position.X < ConvertUnits.ToSimUnits(150))
			{
				platform.LinearVelocity = new Vector2(1, 0);
			}
			if (platform.Position.X > ConvertUnits.ToSimUnits(350))
			{
				platform.LinearVelocity = new Vector2(-1, 0);
			}

			// resize body

			if (keyState.IsKeyDown(Keys.Tab) && !tab)
			{
				//resize box
				//box.CreateFixture(new CircleShape(1.5f,10.0f), boxTexture);
				//box.CreateFixture(BodyFactory.CreateRectangle(world, 1, 1, 1, boxTexture).FixtureList[0], boxTexture);
				box.DestroyFixture(box.FixtureList[0]);

				Vertices rectangleVertices;
				if (!scaled)
				{
					rectangleVertices = PolygonTools.CreateRectangle(ConvertUnits.ToSimUnits(boxTexture.Width / 2) / 2, ConvertUnits.ToSimUnits(boxTexture.Height / 2) / 2);
					scaled = true;
				}
				else
				{
					rectangleVertices = PolygonTools.CreateRectangle(ConvertUnits.ToSimUnits(boxTexture.Width) / 2, ConvertUnits.ToSimUnits(boxTexture.Height) / 2);
					scaled = false;
				}

				PolygonShape rectangleShape = new PolygonShape(rectangleVertices, 10.0f);
				box.CreateFixture(rectangleShape, boxTexture);
				box.OnCollision += OnCollision;
				//box.Friction = 10.0f;
			}
			tab = keyState.IsKeyDown(Keys.Tab);

			MouseState mouseState = Mouse.GetState();
			if (mouseState.RightButton == ButtonState.Pressed && !pressed)
			{
				var addBox = BodyFactory.CreateRectangle(this.world,
					ConvertUnits.ToSimUnits(block.Width),
					ConvertUnits.ToSimUnits(block.Height),
					10.0f,
					ConvertUnits.ToSimUnits(new Vector2(mouseState.Position.X,mouseState.Position.Y)),
					block);//user data
				addBox.BodyType = BodyType.Dynamic;
				//addBox.FixedRotation = true;
				//addBox.OnCollision += OnCollision;
			}

			if (mouseState.LeftButton == ButtonState.Pressed && !pressed)
			{
				var addBox = BodyFactory.CreateRectangle(this.world,
					ConvertUnits.ToSimUnits(block.Width),
					ConvertUnits.ToSimUnits(block.Height),
					10.0f,
					new Vector2(
						(int)ConvertUnits.ToSimUnits(mouseState.Position.X),
						(int)ConvertUnits.ToSimUnits(mouseState.Position.Y)
					),
					//ConvertUnits.ToSimUnits(new Vector2(mouseState.Position.X, mouseState.Position.Y)),
					block);//user data
				addBox.BodyType = BodyType.Static;
				//addBox.FixedRotation = true;
				//addBox.OnCollision += OnCollision;
			}
			pressed = mouseState.RightButton == ButtonState.Pressed || mouseState.LeftButton == ButtonState.Pressed;

			//world.Step(deltaTime);
			world.Step(0.01f);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			var objects = world.BodyList;
			SpriteEffects effects;
			float bodyScale;

			for (int i = 0; i < objects.Count; ++i)
			{
				if (objects[i].LinearVelocity.X < 0)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				else
				{
					effects = SpriteEffects.None;
				}
				if (objects[i] == box && scaled)
				{
					bodyScale = 0.5f;
				}
				else
				{
					bodyScale = 1.0f;
				}
				Draw(objects[i], Vector2.Zero, bodyScale,effects);
			}

			spriteBatch.Draw(block, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, Color.White, 0.0f, new Vector2(block.Width / 2.0f, block.Height / 2.0f), 1.0f, SpriteEffects.None, 0.01f);

			//if (scaled)
			//{
			//	Draw(box, boxTexture, Vector2.Zero, 0.5f);
			//}
			//else
			//{
			//	Draw(box, boxTexture, Vector2.Zero);
			//}

			//if (platform.LinearVelocity.X < 0)
			//{
			//	Draw(platform, platformTexture, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally);
			//}
			//else
			//{
			//	Draw(platform, platformTexture, Vector2.Zero);
			//}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void Draw(Body body, Vector2 positionOffset, float scale = 1.0f, SpriteEffects effects = SpriteEffects.None, float depthLayer = 0.0f)
		{
			Texture2D texture = (Texture2D)body.UserData;
			if (texture == null)
			{
				return;
			}

			Vector2 bodyPosition = ConvertUnits.ToDisplayUnits(body.Position);
			Vector2 bodyOffset = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);// ConvertUnits.ToDisplayUnits(platform.LocalCenter);
			float bodyRotation = body.Rotation;
			float bodyScale = scale;// / ConvertUnits.ToSimUnits(platformTexture.Width);

			spriteBatch.Draw(texture, bodyPosition, null, Color.White, bodyRotation, bodyOffset, bodyScale, effects, depthLayer);
		}
	}
}
