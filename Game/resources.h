
#pragma once

#include "includes.h"
#include "../Scripting/scriptengine.h"
#include "Entities/world.h"
#include "Controllers/keyboard.h"
#include "Controllers/joystick.h"

class GameDirection
{
	public:
		enum Direction
		{
			UNCHANGED = -1,
			NORTH = 1,
			EAST = 2,
			SOUTH = 4,
			WEST = 8
		};
};

class GameResources
{

  public:
    static PalettedBitmap* GameOverlay;
    static ConfigFile* GameDataFile;
    static std::vector<PalettedBitmap*> CombatantGraphics;
    static PanelSheet* ObjectGraphics;
    static ScriptEngine* Scripting;
    static World* GameWorld;
};
