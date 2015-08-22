
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
		int TotalRooms;
    std::vector<Room*> Rooms;
    std::string Script_OnUpdate;
		int Start_RoomID;
		int Start_ScreenX;
		int Start_ScreenY;
		int Start_Facing;

    World( std::string Filename, int GameID );
    ~World();

    void Save();
		void SaveRoom( int RoomID );

};
