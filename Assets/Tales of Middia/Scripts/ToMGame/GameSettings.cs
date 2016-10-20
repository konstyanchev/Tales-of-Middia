using System;
using UnityEngine;

namespace ToM
{
	public class GameSettings : ScriptableObject
	{
		private static GameSettings instance;
		public static GameSettings Instance
		{
			get
			{
				if (GameSettings.instance == null)
				{
					GameSettings gs = Resources.Load<GameSettings>("Settings/GameSettings");
					if (gs != null)
					{
						GameSettings.instance = gs;
						return gs;
					}
				}
				return GameSettings.instance;
			}
		}

		public Action OnSettingsChanged;

		[System.Serializable]
		public class GameplaySettings
		{
			public float mapScrollSpeed = 2f;
		}

		public GameplaySettings gampeplay = new GameplaySettings();

		[System.Serializable]
		public class MiscSettings
		{
			public string ToMResourceFolder = "Assets/ToM/Resources/";
			public string ToMMapsFolder = "Assets/ToM/Resources/Maps/";
		}

		public MiscSettings miscSettings = new MiscSettings();
	}
}
