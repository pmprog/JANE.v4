
#pragma once

#include "../includes.h"
#include "../../Framework/Primitives/polygon.h"

#ifndef Room
class Room;
#endif // World

class RoomZone
{

  private:
    Room* owner;

  public:
    Polyhedron* Area;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;

    RoomZone( Room* Owner );
    ~RoomZone();

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

    void Load(SQLiteDB* Database, int GameID, int RoomID, int ZoneID);
    void Save(SQLiteDB* Database, int GameID, int RoomID, int ZoneID);

};
