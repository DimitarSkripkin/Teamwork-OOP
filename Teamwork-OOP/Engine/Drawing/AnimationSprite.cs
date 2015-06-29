using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Teamwork_OOP.Engine.Drawing
{
	// TODO: move to separate file !!!!
	public struct Frame
	{
		public Rectangle TextureCoordinates { get; set; }
		public float TimePerFrame { get; set; }
		public Vector2 DrawOffset { get; set; }

		public Frame(Rectangle textureCoordinates, float timePerFrame, Vector2 drawOffset)
			:this()
		{
			this.TextureCoordinates = textureCoordinates;
			this.TimePerFrame = timePerFrame;
			this.DrawOffset = drawOffset;
		}

		public override bool Equals(object obj)
		{
			var otherFrame = (Frame)obj;
			return this.DrawOffset == otherFrame.DrawOffset
				&& this.TextureCoordinates == otherFrame.TextureCoordinates;
		}

		public static bool operator ==(Frame frameA, Frame frameB)
		{
			return frameA.Equals(frameB);
		}

		public static bool operator !=(Frame frameA, Frame frameB)
		{
			return !frameA.Equals(frameB);
		}
	}

	public enum AnimationType
	{
		Run,
		Walk
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
		}

		public AnimationSprite(AnimationSprite other)
		{
			this.frameList = new List<Frame>(other.frameList);
		}

		public AnimationSprite Clone()
		{
			return new AnimationSprite(this);
		}

		public Texture2D Sprite { get; set; }

		public Frame CurrentFrame
		{
			get
			{
				return this.frameList[this.currentFrame];
			}
		}

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
			}
		}

		public void GenerateWithFixedFrameSize(Point startPosition, Point frameSize, Point textureSize, int framesCount, float animationLenght)
		{
			GenerateWithFixedFrameSize(startPosition, frameSize, textureSize, animationLenght / framesCount, framesCount);
		}

		public void GenerateWithFixedFrameSize(Point startPosition, Point frameSize, Point textureSize, float frameTime, int framesCount)
		{
			frameList.Clear();

			var currentPosition = startPosition;
			for (int i = 0; i < framesCount; ++i)
			{
				frameList.Add(new Frame(
					new Rectangle(currentPosition, frameSize),
					frameTime,
					Vector2.Zero
					)
				);

				currentPosition.X += frameSize.X;

				if (currentPosition.X >= textureSize.X)
				{
					currentPosition.X = 0;
					currentPosition.Y += frameSize.Y;
				}
			}
		}
	}
}
