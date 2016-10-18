using UnityEngine;
using System.Collections;

namespace HoMM
{
	public class PlayerCamera : MonoBehaviour
	{
		private Tile currentTile;
		private Camera mapCamera;
		private Player player;
		private float mapScrollSpeed = 2F; // TODO: part of gamesettings


		protected void Awake()
		{
			this.mapCamera = this.GetComponent<Camera>();
			this.player = this.GetComponent<Player>();
		}

		protected void Update()
		{
			if (this.currentTile != this.player.currentHero.currentTile)
			{
				this.currentTile = this.player.currentHero.currentTile;
				var pos = Vector3.MoveTowards(this.transform.position, this.player.currentHero.transform.position, this.mapScrollSpeed);
				pos.z = this.transform.position.z;
				this.transform.position = pos;
			}
		}


		public void Init(Tile startPosition)
		{
			this.currentTile = startPosition;
			this.transform.position = new Vector3(this.currentTile.position.x + 0.5F, -this.currentTile.position.y + 0.5F, 0F);
		}
	}
}
