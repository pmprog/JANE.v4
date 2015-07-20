
#pragma once

#include "../includes.h"

#ifndef Room
class Room;
#endif // Room

class World
{

  private:
    std::string DataFilename;
    ConfigFile* DataFile;
		SQLiteDB* gamedb;
		int gameid;

  public:
    std::vector<Room*> Rooms;
    std::string Script_OnUpdate;

    World( std::string Filename, int GameID );
    ~World();

    void Save();

};
