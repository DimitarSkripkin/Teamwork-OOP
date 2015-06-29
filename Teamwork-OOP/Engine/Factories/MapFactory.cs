using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Teamwork_OOP.Engine.Drawing;
using Teamwork_OOP.Engine.Items;
using Vector2 = OpenTK.Vector2;

namespace Teamwork_OOP.Engine.Factories
{
	using Map;
	using Physics;
	class MapFactory
	{
		public MapManager MapLoad(string filePath)
		{
			MapManager map = new MapManager();
			using (var streamReader = new StreamReader(filePath))
			{
				var input = streamReader.ReadLine();
				string[] keywords = new string[] { "[RESOURCES]", "[BLOCKS]", "[PLATFORMS]" };

				//todo: Load Textures
				//var textures = new TextureNode();

				while (!String.IsNullOrEmpty(input))
				{
					if (input.StartsWith(";"))
					{
						continue;
					}

					string[] tempLineArray = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
					var blockName = tempLineArray[0];
					float positionX = float.Parse(tempLineArray[1]);
					float positionY = float.Parse(tempLineArray[2]);
					int sizeX = int.Parse(tempLineArray[3]);
					int sizeY = int.Parse(tempLineArray[4]);
					int platformStartPoint; //??
					int platformEndPoint;// ???
					if (tempLineArray.Length < 5) //
					{
						var blockPosition = new Vector2(positionX, positionY);
						var blockSize = new Point(sizeX, sizeY);
						//var block = new MapBlock(blockPosition,blockSize, todo);
						//map.AddBlock(block);
					}
					if (tempLineArray.Length > 5)
					{
						platformStartPoint = int.Parse(tempLineArray[5]);
						platformEndPoint = int.Parse(tempLineArray[6]);

						var platformPosition = new Vector2(positionX, positionY);
						var platformSize = new Point(sizeX, sizeY);

						//var platform = new MapPlatform(platformPosition, platformSize , todo);
						//map.AddPlatform(platform);
					}

				}

			}
			return map;
		}
	}
}
