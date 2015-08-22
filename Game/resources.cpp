
#include "resources.h"

PalettedBitmap* GameResources::GameOverlay;
PalettedBitmap* GameResources::NinjaPower;
PalettedBitmap* GameResources::EnemyPower;
PalettedBitmap* GameResources::MagicPower;
ConfigFile* GameResources::GameDataFile = nullptr;
std::vector<PalettedBitmap*> GameResources::CombatantGraphics;
PanelSheet* GameResources::ObjectGraphics = nullptr;
ScriptEngine* GameResources::Scripting = nullptr;
World* GameResources::GameWorld = nullptr;
