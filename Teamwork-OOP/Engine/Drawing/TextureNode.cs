using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Drawing
{
	public struct TextureNode
	{
		private static readonly TextureNode empty = new TextureNode();

		public static TextureNode Empty
		{
			get
			{
				return empty;
			}
		}

		public TextureNode(string name, Texture2D texture, Rectangle sourceRectangle)
			: this()
		{
			this.Name = name;
			this.Texture = texture;
			this.SourceRectangle = sourceRectangle;
		}

		public string Name { get; set; }
		public Texture2D Texture { get; set; }
		public Rectangle SourceRectangle { get; set; }
	}
}
