using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Teamwork_OOP.Engine.BaseClasses;
using Teamwork_OOP.Engine.Drawing;

namespace Teamwork_OOP.Engine.Factories
{
	class ProjectileFactory
	{
		public void LoadProjectile(Projectile projectile, TextureManager textureManager, string filePath, string projectileName)
		{
			var texture = textureManager.GetOrLoadTexture(filePath + "/" + projectileName);
			GetAnimation(projectile, filePath, texture, projectileName);
		}
		private static void GetAnimation(Projectile projectile, string filePath, Texture2D texture, string animationName)
		{
			var animationSprite2 = new AnimationSprite();
			animationSprite2.Sprite = texture;
			AnimationFactory.LoadFromFile(ref animationSprite2, 0.1f, "Content/" + filePath + "/" + animationName + ".txt", false);
			projectile.AnimationSprite = animationSprite2.Clone();

		}
	}
}
