
#include "combatant.h"

Combatant::Combatant(Controller* Controls)
{
	this->Controls = Controls;

	SkinFilename = "resources/skin_ninja.png";
	SkinGraphic = new PalettedBitmap( SkinFilename );
	SkinRenderStyle = CombatantRenderStyle::HUMAN;
	for( int c = 0; c < 16; c++ )
	{
		ColourRemap[c] = c;
	}

	CurrentState = CombatantState::STANDING;
	CurrentStateTime = 0;

  weapon_change_on_stand = false;
  weapon_current_index = 0;
  weapon_change_index = 0;

	magicrampindex = 0;
	magicrampdelay = 0;

	speed_delay = 0;
	Speed = 2;
}

Combatant::~Combatant()
{
	delete SkinGraphic;
}

void Combatant::Load(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void Combatant::Save(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void Combatant::Load(SQLiteDB* Database, int GameID, int RoomID)
{
}

void Combatant::Save(SQLiteDB* Database, int GameID, int RoomID)
{
}

void Combatant::OnUpdate()
{
	CombatantState* curState;
	int directionheld;

	OnUpdateMagic();

	speed_delay = (speed_delay + 1) % Speed;
	if( speed_delay == 0 )
	{
		curState = CombatantState::StateList[CurrentState];
		directionheld = (Controls->GetState() & Controller::MASK_DIRECTIONS);

		CurrentStateTime = (CurrentStateTime + 1) % curState->FrameNumbers.size();
		if( CurrentStateTime == 0 && !curState->Loops )
		{
			if( curState->NextState == CombatantState::WEAPONCHANGE_OUT )
      {
        weapon_current_index = weapon_change_index;
        weapon_change_on_stand = false;
      }
      SetNewState( curState->NextState );
		}

		if( !curState->LockControls )
		{
			int buttonsheld = (Controls->GetState() & Controller::MASK_BUTTONS);

      if( (Controls->GetState() & Controller::NORTH) == Controller::NORTH && ( CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST ) )
      {
        CurrentDirection = GameDirection::NORTH;
      }
      if( (Controls->GetState() & Controller::EAST) == Controller::EAST && ( CurrentDirection == GameDirection::NORTH || CurrentDirection == GameDirection::SOUTH ) )
      {
        CurrentDirection = GameDirection::EAST;
      }
      if( (Controls->GetState() & Controller::SOUTH) == Controller::SOUTH && ( CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST ) )
      {
        CurrentDirection = GameDirection::SOUTH;
      }
      if( (Controls->GetState() & Controller::WEST) == Controller::WEST && ( CurrentDirection == GameDirection::NORTH || CurrentDirection == GameDirection::SOUTH ) )
      {
        CurrentDirection = GameDirection::WEST;
      }

			if( (Controls->GetState() & Controller::WEAPON) == Controller::WEAPON )
      {
        weapon_change_on_stand = true;
        // TODO: Change weapon_change_index to next weapon available after weapon_change_index
      }

			if( directionheld != 0 && buttonsheld == 0 )
			{
				// Directions Only
				//if( CurrentState == CombatantState::STANDING || CurrentState == CombatantState::WALKING || CurrentState == CombatantState::WALKINGBACKWARDS )
				//{
					CombatantState::States newstate = CombatantState::WALKING;
					if( (CurrentDirection == GameDirection::NORTH && (directionheld & Controller::SOUTH) == Controller::SOUTH)
						|| (CurrentDirection == GameDirection::EAST && (directionheld & Controller::WEST) == Controller::WEST)
						|| (CurrentDirection == GameDirection::SOUTH && (directionheld & Controller::NORTH) == Controller::NORTH)
						|| (CurrentDirection == GameDirection::WEST && (directionheld & Controller::EAST) == Controller::EAST) )
					{
						newstate = CombatantState::WALKINGBACKWARDS;
					}
					SetNewState( newstate );
				//}
			} else if( directionheld != 0 && buttonsheld != 0 ) {

			  if( CurrentState == CombatantState::WALKING )
        {
          SetNewState( CombatantState::LONGJUMP );
        }


			} else if( buttonsheld != 0 ) {
			} else {
				if( CurrentState == CombatantState::BLOCK )
				{
					SetNewState( CombatantState::BLOCK_OUT );
				}
				if( CurrentState == CombatantState::WALKING || CurrentState == CombatantState::WALKINGBACKWARDS )
				{
					SetNewState( CombatantState::STANDING );
				}
			}

      if( weapon_change_on_stand && CurrentState == CombatantState::STANDING )
      {
        SetNewState( CombatantState::WEAPONCHANGE_IN );
      }
		}

    curState = CombatantState::StateList[CurrentState];
    if( curState->FrameNumbers.at(CurrentStateTime).MoveCombatant )
    {
      if( directionheld == Controller::NORTH )
      {
        ScreenX += 4;
        ScreenY -= 1;
      } else if( directionheld == Controller::SOUTH ) {
        ScreenX -= 4;
        ScreenY += 1;
      } else if( directionheld == Controller::EAST ) {
        ScreenX += 4;
        ScreenY += 1;
      } else if( directionheld == Controller::WEST ) {
        ScreenX -= 4;
        ScreenY -= 1;
      }
    }



	}

}

void Combatant::OnRender()
{
	int framenumber = 0;
	int zpos = 0;
	bool shadowed = false;
	int flipflag = 0;

	// TODO: Get frame number
	framenumber = CombatantState::StateList[CurrentState]->FrameNumbers.at(CurrentStateTime).FrameNumber;
	zpos = CombatantState::StateList[CurrentState]->FrameNumbers.at(CurrentStateTime).Z;
	shadowed = CombatantState::StateList[CurrentState]->FrameNumbers.at(CurrentStateTime).AddShadow;

	// Flip
	if( CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST )
	{
		flipflag = ALLEGRO_FLIP_HORIZONTAL;
	}

	// Point to other direction
	if( framenumber < 26 && (CurrentDirection == GameDirection::WEST || CurrentDirection == GameDirection::NORTH) )
	{
		framenumber += 26;
	}

	// Render
	if( shadowed )
	{
		SkinGraphic->DrawPartial( 0, 168, 32, 42, ScreenX - 16, ScreenY - 40, 0 );
	}

	SkinGraphic->DrawPartial( (framenumber % 13) * 32, (framenumber / 13) * 42, 32, 42, ScreenX - 16, ScreenY - 40 - zpos, flipflag );
}

void Combatant::OnUpdateMagic()
{
	if( CollectedMagicRemaining > 0 )
	{
		CollectedMagicRemaining--;
		if( CollectedMagicRemaining == 0 )
		{
			CollectedMagic = 0;
			magicrampindex = 0;
			magicrampdelay = 0;
			for( int i = 0; i < 16; i++ )
			{
				ColourRemap[i] = i;
			}
		} else {

			magicrampdelay = (magicrampdelay + 1) % 3;
			if( magicrampdelay == 0 )
			{
				magicrampindex = (magicrampindex + 1) % 8;
				for( int i = 0; i < 16; i++ )
				{
					if( i != 10 )
					{
						ColourRemap[i] = Palette::RampBlue[magicrampindex];
					}
				}
			}
		}
	}
}

void Combatant::SetNewState(CombatantState::States NewState)
{
	if( CurrentState == NewState )
	{
		return;
	}

	CurrentState = NewState;
	CurrentStateTime = 0;

	if( CombatantState::StateList[CurrentState]->LockControls )
  {
    Controls->LockControls();
  } else {
    Controls->UnlockControls();
  }
}
