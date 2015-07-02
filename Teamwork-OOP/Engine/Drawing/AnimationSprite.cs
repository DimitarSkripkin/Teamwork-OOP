using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

namespace Teamwork_OOP.Engine.Drawing
{
	public enum AnimationType
	{
		Idle,
		Run,
		Jump,
		Attack,
		Death,
		SpecialSkill
	}

	// NOT FINISHED
	public class AnimationSprite
	{
		private static readonly AnimationSprite empty = new AnimationSprite();//Point.Zero, Point.Zero, 0, 0, 0.0f);

		public static AnimationSprite Empty
		{
			get
			{
				return empty;
			}
		}

		private List<Frame> frameList;
		private float currentFrameTime;
		private int currentFrame;

		public AnimationSprite()
		{
			this.frameList = new List<Frame>();
			this.Effects = SpriteEffects.None;
		}

		public AnimationSprite(AnimationSprite other)
		{
			this.frameList = new List<Frame>(other.frameList);
			this.Sprite = other.Sprite;
		}

		public bool Ended { get; set; }

		public Texture2D Sprite { get; set; }

		public Frame CurrentFrame
		{
			get
			{
				return this.frameList[this.currentFrame];
			}
		}

		public IList<Frame> FrameList
		{
			get
			{
				return this.frameList;
			}
		}

		public SpriteEffects Effects { get; set; }

		public void UpdateAnimation(float deltaTime)
		{
			this.currentFrameTime += deltaTime;

			if (this.currentFrameTime > this.frameList[this.currentFrame].TimePerFrame)
			{
				this.currentFrameTime = 0.0f;
				++this.currentFrame;
			}

			if (this.currentFrame >= frameList.Count)
			{
				this.currentFrame = 0;

				this.Ended = true;
			}
		}

		public void Reset()
		{
			this.Effects = SpriteEffects.None;
			this.currentFrameTime = 0.0f;
			this.currentFrame = 0;

			this.Ended = false;
		}

		public AnimationSprite Clone()
		{
			return new AnimationSprite(this);
		}
	}
}
