using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;

using Teamwork_OOP.Engine.Drawing;
using Teamwork_OOP.Engine.Map;
using Teamwork_OOP.Engine.Factories;

namespace MonogameTestProject.Editor
{
	public enum EditorModes
	{
		AddBlocks,
		AddFlags,
		AddPlatforms,
		AddImportanPoints,
		AddBackground
	}

	public class Editor
	{
		private const int ResourceCount = 5;
		private const int BlocksCount = 13;
		private const int FlagCount = 1;
		private const int PlatformCount = 3;
		private const int ImportantPointsCount = 3;
		private const int BackgroundCount = 6;

		private Vector2 mousePosition, cameraPosition;
		private Vector2 windowHalfSize;

		private Matrix scaleMatrix;
		private float scaleValue;

		private TextureManager textureManager;

		private List<TextureNode>[] resources;
		private EditorModes editorMode;

		private int currentResourceSelection;

		private int lastScrollWheelValue;

		private bool pressedMLB;
		private bool pressedMRB;

		// used to put blocks and platforms
		private Vector2 startPosition, endPosition;

		private List<TextureNode> mouseCursors;

		private MapBlock tempBlock;
		private MapItem mouseOverBlock;
		//private MapPlatform tempPlatform;

		public Editor()
		{
			this.resources = new List<TextureNode>[ResourceCount];
			for(int i = 0; i < ResourceCount; ++i )
			{
				this.resources[i] = new List<TextureNode>();
			};

			this.mouseCursors = new List<TextureNode>();

			this.scaleMatrix = Matrix.Identity;
			this.scaleValue = 1.0f;
		}

		public World PhysicsWorld { get; set; }
		public SpriteBatch SpriteBatch { get; set; }

		public Vector2 CameraPosition
		{
			get
			{
				return this.cameraPosition;
			}
		}

		public float Scale
		{
			get
			{
				return this.scaleValue;
			}
		}

		public MapManager Map { get; set; }

		public void Init(World world, TextureManager textureManager, SpriteBatch spriteBatch)
		{
			this.textureManager = textureManager;

			this.PhysicsWorld = world;
			this.SpriteBatch = spriteBatch;

			var mouseCursorsTexture = this.textureManager.LoadTexture("Textures/MouseCursors/mouseCursors");
			for (int i = 0; i < ResourceCount; ++i)
			{
				var toAdd = new TextureNode();

				toAdd.Name = string.Format("cursor{0}", i);
				toAdd.Texture = mouseCursorsTexture;
				toAdd.SourceRectangle = new Rectangle(0, i * 32, mouseCursorsTexture.Width, 32);

				this.mouseCursors.Add(toAdd);
			}

			var blocksTexture = this.textureManager.LoadTexture("Textures/Blocks/Blocks");
			var blocksNodes = this.resources[(int)EditorModes.AddBlocks];
			for (int i = 0; i < BlocksCount; ++i)
			{
				var toAdd = new TextureNode();

				toAdd.Name = string.Format("block{0}", i);
				toAdd.Texture = blocksTexture;
				toAdd.SourceRectangle = new Rectangle(0, i * 32, blocksTexture.Width, 32);

				blocksNodes.Add(toAdd);
			}

			var triggerTexture = this.textureManager.LoadTexture("Textures/Blocks/Triggers");
			var triggersNodes = this.resources[(int)EditorModes.AddFlags];
			for (int i = 0; i < FlagCount; ++i)
			{
				var toAdd = new TextureNode();

				toAdd.Name = string.Format("flag{0}", i);
				toAdd.Texture = triggerTexture;
				toAdd.SourceRectangle = new Rectangle(0, i * 32, triggerTexture.Width, 32);

				triggersNodes.Add(toAdd);
			}

			var platformTexture = this.textureManager.LoadTexture("Textures/Blocks/Platforms");
			var platformNodes = this.resources[(int)EditorModes.AddPlatforms];
			for (int i = 0; i < PlatformCount; ++i)
			{
				var toAdd = new TextureNode();

				toAdd.Name = string.Format("platform{0}", i);
				toAdd.Texture = platformTexture;
				toAdd.SourceRectangle = new Rectangle(0, i * 32, platformTexture.Width, 32);

				platformNodes.Add(toAdd);
			}

			/////// ADD IMPORTANT POINTS
			var importantPointsTexture = this.textureManager.LoadTexture("Textures/Blocks/ImportantPoints");
			var importantPointsNodes = this.resources[(int)EditorModes.AddImportanPoints];

			var toAddImportantPoint = new TextureNode();
			toAddImportantPoint.Name = string.Format("EndOfLevel");
			toAddImportantPoint.Texture = importantPointsTexture;
			toAddImportantPoint.SourceRectangle = new Rectangle(0, 0, 163, 163);
			importantPointsNodes.Add(toAddImportantPoint);

			toAddImportantPoint = new TextureNode();
			toAddImportantPoint.Name = string.Format("CheckPoint");
			toAddImportantPoint.Texture = importantPointsTexture;
			toAddImportantPoint.SourceRectangle = new Rectangle(164, 0, 147, 36);
			importantPointsNodes.Add(toAddImportantPoint);

			toAddImportantPoint = new TextureNode();
			toAddImportantPoint.Name = string.Format("SpawnPoint");
			toAddImportantPoint.Texture = importantPointsTexture;
			toAddImportantPoint.SourceRectangle = new Rectangle(330, 0, 32, 32);
			importantPointsNodes.Add(toAddImportantPoint);
			/////////

			var backgroundNodes = this.resources[(int)EditorModes.AddBackground];
			for (int i = 0; i < BackgroundCount; ++i)
			{
				var textureNode = new TextureNode();
				textureNode.Name = string.Format("Backgrounds/{0}a", i);
				textureNode.Texture = this.textureManager.LoadTexture(textureNode.Name);
				textureNode.SourceRectangle = textureNode.Texture.Bounds;

				backgroundNodes.Add(textureNode);
			}
		}

