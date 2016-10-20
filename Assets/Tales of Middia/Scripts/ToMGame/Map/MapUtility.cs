using UnityEngine;
using System.Collections.Generic;

namespace ToM
{
	public static class MapUtility
	{
		private static Map currentMap;
		private static int diameter;

		static MapUtility()
		{
			currentMap = GameManager.Instance.currentMap;
			diameter = currentMap.Diameter;
		}

		public static Tile TileFromWorldPosition(Vector3 position)
		{
			int x = Mathf.Abs(Mathf.RoundToInt(position.x));
			int y = Mathf.Abs(Mathf.RoundToInt(position.y));

			Mathf.Clamp(x, 0, MapUtility.diameter - 1);
			Mathf.Clamp(y, 0, MapUtility.diameter - 1);

			return MapUtility.currentMap.tiles[x, y];
		}

		public static Tile GetRandomTile()
		{
			var pos = Random.insideUnitCircle * (MapUtility.diameter - 1);
			int x = Mathf.Abs(Mathf.RoundToInt(pos.x));
			int y = Mathf.Abs(Mathf.RoundToInt(pos.y));
			return MapUtility.currentMap.tiles[x, y];
		}

		public static List<Tile> GetNeighbours(Tile t)
		{
			List<Tile> neighbours = new List<Tile>();
			int tx = (int)t.position.x;
			int ty = (int)t.position.y;

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
						continue;

					int checkX = tx + x;
					int checkY = ty + y;

					if (checkX >= 0 && checkX < MapUtility.diameter && 
						checkY >= 0 && checkY < MapUtility.diameter)
						neighbours.Add(MapUtility.currentMap.tiles[checkX, checkY]);
				}
			}
			return neighbours;
		}
	}
}
