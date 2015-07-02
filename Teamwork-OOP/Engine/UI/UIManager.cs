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
		private List<UIMenu> menus;

		public UIManager()
		{
			this.menus = new List<UIMenu>();
		}

		public void AddMenu(UIMenu menu)
		{
			this.Menus.Add(menu);
		}

		IList<UIMenu> Menus
		{
			get
			{
				return this.menus;
			}
		}
		public UIMenu ActiveMenu { get; set; }

		public void RegisterClickEvent(string itemName, OnClickEventHandler clickFunction)
		{
			for (int i = 0; i < this.menus.Count; ++i)
			{
				this.menus[i].RegisterClickEvent(itemName, clickFunction);
			}
		}
		// TODO: implement ui manager that uses physics engine to test if button is clicked

		public void ProcessInput(MouseState mouseState)
		{
			Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
			foreach (var item in ActiveMenu.Items)
			{
				if (item.IsVisible)
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
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var menu in this.Menus)
			{
				if (menu.IsVisible)
				{
					menu.Draw(spriteBatch);
				}
			}
		}
	}
}