		public void ResizeWindow(Vector2 newSize)
		{
			this.windowHalfSize = newSize / 2;
		}

		private void MoveCamera(KeyboardState keyState, MouseState mouseState, int deltaScrollWheel)
		{
			if (keyState.IsKeyDown(Keys.A))
			{
				this.cameraPosition += new Vector2(-10, 0) / this.scaleValue;
			}
			if (keyState.IsKeyDown(Keys.D))
			{
				this.cameraPosition += new Vector2(10, 0) / this.scaleValue;
			}
			if (keyState.IsKeyDown(Keys.W))
			{
				this.cameraPosition += new Vector2(0, -10) / this.scaleValue;
			}
			if (keyState.IsKeyDown(Keys.S))
			{
				this.cameraPosition += new Vector2(0, 10) / this.scaleValue;
			}

			if (keyState.IsKeyDown(Keys.LeftControl) && deltaScrollWheel != 0)
			{
				if (deltaScrollWheel > 0)
				{
					this.scaleValue -= 0.1f;
				}
				else
				{
					this.scaleValue += 0.1f;
				}

				if (this.scaleValue >= 1.0f)
				{
					this.scaleValue = 1.0f;
					this.scaleMatrix.M11 = this.scaleValue;
					this.scaleMatrix.M22 = this.scaleValue;
					return;
				}
				else if (this.scaleValue <= 0)
				{
					this.scaleValue = 0.1f;
					this.scaleMatrix.M11 = this.scaleValue;
					this.scaleMatrix.M22 = this.scaleValue;
					return;
				}

				this.scaleMatrix.M11 = this.scaleValue;
				this.scaleMatrix.M22 = this.scaleValue;
				//this.scaleMatrix = Matrix.CreateScale(this.scaleValue, this.scaleValue, 1);

				if (deltaScrollWheel < 0)
				{
					this.cameraPosition += (new Vector2(mouseState.Position.X, mouseState.Position.Y) - windowHalfSize) / this.scaleValue;
				}
			}
		}

		public void ProcessInput(KeyboardState keyboardState, MouseState mouseState)
		{
			int deltaScrollWheel = this.lastScrollWheelValue - mouseState.ScrollWheelValue;
			this.lastScrollWheelValue = mouseState.ScrollWheelValue;
			
			MoveCamera(keyboardState, mouseState, deltaScrollWheel);

			if (!keyboardState.IsKeyDown(Keys.LeftControl))
			{
				if (deltaScrollWheel > 0)
				{
					++this.currentResourceSelection;
				}
				else if (deltaScrollWheel < 0)
				{
					--this.currentResourceSelection;
				}
			}

			if (keyboardState.IsKeyDown(Keys.D1))
			{
				this.editorMode = EditorModes.AddBlocks;
			}
			else if (keyboardState.IsKeyDown(Keys.D2))
			{
				this.editorMode = EditorModes.AddFlags;
			}
			else if (keyboardState.IsKeyDown(Keys.D3))
			{
				this.editorMode = EditorModes.AddPlatforms;
			}
			else if (keyboardState.IsKeyDown(Keys.D4))
			{
				this.editorMode = EditorModes.AddImportanPoints;
			}
			else if (keyboardState.IsKeyDown(Keys.D5))
			{
				this.editorMode = EditorModes.AddBackground;
			}

			if (this.currentResourceSelection < 0)
			{
				this.currentResourceSelection = this.resources[(int)this.editorMode].Count - 1;
			}
			else if (this.currentResourceSelection >= this.resources[(int)this.editorMode].Count)
			{
				this.currentResourceSelection = 0;
			}

			this.mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y) / this.scaleValue;

