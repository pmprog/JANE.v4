
#include "debugstage.h"
#include "./Controllers/keyboard.h"

void DebugStage::Begin()
{
	ninja = new Combatant( new KeyboardController() );
	ninja->ScreenX = 100;
	ninja->ScreenY = 100;
	ninja->CurrentDirection = GameDirection::EAST;
	ninja->SetNewState( CombatantState::WALKING );
}

void DebugStage::Pause()
{
}

void DebugStage::Resume()
{
}

void DebugStage::Finish()
{
	delete ninja->Controls;
	delete ninja;
}

void DebugStage::EventOccurred(Event *e)
{
	if( e->Type == EVENT_KEY_DOWN )
	{
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_ESCAPE )
		{
			delete FRAMEWORK->ProgramStages->Pop();
			return;
		}
	}

	ninja->Controls->EventOccurred(e);
}

void DebugStage::Update()
{
	ninja->OnUpdate();
}

void DebugStage::Render()
{
	al_clear_to_color( Palette::ColourPalette[12] );
	ninja->OnRender();
}

bool DebugStage::IsTransition()
{
	return false;
}
