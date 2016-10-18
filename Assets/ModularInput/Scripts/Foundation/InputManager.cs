using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModularInput
{
	public enum TargetPlatform
	{
		Standalone,
		XboxOne,
		UWP,
	}

	[Serializable]
	public class ControllerEvent : UnityEvent<Controller> { };

	public class InputManager : Singleton<InputManager>
	{
		public InputContext[] contexts;
		public TargetPlatform targetPlatform;

		public ControllerEvent OnControllerChanged;

		public Controller Controller { get { return this.controller; } }

		private List<InputContext> m_highPriorityContext	= new List<InputContext>();
		private List<InputContext> m_lowPriorityContext		= new List<InputContext>();

		private Controller controller;

		protected void Awake()
		{
			InputManager.instance = this;
			
			if (this.targetPlatform == TargetPlatform.Standalone)
				this.controller = Activator.CreateInstance(typeof(StandaloneController)) as Controller;
			else if (this.targetPlatform == TargetPlatform.UWP)
				this.controller = Activator.CreateInstance(typeof(StandaloneController)) as Controller;
			foreach (var context in this.contexts)
			{
				context.Init();
				this.AddInputContext(context);
			}
		}

		// If there are any high priority contexts, the input won't propagate to the low priority contexts
		protected void Update()
		{
			if(m_highPriorityContext.Count > 0)
			{
				for (int i = this.m_highPriorityContext.Count - 1; i >= 0; --i)
					if (this.m_highPriorityContext[i].OnInput() == true)
						break;
			}
			else if(m_lowPriorityContext.Count > 0)
			{
				for (int i = this.m_lowPriorityContext.Count - 1; i >= 0; --i)
					if (this.m_lowPriorityContext[i].OnInput() == true)
						break;
			}
		}

		private void ChangeController<T>() where T : Controller
		{
			Debug.Log("Changing the controller to a " + typeof(T).Name + ".");
			this.controller = Activator.CreateInstance(typeof(T)) as Controller;
			this.OnControllerChanged.Invoke(this.Controller);
		}

		public void AddInputContext(InputContext ic)
		{
			if (ic.priority == ContextPriority.High)
				this.m_highPriorityContext.Add(ic);
			else
				this.m_lowPriorityContext.Add(ic);
			ic.OnContextAdded();
		}

		public void RemoveInputContext(InputContext ic)
		{
			if (ic.priority == ContextPriority.High)
				this.m_highPriorityContext.Remove(ic);
			else
				this.m_lowPriorityContext.Remove(ic);
			ic.OnContextRemoved();
		}

	}
}

