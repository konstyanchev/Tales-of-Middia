using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ToM
{
	public class MapMovement : MonoBehaviour
	{
		private Pathfinding pathfinder;
		private MapHero mapHero;

		private Map map;
		private List<Tile> currentPath = new List<Tile>();
		private Tile currentDestination;
		private Coroutine movementCoroutine;
		private bool isMoving = false;

		private float movementSpeed = 0.1F; // TODO: global setting

		protected void Awake()
		{
			this.pathfinder = Pathfinding.Instance;
			this.mapHero = GetComponent<MapHero>();
			this.map = GameManager.Instance.currentMap;
		}


		protected void DrawPath()
		{
			for (int i = 0; i < this.currentPath.Count - 1; i++)
				this.currentPath[i].TogglePath(true);
			this.currentPath[this.currentPath.Count - 1].ToggleMarker(true);
		}

		protected void ClearPath()
		{
			if (this.currentPath.Count == 0)
				return;

			for (int i = 0; i < this.currentPath.Count - 1; i++)
				this.currentPath[i].TogglePath(false);
			this.currentPath[this.currentPath.Count - 1].ToggleMarker(false);
		}

		protected void RequestPath()
		{
			this.pathfinder.FindPath(this.mapHero.currentTile, this.currentDestination, out currentPath);
			if (currentPath.Count > 0)
				this.DrawPath();
		}

		public IEnumerator BeginMovement()
		{
			this.isMoving = true;
			int limit = this.currentDestination.traversable == true ? this.currentPath.Count: this.currentPath.Count - 1;
			int i = 0;
			while (i < limit)
			{
				this.mapHero.currentTile.TogglePath(false);
				this.mapHero.currentTile = this.currentPath[i];
				this.map.ShowTiles(this.currentPath[i].position, this.mapHero.hero.sight);
				Vector3 position = this.currentPath[i].position.ToVector2();
				position.z = 5F;
				this.transform.position = position;
				++i;
				yield return new WaitForSeconds(movementSpeed);
			}
			this.mapHero.currentTile.TogglePath(false);
			this.isMoving = false;
			if (this.currentDestination.interactable == true)
			{
				this.currentDestination.interaction.Invoke(this.mapHero.hero);
				this.currentDestination.ToggleMarker(false);
			}
			this.currentPath.Clear();
		}

		public void SetDestination(Tile destination)
		{
			if (this.isMoving == true)
			{
				this.StopCoroutine(this.movementCoroutine);
				this.isMoving = false;
				return;
			}
			if (this.currentDestination == destination && this.currentPath.Count > 0)
				this.movementCoroutine = this.StartCoroutine(this.BeginMovement());
			else if ((destination.traversable == true || destination.interactable == true) && destination.visible == true)
			{
				this.ClearPath();
				this.currentDestination = destination;
				this.RequestPath();
			}
		}

		protected void OnDrawGizmos()
		{
			Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.up * this.mapHero.hero.sight));
			Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down * this.mapHero.hero.sight));
			Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.left * this.mapHero.hero.sight));
			Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.right * this.mapHero.hero.sight));
		}

	}
}
