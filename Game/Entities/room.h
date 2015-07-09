
#pragma once

#include "../includes.h"
#include "panel.h"
#include "roomzone.h"
#include "combatant.h"

class Room
{
  private:
    int RoomTime;

  public:
    int BackgroundColour;
    std::vector<Panel*> Panels;
    std::vector<RoomZone*> Zones;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;
    std::string Script_OnUpdate;

    Combatant* Enemy;


    Room();
    Room( int RoomID );
    ~Room();

    void OnEnter();
    void OnLeave();
    void Update();
    void Render(int FromY, int ToY);

    int SortPanels();
    int SortPanels(int Current);

};
