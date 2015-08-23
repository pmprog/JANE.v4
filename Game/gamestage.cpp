
#include "gamestage.h"
#include "./Controllers/keyboard.h"
// #include "../Framework/framework.h"
#include "./Entities/room.h"

void GameStage::Begin()
{
	ninja = new Combatant( new KeyboardController() );
	ninja->ScreenX = GameResources::GameWorld->Start_ScreenX;
	ninja->ScreenY = GameResources::GameWorld->Start_ScreenY;
	ninja->CurrentDirection = (GameDirection::Direction)GameResources::GameWorld->Start_Facing;
	ninja->SetNewState( CombatantState::STANDING );

  curroom = GameResources::GameWorld->Start_RoomID;
	GameResources::GameWorld->Rooms.at( curroom )->OnEnter();

	RoomZone* z = GameResources::GameWorld->Rooms.at( curroom )->FindZoneForPoint( ninja->ScreenX, ninja->ScreenY );
	if( z != nullptr )
	{
		ninja->SetRoomZone( z, true );
	}

	AUDIO->PlayMusic( "resources/Paul Hannay (Feekzoid) - Last_Ninja_4_loader [1].ogg", true );
}

void GameStage::Pause()
{
}

void GameStage::Resume()
{
}

void GameStage::Finish()
{
	delete ninja->Controls;
	delete ninja;
}

void GameStage::EventOccurred(Event *e)
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

void GameStage::Update()
{
  GameResources::GameWorld->Rooms.at( curroom )->Update();
	ninja->OnUpdate();

	if( ninja->CurrentRoomID != curroom )
  {
    GameResources::GameWorld->Rooms.at( curroom )->OnLeave();
    curroom = ninja->CurrentRoomID;
    GameResources::GameWorld->Rooms.at( curroom )->OnEnter();
  }

}

void GameStage::Render()
{
	al_clear_to_color( Palette::ColourPalette[ GameResources::GameWorld->Rooms.at( curroom )->BackgroundColour ] );
	GameResources::GameWorld->Rooms.at( curroom )->Render( 0, ninja->ScreenY );
	ninja->OnRender();
	GameResources::GameWorld->Rooms.at( curroom )->Render( ninja->ScreenY, 200 );

	GameResources::GameOverlay->Draw( 0, 0, 0 );

	int powheight = (ninja->CurrentPower / (float)COMBATANT_POWER) * GameResources::NinjaPower->GetHeight();

	GameResources::NinjaPower->DrawPartial( GameResources::NinjaPower->GetHeight() - powheight, 0, GameResources::NinjaPower->GetWidth(), powheight, 6, 162, 0 );

}

bool GameStage::IsTransition()
{
	return false;
}
