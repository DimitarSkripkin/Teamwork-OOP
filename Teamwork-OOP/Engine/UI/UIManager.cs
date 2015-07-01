using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Teamwork_OOP.Engine.Drawing;

namespace Teamwork_OOP.Engine.UI
{
	using Physics;

	public class UIManager
	{
		private List<UIItem> buttons;

		public UIManager()
		{
			this.buttons = new List<UIItem>();
		}

		public Texture2D MenuBackground
		{
			get;
			set;

		}

		public List<UIItem> Buttons
		{
			get
			{
				return this.buttons;

			}
		}

		public void AddButton(string buttonName, TextureNode texture, Vector2 position)
		{
			this.Buttons.Add(new Button(texture, position, new Vector2(texture.SourceRectangle.Width, texture.SourceRectangle.Height), buttonName));
		}


		// TODO: implement ui manager that uses physics engine to test if button is clicked

		public void ProcessInput(MouseState mouseState)
		{
			Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
			foreach (var item in buttons)
			{
				if (CollisionChecker.IsPointInsideAABB(mousePosition, item.CollisionBox))
				{
					item.IsMouseOver = true;
					if (mouseState.LeftButton == ButtonState.Pressed)
					{
						item.RaiseClickEvent();
					}
				}
				else
				{
					item.IsMouseOver = false;
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
			AddButton("New Game", button1, new Vector2(20, 20));

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
	}
}
