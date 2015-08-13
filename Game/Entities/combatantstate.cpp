
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
	s->FrameNumbers.push_back( { 0, false, false, 0, false } );
	StateList.push_back(s);

	// States::WALKING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::WALKING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	s->FrameNumbers.push_back( { 2, false, false, 0, false } );
	s->FrameNumbers.push_back( { 3, false, false, 0, false } );
	s->FrameNumbers.push_back( { 4, false, false, 0, false } );
	s->FrameNumbers.push_back( { 5, false, false, 0, false } );
	s->FrameNumbers.push_back( { 6, false, false, 0, false } );
	s->FrameNumbers.push_back( { 7, false, false, 0, false } );
	s->FrameNumbers.push_back( { 8, false, false, 0, false } );
	StateList.push_back(s);

	// States::SHORTJUMP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 16, true, true, 2, false } );
	s->FrameNumbers.push_back( { 17, true, true, 4, false } );
	s->FrameNumbers.push_back( { 18, true, true, 6, false } );
	s->FrameNumbers.push_back( { 19, true, true, 4, false } );
	s->FrameNumbers.push_back( { 16, true, true, 2, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false } );
	StateList.push_back(s);

	// States::LONGJUMP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 16, true, true, 2, false } );
	s->FrameNumbers.push_back( { 17, true, true, 4, false } );
	s->FrameNumbers.push_back( { 18, true, true, 6, false } );
	s->FrameNumbers.push_back( { 19, true, true, 6, false } );
	s->FrameNumbers.push_back( { 17, true, true, 6, false } );
	s->FrameNumbers.push_back( { 18, true, true, 6, false } );
	s->FrameNumbers.push_back( { 19, true, true, 4, false } );
	s->FrameNumbers.push_back( { 16, true, true, 2, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false } );
	StateList.push_back(s);

	// States::CLIMBINGUP
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::CLIMBINGTOP;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::CLIMBINGTOP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::CLIMBINGDOWN
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::CLIMBINGBOTTOM
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::ROLLING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 14, false, false, 0, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false } );
	s->FrameNumbers.push_back( { 17, true, true, 0, false } );
	s->FrameNumbers.push_back( { 18, true, true, 0, false } );
	s->FrameNumbers.push_back( { 19, true, true, 0, false } );
	s->FrameNumbers.push_back( { 15, false, false, 0, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, false } );
	StateList.push_back(s);

	// States::JAB
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 11, false, false, 0, false } );
	s->FrameNumbers.push_back( { 12, false, false, 0, true } );
	s->FrameNumbers.push_back( { 11, false, false, 0, false } );
	StateList.push_back(s);

	// States::SLASH
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::LUNGE
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 24, false, false, 0, false } );
	s->FrameNumbers.push_back( { 14, false, false, 0, true } );
	s->FrameNumbers.push_back( { 14, false, false, 0, true } );
	s->FrameNumbers.push_back( { 24, false, false, 0, false } );
	StateList.push_back(s);

	// States::KICK
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 9, false, false, 0, false } );
	s->FrameNumbers.push_back( { 10, false, false, 0, true } );
	s->FrameNumbers.push_back( { 10, false, false, 0, true } );
	s->FrameNumbers.push_back( { 9, false, false, 0, false } );
	StateList.push_back(s);

	// States::BLOCK
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::PICKUP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::WEAPONCHANGE_IN
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::WEAPONCHANGE_OUT
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::KNEELING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::SINKING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 1, false, false, 0, false } );
	StateList.push_back(s);

	// States::HITHIGH
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 23, false, false, 0, false } );
	StateList.push_back(s);

	// States::HITLOW
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( { 54, false, false, 0, false } );
	StateList.push_back(s);

	// States::DYING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::DEAD;
	s->FrameNumbers.push_back( { 55, false, false, 0, false } );
	s->FrameNumbers.push_back( { 55, false, false, 0, false } );
	StateList.push_back(s);

	// States::DEAD
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::DEAD;
	s->FrameNumbers.push_back( { 56, false, false, 0, false } );
	StateList.push_back(s);

}
