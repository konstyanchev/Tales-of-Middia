using UnityEngine;

namespace HoMM
{
	public class MapResource : InteractableMapObject
	{
		public Resource resource;

		protected override void Awake()
		{
			base.Awake();
			this.width = 1;
			this.height = 1;
			this.blockedTiles = new TilePositon[] { new TilePositon(0, 0) };
			this.interactablePosition = new TilePositon(0, 0);
		}

		public override void Init(Tile origin)
		{
			base.Init(origin);
			this.interactableTile.traversable = false;
			this.interactableTile.interactable = true;
		}

		public override void OnVisit(Hero visitor)
		{
			Debug.Log("You have just collected " + this.resource.quantity + " " + this.resource.type.ToString());
			Destroy(this.gameObject);
			this.interactableTile.interaction -= this.OnVisit;
			this.interactableTile.traversable = true;
			this.interactableTile.interactable = false;
		}
	}
}
