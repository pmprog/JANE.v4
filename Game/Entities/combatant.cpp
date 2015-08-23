
#include "combatant.h"
#include "roomzone.h"
#include "room.h"

Combatant::Combatant(Controller* Controls)
{
	this->Controls = Controls;

	SkinGraphic = GameResources::CombatantGraphics.at(0);
	SkinRenderStyle = CombatantRenderStyle::HUMAN;
	for( int c = 0; c < 16; c++ )
	{
		ColourRemap[c] = c;
	}

	CurrentState = CombatantState::STANDING;
	CurrentStateTime = 0;

	world_z = 0;

  weapon_change_on_stand = false;
  weapon_current_index = 0;
  weapon_change_index = 0;

  item_held_index = -1;

  sinking_depth = 0;

	magicrampindex = 0;
	magicrampdelay = 0;

	speed_delay = 0;
	Speed = 4;

	CurrentPower = COMBATANT_POWER;
	world_zone = nullptr;

	ZoneClipping = false;
	UnlimitedPower = false;
	UnlimitedMagic = false;
}

Combatant::~Combatant()
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
      ProposeMove( ScreenX, ScreenY );
		}

    if( (Controls->GetState() & Controller::WEAPON) == Controller::WEAPON )
    {
      weapon_change_on_stand = true;
      // TODO: Change weapon_change_index to next weapon available after weapon_change_index
    }

		if( !curState->LockControls )
		{
			int buttonsheld = (Controls->GetState() & Controller::FIRE);

			if( directionheld != 0 && buttonsheld == 0 )
			{
        if( Controls->GetState() == Controller::NORTH && ( CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST ) )
        {
          CurrentDirection = GameDirection::NORTH;
        }
        if( Controls->GetState() == Controller::EAST && ( CurrentDirection == GameDirection::NORTH || CurrentDirection == GameDirection::SOUTH ) )
        {
          CurrentDirection = GameDirection::EAST;
        }
        if( Controls->GetState() == Controller::SOUTH && ( CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST ) )
        {
          CurrentDirection = GameDirection::SOUTH;
        }
        if( Controls->GetState() == Controller::WEST && ( CurrentDirection == GameDirection::NORTH || CurrentDirection == GameDirection::SOUTH ) )
        {
          CurrentDirection = GameDirection::WEST;
        }

        CombatantState::States newstate = CombatantState::WALKING;
        if( (CurrentDirection == GameDirection::NORTH && (directionheld & Controller::SOUTH) == Controller::SOUTH)
          || (CurrentDirection == GameDirection::EAST && (directionheld & Controller::WEST) == Controller::WEST)
          || (CurrentDirection == GameDirection::SOUTH && (directionheld & Controller::NORTH) == Controller::NORTH)
          || (CurrentDirection == GameDirection::WEST && (directionheld & Controller::EAST) == Controller::EAST) )
        {
          newstate = CombatantState::WALKINGBACKWARDS;
        }
        SetNewState( newstate );

			} else if( directionheld != 0 && buttonsheld != 0 ) {

			  if( CurrentState == CombatantState::WALKING )
        {
          if( GetSecondaryControllerState() == Controller::NONE )
          {
            SetNewState( CombatantState::LONGJUMP );
          } else {
            SetNewState( CombatantState::SHORTJUMP );
          }
        }

				if( CurrentState == CombatantState::WALKINGBACKWARDS )
				{
					SetNewState( CombatantState::ROLLING );
				}

				if( CurrentState == CombatantState::STANDING )
				{
					// Only one direction pressed
					if( GetPrimaryControllerState() == Controller::NONE || GetSecondaryControllerState() == Controller::NONE )
					{
						if( (directionheld & Controller::NORTH) == Controller::NORTH )
						{
							SetNewState( CombatantState::JAB );
						}
						if( (directionheld & Controller::EAST) == Controller::EAST )
						{
							SetNewState( CombatantState::LUNGE );
						}
						if( (directionheld & Controller::SOUTH) == Controller::SOUTH )
						{
							SetNewState( CombatantState::KICK );
						}
						if( (directionheld & Controller::WEST) == Controller::WEST )
						{
							SetNewState( CombatantState::SLASH );
						}
					} else {
						// Diagonals

						if( (directionheld & Controller::SOUTH) == Controller::SOUTH )
						{
							SetNewState( CombatantState::PICKUP );
						} else {
							SetNewState( CombatantState::BLOCK_IN );
						}

					}
				}


			} else if( buttonsheld != 0 ) {

				if( CurrentState != CombatantState::BLOCK )
				{
					SetNewState( CombatantState::STANDING );
				}

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
      switch( GetPrimaryControllerState() )
      {
      case Controller::NORTH:
          switch( GetSecondaryControllerState() )
          {
            case Controller::EAST:
              ProposeMove( ScreenX + 4, ScreenY );
              break;
            case Controller::WEST:
							ProposeMove( ScreenX + 2, ScreenY - 2 );
              break;
            default:
							ProposeMove( ScreenX + 4, ScreenY - 1 );
              break;
          }
          break;

        case Controller::EAST:
          switch( GetSecondaryControllerState() )
          {
            case Controller::NORTH:
              ProposeMove( ScreenX + 4, ScreenY );
              break;
            case Controller::SOUTH:
              ProposeMove( ScreenX + 2, ScreenY + 2 );
              break;
            default:
              ProposeMove( ScreenX + 4, ScreenY + 1 );
              break;
          }
          break;

        case Controller::SOUTH:
          switch( GetSecondaryControllerState() )
          {
            case Controller::EAST:
              ProposeMove( ScreenX - 2, ScreenY + 2 );
              break;
            case Controller::WEST:
              ProposeMove( ScreenX - 4, ScreenY );
              break;
            default:
              ProposeMove( ScreenX - 4, ScreenY + 1 );
              break;
          }
          break;

        case Controller::WEST:
          switch( GetSecondaryControllerState() )
          {
            case Controller::NORTH:
              ProposeMove( ScreenX - 2, ScreenY - 2 );
              break;
            case Controller::SOUTH:
              ProposeMove( ScreenX - 4, ScreenY );
              break;
            default:
              ProposeMove( ScreenX - 4, ScreenY - 1 );
              break;
          }
          break;
      }

    }

    if( CurrentState == CombatantState::FALLING )
    {
      if( world_z > world_zone->WorldZ )
      {
        world_z -= 4;
        ScreenY += 4;
      } else {
        world_z = world_zone->WorldZ;
        SetNewState( CombatantState::FALLING_LANDED );
        ProposeMove( ScreenX, ScreenY );
      }
    }

    if( CurrentState == CombatantState::SINKING )
    {
      sinking_depth += 4;
      if( sinking_depth >= 42 )
      {
        // Check if transporting first
        SetNewState( CombatantState::DEAD );
        ScreenX = -60;
        return;
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
	  if( CurrentState == CombatantState::FALLING || CurrentState == CombatantState::LONGJUMP || CurrentState == CombatantState::SHORTJUMP )
    {
      SkinGraphic->DrawPartial( 0, 168, 32, 42, ScreenX - 16, ScreenY - 40 + world_z - world_zone->WorldZ, 0 );
    } else {
      SkinGraphic->DrawPartial( 0, 168, 32, 42, ScreenX - 16, ScreenY - 40, 0 );
    }
	}

  if( CurrentState == CombatantState::SINKING )
  {
    if( sinking_depth < 42 )
    {
      SkinGraphic->DrawPartial( (framenumber % 13) * 32, (framenumber / 13) * 42, 32, 42 - sinking_depth, ScreenX - 16, ScreenY - 40 - zpos + sinking_depth, flipflag );
    }
  } else {
    SkinGraphic->DrawPartial( (framenumber % 13) * 32, (framenumber / 13) * 42, 32, 42, ScreenX - 16, ScreenY - 40 - zpos, flipflag );
  }
}

void Combatant::OnUpdateMagic()
{
	if( CollectedMagicRemaining > 0 )
	{
		if( !UnlimitedMagic )
		{
			CollectedMagicRemaining--;
		}
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
	sinking_depth = 0;

	if( CombatantState::StateList[CurrentState]->LockControls )
  {
    Controls->LockControls();
  } else {
    Controls->UnlockControls();
  }
}

Controller::ControllerStateFlags Combatant::GetPrimaryControllerState()
{
  if( (CurrentDirection == GameDirection::NORTH || CurrentDirection == GameDirection::SOUTH) && (Controls->GetState() & (Controller::NORTH | Controller::SOUTH)) != Controller::NONE )
  {
    return (Controller::ControllerStateFlags)(Controls->GetState() & (Controller::NORTH | Controller::SOUTH));
  }
  if( (CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST) && (Controls->GetState() & (Controller::EAST | Controller::WEST)) != Controller::NONE )
  {
    return (Controller::ControllerStateFlags)(Controls->GetState() & (Controller::EAST | Controller::WEST));
  }
  return Controller::NONE;
}

Controller::ControllerStateFlags Combatant::GetSecondaryControllerState()
{
  if( (CurrentDirection == GameDirection::NORTH || CurrentDirection == GameDirection::SOUTH) && (Controls->GetState() & (Controller::EAST | Controller::WEST)) != Controller::NONE )
  {
    return (Controller::ControllerStateFlags)(Controls->GetState() & (Controller::EAST | Controller::WEST));
  }
  if( (CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST) && (Controls->GetState() & (Controller::NORTH | Controller::SOUTH)) != Controller::NONE )
  {
    return (Controller::ControllerStateFlags)(Controls->GetState() & (Controller::NORTH | Controller::SOUTH));
  }
  return Controller::NONE;
}

void Combatant::SetRoomZone(RoomZone* CurrentZone, bool IsWarped)
{
  CurrentRoomID = CurrentZone->InRoomID;
	world_zone = CurrentZone;
	if( IsWarped )
	{
		world_z = CurrentZone->WorldZ;
	}
	// TODO: Run Enter Script

  if( world_z > CurrentZone->WorldZ && CurrentState != CombatantState::LONGJUMP && CurrentState != CombatantState::SHORTJUMP )
  {
    SetNewState( CombatantState::FALLING );
    return;
  }
}

void Combatant::ProposeMove( int ScreenX, int ScreenY )
{
	if( ZoneClipping )
  {
    // Clipping
    this->ScreenX = ScreenX;
    this->ScreenY = ScreenY;
    return;
  }

  // Check if we're in a valid place
	if( world_zone == nullptr )
	{
		return;
	}

  // Can we locate the next zone?
	RoomZone* z = world_zone->InRoom->FindZoneForPoint( ScreenX, ScreenY );
	if( z != nullptr )
	{
    // Drowning time
    if( z->DrowningZone && CurrentState != CombatantState::LONGJUMP && CurrentState != CombatantState::SHORTJUMP && z->WorldZ == world_z )
    {
      SetNewState( CombatantState::SINKING );
      return;
    }

		if( z == world_zone )
		{
			// Same zone
			this->ScreenX = ScreenX;
			this->ScreenY = ScreenY;

      // Check for falling
      if( world_z > z->WorldZ && CurrentState != CombatantState::LONGJUMP && CurrentState != CombatantState::SHORTJUMP )
      {
        SetNewState( CombatantState::FALLING );
      }

		} else {

      bool allowMove = true;

      // Can't magically walk up cliffs
      if( z->WorldZ > world_z )
      {
        allowMove = false;
      }

			// Got to roll into here
			if( z->RollingZone && CurrentState != CombatantState::ROLLING )
      {
        allowMove = false;
      }

      if( z->WorldZ < world_z )
      {
        if( z->ClimbingZone )
        {
          // TODO: Handle climbing down
          allowMove = false;
        }
      }

      if( z->TransportZone && (z->TransportRequiresItemHeld < 0 || z->TransportRequiresItemHeld == item_held_index) )
      {
        // TODO: Handle "animated" transports

        Room* tr = GameResources::GameWorld->Rooms.at( z->TransportRoomID );
        RoomZone* tz = tr->FindZoneForPoint( z->TransportScreenX, z->TransportScreenY );
        this->ScreenX = z->TransportScreenX;
        this->ScreenY = z->TransportScreenY;
        if( z->TransportFacing != GameDirection::UNCHANGED )
        {
          CurrentDirection == z->TransportFacing;
        }
        if( z->TransportClearInput )
        {
          Controls->ClearState();
        }
        SetRoomZone( tz, true );
        allowMove = false;

      }

      // TODO: Check valid to enter, then handle any conditions

      if( allowMove )
      {
        this->ScreenX = ScreenX;
        this->ScreenY = ScreenY;
        SetRoomZone( z, false );
      }

		}
	}
}
