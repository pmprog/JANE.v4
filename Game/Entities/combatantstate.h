
#pragma once

#include "../includes.h"
#include "../resources.h"

class CombatantState
{
	public:

		struct StateFrameData
		{
			int FrameNumber;
			bool MoveCombatant;
			bool AddShadow;
			int Z;
			bool InflictDamage;
			bool Pickup;
		};

		enum States
		{
			STANDING,
			WALKING,
			WALKINGBACKWARDS,
			SHORTJUMP,				// Whilst walking - Fire + UDLR
			LONGJUMP,					// Whilst walking - Fire + Diagonal
			CLIMBINGUP,
			CLIMBINGTOP,
			CLIMBINGDOWN,
			CLIMBINGBOTTOM,
			ROLLING,
			JAB,							// Fire + Up
			SLASH,						// Fire + Right
			LUNGE,						// Fire + Left
			KICK,							// Fire + Down
			BLOCK_IN,					// Fire + Diagonal Away (ie, if fighting facing north (top right), block is south (down left))
			BLOCK,						// Fire + Diagonal Away (ie, if fighting facing north (top right), block is south (down left))
			BLOCK_OUT,				// Fire + Diagonal Away (ie, if fighting facing north (top right), block is south (down left))
			PICKUP,						// Fire + Diagonal Towards
			WEAPONCHANGE_IN,
			WEAPONCHANGE_OUT,
			KNEELING_IN,
			KNEELING,
			KNEELING_OUT,
			SINKING,
			HITHIGH,
			HITLOW,
			DYING,
			DEAD
		};

		bool Loops;
		States NextState;
		bool LockControls;
		std::vector<StateFrameData> FrameNumbers;

		static std::vector<CombatantState*> StateList;
		static void InitialiseCombatantStates();

};
