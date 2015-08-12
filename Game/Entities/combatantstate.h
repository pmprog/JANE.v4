
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
			CLIMBINGTOP,
			CLIMBINGDOWN,
			CLIMBINGBOTTOM,
			JAB,							// Fire + Up
			SLASH,						// Fire + Right
			LUNGE,						// Fire + Left
			KICK,							// Fire + Down
			BLOCK,						// Fire + Diagonal Away (ie, if fighting facing north (top right), block is south (down left))
			PICKUP,						// Fire + Diagonal Towards
			WEAPONCHANGE_IN,
			WEAPONCHANGE_OUT,
			KNEELING,
			SINKING,
			HITHIGH,
			HITLOW,
			DYING,
			DEAD
		};

		bool Loops;
		States NextState;
		bool LockControls;
		std::vector<int> FrameNumbers;

		static std::vector<CombatantState*> StateList;
		static void InitialiseCombatantStates();

};
