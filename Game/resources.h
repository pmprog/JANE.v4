
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
			NORTH,
			EAST,
			SOUTH,
			WEST
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
