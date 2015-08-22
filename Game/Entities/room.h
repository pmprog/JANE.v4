
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
		void Render(int RenderOffsetX, int RenderOffsetY, int FromY, int ToY);

    int SortPanels();
    int SortPanels(int Current);

		void ResetRoomTime();

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

    void Load(SQLiteDB* Database, int GameID, int RoomID);
    void Save(SQLiteDB* Database, int GameID, int RoomID);

		RoomZone* FindZoneForPoint(int X, int Y);

};
