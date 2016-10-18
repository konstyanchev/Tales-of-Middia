using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.Events;

namespace HoMM
{
	public enum MapSize
	{
		ExtraSmall	= 32,
		Small		= 48,
		Normal		= 72,
		Large		= 96,
		ExtraLarge	= 128,
		Gigantic	= 192,
	}

	public class MapCreator : MonoBehaviour
	{
		public UnityEvent OnMapCreated;

		public List<Tile> path;
		private Map currentMap;

		private GameObject[] tilePrefabs;
		private GameObject[] pawnPrefabs;
		private GameObject[] castlePrefabs;
		private GameObject[] sceneryPrefabs;
		private GameObject[] resourcesPrefabs;
		private GameObject[] creaturePrefabs;
		private GameObject[] buildingPrefabs;


		protected void Awake()
		{
			this.currentMap = GameManager.Instance.currentMap;

			this.StartCoroutine(this.LoadGameAssets());
			this.StartCoroutine(this.CreateMap());
		}

		private IEnumerator LoadGameAssets()
		{
			this.tilePrefabs = Resources.LoadAll<GameObject>("Tiles");
			this.pawnPrefabs = Resources.LoadAll<GameObject>("PlayerPawns");
			this.sceneryPrefabs = Resources.LoadAll<GameObject>("Scenery");
			this.resourcesPrefabs = Resources.LoadAll<GameObject>("MapResources");
			this.creaturePrefabs = Resources.LoadAll<GameObject>("Creatures");
			this.buildingPrefabs = Resources.LoadAll<GameObject>("Buildings");
			this.castlePrefabs = Resources.LoadAll<GameObject>("Castles");
			yield return null;
		}

		private IEnumerator CreateMap()
		{
			while (tilePrefabs == null)
				yield return null;
			GameObject mapContainer = new GameObject("Map");
			// Ground
			GameObject tileContainer = new GameObject("Tiles");
			tileContainer.transform.parent = mapContainer.transform;
			int mapSize = GameManager.Instance.currentMap.Diameter;
			for (int i = 0; i < mapSize; i++)
			{
				for (int j = 0; j < mapSize; j++)
				{
					var go = Instantiate<GameObject>(this.tilePrefabs[0]);
					go.transform.position = new Vector3(i, - j, 20F);
					go.transform.parent = tileContainer.transform;
					var so = new Tile(new Vector2(i,j), false,true, go.GetComponent<SpriteRenderer>());
					this.currentMap.tiles[i,j] = so;
				}
			}
			// Scenery
			GameObject sceneryContainer = new GameObject("Scenery");
			sceneryContainer.transform.parent = mapContainer.transform;

			for (int i = 0; i < 10; i++)
			{
				var scenery = Instantiate<GameObject>(this.sceneryPrefabs[0]);
				scenery.transform.parent = sceneryContainer.transform;
				var mo = scenery.GetComponent<MapObject>();
				mo.Init(this.currentMap.tiles[5, i]);
				
			}
			// Resources
			GameObject resourcesContainer = new GameObject("Resources");
			resourcesContainer.transform.parent = mapContainer.transform;
			for (int i = 0; i < 3; i++)
			{
				var resource = Instantiate<GameObject>(this.resourcesPrefabs[0]);
				resource.transform.parent = resourcesContainer.transform;
				var mr = resource.GetComponent<MapResource>();
				mr.Init(this.currentMap.tiles[3, i * 2]);
			}
			// Creatures
			GameObject creaturesContainer = new GameObject("Creatures");
			creaturesContainer.transform.parent = mapContainer.transform;
			for (int i = 0; i < 3; i++)
			{
				var creature = Instantiate<GameObject>(this.creaturePrefabs[0]);
				creature.transform.parent = creaturesContainer.transform;
				var mc = creature.GetComponent<MapCreature>();
				mc.Init(this.currentMap.tiles[11, i * 2]);
			}
			// Buildings
			GameObject buildingsContainer = new GameObject("Buildings");
			buildingsContainer.transform.parent = mapContainer.transform;
			var building = Instantiate<GameObject>(this.buildingPrefabs[0]);
			building.transform.parent = buildingsContainer.transform;
			var mb = building.GetComponent<MapBuilding>();
			mb.Init(this.currentMap.tiles[11,10]);
			// Castles
			GameObject castlesContainer = new GameObject("Castles");
			castlesContainer.transform.parent = mapContainer.transform;
			var castle = Instantiate<GameObject>(this.castlePrefabs[0]);
			castle.transform.parent = castlesContainer.transform;
			var cas = castle.GetComponent<MapCastle>();
			cas.Init(this.currentMap.tiles[4, 15]);
			// Player Pawns
			GameObject pawnContainer = new GameObject("Player Pawns");
			pawnContainer.transform.parent = mapContainer.transform;

			var player = Instantiate<GameObject>(this.pawnPrefabs[0]);
			player.transform.parent = pawnContainer.transform;
			var mh = player.GetComponent<MapHero>();
			mh.Init(this.currentMap.tiles[0,0]);
			this.currentMap.mapHeroes.Add(mh);
			this.currentMap.ShowTiles(mh.currentTile.position, 10);

			GameManager.Instance.CreatePlayers(1);

			this.OnMapCreated.Invoke();
		}

		private IEnumerator CreateMap(MapSaveData rawData)
		{
			yield return null;
		}
	}
}
