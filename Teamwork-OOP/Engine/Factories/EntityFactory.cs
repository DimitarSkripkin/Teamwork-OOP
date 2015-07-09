using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Teamwork_OOP.Engine.BaseClasses;

namespace Teamwork_OOP.Engine.Factories
{
	using Drawing;
	using Characters.CharacterClasses;

	public static class EntityFactory
	{
		public static void LoadEntity(Entity entity, TextureManager textureManager, string filePath, string entityName)
		{
			var texture = textureManager.GetOrLoadTexture(filePath + "/" + entityName);

			GetAnimation(entity, filePath, texture, "IDLE");

			GetAnimation(entity, filePath, texture, "RUN");

			GetAnimation(entity, filePath, texture, "JUMP");

			GetAnimation(entity, filePath, texture, "ATTACK");

			GetAnimation(entity, filePath, texture, "DEATH");
		}

		private static void GetAnimation(Entity entity, string filePath, Texture2D texture, string animationName)
		{
			var animationSprite2 = new AnimationSprite();
			animationSprite2.Sprite = texture;
			try
			{
				AnimationFactory.LoadFromFile(ref animationSprite2, 0.1f, "Content/" + filePath + "/" + animationName + ".txt", false);
				entity.Animations[animationName] = animationSprite2.Clone();
			}
			catch (FileNotFoundException)
			{
			}
		}
	}
}
