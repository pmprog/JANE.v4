
#include "combatant.h"

Combatant::Combatant()
{
	SkinFilename = "resources/ninja.png";
	SkinGraphic = new PalettedBitmap( SkinFilename );
	SkinRenderStyle = CombatantRenderStyle::HUMAN;
	for( int c = 0; c < 16; c++ )
	{
		ColourRemap[c] = c;
	}

	CurrentState = CombatantState::STANDING;
	CurrentStateTime = 0;

	magicrampindex = 0;
	magicrampdelay = 0;
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
	OnUpdateMagic();
}

void Combatant::OnRender()
{
	int framenumber = 0;
	int flipflag = 0;

	// TODO: Get frame number
	switch( CurrentState )
	{
		case CombatantState::STANDING:
			break;
		case CombatantState::WALKING:
			break;
		case CombatantState::SHORTJUMP:
			break;
		case CombatantState::LONGJUMP:
			break;
		case CombatantState::CLIMBINGUP:
			break;
		case CombatantState::CLIMBINGDOWN:
			break;
		case CombatantState::JAB:
			break;
		case CombatantState::SLASH:
			break;
		case CombatantState::LUNGE:
			break;
		case CombatantState::KICK:
			break;
		case CombatantState::BLOCK:
			break;
		case CombatantState::PICKUP:
			break;
		case CombatantState::WEAPONCHANGE_IN:
			break;
		case CombatantState::WEAPONCHANGE_OUT:
			break;
		case CombatantState::KNEELING:
			break;
		case CombatantState::SINKING:
			break;
		default:
			break;
	}

	// Flip
	if( CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::WEST )
	{
		flipflag = ALLEGRO_FLIP_HORIZONTAL;
	}

	// Point to other direction
	if( framenumber < 26 && (CurrentDirection == GameDirection::EAST || CurrentDirection == GameDirection::SOUTH) )
	{
		framenumber += 26;
	}

	// Render
	SkinGraphic->DrawPartial( (framenumber % 13) * 32, (framenumber / 13) * 42, 32, 42, ScreenX, ScreenY, flipflag );
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