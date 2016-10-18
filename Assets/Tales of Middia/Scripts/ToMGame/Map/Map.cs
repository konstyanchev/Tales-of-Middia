using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoMM
{
	public class Map
	{
		public MapSize size = MapSize.Small;
		public int Diameter { get { return (int)this.size; } }

		public string mapName = "Test Map";
		public string description = "A map for testing purposes";

		[Range(1,8)]
		public int numberOfPlayers = 1;
		[Range(1, 8)]
		public int numberOfTeams = 1;

		//TODO:
		//Starting conditions(specific hero, race, team)
		//Victory condition(s)
		//Loss condition(s)
		//Timed events
		//Allowed/Forbidden items, heroes, creatures, castles, spells, secondary skills

		public Tile[,] tiles;
		public List<MapHero> mapHeroes = new List<MapHero>();

		public Map()
		{
			this.tiles = new Tile[this.Diameter, this.Diameter];
		}

		public void ShowTiles(TilePositon origin, int visionRadius)
		{
			int totalIterations		= 0;
			int currentIteration	= 0;
			int minX = Mathf.Max(origin.x - visionRadius, 0);
			//Handles right border
			int maxX = Mathf.Min(origin.x + visionRadius, this.Diameter - 1);
			//Handle left border
			if (origin.x - visionRadius < 0)
			{
				totalIterations = -(origin.x - visionRadius);
				currentIteration = totalIterations;
			}
			for (int i = minX; i <= maxX; i++, totalIterations++)
			{
				//Handles upper and lower border
				int minY = Mathf.Max(origin.y - currentIteration, 0);
				int maxY = Mathf.Min(origin.y + currentIteration + 1, this.Diameter);
				for (int j = minY; j < maxY; j++)
					this.tiles[i, j].ToggleVisibility(true);
				currentIteration = totalIterations < visionRadius ? currentIteration + 1 : currentIteration - 1;
			}
		}

	}
}