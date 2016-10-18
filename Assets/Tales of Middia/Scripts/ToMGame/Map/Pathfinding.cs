using UnityEngine;
using System.Collections.Generic;
using System;

namespace HoMM
{
	public class Pathfinding : Singleton<Pathfinding>
	{
		private Map currentMap;

		private Heap<Tile> openSet;
		private HashSet<Tile> closeSet;



		void Awake()
		{
			this.currentMap = GameManager.Instance.currentMap;

			this.openSet	= new Heap<Tile>(this.currentMap.Diameter * this.currentMap.Diameter);
			this.closeSet	= new HashSet<Tile>();
		}

		public bool FindPath(Vector3 startPos, Vector3 targetPos, out List<Tile> path)
		{
			Tile startTile	= MapUtility.TileFromWorldPosition(startPos);
			Tile finishTile = MapUtility.TileFromWorldPosition(targetPos);
			return this.FindPath_Internal(startTile, finishTile, out path);
		}

		public bool FindPath(Tile startTile, Tile finishTile, out List<Tile> path)
		{
			return this.FindPath_Internal(startTile, finishTile, out path);
		}

		private bool FindPath_Internal(Tile startTile, Tile finishTile, out List<Tile> path)
		{
			this.openSet.Clear();
			this.closeSet.Clear();

			this.openSet.Add(startTile);

			while (this.openSet.Count > 0)
			{
				Tile currentTile = this.openSet.RemoveFirst();
				this.closeSet.Add(currentTile);

				if (currentTile == finishTile)
				{
					RetracePath(startTile, finishTile, out path);
					return true;
				}

				foreach (Tile neighbour in MapUtility.GetNeighbours(currentTile))
				{
					if (neighbour.visible == false || neighbour.traversable == false && neighbour.interactable == false || closeSet.Contains(neighbour) == true)
						continue;

					int costToNeighbor = currentTile.gCost + GetDistance(currentTile, neighbour); // + movementPenalty
					if (costToNeighbor < neighbour.gCost || !openSet.Contains(neighbour))
					{
						neighbour.gCost = costToNeighbor;
						neighbour.hCost = GetDistance(neighbour, finishTile);
						neighbour.parent = currentTile;

						if (this.openSet.Contains(neighbour) == false)
							this.openSet.Add(neighbour);
						else
							this.openSet.UpdateItem(neighbour);
					}
				}
			}
			path = new List<Tile>();
			return false;
		}

		void RetracePath(Tile start, Tile finish, out List<Tile> path)
		{
			path = new List<Tile>();
			Tile currenttile = finish;

			while (currenttile != start)
			{
				path.Add(currenttile);
				currenttile = currenttile.parent;
			}
			path.Reverse();
		}

		int GetDistance(Tile a, Tile b)
		{
			int dstX = Mathf.Abs(a.position.x - b.position.x);
			int dstY = Mathf.Abs(a.position.y - b.position.y);

			if (dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);
			return 14 * dstX + 10 * (dstY - dstX);
		}

	}

}
