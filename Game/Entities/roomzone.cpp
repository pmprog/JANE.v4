
#include "roomzone.h"
#include "room.h"

RoomZone::RoomZone( Room* Owner )
{
	Area = new Polyhedron();
	Script_OnCombatantEnter = "";
	Script_OnCombatantLeave = "";
	WorldZ = 0;
	ClimbingZone = false;
	ClimbingUpperZ = 0;
	ClimbingRequiresItemHeld = -1;
	ClimbingFace = GameDirection::NORTH;
	DrowningZone = false;
	TransportingZone = false;
	TransportingMode = RoomZoneTeleportMode::INSTANT;
	TransportingRoomID = 0;
	TransportingScreenX = 0;
	TransportingScreenY = 0;
	TransportingFacing = GameDirection::UNCHANGED;
	TransportingClearInput = false;
	RollingZone = false;

}

RoomZone::~RoomZone()
{

}

void RoomZone::Load(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void RoomZone::Save(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void RoomZone::Load(SQLiteDB* Database, int GameID, int RoomID, int ZoneID)
{
}

void RoomZone::Save(SQLiteDB* Database, int GameID, int RoomID, int ZoneID)
{
}
