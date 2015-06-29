using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Teamwork_OOP.Engine
{
	using Physics;
	using Drawing;
	using Map;
	using BaseClasses;

	// TODO: RENAME WITH SOMETHING MORE PROPER !!!
	public class SceneManager
	{
		private const float DrawRadius = 1000.0f;

		//private TextureManager textureManager;
		//private DrawManager drawManager;
		private MapManager mapManager;
		private PhysicsEngine physicsEngine;

		// here or in MapManager class ????
		//private List<Items> items;
		//private List<Entity> entity;
		//public IList<Entity> Entities { get; };

		public Entity CameraAttachedTo { get; set; }

		public SceneManager()
		{
			// WILL BE REMOVED
			this.physicsEngine = new PhysicsEngine();

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
				//this.physicsEngine = this.MapManager.PhysicsEngine;
			}
		}
		public TextureManager TextureManager { get; set; }

		public void Init(TextureManager textureManager, SpriteBatch spriteBatch)
		{
			this.TextureManager = textureManager;
			this.SpriteBatch = spriteBatch;
		}

		/*
		 * TODO: NEEDS PROPER MAP LOADER
		 */
		public void LoadTestLevel()
		{
			Texture2D mapTexture = this.TextureManager.LoadTexture("TestTextures/mapTestBlocks");

			this.TextureManager.AddTextureNode("TestTextures/mapTestBlocks", "mapBlock1", new Rectangle(0, 0, 32, 32));

			TextureNode platformTextureNode = new TextureNode();
			platformTextureNode.Name = "mapBlock2";
			platformTextureNode.Texture = mapTexture;
			platformTextureNode.SourceRectangle = new Rectangle(32, 0, 32, 32);
			this.TextureManager.AddTextureNode(platformTextureNode);

			//MapPlatform platform = new MapPlatform(new Vector2(10), 4, platformTextureNode);
			//this.MapManager.AddPlatform(platform);

			//TextureNode block;
			//this.TextureManager.GetTexture(out block, "mapBlock1");

			//this.MapManager.AddBlock(new MapBlock(new Vector2(10, 150), block, CollisionObjectFlags.Dynamic, 0));
			//this.MapManager.AddBlock(new MapBlock(new Vector2(10, 500), block, CollisionObjectFlags.Dynamic, 0));
			//this.MapManager.AddBlock(new MapBlock(new Vector2(42, 500), block, CollisionObjectFlags.Dynamic, 0));
		}

		public void Update(float deltaTime)
		{
			this.physicsEngine.Update(deltaTime);
			// update entities animation sprites
		}

		public void Draw()
		{
			// BEGIN DRAW
			this.SpriteBatch.Begin(SpriteSortMode.BackToFront);

			// draw only things that are in certain radius

			Vector2 cameraPosition = Vector2.Zero;
			if (this.CameraAttachedTo != null)
			{
				cameraPosition = this.CameraAttachedTo.CollisionHull.Position;
			}

			foreach (var block in mapManager.Blocks)
			{
				DrawManager.Draw(this.SpriteBatch, block.TextureNode.Texture, block.Position - cameraPosition, block.TextureNode.SourceRectangle);
			}

			foreach (var platform in mapManager.Platforms)
			{
				//foreach (var block in platform.Blocks)
				//{
				//	this.DrawManager.Draw(block.TextureNode.Texture, block.Position - cameraPosition, block.TextureNode.SourceRectangle);
				//}
			}

			// END DRAW
			this.SpriteBatch.End();
		}
	}
}
