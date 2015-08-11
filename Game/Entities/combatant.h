
#pragma once

#include "../includes.h"
#include "../resources.h"

class CombatantState
{
	public:
		enum States
		{
			STANDING,
			WALKING,
			SHORTJUMP,				// Whilst walking - Fire + UDLR
			LONGJUMP,					// Whilst walking - Fire + Diagonal
			CLIMBINGUP,
			CLIMBINGDOWN,
			JAB,							// Fire + Up
			SLASH,						// Fire + Right
			LUNGE,						// Fire + Left
			KICK,							// Fire + Down
			BLOCK,						// Fire + Diagonal Away (ie, if fighting facing north (top right), block is south (down left))
			PICKUP,						// Fire + Diagonal Towards
			WEAPONCHANGE_IN,
			WEAPONCHANGE_OUT,
			KNEELING,
			SINKING
		};
};

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
		int magicrampindex;
		int magicrampdelay;

		void OnUpdateMagic();

  public:
    std::string SkinFilename;
		PalettedBitmap* SkinGraphic;
		int ColourRemap[16];
		CombatantRenderStyle::RenderStyles SkinRenderStyle;

		int CurrentPower;
		int CollectedMagic;
		int CollectedMagicRemaining;
		int ScreenX;
		int ScreenY;
		GameDirection::Direction CurrentDirection;

		CombatantState::States CurrentState;
		int CurrentStateTime;


    Combatant();
    ~Combatant();

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

    void Load(SQLiteDB* Database, int GameID, int RoomID);
    void Save(SQLiteDB* Database, int GameID, int RoomID);


		void OnUpdate();
		void OnRender();

};
