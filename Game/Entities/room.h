
#pragma once

#include "../includes.h"
#include "panel.h"
#include "roomzone.h"
#include "combatant.h"

class Room
{
  private:
    int RoomTime;

    void SetDefaults();

  public:
    int BackgroundColour;
    std::vector<Panel*> Panels;
    std::vector<RoomZone*> Zones;
    std::string Script_OnCombatantEnter;
    std::string Script_OnCombatantLeave;
    std::string Script_OnUpdate;

    Combatant* Enemy;


    Room();
    ~Room();

    void OnEnter();
    void OnLeave();
    void Update();
    void Render(int FromY, int ToY);

    int SortPanels();
    int SortPanels(int Current);

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

};
