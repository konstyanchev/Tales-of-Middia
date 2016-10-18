using UnityEngine;

namespace HoMM
{
	public class MapHero : MonoBehaviour
	{
		public Hero hero;
		public MapMovement movement;
		public Tile currentTile;

		protected void Awake()
		{
			this.hero = new Hero();
			this.movement = this.GetComponent<MapMovement>();
		}

		public void Init(Tile startPosition)
		{
			this.currentTile = startPosition;
			this.transform.position = new Vector3(this.currentTile.position.x, -this.currentTile.position.y, 5F);
		}
	}
}
