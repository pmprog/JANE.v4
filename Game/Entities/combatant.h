
#pragma once

#include "../includes.h"
#include "../resources.h"
#include "combatantstate.h"
#include "../Controllers/controller.h"

class CombatantRenderStyle
{
	public:
		enum RenderStyles
		{
			HUMAN,
			HUMAN_OVERSIZED
		};
};

class Combatant
{

	private:
		int speed_delay;

		int magicrampindex;
		int magicrampdelay;

		void OnUpdateMagic();

  public:
		Controller* Controls;

    std::string SkinFilename;
		PalettedBitmap* SkinGraphic;
		int ColourRemap[16];
		CombatantRenderStyle::RenderStyles SkinRenderStyle;

		int Speed;
		int CurrentPower;
		int CollectedMagic;
		int CollectedMagicRemaining;
		int ScreenX;
		int ScreenY;
		GameDirection::Direction CurrentDirection;

		CombatantState::States CurrentState;
		int CurrentStateTime;
		


    Combatant(Controller* Controls);
    ~Combatant();

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

    void Load(SQLiteDB* Database, int GameID, int RoomID);
    void Save(SQLiteDB* Database, int GameID, int RoomID);


		void OnUpdate();
		void OnRender();

		void SetNewState(CombatantState::States NewState);
};
