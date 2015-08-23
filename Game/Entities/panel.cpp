
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

void Panel::Load(SQLiteDB* Database, int GameID, int RoomID, int PanelID)
{
	std::string Query;
	int remapid = 0;

	Database->LoadPanel( GameID, RoomID, PanelID, this );

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

	if( !Database->RowExists("SELECT * FROM `Panels` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";" ) )
	{
		Query = "INSERT INTO Panels ( GameID, RoomID, PanelID, GraphicID, RemapID, ScreenX, ScreenY, FlipHorizontal, FlipVertical, BackgroundAtY, Visible ) ";
		Query += "SELECT " + Strings::FromNumber( GameID ) + ", " + Strings::FromNumber( RoomID ) + ", " + Strings::FromNumber( PanelID ) + ", ";
		Query += Strings::FromNumber( ObjectGraphicIndex ) + ", " + Strings::FromNumber( remapid ) + ", " + Strings::FromNumber( ScreenX ) + ", ";
		Query += Strings::FromNumber( ScreenY ) + ", " + Strings::FromNumber( (FlipHorizontal ? 1 : 0) ) + ", " + Strings::FromNumber( (FlipVertical ? 1 : 0) ) + ", ";
		Query += Strings::FromNumber( BackgroundAtY ) + ", " + Strings::FromNumber( (Visible ? 1 : 0) ) + ";";
	} else {
		Query = "UPDATE Panels ";
		Query += "SET GraphicID = " + Strings::FromNumber( ObjectGraphicIndex ) + ", ";
		Query += " RemapID = " + Strings::FromNumber( remapid ) + ", ";
		Query += " ScreenX = " + Strings::FromNumber( ScreenX ) + ", ";
		Query += " ScreenY = " + Strings::FromNumber( ScreenY ) + ", ";
		Query += " FlipHorizontal = " + Strings::FromNumber( (FlipHorizontal ? 1 : 0 ) ) + ", ";
		Query += " FlipVertical = " + Strings::FromNumber( (FlipVertical ? 1 : 0 ) ) + ", ";
		Query += " BackgroundAtY = " + Strings::FromNumber( BackgroundAtY ) + ", ";
		Query += " Visible = " + Strings::FromNumber( (Visible ? 1 : 0 ) ) + " ";
		Query += "WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";";
	}
	Database->ExecuteStatement( Query );
}
