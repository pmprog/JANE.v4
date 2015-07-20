
#include "panel.h"
#include "../../Framework/Primitives/strings.h"

Panel::Panel()
{
  ObjectGraphicIndex = 0;
  ScreenX = 0;
  ScreenY = 0;
  FlipHorizontal = false;
  FlipVertical = false;
  BackgroundAtY = 0;
  for( int i = 0; i < 16; i++ )
  {
    ColourRemap[i] = i;
  }
	Visible = true;
}

bool Panel::SortingPredicate(const Panel* A, const Panel* B)
{
  if( A->BackgroundAtY == B->BackgroundAtY )
  {
    // If ForegroundY is the same, the lesser is futher up the screen
    return ( A->ScreenY < B->ScreenY );
  } else if( A->BackgroundAtY < B->BackgroundAtY ) {
    // If ForegroundY greater (harder for combatants to be in front), then it's more "background"
    return true;
  }
  return false;
}


void Panel::Load(ConfigFile* DataFile, std::string KeyPrefix)
{
  ObjectGraphicIndex = DataFile->GetQuickIntegerValue( KeyPrefix, 0, ObjectGraphicIndex );
  for( int i = 0; i < 16; i++ )
  {
    ColourRemap[i] = DataFile->GetQuickIntegerValue( KeyPrefix, i + 1, ColourRemap[i] );
  }
  ScreenX = DataFile->GetQuickIntegerValue( KeyPrefix, 17, ScreenX );
  ScreenY = DataFile->GetQuickIntegerValue( KeyPrefix, 18, ScreenY );
  FlipHorizontal = DataFile->GetQuickBooleanValue( KeyPrefix, 19, FlipHorizontal );
  FlipVertical = DataFile->GetQuickBooleanValue( KeyPrefix, 20, FlipVertical );
  BackgroundAtY = DataFile->GetQuickIntegerValue( KeyPrefix, 21, BackgroundAtY );
	Visible = DataFile->GetQuickBooleanValue( KeyPrefix, 22, Visible );
}

void Panel::Save(ConfigFile* DataFile, std::string KeyPrefix)
{
  DataFile->SetIntegerValue( KeyPrefix, 0, ObjectGraphicIndex );
  for( int i = 0; i < 16; i++ )
  {
    DataFile->SetIntegerValue( KeyPrefix, i + 1, ColourRemap[i] );
  }
  DataFile->SetIntegerValue( KeyPrefix, 17, ScreenX );
  DataFile->SetIntegerValue( KeyPrefix, 18, ScreenY );
  DataFile->SetBooleanValue( KeyPrefix, 19, FlipHorizontal );
  DataFile->SetBooleanValue( KeyPrefix, 20, FlipVertical );
  DataFile->SetIntegerValue( KeyPrefix, 21, BackgroundAtY );
	DataFile->SetBooleanValue( KeyPrefix, 22, Visible );
}

void Panel::Load(SQLiteDB* Database, int GameID, int RoomID, int PanelID)
{
	std::string Query;
	int remapid = 0;

	ObjectGraphicIndex = Database->QueryIntegerValue( "SELECT GraphicID FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" );
	remapid = Database->QueryIntegerValue( "SELECT RemapID FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" );
	Query = "SELECT RemapID FROM ColourRemaps WHERE RemapID = " + Strings::FromNumber( remapid ) + ";";
	if( Database->RowExists( Query ) )
	{
		for( int i = 0; i < 16; i++ )
		{
			ColourRemap[i] = Database->QueryIntegerValue( "SELECT Colour" + Strings::FromNumber( i ) + " FROM ColourRemaps WHERE RemapID = " + Strings::FromNumber( remapid ) + ";" );
		}
	}
	ScreenX = Database->QueryIntegerValue( "SELECT ScreenX FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" );
	ScreenY = Database->QueryIntegerValue( "SELECT ScreenY FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" );
	FlipHorizontal = (Database->QueryIntegerValue( "SELECT FlipHorizontal FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" ) == 0 ? false : true );
	FlipVertical = (Database->QueryIntegerValue( "SELECT FlipVertical FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" ) == 0 ? false : true );
	BackgroundAtY = Database->QueryIntegerValue( "SELECT BackgroundAtY FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" );
	Visible = (Database->QueryIntegerValue( "SELECT Visible FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" ) == 0 ? false : true );
}

void Panel::Save(SQLiteDB* Database, int GameID, int RoomID, int PanelID)
{
	std::string Query;
	std::string QueryIns;
	int remapid = 0;

	Query = "SELECT RemapID FROM ColourRemaps WHERE ";
	for( int i = 0; i < 15; i++ )
	{
		Query += "Colour" + Strings::FromNumber( i ) + " = " + Strings::FromNumber( ColourRemap[i] ) + " AND ";
	}
	Query += "Colour15 = " + Strings::FromNumber( ColourRemap[15] ) + ";";
	if( Database->RowExists( Query ) )
	{
		remapid = Database->QueryIntegerValue( Query );
	} else {
		QueryIns = "INSERT INTO ColourRemaps ( ";
		for( int i = 0; i < 16; i++ )
		{
			if( i != 0 )
			{
				QueryIns += ", ";
			}
			QueryIns += "Colour" + Strings::FromNumber( i );
		}
		QueryIns += ") SELECT ";
		for( int i = 0; i < 16; i++ )
		{
			if( i != 0 )
			{
				QueryIns += ", ";
			}
			QueryIns += Strings::FromNumber( ColourRemap[i] );
		}
		Database->ExecuteStatement( QueryIns );
		remapid = Database->QueryIntegerValue( Query );
	}

	Database->ExecuteStatement("DELETE FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" );

	Query = "INSERT INTO Panels ( GameID, RoomID, PanelID, GraphicID, RemapID, ScreenX, ScreenY, FlipHorizontal, FlipVertical, BackgroundAtY, Visible ) ";
	Query += "SELECT " + Strings::FromNumber( GameID ) + ", " + Strings::FromNumber( RoomID ) + ", " + Strings::FromNumber( PanelID ) + ", ";
	Query += Strings::FromNumber( ObjectGraphicIndex ) + ", " + Strings::FromNumber( remapid ) + ", " + Strings::FromNumber( ScreenX ) + ", ";
	Query += Strings::FromNumber( ScreenY ) + ", " + Strings::FromNumber( (FlipHorizontal ? 1 : 0) ) + ", " + Strings::FromNumber( (FlipVertical ? 1 : 0) ) + ", ";
	Query += Strings::FromNumber( BackgroundAtY ) + ", " + Strings::FromNumber( (Visible ? 1 : 0) ) + ";";
	Database->ExecuteStatement( Query );
}
