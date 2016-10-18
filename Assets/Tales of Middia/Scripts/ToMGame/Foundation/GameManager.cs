using System.Collections.Generic;
using UnityEngine;

namespace HoMM
{
	public class GameManager : MonoBehaviour
	{

		public GameObject playerPrefab;
		public GameObject castleScreen;
		public GameObject battleScreen;


		private Camera playerCam;
		public CastleUI castleUI;

		protected static GameManager instance;

		public static GameManager Instance
		{
			get
			{
				if (instance == null)
				{
					GameObject go = new GameObject("_GameManager");
					DontDestroyOnLoad(go);
					GameManager.instance = go.AddComponent<GameManager>();
				}
				return instance;
			}
		}


		public List<Player> players;

		public Map currentMap;

		void Awake()
		{
			if (GameManager.instance == null)
			{
				GameManager.instance = this;
				Object.DontDestroyOnLoad(GameManager.instance);
			}
			else
			{
				Object.Destroy(this.gameObject);
				return;
			}

			currentMap = new Map();
			//this.castleUI = FindObjectOfType<CastleUI>();
		}

		public void CreatePlayers(int numberOfPlayers)
		{
			var playersContainer = new GameObject("Players");
			for (int i = 0; i < numberOfPlayers; i++)
			{
				var player = Instantiate<GameObject>(this.playerPrefab);
				player.name = (Team)i + "_Player";
				player.transform.parent = playersContainer.transform;
				player.GetComponent<Player>().Init(currentMap.mapHeroes, (Team)i);
				this.players.Add(player.GetComponent<Player>());
				this.playerCam = player.GetComponent<Camera>();
			}
		}

		public void ShowCastleScreen(Castle castle)
		{
			this.playerCam.enabled = false;
			this.castleScreen.SetActive(true);
			this.castleUI.Init(castle);
		}

		public void ShowBattleScreen()
		{
			this.battleScreen.SetActive(true);
		}

		public void ShowMapScreen()
		{
			this.playerCam.enabled = true;
			this.castleScreen.SetActive(false);
			this.battleScreen.SetActive(false);
		}

	}
}
