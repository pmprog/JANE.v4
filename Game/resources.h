
#pragma once

#include "includes.h"
#include "../Scripting/scriptengine.h"

class GameResources
{

  public:
    static ConfigFile* GameDataFile;
    static std::vector<PalettedBitmap*> CombatantGraphics;
    static PanelSheet* ObjectGraphics;
    static ScriptEngine* Scripting;
};
