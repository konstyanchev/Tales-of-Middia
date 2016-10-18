using UnityEngine;
using System.Collections;

namespace HoMM
{
	public class MapBuilding : InteractableMapObject
	{
		public Building building;

		public override void Init(Tile origin)
		{
			base.Init(origin);
			this.interactableTile.traversable = true;
			this.interactableTile.interactable = true;
		}

		public override void OnVisit(Hero visitor)
		{
			this.building.OnVisit(visitor);
		}
	}
}