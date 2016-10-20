using UnityEngine;
using System.Collections;

namespace ToM
{
	public class MapCastle : InteractableMapObject
	{
		public Castle castle;

		private GameManager gm;

		public override void Init(Tile origin)
		{
			base.Init(origin);
			this.gm = GameManager.Instance;
			this.interactableTile.traversable = true;
			this.interactableTile.interactable = true;
		}

		public override void OnVisit(Hero visitor)
		{
			gm.ShowCastleScreen(this.castle);
		}
	}
}