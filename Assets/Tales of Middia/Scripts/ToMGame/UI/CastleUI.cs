using UnityEngine;
using ModularInput;
using UnityEngine.UI;

namespace ToM
{
	public class CastleUI : MonoBehaviour
	{
		public Button backButton;

		private GameManager gm;
		private InputManager im;
		private InputContext context;
		private Controller controller;

		protected void Awake()
		{
			this.backButton.onClick.AddListener(() => { this.Disable(); });

			this.gm = GameManager.Instance;
			this.im = InputManager.Instance;
			this.context = ScriptableObject.CreateInstance<CriticalInputContext>();
			this.context.Init("Castle");
			this.context.onInput += this.OnInput;
			this.controller = this.im.Controller;
		}

		protected bool OnInput()
		{
			if (this.controller.Cancel() == true)
			{
				this.Disable();
				return true;
			}
			return false;
		}

		public void Init(Castle castle)
		{
			this.im.AddInputContext(this.context);
			Debug.Log("You are visiting " + castle.name);
		}

		private void Disable()
		{
			this.gm.ShowMapScreen();
			this.im.RemoveInputContext(this.context);
		}

	}
}
