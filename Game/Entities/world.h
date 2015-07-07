
#pragma once

#include "../includes.h"

#ifndef Room
class Room;
#endif // Room

class World
{

  public:
    std::vector<Room*> Rooms;

    World( std::string Filename );
    ~World();


};
