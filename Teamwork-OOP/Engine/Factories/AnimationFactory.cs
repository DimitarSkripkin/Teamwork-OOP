using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Factories
{
	using Drawing;

	public static class AnimationFactory
	{
		public static void GenerateWithFixedFrameSize(ref AnimationSprite animation, Point startPosition, Point frameSize, Point textureSize, int framesCount, float animationLenght)
		{
			GenerateWithFixedFrameSize(ref animation, startPosition, frameSize, textureSize, animationLenght / framesCount, framesCount);
		}

		public static void GenerateWithFixedFrameSize(ref AnimationSprite animation, Point startPosition, Point frameSize, Point textureSize, float frameTime, int framesCount)
		{
			animation.FrameList.Clear();

			var currentPosition = startPosition;
			for (int i = 0; i < framesCount; ++i)
			{
				animation.FrameList.Add(new Frame(
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

		public static void LoadFromFile(ref AnimationSprite animation, float timePerFrame, string filePath)
		{
			try
			{
				using (StreamReader sr = new StreamReader(filePath))
				{
					animation.FrameList.Clear();

					string input = sr.ReadLine();

					while (!String.IsNullOrEmpty(input))
					{
						var inputArray = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						animation.FrameList.Add(new Frame(
							new Rectangle(int.Parse(inputArray[0]), int.Parse(inputArray[1]), int.Parse(inputArray[2]), int.Parse(inputArray[3])),
							timePerFrame,
							Vector2.Zero
							//new Vector2(int.Parse(inputArray[2]) / 2.0f, int.Parse(inputArray[3]) / 2.0f)
							)
						);
						input = sr.ReadLine();
					}
				}
			}
			catch (FileNotFoundException)
			{
			}
		}
	}
}
