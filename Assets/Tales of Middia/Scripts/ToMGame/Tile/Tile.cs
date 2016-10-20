using System;
using UnityEngine;

namespace ToM
{
	[Serializable]
	public struct TilePositon
	{
		public int x;
		public int y;

		public Vector2 ToVector2()
		{
			return new Vector2(x, -y);
		}

		public TilePositon(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public class Tile : IHeapItem<Tile>
	{
		public bool visible;
		public bool traversable;
		public bool interactable;
		public TilePositon position;
		public TerrainType terrain;
		public Action<Hero> interaction;

		private SpriteRenderer renderer;

		#region pathfinding
		public Tile parent;
		public int gCost;
		public int hCost;
		public int fCost { get { return this.gCost + this.hCost; } }

		public int HeapIndex
		{
			get	{ return heapIndex;	}
			set	{this.heapIndex = value; }
		}
		private int heapIndex;
		#endregion

		public Tile(Vector2 pos, bool visible, bool traversable, SpriteRenderer renderer)
		{
			this.Init(pos, visible, traversable, renderer);
		}

		public void Init(Vector2 pos, bool visible, bool traversable, SpriteRenderer renderer)
		{
			this.position.x = (int)pos.x;
			this.position.y = (int)pos.y;

			this.visible = visible;
			this.renderer = renderer;
			this.renderer.enabled = this.visible;
			this.traversable = traversable;
		}

		public void ToggleVisibility(bool toggle)
		{
			this.renderer.enabled = toggle;
			this.visible = toggle;
		}

		public void ToggleBlock(bool toggle)
		{
			this.renderer.color = toggle == true ? Color.black : Color.white;
			this.traversable = !toggle;
		}

		public void ToggleMarker(bool toggle)
		{
			this.renderer.color = toggle == true ? Color.blue: Color.white;
		}

		public void TogglePath(bool toggle)
		{
			this.renderer.color = toggle == true ? Color.yellow : Color.white;
		}

		public int CompareTo(Tile other)
		{
			int compare = fCost.CompareTo(other.fCost);
			if (compare == 0)
				compare = hCost.CompareTo(other.hCost);
			return -compare;
		}
	}
}
