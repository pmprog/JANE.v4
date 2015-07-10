
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


};
