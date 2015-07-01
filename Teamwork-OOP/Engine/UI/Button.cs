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
			this.ButtonTexture = texture;
			this.ButtonPosition = position;
			this.ButtonSize = size;
			this.ButtonName = name;
		}

		public TextureNode ButtonTexture
		{
			get;
			set;
		}

		public Vector2 ButtonPosition
		{
			get;
			set;
		}

		public Vector2 ButtonSize
		{
			get;
			set;
		}

		public string ButtonName
		{
			get;
			set;
		}


	}
}
