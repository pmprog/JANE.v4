
#pragma once

#include "../includes.h"


#ifndef World
class World;
#endif // World

class Room
{

  private:
    World* owner;

  public:
    std::vector<Polygon*> Zones;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;


    Room( World* Owner );
    ~Room();


};
