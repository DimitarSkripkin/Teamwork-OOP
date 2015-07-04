﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using FarseerPhysics;
using Teamwork_OOP.Engine.UI;

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
		private const float DisplayUnitToSimUnitRatio = 32.0f;

		private const int WindowWidth = 800;
		private const int WindowHeight = 600;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private TextureManager textureManager;
		private SceneManager sceneManager;
		private UIManager uiManager;

		private UIMenu mainMenu;

		bool isInGame;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.textureManager = new TextureManager();
			this.sceneManager = new SceneManager();
			this.uiManager = new UIManager();

			this.mainMenu = new UIMenu();
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
			this.IsMouseVisible = true;
			//this.graphics.ToggleFullScreen();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			ConvertUnits.SetDisplayUnitToSimUnitRatio(DisplayUnitToSimUnitRatio);
			
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			this.textureManager.Init(this.Content);

			this.sceneManager.Init(this.textureManager, this.spriteBatch);
			this.sceneManager.ResizeWindow(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

			this.mainMenu.LoadMenu("Textures/MenuItems/Buttons1", "Textures/MenuItems/MenuBackground0", this.textureManager);

			this.uiManager.AddMenu(mainMenu);
			this.uiManager.ActiveMenu = mainMenu;

			this.uiManager.RegisterClickEvent("NewGame", StartNewGame);
			this.uiManager.RegisterClickEvent("Exit", Exit);
		}

		public void StartNewGame()
		{
			this.sceneManager.LoadLevel("map.txt");
			this.mainMenu.IsVisible = false;
			this.isInGame = true;
		}

		public void VictoryMenu()
		{
			// when the game is over
			//this.isInGame = false;

			// remove from here ?
			//this.mainMenu.IsVisible = true;
			//this.sceneManager.Clear();
		}

		public void DefeatMenu()
		{
			this.isInGame = false;
			this.mainMenu.IsVisible = true;
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

			// TODO: add update logic here
			TimeSpan timeSpan = gameTime.ElapsedGameTime;
			float deltaTime = (float)(timeSpan.TotalMilliseconds / 1000.0f);

			//this.isInGame = (this.sceneManager.GameState == GameState.InGame && this.sceneManager.GameState != GameState.NotInGame);

			if (this.IsActive)
			{
				KeyboardState keyboardState = Keyboard.GetState();
				MouseState mouseState = Mouse.GetState();

				if (this.isInGame)
				{
					this.sceneManager.ProcessInput(keyboardState, mouseState);
				}

				this.uiManager.ProcessInput(mouseState);
			}

			if (this.isInGame)
			{
				this.sceneManager.Update(deltaTime);
			}

			// remove from here ?
			if (this.sceneManager.GameState == GameState.FinishedVictory)
			{
				VictoryMenu();
			}
			else if (this.sceneManager.GameState == GameState.FinishedDefeat)
			{
				DefeatMenu();
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			if (this.isInGame)
			{
				this.sceneManager.Draw();
			}
			this.uiManager.Draw(this.spriteBatch);
			base.Draw(gameTime);
		}
	}
}