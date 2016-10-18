using UnityEngine;
using System.Collections;

namespace HoMM
{


	public enum HeroClass
	{

	}

	public struct PrimarySkills
	{
		int attack;
		int defense;
		int spellPower;
		int knowledge;
		int morale;
		int luck;
		int mana;
	}

	public class Hero
	{
		public string name;
		public string Description;
		public Race race;
		public Player owner;
		public HeroClass heroClass;
		public int level;
		public PrimarySkills primarySkills;
		public int movementPoints;
		public int sight = 7;

//Secondary skills
//Navigation, Pathfinding, Fire, Water, Air, Earth, etc...
//Creatures
//Items
//Spells
//Slots(runtime)



	}
}


