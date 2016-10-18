using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoMM
{
	public class Player : MonoBehaviour
	{
		public List<MapHero> heroes;

		public MapHero currentHero;
		public Team team;

		private PlayerCamera mapCamera;


		protected void Awake()
		{
			this.mapCamera = this.GetComponent<PlayerCamera>();
		}

		public void Init(List<MapHero> heroes, Team team)
		{
			this.heroes = heroes;
			this.currentHero = heroes[0];
			this.team = team;

			this.mapCamera.Init(this.currentHero.currentTile);
		}

	}
}
