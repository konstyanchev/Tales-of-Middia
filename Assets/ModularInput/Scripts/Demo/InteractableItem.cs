using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ModularInput
{
	public enum Capability
	{
		Color,
		Movement,
		Both,
	}

	public class InteractableItem : MonoBehaviour
	{
		public Capability capability;
		public ContextPriority priority;
		public string contextName;

		public UnityEvent onValidate;
		public UnityEvent onCancel;

		private InputContext context;
		private InputManager inputManager;
		private Controller controller;

		private Image image;
		private RectTransform rect;

		protected virtual void Awake()
		{
			this.image = this.GetComponent<Image>();
			this.rect = this.GetComponent<RectTransform>();
			this.inputManager = InputManager.Instance;
			this.inputManager.OnControllerChanged.AddListener(this.HandleControllerChange);
			if (this.priority == ContextPriority.Low)
				this.context = ScriptableObject.CreateInstance<StandardInputContext>();
			else
				this.context = ScriptableObject.CreateInstance<CriticalInputContext>();
			this.context.Init(this.contextName);
			this.context.onInput = this.OnInput;
			this.controller = this.inputManager.Controller;
		}

		private void HandleControllerChange(Controller controller)
		{
			this.controller = controller;
		}

		protected virtual void OnEnable()
		{
			this.inputManager.AddInputContext(this.context);
		}

		protected virtual void OnDisable()
		{
			this.inputManager.OnControllerChanged.RemoveListener(this.HandleControllerChange);
			this.inputManager.RemoveInputContext(this.context);
		}

		private bool OnInput()
		{
			CrossDirection dir = this.controller.GetDirection();
			CrossDirection menu = this.controller.GetMenuDirection();
			if (dir != CrossDirection.None)
			{
				if (this.capability == Capability.Both || this.capability == Capability.Color)
				{
					this.image.ChangeColor(dir);
					return true;
				}
			}
			else if (menu != CrossDirection.None)
			{
				if (this.capability == Capability.Both || this.capability == Capability.Movement)
				{
					this.rect.ChangePosition(menu);
					return true;
				}
			}
			else if (this.controller.Validate() == true)
			{
				this.onValidate.Invoke();
				return true;
			}
			else if (this.controller.Cancel() == true)
			{
				this.onCancel.Invoke();
				return true;
			}
			return false;
		}
	}
}

