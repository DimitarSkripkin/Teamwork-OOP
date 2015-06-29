using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Drawing
{
	using Map;

	public static class DrawManager
	{
		public static void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle? sourceRectangle = null, SpriteEffects effect = SpriteEffects.None, float scale = 1.0f, float rotation = 0.0f)
		{
			// check if texture is null and don't draw or let SpriteBactch to raise exeption ?
			spriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, Vector2.Zero, scale, effect, 0.0f);
		}

		public static void Draw(SpriteBatch spriteBatch, Body body, Vector2 positionOffset, SpriteEffects effect = SpriteEffects.None, float depthLayer = 0.0f)
		{
			if (body.UserData is MapPlatform)
			{
				Draw(spriteBatch, (MapPlatform)body.UserData, positionOffset, Color.White, depthLayer);
			}
			else if (body.UserData is MapTriggerBlock)
			{
			}
			else if (body.UserData is MapBlock)
			{
				Draw(spriteBatch, (MapBlock)body.UserData, positionOffset, Color.White, depthLayer);
			}
			else if (body.UserData is AnimationSprite)
			{
				Draw(spriteBatch, (AnimationSprite)body.UserData, body.Position - positionOffset, effect, body.Rotation);
			}
			else if (body.UserData is TextureNode)
			{
				var texture = ((TextureNode)body.UserData).Texture;
				if (texture == null)
				{
					return;
				}

				Vector2 bodyPosition = ConvertUnits.ToDisplayUnits(body.Position);
				Vector2 bodyOffset = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);// ConvertUnits.ToDisplayUnits(platform.LocalCenter);
				float bodyRotation = body.Rotation;

				spriteBatch.Draw(texture, bodyPosition - positionOffset, null, Color.White, bodyRotation, bodyOffset, 1.0f, effect, depthLayer);
			}
		}

		public static void Draw(SpriteBatch spriteBatch, MapBlock block, Vector2 offset, Color color, float depthLayer = 0.0f)
		{
			Vector2 offsetPosition;
			if (block.CollisionHull != null)
			{
				offsetPosition = ConvertUnits.ToDisplayUnits(block.CollisionHull.Position) - offset;
			}
			else
			{
				offsetPosition = ConvertUnits.ToDisplayUnits(block.Position) - offset;
			}
			for (int y = 0; y < block.Size.Y; ++y)
			{
				for (int x = 0; x < block.Size.X; ++x)
				{
					var blockOffset = ConvertUnits.ToDisplayUnits(new Vector2(x, y));
					spriteBatch.Draw(block.TextureNode.Texture, offsetPosition + blockOffset, block.TextureNode.SourceRectangle, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
				}
			}
		}

		// TODO: raname variable animation ?
		public static void Draw(SpriteBatch spriteBatch, AnimationSprite animation, Vector2 position, SpriteEffects spriteEffect = SpriteEffects.None, float rotation = 0.0f)
		{
			// check if animation is null and don't draw or let SpriteBactch to raise exeption ?
			Frame currentFrame = animation.CurrentFrame;// animation.GetCurrentFrame();
			spriteBatch.Draw(
				animation.Sprite,
				position,
				currentFrame.TextureCoordinates,
				Color.White,
				rotation,
				currentFrame.DrawOffset,
				1.0f, // scale
				spriteEffect, // to flip the texture
				0.01f);
		}

		public static void Draw(SpriteBatch spriteBatch, MapPlatform platform, Vector2 positionOffset, Color color, float depthLayer = 0.0f)
		{
			var textureNode = platform.TextureNode;
			spriteBatch.Draw(textureNode.Texture,
				ConvertUnits.ToDisplayUnits(platform.CollisionHull.Position) - positionOffset,
				textureNode.SourceRectangle,
				color,
				0.0f,
				Vector2.Zero,
				1.0f,
				SpriteEffects.None,
				depthLayer);
		}

		public static void Draw(SpriteBatch spriteBatch, MapTriggerBlock triggerBlock, Vector2 positionOffset)
		{
			var textureNode = triggerBlock.TextureNode;
			spriteBatch.Draw(textureNode.Texture,
				ConvertUnits.ToDisplayUnits(triggerBlock.CollisionHull.Position) - positionOffset,
				textureNode.SourceRectangle,
				Color.White,
				0.0f,
				Vector2.Zero,
				1.0f,
				SpriteEffects.None,
				0.0f);
		}
	}
}
