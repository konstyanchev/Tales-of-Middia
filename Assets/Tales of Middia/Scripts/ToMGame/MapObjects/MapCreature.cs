using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ToM
{
	//Temporal while an editor is coded
	[System.Serializable]
	public class ArmyTuple : Tuple<int, Creature>
	{
		public ArmyTuple(int first, Creature second) : base(first, second) {}
	}

	public class MapCreature : InteractableMapObject
	{

		public List<ArmyTuple> army;

		private CreatureDialog dialogUI;
		private GameManager gm;

		protected override void Awake()
		{
			base.Awake();
			this.blockedTiles = new TilePositon[] { new TilePositon(0, 0) };
			this.interactablePosition = new TilePositon(0, 0);
			this.dialogUI = FindObjectOfType<CreatureDialog>();
			this.gm = GameManager.Instance;
		}

		public override void Init(Tile origin)
		{
			base.Init(origin);
			this.interactableTile.traversable = false;
			this.interactableTile.interactable = true;
		}

		public override void OnVisit(Hero visitor)
		{
			dialogUI.Init(this, army);
		}

		public void HandleChoice(bool isPositive)
		{
			if(isPositive == true)
			{
				Debug.Log("You have defeated the army");
				Destroy(this.gameObject);
				this.interactableTile.interaction -= this.OnVisit;
				this.interactableTile.traversable = true;
				this.interactableTile.interactable = false;
			}
		}
	}
}
