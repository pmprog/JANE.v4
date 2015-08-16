
#include "debugstage.h"
#include "./Controllers/keyboard.h"
// #include "../Framework/framework.h"
#include "./Entities/room.h"

void DebugStage::Begin()
{
	ninja = new Combatant( new KeyboardController() );
	ninja->ScreenX = 100;
	ninja->ScreenY = 100;
	ninja->CurrentDirection = GameDirection::EAST;
	ninja->SetNewState( CombatantState::WALKING );

  curroom = 0;
	GameResources::GameWorld->Rooms.at( curroom )->OnEnter();
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
	FRAMEWORK->SetSlowMode( false );
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
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_OPENBRACE )
    {
      GameResources::GameWorld->Rooms.at( curroom )->OnLeave();
      curroom = (curroom + 1) % GameResources::GameWorld->Rooms.size();
      GameResources::GameWorld->Rooms.at( curroom )->OnEnter();
    }
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_CLOSEBRACE )
    {
      GameResources::GameWorld->Rooms.at( curroom )->OnLeave();
      curroom = (curroom + (GameResources::GameWorld->Rooms.size() - 1)) % GameResources::GameWorld->Rooms.size();
      GameResources::GameWorld->Rooms.at( curroom )->OnEnter();
    }
	}

	ninja->Controls->EventOccurred(e);
}

void DebugStage::Update()
{
  GameResources::GameWorld->Rooms.at( curroom )->Update();
	ninja->OnUpdate();
}

void DebugStage::Render()
{
	al_clear_to_color( Palette::ColourPalette[ GameResources::GameWorld->Rooms.at( curroom )->BackgroundColour ] );
	GameResources::GameWorld->Rooms.at( curroom )->Render( 0, ninja->ScreenY );
	ninja->OnRender();
	GameResources::GameWorld->Rooms.at( curroom )->Render( ninja->ScreenY, 200 );

	GameResources::GameOverlay->Draw( 0, 0, 0 );
}

bool DebugStage::IsTransition()
{
	return false;
}
