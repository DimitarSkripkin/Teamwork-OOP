using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using Microsoft.Xna.Framework;

namespace Teamwork_OOP.Engine.Factories
{
	using Map;
	using Drawing;
	//using Items;

	public static class MapFactory
	{
		private static readonly string[] Keywords = new string[] { "[RESOURCES]", "[BLOCKS]", "[MAP_FLAGS]", "[PLATFORMS]", "[IMPORTANT_POINTS]", "[MONSTERS]" };
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
							case"[IMPORTANT_POINTS]":
								input = streamReader.ReadLine();
								input = LoadImportantPoints(textureManager, map, streamReader, input);
								break;
							case "[MAP_FLAGS]":
								input = streamReader.ReadLine();
								input = LoadMapFlags(textureManager, map, streamReader, input);
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

		private static string LoadImportantPoints(TextureManager textureManager, MapManager map, StreamReader streamReader, string input)
		{
			while (!String.IsNullOrEmpty(input) && !Keywords.Contains(input))
			{
				if (input.StartsWith(CommentSymbol))
				{
					input = streamReader.ReadLine();
					continue;
				}

				var tempLineArray = input.Split(ReadSplitSeparators, StringSplitOptions.RemoveEmptyEntries);

				var blockPosition = new Vector2(float.Parse(tempLineArray[2]), float.Parse(tempLineArray[3]));
				TextureNode textureNode;

				switch (tempLineArray[0])
				{
					case "CP":
						if (textureManager.GetTextureNode(out textureNode, tempLineArray[1]))
						{
							var mapCheckPoint = new MapCheckPoint(blockPosition, textureNode);
							map.AddCheckPoint(mapCheckPoint);
						}
						break;
					case "SP":
						if (textureManager.GetTextureNode(out textureNode, tempLineArray[1]))
						{
							var mapSpawnPoint = new MapSpawnPoint(blockPosition, textureNode);
							map.AddSpawnPoint(mapSpawnPoint);
						}
						break;
					case "EOL":
						if (textureManager.GetTextureNode(out textureNode, tempLineArray[1]))
						{
							var mapEndOfLevel = new MapEndOfLevel(blockPosition, textureNode);
							map.EndOfLevel = mapEndOfLevel;
						}
						break;
				}


				input = streamReader.ReadLine();
			}
			return input;
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

				var platformStartPosition = new Vector2(float.Parse(tempLineArray[1]), float.Parse(tempLineArray[2]));
				var platformEndPosition = new Vector2(float.Parse(tempLineArray[3]), float.Parse(tempLineArray[4]));

				TextureNode textureNode;
				if (textureManager.GetTextureNode(out textureNode, tempLineArray[0]))
				{
					var mapPlatform = new MapPlatform(platformStartPosition, platformEndPosition, textureNode);
					map.AddPlatform(mapPlatform);
				}

				input = streamReader.ReadLine();
			}
			return input;
		}

		private static string LoadMapFlags(TextureManager textureManager, MapManager map, StreamReader streamReader, string input)
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
					var mapTriggerBlock = new MapFlagBlock(blockPosition, blockSize, textureNode);
					map.AddMapFlag(mapTriggerBlock);
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
				else if (tempLineArray.Length < 2)
				{
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
					WriteResource(sw, item, textureManager);
				}

				var resourcePlatformList = map.Platforms.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourcePlatformList)
				{
					WriteResource(sw, item, textureManager);
				}

				// remove ????
				var resourceFlagsList = map.Flags.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourceFlagsList)
				{
					WriteResource(sw, item, textureManager);
				}

				var resourceSpawnPointsList = map.SpawnPoints.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourceSpawnPointsList)
				{
					WriteResource(sw, item, textureManager);
				}

				var resourceCheckPointsList = map.CheckPoints.Select(s => s.TextureNode).Distinct();
				foreach (var item in resourceCheckPointsList)
				{
					WriteResource(sw, item, textureManager);
				}

				if (map.EndOfLevel != null)
				{
					WriteResource(sw, map.EndOfLevel.TextureNode, textureManager);
				}

				sw.WriteLine("[BLOCKS]");
				foreach (var block in map.Blocks)
				{
					sw.WriteLine(string.Join(WriteSeparator, block.TextureNode.Name, block.Position.X, block.Position.Y, block.Size.X, block.Size.Y));
				}

				sw.WriteLine("[IMPORTANT_POINTS]");
				foreach (var checkPoint in map.CheckPoints)
				{
					sw.WriteLine(string.Join(WriteSeparator, "CP", checkPoint.TextureNode.Name, checkPoint.Position.X, checkPoint.Position.Y));
				}
				foreach (var spawnPoint in map.SpawnPoints)
				{
					sw.WriteLine(string.Join(WriteSeparator, "SP", spawnPoint.TextureNode.Name, spawnPoint.Position.X, spawnPoint.Position.Y));
				}

				if (map.EndOfLevel != null)
				{
					sw.WriteLine(string.Join(WriteSeparator, "EOL", map.EndOfLevel.TextureNode.Name, map.EndOfLevel.Position.X, map.EndOfLevel.Position.Y));
				}

				sw.WriteLine("[MAP_FLAGS]");
				foreach (var trigger in map.Flags)
				{
					//TRIGGER TYPE NOT TRIGGER NAME
					sw.WriteLine(string.Join(WriteSeparator, trigger.TextureNode.Name, trigger.Position.X, trigger.Position.Y, trigger.Size.X, trigger.Size.Y));
				}

				sw.WriteLine("[PLATFORMS]");
				foreach (var platform in map.Platforms)
				{
					sw.WriteLine(string.Join(WriteSeparator, platform.TextureNode.Name, platform.Position.X, platform.Position.Y, platform.EndPoint.X, platform.EndPoint.Y));
				}

				// add for monsters
			}
		}

		private static void WriteResource(StreamWriter sw, TextureNode textureNode, TextureManager textureManager)
		{
			var sourceRectangle = textureNode.SourceRectangle;
			sw.WriteLine(string.Join(WriteSeparator, textureManager.GetTextureKey(textureNode.Texture), textureNode.Name, sourceRectangle.X, sourceRectangle.Y, sourceRectangle.Width, sourceRectangle.Height));
		}
	}
}
