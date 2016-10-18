using UnityEngine;

namespace HoMM
{
	[System.Serializable]
	public class Building
	{
		public string name;
		public string description;

		public void OnVisit(Hero visitor)
		{
			Debug.Log("You've just visited " + name);
		}
	}
}
