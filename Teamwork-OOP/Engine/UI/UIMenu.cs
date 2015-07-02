using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FarseerPhysics.Dynamics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Teamwork_OOP.Engine.UI
{
	using Drawing;

	public class UIMenu
	{
		private List<UIItem> items;

		public UIMenu( bool isVisible = true)
		{
			this.items = new List<UIItem>();
			this.IsVisible = isVisible;
		}

		public bool IsVisible { get; set; }

		public Texture2D MenuBackground { get; set; }

		public List<UIItem> Items
		{
			get
			{
				return this.items;

			}
		}

		public void AddButton(string buttonName, TextureNode texture, Vector2 position)
		{
			this.Items.Add(new Button(texture, position, new Vector2(texture.SourceRectangle.Width, texture.SourceRectangle.Height), buttonName));
		}

		public void RegisterClickEvent(string itemName, OnClickEventHandler clickFunction)
		{
			for (int i = 0; i < this.Items.Count; ++i)
			{
				if (this.Items[i].Name == itemName)
				{
					this.Items[i].OnClick += clickFunction;
					break;
				}
			}
		}

		public void LoadMenu(string filePath, string backgroundPath, TextureManager texture)
		{
			texture.GetOrLoadTexture(filePath);
			//Start Button
			texture.AddTextureNode(filePath, "Button1", new Rectangle(0, 0, 286, 100));
			TextureNode button1;
			texture.GetTextureNode(out button1, "Button1");
			AddButton("NewGame", button1, new Vector2(20, 20));

			//Controls Button
			texture.AddTextureNode(filePath, "Button2", new Rectangle(0, 101, 286, 100));
			TextureNode button2;
			texture.GetTextureNode(out button2, "Button2");
			AddButton("Controls", button2, new Vector2(20, 140));

			//Credits Button
			texture.AddTextureNode(filePath, "Button3", new Rectangle(0, 202, 286, 100));
			TextureNode button3;
			texture.GetTextureNode(out button3, "Button3");
			AddButton("Credits", button3, new Vector2(20, 260));

			//Exit Button
			texture.AddTextureNode(filePath, "Button4", new Rectangle(0, 303, 286, 100));
			TextureNode button4;
			texture.GetTextureNode(out button4, "Button4");
			AddButton("Exit", button4, new Vector2(20, 380));

			//Menu Background
			this.MenuBackground = texture.GetOrLoadTexture(backgroundPath);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.MenuBackground != null)
			{
				spriteBatch.Begin();

				spriteBatch.Draw(this.MenuBackground, Vector2.Zero, Color.White);

				spriteBatch.End();
			}

			// BUTTONS
			spriteBatch.Begin(SpriteSortMode.BackToFront);

			foreach (var item in this.Items)
			{
				if (item.IsVisible)
				{
					if (item.IsMouseOver)
					{
						spriteBatch.Draw(item.TextureNode.Texture, item.Position, item.TextureNode.SourceRectangle, Color.White);
					}
					else
					{
						spriteBatch.Draw(item.TextureNode.Texture, item.Position, item.TextureNode.SourceRectangle, Color.LightGray);
					}
				}
			}

			// END DRAW
			spriteBatch.End();
		}
	}
}
