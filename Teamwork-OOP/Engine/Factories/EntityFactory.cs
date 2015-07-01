using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Factories
{
	using Drawing;
	using Characters.CharacterClasses;

	public static class EntityFactory
	{
		public static void LoadCharacterAnimations(Warrior character, TextureManager textureManager, string filePath)
		{
			//character
			var texture = textureManager.GetOrLoadTexture(filePath + "/warrior");

			var animationSprite = new AnimationSprite();
			animationSprite.Sprite = texture;

			AnimationFactory.LoadFromFile(ref animationSprite, 0.1f, "Content/" + filePath + "/IDLE.txt");
			character.Animations.Add(animationSprite.Clone());

			var animationSprite1 = new AnimationSprite();
			animationSprite1.Sprite = texture;
			AnimationFactory.LoadFromFile(ref animationSprite1, 0.1f, "Content/" + filePath + "/RUN.txt");
			character.Animations.Add(animationSprite1.Clone());

			var animationSprite2 = new AnimationSprite();
			animationSprite2.Sprite = texture;
			AnimationFactory.LoadFromFile(ref animationSprite2, 0.1f, "Content/" + filePath + "/JUMP.txt");
			character.Animations.Add(animationSprite2.Clone());
		}
	}
}
