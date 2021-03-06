﻿using UnityEngine;
using System.Collections;

namespace ToM
{
	public enum Team
	{
		Red		= 0,
		Blue	= 1,
		Green	= 2,
		Yellow	= 3,
		Purple	= 4,
		Teal	= 5,
		Brown	= 6,
		Silver	= 7,
	}

	public enum Race
	{
		Human,
		Elf,
		Undead,
		Demon,
		Wizard,
		Orc,
	}

	public enum ResourceTypes
	{
		Wood	= 0,
		Stone	= 1,
		Iron	= 2,
		Crystal = 3,
		Gems	= 4,
		Mercury = 5,
		Sulfur	= 6,
		Gold	= 7,
	}

	public enum Disposition
	{
		Allied		= 100,
		Friendly	= 75,
		Neutral		= 50,
		Hostile		= 25,
		Savage		= 0,
	}

	public enum TerrainType
	{
		Void = 0,
		Water = 1,
		Grass = 2,
		Swamp = 3,
		Snow = 4,
		Sand = 5,
		Lava = 6,
		Rocks = 7,
		Subterran = 8,
	}
}
