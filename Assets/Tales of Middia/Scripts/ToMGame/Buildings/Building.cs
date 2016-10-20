using UnityEngine;

namespace ToM
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
