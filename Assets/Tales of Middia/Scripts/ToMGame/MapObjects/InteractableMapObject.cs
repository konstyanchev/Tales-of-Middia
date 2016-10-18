using UnityEngine;
using System.Collections;
using System;

namespace HoMM
{
	public class InteractableMapObject : MapObject
	{
		public TilePositon interactablePosition;

		protected Tile interactableTile;

		protected virtual void Awake()
		{
		}

		public override void Init(Tile origin)
		{
			base.Init(origin);
			int x = origin.position.x + this.interactablePosition.x;
			int y = origin.position.y - this.interactablePosition.y;
			this.interactableTile = GameManager.Instance.currentMap.tiles[x, y];
			this.interactableTile.interaction += this.OnVisit;
		}

		public virtual void OnVisit(Hero visitor)
		{

		}
	}
}