			var blockPosition = new Vector2(
						(int)ConvertUnits.ToSimUnits(cameraPosition.X + mousePosition.X),
						(int)ConvertUnits.ToSimUnits(cameraPosition.Y + mousePosition.Y));

			this.mouseOverBlock = null;

			switch (this.editorMode)
			{
				case EditorModes.AddBlocks:
					AddBlockMode(ref mouseState, blockPosition);
					break;
				case EditorModes.AddFlags:
					AddFlagMode(ref mouseState, ConvertUnits.ToSimUnits(cameraPosition + this.mousePosition));
					break;
				case EditorModes.AddPlatforms:
					AddPlatform(ref mouseState, blockPosition);//ConvertUnits.ToSimUnits(cameraPosition + this.mousePosition));
					break;
				case EditorModes.AddImportanPoints:
					AddImportantPoints(ref mouseState, ConvertUnits.ToSimUnits(cameraPosition + this.mousePosition));
					break;
				case EditorModes.AddBackground:
					ChangeBackgroundMode(ref mouseState);
					break;
				default:
					throw new ArgumentException("WTF HOW ????");
			}

			if (keyboardState.IsKeyDown(Keys.F10))
			{
				MapFactory.MapSave(this.Map, this.textureManager);
			}
		}

		private void AddBlockMode(ref MouseState mouseState, Vector2 blockPosition)
		{
			var blockNodes = this.resources[(int)this.editorMode];

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				if (!this.pressedMLB)
				{
					this.startPosition = blockPosition;
					this.tempBlock = new MapBlock(startPosition, new Point(1, 1), blockNodes[this.currentResourceSelection]);
					this.pressedMLB = true;
				}
				else
				{
					float X = this.startPosition.X > blockPosition.X ? blockPosition.X : this.startPosition.X;
					float Y = this.startPosition.Y > blockPosition.Y ? blockPosition.Y : this.startPosition.Y;

					this.tempBlock.Position = new Vector2(X, Y);

					this.tempBlock.Size = new Point((int)Math.Abs(this.startPosition.X - blockPosition.X) + 1,
						(int)Math.Abs(this.startPosition.Y - blockPosition.Y) + 1);

					this.tempBlock.TextureNode = blockNodes[this.currentResourceSelection];
				}
			}

			if (mouseState.LeftButton != ButtonState.Pressed && pressedMLB)
			{
				var isEmpty = this.Map.Blocks.Find(p =>
				{
					return p.Position.X < this.tempBlock.Position.X + this.tempBlock.Size.X
						&& p.Position.X + p.Size.X > this.tempBlock.Position.X
						&& p.Position.Y < this.tempBlock.Position.Y + this.tempBlock.Size.Y
						&& p.Position.Y + p.Size.Y > this.tempBlock.Position.Y;
				});

				if (isEmpty == null)
				{
					this.tempBlock.AddToWorld(this.PhysicsWorld);
					this.Map.AddBlock(this.tempBlock);
				}

				this.tempBlock = null;
				this.pressedMLB = false;
			}

			var cursorPosition = ConvertUnits.ToSimUnits(this.cameraPosition + this.mousePosition);

			this.mouseOverBlock = this.Map.Blocks.Find(p =>
			{
				return p.Position.X < cursorPosition.X
					&& p.Position.X + p.Size.X > cursorPosition.X
					&& p.Position.Y < cursorPosition.Y
					&& p.Position.Y + p.Size.Y > cursorPosition.Y;
			});

