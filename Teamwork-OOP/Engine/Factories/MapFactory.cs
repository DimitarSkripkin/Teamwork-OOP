using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using Microsoft.Xna.Framework;

using Teamwork_OOP.Engine.Drawing;
using Teamwork_OOP.Engine.Items;

namespace Teamwork_OOP.Engine.Factories
{
	using Map;

	public static class MapFactory
	{
		private static readonly string[] Keywords = new string[] { "[RESOURCES]", "[BLOCKS]", "[TRIGGERS]", "[PLATFORMS]", "[MONSTERS]" };
		private static readonly char[] ReadSplitSeparators = new char[] { ',', ' ' };
		private const string WriteSeparator = ",";
		private const string CommentSymbol = ";";

		public static MapManager MapLoad(TextureManager textureManager, string filePath)
		{
			Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

			MapManager map = new MapManager();

			try
			{
				using (var streamReader = new StreamReader(filePath))
				{
					var input = streamReader.ReadLine();

					while (!String.IsNullOrEmpty(input))
					{
						if (!Keywords.Contains(input))
						{
							input = streamReader.ReadLine();
						}
						if (input.StartsWith(CommentSymbol))
						{
							continue;
						}

						switch (input)
						{
							case "[RESOURCES]":
								input = streamReader.ReadLine();
								input = LoadResources(textureManager, map, streamReader, input);
								break;
							case "[BLOCKS]":
								input = streamReader.ReadLine();
								input = LoadBlocks(textureManager, map, streamReader, input);
								break;
							case "[TRIGGERS]":
								input = streamReader.ReadLine();
								input = LoadTriggers(textureManager, map, streamReader, input);
								break;
							case "[PLATFORMS]":
								input = streamReader.ReadLine();
								input = LoadPlatforms(textureManager, map, streamReader, input);
								break;
							case "[MONSTERS]":
								input = streamReader.ReadLine();
								break;
							default:
								break;
						}
					}
				}
			}
			catch (FileNotFoundException)
			{
			}
			return map;
		}

		private static string LoadPlatforms(TextureManager textureManager, MapManager map, StreamReader streamReader, string input)
		{
			while (!String.IsNullOrEmpty(input) && !Keywords.Contains(input))
			{
				if (input.StartsWith(CommentSymbol))
				{
					input = streamReader.ReadLine();
					continue;
				}

				var tempLineArray = input.Split(ReadSplitSeparators, StringSplitOptions.RemoveEmptyEntries);

				var platformSize = new Point(int.Parse(tempLineArray[1]), int.Parse(tempLineArray[2]));
				var platformStartPosition = new Vector2(float.Parse(tempLineArray[3]), float.Parse(tempLineArray[4]));
				var platformEndPosition = new Vector2(float.Parse(tempLineArray[5]), float.Parse(tempLineArray[6]));

				TextureNode textureNode;
				if (textureManager.GetTextureNode(out textureNode, tempLineArray[0]))
				{
					var mapPlatform = new MapPlatform(platformStartPosition, platformEndPosition, platformSize, textureNode);
					map.AddPlatform(mapPlatform);
				}

				input = streamReader.ReadLine();
			}
			return input;
		}

		private static string LoadTriggers(TextureManager textureManager, MapManager map, StreamReader streamReader, string input)
		{
			while (!String.IsNullOrEmpty(input) && !Keywords.Contains(input))
			{
				if (input.StartsWith(CommentSymbol))
				{
					input = streamReader.ReadLine();
					continue;
				}

				var tempLineArray = input.Split(ReadSplitSeparators, StringSplitOptions.RemoveEmptyEntries);

				var blockPosition = new Vector2(float.Parse(tempLineArray[1]), float.Parse(tempLineArray[2]));
				var blockSize = new Point(int.Parse(tempLineArray[3]), int.Parse(tempLineArray[4]));

				TextureNode textureNode;
				if (textureManager.GetTextureNode(out textureNode, tempLineArray[0]))
				{
					var mapTriggerBlock = new MapTriggerBlock(blockPosition, blockSize, textureNode);
					map.AddTrigger(mapTriggerBlock);
				}

				input = streamReader.ReadLine();
			}
			return input;
		}

