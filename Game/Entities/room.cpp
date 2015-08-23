
#include "room.h"
#include "../resources.h"
#include "../../Framework/Primitives/strings.h"

Room::Room()
{
  SetDefaults();
}

void Room::SetDefaults()
{
  RoomTime = 0;
  BackgroundColour = 13;
  Enemy = nullptr;
  FullyRendered = false;
}

Room::~Room()
{

}

void Room::ResetRoomTime()
{
	RoomTime = 0;
	FullyRendered = false;
}

void Room::OnEnter()
{
  RoomTime = 0;
  FullyRendered = false;
  if( Script_OnCombatantEnter != "" )
  {
    GameResources::Scripting->ExecuteAsync( Script_OnCombatantEnter );
  }
}

void Room::OnLeave()
{
  if( Script_OnCombatantLeave != "" )
  {
    GameResources::Scripting->ExecuteAsync( Script_OnCombatantLeave );
  }
}

void Room::Update()
{
  RoomTime++;

  if( RoomTime >= FRAMEWORK->GetFramesPerSecond() )
  {
    FullyRendered = true;
  }

  if( Script_OnUpdate != "" )
  {
    GameResources::Scripting->ExecuteAsync( Script_OnUpdate );
  }
}

void Room::Render(int FromY, int ToY)
{
	Render( 0, 0, FromY, ToY );
}

void Room::Render(int RenderOffsetX, int RenderOffsetY, int FromY, int ToY)
{
  int panelstodraw = Panels.size();

  // Always 2 seconds to render a room
  if( !FullyRendered )
  {
    float c = (float)RoomTime / (float)FRAMEWORK->GetFramesPerSecond(); // % through the build
    c *= Panels.size(); // Number of panels to draw at this percentage
    panelstodraw = (int)c;
  }

  for( int idx = 0; idx < panelstodraw; idx++ )
  {
    Panel* p = Panels.at( idx );
    if( p->BackgroundAtY >= FromY && p->BackgroundAtY <= ToY )
    {
      PalettedBitmap* b = GameResources::ObjectGraphics->GetPanel( p->ObjectGraphicIndex );
      b->SetOverrides( p->ColourRemap );
      b->Draw( RenderOffsetX + p->ScreenX, RenderOffsetY + p->ScreenY, (p->FlipHorizontal ? ALLEGRO_FLIP_HORIZONTAL : 0) | (p->FlipVertical ? ALLEGRO_FLIP_VERTICAL : 0) );
    }
  }

}

int Room::SortPanels()
{
  return SortPanels( Panels.size() - 1 );
}

int Room::SortPanels(int Current)
{
  Panel* p = Panels.at( Current );
  std::sort( Panels.begin(), Panels.end(), Panel::SortingPredicate );
  for(int i = 0; i < Panels.size(); i++ )
  {
    if( Panels.at( i ) == p )
    {
      return i;
    }
  }
  return 0;
}

void Room::Load(SQLiteDB* Database, int GameID, int RoomID)
{
	int counter;

	this->RoomID = RoomID;

	Database->LoadRoom( GameID, RoomID, this );

	counter = Database->QueryIntegerValue( "SELECT COUNT(*) FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + ";" );
  for( int i = 0; i < counter; i++ )
  {
    Panel* p = new Panel();
    p->Load( Database, GameID, RoomID, i );
    Panels.push_back( p );
  }

	counter = Database->QueryIntegerValue( "SELECT COUNT(*) FROM `Zones` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + ";" );
  for( int i = 0; i < counter; i++ )
  {
    RoomZone* z = new RoomZone( this, RoomID );
    z->Load( Database, GameID, RoomID, i );
    Zones.push_back( z );
  }

	// TODO: Load Enemy
}

void Room::Save(SQLiteDB* Database, int GameID, int RoomID)
{
	std::string Query;

	if( !Database->RowExists("SELECT * FROM Rooms WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + ";" ) )
	{
		Query = "INSERT INTO Rooms ( GameID, RoomID, BackgroundColour, OnCombatantEnter, OnCombatantLeave, OnUpdate ) ";
		Query += "SELECT " + Strings::FromNumber( GameID ) + ", " + Strings::FromNumber( RoomID ) + ", " + Strings::FromNumber( BackgroundColour ) + ", ";
		Query += "'" + Strings::Replace( Script_OnCombatantEnter, "'", "''" ) + "', '" + Strings::Replace( Script_OnCombatantLeave, "'", "''" ) + "', '" + Strings::Replace( Script_OnUpdate, "'", "''" ) + "';";
	} else {
		Query = "UPDATE Rooms ";
		Query += "SET BackgroundColour = " + Strings::FromNumber( BackgroundColour ) + ", ";
		Query += " OnCombatantEnter = '" + Strings::Replace( Script_OnCombatantEnter, "'", "''" ) + "', ";
		Query += " OnCombatantLeave = '" + Strings::Replace( Script_OnCombatantLeave, "'", "''" ) + "', ";
		Query += " OnUpdate = '" + Strings::Replace( Script_OnUpdate, "'", "''" ) + "' ";
		Query += "WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + ";";
	}
	Database->ExecuteStatement( Query );


  for(int i = 0; i < Panels.size(); i++ )
  {
    Panels.at( i )->Save( Database, GameID, RoomID, i );
  }
  for(int i = 0; i < Zones.size(); i++ )
  {
    Zones.at( i )->Save( Database, GameID, RoomID, i );
  }
  if( Enemy != nullptr )
  {
    Enemy->Save( Database, GameID, RoomID );
  }
}

RoomZone* Room::FindZoneForPoint(int X, int Y)
{
	RoomZone* z;

	for( int zidx = Zones.size() - 1; zidx >= 0; zidx-- )
	{
		z = Zones.at(zidx);
		if( z->Area->HitTest( new Vector2( X, Y ) ) )
		{
			return z;
		}
	}
	return nullptr;
}
