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
	using BaseClasses;
	using Characters.CharacterClasses;

	public static class DrawManager
	{
		public static void Draw(SpriteBatch spriteBatch, Body body, Vector2 positionOffset, SpriteEffects effect = SpriteEffects.None, float depthLayer = 0.0f)
		{
			if (body.UserData is MapPlatform)
			{
				Draw(spriteBatch, (MapPlatform)body.UserData, positionOffset, Color.White, depthLayer);
			}
			else if (body.UserData is MapFlagBlock)
			{
			}
			else if (body.UserData is MapBlock)
			{
				Draw(spriteBatch, (MapBlock)body.UserData, positionOffset, Color.White, depthLayer);
			}
			else if (body.UserData is MapSpawnPoint)
			{
			}
			else if (body.UserData is MapCheckPoint)
			{
				Draw(spriteBatch, (MapCheckPoint)body.UserData, positionOffset, Color.White, depthLayer);
			}
			else if (body.UserData is MapEndOfLevel)
			{
				Draw(spriteBatch, (MapEndOfLevel)body.UserData, positionOffset, Color.White, depthLayer);
			}
			else if (body.UserData is Entity)
			{
				Draw(spriteBatch, (Entity)body.UserData, positionOffset);
			}
		}

		private static void Draw(SpriteBatch spriteBatch, Entity entity, Vector2 positionOffset)
		{
			var offsetPosition = ConvertUnits.ToDisplayUnits(entity.CollisionHull.Position) - positionOffset;

			Draw(spriteBatch, (AnimationSprite)((Entity)entity.CollisionHull.UserData).CurrentAnimation, offsetPosition, entity.CollisionHull.Rotation);
		}

		public static void Draw(SpriteBatch spriteBatch, MapBlock block, Vector2 offset, Color color, float depthLayer = 0.0f)
		{
			Vector2 offsetPosition;
			if (block.CollisionHull != null)
			{
				offsetPosition = ConvertUnits.ToDisplayUnits(block.CollisionHull.Position) - offset - ConvertUnits.ToDisplayUnits(new Vector2(block.Size.X, block.Size.Y)) / 2.0f;
			}
			else
			{
				offsetPosition = ConvertUnits.ToDisplayUnits(block.Position) - offset;
			}
			var textureNode = block.TextureNode;
			var origin = Vector2.Zero;// new Vector2(textureNode.SourceRectangle.Size.X / 2.0f, textureNode.SourceRectangle.Size.Y / 2.0f);

			//offsetPosition -= ConvertUnits.ToDisplayUnits(new Vector2(block.Size.X, block.Size.Y)) / 2.0f;

			for (int y = 0; y < block.Size.Y; ++y)
			{
				for (int x = 0; x < block.Size.X; ++x)
				{
					var blockOffset = offsetPosition + ConvertUnits.ToDisplayUnits(new Vector2(x, y));
					spriteBatch.Draw(textureNode.Texture, blockOffset, textureNode.SourceRectangle, color, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
				}
			}
		}

		// TODO: raname variable animation ?
		public static void Draw(SpriteBatch spriteBatch, AnimationSprite animation, Vector2 position, float rotation = 0.0f)
		{
			// check if animation is null and don't draw or let SpriteBactch to raise exeption ?
			Frame currentFrame = animation.CurrentFrame;// animation.GetCurrentFrame();
			spriteBatch.Draw(
				animation.Sprite,
				position,
				currentFrame.SourceRectangle,
				Color.White,
				rotation,
				currentFrame.DrawOffset,
				1.0f, // scale
				animation.Effects, // to flip the texture
				0.01f);
		}

		public static void Draw(SpriteBatch spriteBatch, MapPlatform platform, Vector2 positionOffset, Color color, float depthLayer = 0.0f)
		{
			var textureNode = platform.TextureNode;
			var origin = new Vector2(textureNode.SourceRectangle.Size.X / 2.0f, textureNode.SourceRectangle.Size.Y / 2.0f);

			spriteBatch.Draw(textureNode.Texture,
				ConvertUnits.ToDisplayUnits(platform.CollisionHull.Position) - positionOffset,
				textureNode.SourceRectangle,
				color,
				0.0f,
				origin,
				1.0f,
				SpriteEffects.None,
				depthLayer);
		}

		public static void Draw(SpriteBatch spriteBatch, MapFlagBlock triggerBlock, Vector2 positionOffset)
		{
			var textureNode = triggerBlock.TextureNode;
			var origin = new Vector2(textureNode.SourceRectangle.Size.X / 2.0f, textureNode.SourceRectangle.Size.Y / 2.0f);

			spriteBatch.Draw(textureNode.Texture,
				ConvertUnits.ToDisplayUnits(triggerBlock.CollisionHull.Position) - positionOffset,
				textureNode.SourceRectangle,
				Color.White,
				0.0f,
				origin,
				1.0f,
				SpriteEffects.None,
				0.0f);
		}

		public static void Draw(SpriteBatch spriteBatch, MapItem mapItem, Vector2 positionOffset, Color color, float depthLayer)
		{
			if (mapItem is MapBlock)
			{
				Draw(spriteBatch, (MapBlock)mapItem, positionOffset, color, depthLayer);
				return;
			}
			var textureNode = mapItem.TextureNode;
			var origin = new Vector2(textureNode.SourceRectangle.Size.X / 2.0f, textureNode.SourceRectangle.Size.Y / 2.0f);

			spriteBatch.Draw(textureNode.Texture,
				ConvertUnits.ToDisplayUnits(mapItem.CollisionHull.Position) - positionOffset,
				textureNode.SourceRectangle,
				color,
				0.0f,
				origin,
				1.0f,
				SpriteEffects.None,
				depthLayer);
		}

		public static void Draw(SpriteBatch spriteBatch, MapSpawnPoint mapSpawnPoint, Vector2 positionOffset, float depthLayer = 0.0f)
		{
			var textureNode = mapSpawnPoint.TextureNode;
			var origin = new Vector2(textureNode.SourceRectangle.Size.X / 2.0f, textureNode.SourceRectangle.Size.Y / 2.0f);

			spriteBatch.Draw(textureNode.Texture,
				ConvertUnits.ToDisplayUnits(mapSpawnPoint.CollisionHull.Position) - positionOffset,
				textureNode.SourceRectangle,
				Color.White,
				0.0f,
				origin,
				1.0f,
				SpriteEffects.None,
				depthLayer);
		}
	}
}
