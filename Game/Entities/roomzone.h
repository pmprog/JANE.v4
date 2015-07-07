
#pragma once

#include "../includes.h"


#ifndef Room
class Room;
#endif // World

class RoomZone
{

  private:
    Room* owner;

  public:
    Polygon* Area;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;

    RoomZone( Room* Owner );
    ~RoomZone();


};