		private static string LoadBlocks(TextureManager textureManager, MapManager map, StreamReader streamReader, string input)
		{
			while (!String.IsNullOrEmpty(input) && !Keywords.Contains(input))
			{
				if (input.StartsWith(CommentSymbol))
				{
					input = streamReader.ReadLine();
					continue;
				}

				var tempLineArray = input.Split(ReadSplitSeparators, StringSplitOptions.RemoveEmptyEntries);

				var blockPosition = new Vector2(float.Parse(tempLineArray[1]), float.Parse(tempLineArray[2]));
				var blockSize = new Point(int.Parse(tempLineArray[3]), int.Parse(tempLineArray[4]));

				TextureNode textureNode;
				if (textureManager.GetTextureNode(out textureNode, tempLineArray[0]))
				{
					var mapBlock = new MapBlock(blockPosition, blockSize, textureNode);
					map.AddBlock(mapBlock);
				}

				input = streamReader.ReadLine();
			}
			return input;
		}

		private static string LoadResources(TextureManager textureManager, MapManager map, StreamReader streamReader, string input)
		{
			while (!String.IsNullOrEmpty(input) && !Keywords.Contains(input))
			{
				if (input.StartsWith(CommentSymbol))
				{
					input = streamReader.ReadLine();
					continue;
				}

				var tempLineArray = input.Split(ReadSplitSeparators, StringSplitOptions.RemoveEmptyEntries);

				if (tempLineArray[0] == "background" && tempLineArray.Length == 2)
				{
					map.Background = textureManager.GetOrLoadTexture(tempLineArray[1]);
				}
				else
				{
					var nodeName = tempLineArray[1];
					var texture = textureManager.GetOrLoadTexture(tempLineArray[0]);
					var sourceRectangle = new Rectangle(int.Parse(tempLineArray[2]), int.Parse(tempLineArray[3]), int.Parse(tempLineArray[4]), int.Parse(tempLineArray[5]));

					//var textureNode = new TextureNode(nodeName, texture, sourceRectangle);
					//textureManager.AddTextureNode(textureNode);

					textureManager.AddTextureNode(new TextureNode(nodeName, texture, sourceRectangle));
				}

				input = streamReader.ReadLine();
			}
			return input;
		}

		public static void MapSave(MapManager map, TextureManager textureManager, string filePath = "map.txt")
		{
			Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

			using (StreamWriter sw = new StreamWriter(filePath, false))
			{
				sw.WriteLine("[RESOURCES]");
				sw.WriteLine("background{0}{1}", WriteSeparator, textureManager.GetTextureKey(map.Background));

				var resourceBlockList = map.Blocks.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourceBlockList)
				{
					var sourceRectangle = item.SourceRectangle;
					sw.WriteLine(string.Join(WriteSeparator, textureManager.GetTextureKey(item.Texture), item.Name, sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height));
				}

				var resourcePlatformList = map.Platforms.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourcePlatformList)
				{
					var sourceRectangle = item.SourceRectangle;
					sw.WriteLine(string.Join(WriteSeparator, textureManager.GetTextureKey(item.Texture), item.Name, sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height));
				}

				// remove ????
				var resourceTriggerList = map.Triggers.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourceTriggerList)
				{
					var sourceRectangle = item.SourceRectangle;
					sw.WriteLine(string.Join(WriteSeparator, textureManager.GetTextureKey(item.Texture), item.Name, sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height));
				}

				sw.WriteLine("[BLOCKS]");
				foreach (var block in map.Blocks)
				{
					sw.WriteLine(string.Join(WriteSeparator, block.TextureNode.Name, block.Position.X, block.Position.Y, block.Size.X, block.Size.Y));
				}

				sw.WriteLine("[TRIGGERS]");
				foreach (var trigger in map.Triggers)
				{
					//TRIGGER TYPE NOT TRIGGER NAME
					sw.WriteLine(string.Join(WriteSeparator, trigger.TextureNode.Name, trigger.Position.X, trigger.Position.Y, trigger.Size.X, trigger.Size.Y));
				}

				sw.WriteLine("[PLATFORMS]");
				foreach (var platform in map.Platforms)
				{
					sw.WriteLine(string.Join(WriteSeparator, platform.TextureNode.Name, platform.Size.X, platform.Size.Y, platform.Position.X, platform.Position.Y, platform.EndPoint.X, platform.EndPoint.Y));
				}

				// add for monsters
			}
		}
	}
}
