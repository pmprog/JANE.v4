
#pragma once

#include "../includes.h"
#include "../../Framework/Primitives/polygon.h"
#include "../resources.h"

#ifndef Room
class Room;
#endif // World

class RoomZoneTeleportMode
{
	public:
		enum TeleportModes
		{
			INSTANT = 0,
			SINK = 1,
			KNEEL = 2,
			GLOW = 128
		};

};


class RoomZone
{

  private:
    Room* owner;

  public:
    Polyhedron* Area;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;

		int WorldZ;

		bool ClimbingZone;
		int ClimbingUpperZ;
		int ClimbingRequiresItemHeld;
		GameDirection::Direction ClimbingFace;

		bool DrowningZone;

		bool TransportZone;
		RoomZoneTeleportMode::TeleportModes TransportMode;
		int TransportRoomID;
		int TransportScreenX;
		int TransportScreenY;
		GameDirection::Direction TransportFacing;
		bool TransportClearInput;
		int TransportRequiresItemHeld;

		bool RollingZone;	// Can only enter via rolling

    RoomZone( Room* Owner );
    ~RoomZone();

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

    void Load(SQLiteDB* Database, int GameID, int RoomID, int ZoneID);
    void Save(SQLiteDB* Database, int GameID, int RoomID, int ZoneID);

};
