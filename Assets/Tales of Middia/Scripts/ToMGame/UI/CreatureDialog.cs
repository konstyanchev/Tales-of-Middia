using ModularInput;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ToM
{
	public class CreatureDialog : MonoBehaviour
	{

		public GameObject UI;
		public GameObject portraitContainer;
		public Button yesButton;
		public Button noButton;

		private MapCreature owner;
		private Image[] creatureIcons;
		private Text[] creatureCount;

		private InputManager im;
		private InputContext context;
		private Controller controller;

		protected void Awake()
		{
			this.creatureIcons = this.portraitContainer.GetComponentsInChildren<Image>();
			this.creatureCount = this.portraitContainer.GetComponentsInChildren<Text>();

			this.yesButton.onClick.AddListener(() => { this.owner.HandleChoice(true); this.Disable(); });
			this.noButton.onClick.AddListener(() => { this.owner.HandleChoice(false); this.Disable(); });

			for (int i = 0; i < 7; i++)
				this.portraitContainer.transform.GetChild(i).gameObject.SetActive(false);

			this.im = InputManager.Instance;
			this.context = ScriptableObject.CreateInstance<CriticalInputContext>();
			this.context.Init("Creature Dialog");
			this.context.onInput += this.OnInput;
			this.controller = this.im.Controller;
		}

		protected bool OnInput()
		{
			if (this.controller.Validate() == true)
			{
				this.owner.HandleChoice(true);
				this.Disable();
				return true;
			}
			else if (this.controller.Cancel() == true)
			{
				this.owner.HandleChoice(false);
				this.Disable();
				return true;
			}
			return false;
		}

		private void Disable()
		{
			this.UI.SetActive(false);
			this.im.RemoveInputContext(this.context);
		}

		public void Init(MapCreature owner, List<ArmyTuple> army)
		{
			this.UI.SetActive(true);
			this.im.AddInputContext(this.context);
			this.owner = owner;
			for (int i = 0; i < army.Count; i++)
			{
				portraitContainer.transform.GetChild(i).gameObject.SetActive(true);
				this.creatureIcons[i].sprite = army[i].second.icon;
				this.creatureCount[i].text = army[i].first.ToString();
			}
		}
	}
}
