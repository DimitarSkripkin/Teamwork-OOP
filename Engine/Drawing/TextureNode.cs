using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Drawing
{
	public struct TextureNode
	{
		public static readonly TextureNode empty = new TextureNode();

		public static TextureNode Empty
		{
			get
			{
				return empty;
			}
		}

		public Texture2D Texture;
		public Rectangle SourceRectangle;
	}
}
