using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CodeName.Engine
{
	public class AnimationSprite
	{
		private float animationTime, animationLenght;
		private Texture2D sprite;

		public void UpdateAnimation(float deltaTime)
		{
			animationTime += deltaTime;

			if (animationTime >= animationLenght)
			{
				animationTime = 0.0f;
			}
		}

		public int GetCurrentFrame()
		{
			return (int)(FramesCount * (animationTime / animationLenght));
		}

		public int CurrentFrame { get; set; }

		public int FramesCount { get; set; }

		public AnimationSprite(int allFrames, float animationLenght)
		//: this()
		{
			this.FramesCount = allFrames;
			this.animationLenght = animationLenght;
		}

		public void DrawSprite()
		{
		}
	}
}
