
#pragma once

#include "../includes.h"
#include "panel.h"
#include "roomzone.h"

#ifndef World
class World;
#endif // World

class Room
{

  private:
    World* owner;

  public:
    std::vector<RoomZone*> Zones;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;


    Room( World* Owner );
    ~Room();

};