			if (mouseState.RightButton == ButtonState.Pressed && !pressedMRB)
			{
				if (this.mouseOverBlock != null)
				{
					this.PhysicsWorld.RemoveBody(this.mouseOverBlock.CollisionHull);
					this.Map.RemoveBlock((MapBlock)this.mouseOverBlock);
				}
			}
			this.pressedMRB = mouseState.RightButton == ButtonState.Pressed;
		}

		private void AddFlagMode(ref MouseState mouseState, Vector2 cursorPosition)
		{
			this.mouseOverBlock = this.Map.Flags.Find(p =>
			{
				return p.Position.X < cursorPosition.X
					&& p.Position.X + p.Size.X > cursorPosition.X
					&& p.Position.Y < cursorPosition.Y
					&& p.Position.Y + p.Size.Y > cursorPosition.Y;
			});

			var flagNodes = this.resources[(int)this.editorMode];

			if (mouseState.LeftButton == ButtonState.Pressed && !pressedMLB)
			{
				var textureNode = flagNodes[this.currentResourceSelection];
				var flag = new MapFlagBlock(cursorPosition, new Point(1, 1), textureNode);

				flag.AddToWorld(this.PhysicsWorld);
				this.Map.AddMapFlag(flag);
			}
			this.pressedMLB = (mouseState.LeftButton == ButtonState.Pressed);

			if (this.mouseOverBlock != null && mouseState.RightButton == ButtonState.Pressed)
			{
				this.PhysicsWorld.RemoveBody(this.mouseOverBlock.CollisionHull);
				this.Map.RemoveFlag((MapFlagBlock)this.mouseOverBlock);
				this.mouseOverBlock = null;
			}
		}

		private void AddPlatform(ref MouseState mouseState, Vector2 platformPosition)
		{
			var platformNodes = this.resources[(int)this.editorMode];

			if (!this.pressedMLB)
			{
				if (mouseState.LeftButton == ButtonState.Pressed)
				{
					this.startPosition = platformPosition;
				}
			}
			else
			{
				this.endPosition = platformPosition;

				if (mouseState.LeftButton == ButtonState.Released)
				{
					var textureNode = platformNodes[this.currentResourceSelection];
					var platform = new MapPlatform(this.startPosition, this.endPosition, textureNode);

					platform.AddToWorld(this.PhysicsWorld);
					this.Map.AddPlatform(platform);
				}
			}

			this.pressedMLB = (mouseState.LeftButton == ButtonState.Pressed);
		}

		private void AddImportantPoints(ref MouseState mouseState, Vector2 cursorPosition)
		{
			var importantPointsNodes = this.resources[(int)this.editorMode];
			var textureNode = importantPointsNodes[this.currentResourceSelection];

			switch (this.currentResourceSelection)
			{
				case 0:
					var endOfLevel = this.Map.EndOfLevel;
					if (endOfLevel != null)
					{
						var pos = ConvertUnits.ToSimUnits(endOfLevel.TextureNode.SourceRectangle.Width, endOfLevel.TextureNode.SourceRectangle.Height);
						if (endOfLevel.Position.X < cursorPosition.X
							&& endOfLevel.Position.X + pos.X > cursorPosition.X
							&& endOfLevel.Position.Y < cursorPosition.Y
							&& endOfLevel.Position.Y + pos.Y > cursorPosition.Y)
						{
							this.mouseOverBlock = endOfLevel;
						}
						else
						{
							this.mouseOverBlock = null;
						}
					}

					if (mouseState.LeftButton == ButtonState.Pressed && !pressedMLB)
					{
						var eol = new MapEndOfLevel(cursorPosition, textureNode);

						eol.AddToWorld(this.PhysicsWorld);
						this.Map.EndOfLevel = eol;
					}
					this.pressedMLB = (mouseState.LeftButton == ButtonState.Pressed);

					if (this.mouseOverBlock != null && mouseState.RightButton == ButtonState.Pressed)
					{
						this.PhysicsWorld.RemoveBody(this.mouseOverBlock.CollisionHull);
						this.Map.EndOfLevel = null;
						this.mouseOverBlock = null;
					}
					break;
				case 1:
					this.mouseOverBlock = this.Map.CheckPoints.Find(p =>
					{
						var position = ConvertUnits.ToSimUnits(p.TextureNode.SourceRectangle.Width, p.TextureNode.SourceRectangle.Height);
						return p.Position.X < cursorPosition.X
							&& p.Position.X + position.X > cursorPosition.X
							&& p.Position.Y < cursorPosition.Y
							&& p.Position.Y + position.Y > cursorPosition.Y;
					});


					if (mouseState.LeftButton == ButtonState.Pressed && !pressedMLB)
					{
						var checkPoint = new MapCheckPoint(cursorPosition, textureNode);

						checkPoint.AddToWorld(this.PhysicsWorld);
						this.Map.AddCheckPoint(checkPoint);
					}
					this.pressedMLB = (mouseState.LeftButton == ButtonState.Pressed);

					if (this.mouseOverBlock != null && mouseState.RightButton == ButtonState.Pressed)
					{
						this.PhysicsWorld.RemoveBody(this.mouseOverBlock.CollisionHull);
						this.Map.RemoveCheckPoint((MapCheckPoint)this.mouseOverBlock);
						this.mouseOverBlock = null;
					}
					break;
				case 2:
					this.mouseOverBlock = this.Map.SpawnPoints.Find(p =>
					{
						var position = ConvertUnits.ToSimUnits(p.TextureNode.SourceRectangle.Width, p.TextureNode.SourceRectangle.Height);
						return p.Position.X < cursorPosition.X
							&& p.Position.X + position.X > cursorPosition.X
							&& p.Position.Y < cursorPosition.Y
							&& p.Position.Y + position.Y > cursorPosition.Y;
					});


					if (mouseState.LeftButton == ButtonState.Pressed && !pressedMLB)
					{
						var spawnPoint = new MapSpawnPoint(cursorPosition, textureNode);

						spawnPoint.AddToWorld(this.PhysicsWorld);
						this.Map.AddSpawnPoint(spawnPoint);
					}
					this.pressedMLB = (mouseState.LeftButton == ButtonState.Pressed);

					if (this.mouseOverBlock != null && mouseState.RightButton == ButtonState.Pressed)
					{
						this.PhysicsWorld.RemoveBody(this.mouseOverBlock.CollisionHull);
						this.Map.RemoveSpawnPoint((MapSpawnPoint)this.mouseOverBlock);
						this.mouseOverBlock = null;
					}
					break;
			}
		}

		private void ChangeBackgroundMode(ref MouseState mouseState)
		{
			var backgroundNodes = this.resources[(int)this.editorMode];
			if (mouseState.LeftButton == ButtonState.Pressed)// && !this.pressedMLB)
			{
				this.Map.Background = backgroundNodes[this.currentResourceSelection].Texture;
			}
			//this.pressedMLB = (mouseState.LeftButton == ButtonState.Pressed);

			if (mouseState.RightButton == ButtonState.Pressed)
			{
				this.Map.Background = null;
			}
		}

		public void Draw()
		{
			if (this.Map.Background != null)
			{
				this.SpriteBatch.Begin();

				// TODO: move the background with the camera
				// var mapSize = this.Map.GetMapSize();
				this.SpriteBatch.Draw(this.Map.Background, Vector2.Zero, Color.White);

				this.SpriteBatch.End();
			}


			if (this.Map.Flags.Count > 0 || this.Map.SpawnPoints.Count > 0)
			{
				this.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, this.scaleMatrix);

				for (int i = 0; i < this.Map.Flags.Count; ++i)
				{
					if (this.Map.Flags[i] != this.mouseOverBlock)
					{
						DrawManager.Draw(this.SpriteBatch, this.Map.Flags[i], this.cameraPosition);
					}
				}

				for (int i = 0; i < this.Map.SpawnPoints.Count; ++i)
				{
					if (this.Map.SpawnPoints[i] != this.mouseOverBlock)
					{
						DrawManager.Draw(this.SpriteBatch, this.Map.SpawnPoints[i], this.cameraPosition);
					}
				}

				this.SpriteBatch.End();
			}

			// draw map and mouse
			this.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, this.scaleMatrix);

			if (this.mouseOverBlock != null)
			{
				DrawManager.Draw(this.SpriteBatch, this.mouseOverBlock, this.cameraPosition, Color.GreenYellow, 0.9f);
			}

			var mouseCursor = this.mouseCursors[(int)this.editorMode];
			this.SpriteBatch.Draw(mouseCursor.Texture, this.mousePosition, mouseCursor.SourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.9f);

			float scale = 1.0f;
			var resource = this.resources[(int)this.editorMode][this.currentResourceSelection];

			switch (this.editorMode)
			{
				case EditorModes.AddImportanPoints:
					break;
				case EditorModes.AddBackground:
					scale = 0.2f;
					break;
			}
			this.SpriteBatch.Draw(resource.Texture, this.mousePosition, resource.SourceRectangle, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.8f);

			if (this.tempBlock != null)
			{
				DrawManager.Draw(this.SpriteBatch, this.tempBlock, this.cameraPosition, Color.White);
			}

			var objects = this.PhysicsWorld.BodyList;

			for (int i = 0; i < objects.Count; ++i)
			{
				if (objects[i].UserData != this.mouseOverBlock)
				{
					DrawManager.Draw(this.SpriteBatch, objects[i], this.cameraPosition);
				}
			}

			this.SpriteBatch.End();
		}
	}
}
