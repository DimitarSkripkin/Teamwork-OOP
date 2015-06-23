using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Drawing
{
	public class DrawManager
	{
		public void Init(SpriteBatch spriteBatch)
		{
			this.SpriteBatch = spriteBatch;
		}

		public SpriteBatch SpriteBatch { get; private set; }

		// TODO: raname variable animation ?
		public void Draw(AnimationSprite animation, Vector2 position, SpriteEffects spriteEffect = SpriteEffects.None, float scale = 1.0f, float rotation = 0.0f)
		{
			// check if animation is null and don't draw or let SpriteBactch to raise exeption ?
			Frame currentFrame = animation.CurrentFrame;// animation.GetCurrentFrame();
			this.SpriteBatch.Draw(
				animation.Sprite,
				position,
				currentFrame.TextureCoordinates,
				Color.White,
				rotation,
				currentFrame.DrawOffset,
				scale, // scale
				spriteEffect, // to flip the texture
				0.01f);
		}

		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle = null, SpriteEffects effect = SpriteEffects.None, float scale = 1.0f, float rotation = 0.0f)
		{
			// check if texture is null and don't draw or let SpriteBactch to raise exeption ?
			this.SpriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, Vector2.Zero, scale, effect, 0.0f);
		}
	}
}
