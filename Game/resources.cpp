
#include "resources.h"

PalettedBitmap* GameResources::GameOverlay;
ConfigFile* GameResources::GameDataFile = nullptr;
std::vector<PalettedBitmap*> GameResources::CombatantGraphics;
PanelSheet* GameResources::ObjectGraphics = nullptr;
ScriptEngine* GameResources::Scripting;
