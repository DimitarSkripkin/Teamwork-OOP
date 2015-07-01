using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.ES10;
using Teamwork_OOP.Engine.Drawing;

namespace Teamwork_OOP.Engine.UI
{
	using Physics;
	public delegate void OnClickEventHandler();
	public abstract class UIItem
	{
		private AABB collisionBox;

		public UIItem(TextureNode texture, Vector2 position, Vector2 size, string name)
		{
			this.collisionBox = new AABB(position, size, CollisionObjectFlags.Kinematic);
		}

		public AABB CollisionBox
		{
			get { return collisionBox; }
		}
		public Vector2 Position
		{
			get
			{
				return this.CollisionBox.Position;
			}
		}
		public TextureNode TextureNode { get; set; }

		public bool IsMouseOver { get; set; }

		public event OnClickEventHandler OnClick;

		public void RaiseClickEvent()
		{
			if (OnClick != null)
			{
				OnClick();
			}
		}
	}
}
