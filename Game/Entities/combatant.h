
#pragma once

#include "../includes.h"
#include "../resources.h"
#include "combatantstate.h"
#include "../Controllers/controller.h"

#ifndef RoomZone
class RoomZone;
#endif

#define COMBATANT_POWER		32

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
		RoomZone* world_zone;
		int world_z;

	  bool weapon_change_on_stand;
	  int weapon_current_index;
	  int weapon_change_index;

	  int item_held_index;

		int speed_delay;


		int magicrampindex;
		int magicrampdelay;

		void OnUpdateMagic();

		void ProposeMove( int ScreenX, int ScreenY );

		Controller::ControllerStateFlags GetPrimaryControllerState();
		Controller::ControllerStateFlags GetSecondaryControllerState();

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
		int CurrentRoomID;

		CombatantState::States CurrentState;
		int CurrentStateTime;

		bool UnlimitedPower;
		bool UnlimitedMagic;
		bool ZoneClipping;

    Combatant(Controller* Controls);
    ~Combatant();

    void Load(SQLiteDB* Database, int GameID, int RoomID);
    void Save(SQLiteDB* Database, int GameID, int RoomID);


		void OnUpdate();
		void OnRender();

		void SetNewState(CombatantState::States NewState);


		void SetRoomZone(RoomZone* CurrentZone, bool IsWarped);

};
