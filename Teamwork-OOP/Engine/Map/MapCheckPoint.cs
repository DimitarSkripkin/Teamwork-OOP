using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;

namespace Teamwork_OOP.Engine.Map
{
	using Drawing;
	using BaseClasses;

	public class MapCheckPoint : MapItem
	{
		public MapCheckPoint(Vector2 position, TextureNode textureNode)
			: base(position, textureNode)
		{
		}

		//public Entity Activated { get; set; }
	}
}
