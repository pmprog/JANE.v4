
#include "combatant.h"

Combatant::Combatant()
{
	SkinFilename = "resources/ninja.png";
	SkinGraphic = new PalettedBitmap( SkinFilename );
	SkinRenderStyle = CombatantRenderStyle::HUMAN;
	for( int c = 0; c < 16; c++ )
	{
		ColourRemap[c] = c;
	}

	CurrentState = CombatantState::STANDING;
	CurrentStateTime = 0;

}

Combatant::~Combatant()
{
	delete SkinGraphic;
}

void Combatant::Load(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void Combatant::Save(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void Combatant::Load(SQLiteDB* Database, int GameID, int RoomID)
{
}

void Combatant::Save(SQLiteDB* Database, int GameID, int RoomID)
{
}

void OnUpdate()
{

}

void OnRender()
{
}
