﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Drawing
{
	public class TextureManager
	{
		private Dictionary<string, AnimationSprite> animations;
		private Dictionary<string, TextureNode> textureNodes;
		private Dictionary<string, Texture2D> textures;

		public TextureManager()
		{
			this.animations = new Dictionary<string, AnimationSprite>();
			this.textureNodes = new Dictionary<string, TextureNode>();
			this.textures = new Dictionary<string, Texture2D>();
		}

		public ContentManager ContentManager { get; set; }

		public IDictionary<string, TextureNode> TextureNodes
		{
			get
			{
				return this.textureNodes;
			}
		}

		public IDictionary<string, Texture2D> Textures
		{
			get
			{
				return this.textures;
			}
		}

		public void Init(ContentManager contentMngr)
		{
			this.ContentManager = contentMngr;
		}

		public void Dispose()
		{
			foreach (var texture in textures)
			{
				texture.Value.Dispose();
			}
		}

		public string GetTextureKey(Texture2D texture)
		{
			return this.textures.FirstOrDefault(t => t.Value == texture).Key;
		}

		public bool GetAnimation(out AnimationSprite animation, string name)
		{
			if (animations.ContainsKey(name))
			{
				animation = animations[name].Clone();
				return true;
			}

			animation = AnimationSprite.Empty;
			return false;
		}

		public bool GetTextureNode(out TextureNode textureNode, string name)
		{
			if (textureNodes.ContainsKey(name))
			{
				// struct returns copy ?
				textureNode = textureNodes[name];
				return true;
			}

			textureNode = TextureNode.Empty;
			return false;
		}

		public Texture2D GetOrLoadTexture(string textureName)
		{
			return LoadTexture(textureName);
		}

		public Texture2D LoadTexture(string textureName)
		{
			if (!this.textures.ContainsKey(textureName))
			{
				Texture2D toAdd = this.ContentManager.Load<Texture2D>(textureName);
				this.textures[textureName] = toAdd;
				return toAdd;
			}
			return this.textures[textureName];
		}

		public Texture2D LoadTexture(string textureName, bool replaseIfLoaded)
		{
			if (!this.textures.ContainsKey(textureName))
			{
				Texture2D toAdd = this.ContentManager.Load<Texture2D>(textureName);
				this.textures[textureName] = toAdd;
				return toAdd;
			}
			if (replaseIfLoaded)
			{
				this.textures[textureName].Dispose();
				Texture2D toAdd = this.ContentManager.Load<Texture2D>(textureName);
				this.textures[textureName] = toAdd;
				return toAdd;
			}
			return this.textures[textureName];
		}

		public void AddTextureNode(string textureName, string nodeName, Rectangle sourceRectangle)
		{
			TextureNode toAdd = new TextureNode();

			toAdd.Name = nodeName;
			toAdd.Texture = this.textures[textureName];
			toAdd.SourceRectangle = sourceRectangle;

			this.textureNodes[nodeName] = toAdd;
		}

		public void AddTextureNode(TextureNode textureNode)
		{
			this.textureNodes[textureNode.Name] = textureNode;
		}
	}
}
