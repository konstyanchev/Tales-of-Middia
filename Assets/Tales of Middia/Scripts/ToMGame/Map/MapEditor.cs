using UnityEngine;
using UnityEngine.UI;

namespace ToM
{
	public class MapEditor : MonoBehaviour
	{
		public bool showGrid;
		public Sprite gridSprite;

		private GameObject grid;

		private Map map;
		private bool showing;



		protected void Awake()
		{
			this.map = GameManager.Instance.currentMap;
			this.CreateGrid();
			this.showing = this.showGrid;
			this.grid.SetActive(showGrid);
		}

		protected void Update()
		{
			if(this.showGrid != showing)
			{
				this.grid.SetActive(showGrid);
				this.showing = this.showGrid;
			}
		}

		private void CreateGrid()
		{
			this.grid = new GameObject("Grid");
			this.grid.transform.position = new Vector3(0F, 0F, -10F);
			this.grid.transform.parent = this.transform;
			int gridRows = (this.map.Diameter / 16);
			for (int i = 0; i < gridRows; i++)
			{
				for (int j = 0; j < gridRows; j++)
				{
					var gridPiece = new GameObject("GridPiece " + (i+j));
					var image = gridPiece.AddComponent<Image>();
					image.sprite = this.gridSprite;
					image.rectTransform.sizeDelta = new Vector2(16F, 16F);

					gridPiece.transform.parent = this.grid.transform;
					gridPiece.transform.position = new Vector3(7.5F + 16F * j, -7.5F - 16F * i, 20F);
				}
			}
		}

	}
}
