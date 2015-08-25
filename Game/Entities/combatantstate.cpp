
#include "combatantstate.h"

std::vector<CombatantState*> CombatantState::StateList;

void CombatantState::InitialiseCombatantStates()
{
	CombatantState* s;

	// States::STANDING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 0, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::WALKING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::WALKING;
	s->FrameNumbers.push_back( { 1, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 2, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 3, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 4, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 5, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 6, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 7, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 8, true, false, 0, false, false } );
	StateList.push_back(s);

	// States::WALKINGBACKWARDS
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::WALKINGBACKWARDS;
	s->FrameNumbers.push_back( { 8, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 7, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 6, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 5, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 4, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 3, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 2, true, false, 0, false, false } );
	s->FrameNumbers.push_back( { 1, true, false, 0, false, false } );
	StateList.push_back(s);

	// States::SHORTJUMP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 16, true, true, 6, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 20, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 20, false, false } );
	s->FrameNumbers.push_back( { 16, true, true, 6, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::LONGJUMP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 16, true, true, 6, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 20, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 30, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 20, false, false } );
	s->FrameNumbers.push_back( { 16, true, true, 6, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::CLIMBINGUP
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::CLIMBINGTOP;
	s->FrameNumbers.push_back( { 57, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 58, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 59, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 58, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::CLIMBINGTOP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::SHORTJUMP;
	s->FrameNumbers.push_back( { 1, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::CLIMBINGDOWN
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::CLIMBINGBOTTOM;
	s->FrameNumbers.push_back( { 59, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 58, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 57, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 58, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::CLIMBINGBOTTOM
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 20, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::ROLLING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 17, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::JAB
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 11, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 12, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 11, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::SLASH
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 20, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 21, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 21, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 20, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 20, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 24, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 24, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 25, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 25, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 24, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::LUNGE
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 24, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 24, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 24, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 24, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::KICK
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 9, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 9, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 10, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 10, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 10, false, false, 0, true, false } );
	s->FrameNumbers.push_back( { 9, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 9, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::BLOCK_IN
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::BLOCK;
	s->FrameNumbers.push_back( { 13, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::BLOCK
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::BLOCK_OUT;
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::BLOCK_OUT
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 13, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::PICKUP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 13, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, true } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, true } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::WEAPONCHANGE_IN
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = false;
	s->NextState = States::WEAPONCHANGE_OUT;
	s->FrameNumbers.push_back( { 20, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 21, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 22, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::WEAPONCHANGE_OUT
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 22, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 21, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 20, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::KNEELING_IN
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::KNEELING;
	s->FrameNumbers.push_back( { 13, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::KNEELING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 60, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::KNEELING_OUT
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 15, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 13, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::SINKING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::DEAD;  // Usually dead, though sometimes not
	s->FrameNumbers.push_back( { 16, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::FALLING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::FALLING_LANDED;
	s->FrameNumbers.push_back( { 16, false, true, 0, false, false } );
	StateList.push_back(s);

	// States::FALLING_LANDED
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::HITLOW;
	s->FrameNumbers.push_back( { 54, false, true, 0, false, false } );
	StateList.push_back(s);

	// States::HITHIGH
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 23, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 23, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::HITLOW
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 54, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 54, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::DYING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::DEAD;
	s->FrameNumbers.push_back( { 55, false, false, 0, false, false } );
	s->FrameNumbers.push_back( { 55, false, false, 0, false, false } );
	StateList.push_back(s);

	// States::DEAD
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::DEAD;
	s->FrameNumbers.push_back( { 56, false, false, 0, false, false } );
	StateList.push_back(s);

}
