
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
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::WALKING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::WALKING;
	s->FrameNumbers.push_back( 1 );
	s->FrameNumbers.push_back( 2 );
	s->FrameNumbers.push_back( 3 );
	s->FrameNumbers.push_back( 4 );
	s->FrameNumbers.push_back( 5 );
	s->FrameNumbers.push_back( 6 );
	s->FrameNumbers.push_back( 7 );
	s->FrameNumbers.push_back( 8 );
	StateList.push_back(s);

	// States::SHORTJUMP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 16 );
	s->FrameNumbers.push_back( 17 );
	s->FrameNumbers.push_back( 18 );
	s->FrameNumbers.push_back( 19 );
	s->FrameNumbers.push_back( 16 );
	s->FrameNumbers.push_back( 14 );
	StateList.push_back(s);

	// States::LONGJUMP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 16 );
	s->FrameNumbers.push_back( 17 );
	s->FrameNumbers.push_back( 17 );
	s->FrameNumbers.push_back( 18 );
	s->FrameNumbers.push_back( 18 );
	s->FrameNumbers.push_back( 19 );
	s->FrameNumbers.push_back( 19 );
	s->FrameNumbers.push_back( 16 );
	s->FrameNumbers.push_back( 14 );
	StateList.push_back(s);

	// States::CLIMBINGUP
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::CLIMBINGTOP;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::CLIMBINGTOP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::CLIMBINGDOWN
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::CLIMBINGBOTTOM
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::JAB
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::SLASH
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::LUNGE
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::KICK
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::BLOCK
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::PICKUP
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::WEAPONCHANGE_IN
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::WEAPONCHANGE_OUT
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::KNEELING
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = false;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::SINKING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::HITHIGH
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 0 );
	StateList.push_back(s);

	// States::HITLOW
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::STANDING;
	s->FrameNumbers.push_back( 54 );
	StateList.push_back(s);

	// States::DYING
	s = new CombatantState();
	s->Loops = false;
	s->LockControls = true;
	s->NextState = States::DEAD;
	s->FrameNumbers.push_back( 55 );
	StateList.push_back(s);

	// States::DEAD
	s = new CombatantState();
	s->Loops = true;
	s->LockControls = true;
	s->NextState = States::DEAD;
	s->FrameNumbers.push_back( 56 );
	StateList.push_back(s);

}
