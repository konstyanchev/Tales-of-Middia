using UnityEngine;
using ModularInput;

namespace HoMM
{
	public class PlayerController : MonoBehaviour
	{
		public int x;
		public int y;


		private Player player;
		private GameManager gm;

		private InputManager im;
		private InputContext context;
		private Controller controller;

		private float cameraOffset;

		protected void Awake()
		{
			this.player = this.GetComponent<Player>();
			this.gm = GameManager.Instance;
			this.im = InputManager.Instance;
			this.controller = this.im.Controller;
			this.context = ScriptableObject.CreateInstance<StandardInputContext>();
			this.context.Init("Player Context");
			this.context.onInput += this.OnInput;
			this.cameraOffset = -this.transform.position.z;
			this.im.AddInputContext(this.context);
		}

		protected bool OnInput()
		{
			if (this.controller.RightClick() == true)
			{
				var pos = CursorToWorldPos();
				var tile = MapUtility.TileFromWorldPosition(pos);
				tile.ToggleBlock(tile.traversable);
				return true;
			}
			else if (this.controller.LeftClick() == true)
			{
				var pos = CursorToWorldPos();
				this.player.currentHero.movement.SetDestination(MapUtility.TileFromWorldPosition(pos));
				return true;
			}
			else if (this.controller.Space() == true)
			{
				this.player.currentHero.currentTile.interaction.Invoke(this.player.currentHero.hero);
			}
			else if (this.controller.Validate() == true)
				this.gm.currentMap.ShowTiles(new TilePositon(x, y), 5);
			return false;
		}

		private Vector3 CursorToWorldPos()
		{
			var r = Input.mousePosition;
			r.z = this.cameraOffset;
			return Camera.main.ScreenToWorldPoint(r);
		}
	}
}