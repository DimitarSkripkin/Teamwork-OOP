using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Teamwork_OOP.Engine.Drawing;

namespace Teamwork_OOP.Engine.UI
{
	public class Button : UIItem
	{
		//private string newGameButton;

		public Button(TextureNode texture, Vector2 position, Vector2 size, string name)
			: base(texture, position, size, name)
		{
			this.ButtonName = name;
		}

		public string ButtonName
		{
			get;
			set;
		}

	}
}
