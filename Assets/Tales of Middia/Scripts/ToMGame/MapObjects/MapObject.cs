using UnityEngine;

namespace ToM
{
	public class MapObject : MonoBehaviour
	{
		public int width;
		public int height;

		public TilePositon[] blockedTiles;
		//Bottom-Left corner
		public Tile origin;

		public virtual void Init(Tile origin)
		{
			Map currentMap = GameManager.Instance.currentMap;
			Vector3 position = origin.position.ToVector2();
			float offsetX = this.width % 2 == 0 ? this.width / 2 - 0.5F : this.width / 2;
			float offsetY = this.height % 2 == 0 ? this.height / 2 - 0.5F : this.height / 2;
			position.x += offsetX;
			position.y += offsetY;
			position.z = 10F;
			this.transform.position = position;
			this.origin = origin;

			for (int i = 0; i < this.blockedTiles.Length; i++)
				currentMap.tiles[this.origin.position.x + this.blockedTiles[i].x, this.origin.position.y - this.blockedTiles[i].y].ToggleBlock(true);
		}
	}
}
