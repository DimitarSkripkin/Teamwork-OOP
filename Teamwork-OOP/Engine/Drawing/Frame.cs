using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Teamwork_OOP.Engine.Drawing
{
	public struct Frame
	{
		public Rectangle SourceRectangle { get; set; }
		public float TimePerFrame { get; set; }
		public Vector2 DrawOffset { get; set; }

		public Frame(Rectangle textureCoordinates, float timePerFrame, Vector2 drawOffset)
			: this()
		{
			this.SourceRectangle = textureCoordinates;
			this.TimePerFrame = timePerFrame;
			this.DrawOffset = drawOffset;
		}

		// remove ????
		//public override bool Equals(object obj)
		//{
		//	if (obj == null)
		//	{
		//		return false;
		//	}

		//	var otherFrame = (Frame)obj;
		//	return this.DrawOffset == otherFrame.DrawOffset
		//		&& this.SourceRectangle == otherFrame.SourceRectangle;
		//}

		//public static bool operator ==(Frame frameA, Frame frameB)
		//{
		//	return frameA.Equals(frameB);
		//}

		//public static bool operator !=(Frame frameA, Frame frameB)
		//{
		//	return !frameA.Equals(frameB);
		//}
	}
}
