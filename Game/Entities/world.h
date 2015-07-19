
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

  public:
    std::vector<Room*> Rooms;
    std::string Script_OnUpdate;

    World( std::string Filename );
    ~World();

    void Save();

};
