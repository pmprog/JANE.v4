
#include "world.h"
#include "../../Framework/Primitives/strings.h"
#include "room.h"

World::World( std::string Filename, int GameID )
{
  std::string workingkey;

  DataFilename = Filename;
	gameid = GameID;

	gamedb = new SQLiteDB( Filename.c_str() );

	int counter;

	// Load Combatant Skins
	counter = gamedb->QueryIntegerValue( "SELECT COUNT(*) FROM `CombatantSkins`;" );
  for( int i = 1; i <= counter; i++ )
  {
		std::string filename = gamedb->QueryStringValue( "SELECT Filename FROM `CombatantSkins` WHERE SkinID = " + Strings::FromNumber( i ) + ";" );
		ALLEGRO_BITMAP* skin = BitmapCache::LoadBitmap(filename);
		Palette::ApplyColourOverrides( skin );
		GameResources::CombatantGraphics.push_back( new PalettedBitmap( skin ) );
  }

	// Load Panel Sheets
	GameResources::ObjectGraphics = new PanelSheet();
	counter = gamedb->QueryIntegerValue( "SELECT COUNT(*) FROM `PanelSheets`;" );
  for( int i = 1; i <= counter; i++ )
  {
		std::string filename = gamedb->QueryStringValue( "SELECT Filename FROM `PanelSheets` WHERE SheetID = " + Strings::FromNumber( i ) + ";" );
		Palette::ApplyColourOverrides( BitmapCache::LoadBitmap(filename) );
		GameResources::ObjectGraphics->AddFromFile( filename );
  }

	Script_OnUpdate = gamedb->QueryStringValue( "SELECT OnUpdate FROM `World` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );

	Start_RoomID = gamedb->QueryIntegerValue( "SELECT StartRoomID FROM `World` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );
	Start_ScreenX = gamedb->QueryIntegerValue( "SELECT StartX FROM `World` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );
	Start_ScreenY = gamedb->QueryIntegerValue( "SELECT StartY FROM `World` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );
	Start_Facing = gamedb->QueryIntegerValue( "SELECT StartFacing FROM `World` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );

	TotalRooms = gamedb->QueryIntegerValue( "SELECT COUNT(*) FROM `Rooms` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );
  for( int i = 0; i < TotalRooms; i++ )
  {
    Room* r = new Room();
    r->Load( gamedb, GameID, i );
    Rooms.push_back( r );
  }

}

World::~World()
{
}

void World::Save()
{
	int roomid;
	std::string query;

	//gamedb->ExecuteStatement("DELETE FROM `World` WHERE GameID = " + Strings::FromNumber( gameid ) + ";");

	query = "INSERT INTO `World` ( GameID, GameName, OnUpdate, Start_RoomID, Start_ScreenX, Start_ScreenY, Start_Facing ) ";
	query += "SELECT " + Strings::FromNumber( gameid ) + ", 'JANE v4', '" + Strings::Replace( Script_OnUpdate, "'", "''" ) + "', ";
	query += Strings::FromNumber( Start_RoomID ) + ", " + Strings::FromNumber( Start_ScreenX ) + ", " + Strings::FromNumber( Start_ScreenY ) + ", " + Strings::FromNumber( Start_Facing ) + ";";
	//gamedb->ExecuteStatement( query );

  roomid = 0;
  for( std::vector<Room*>::const_iterator roomptr = Rooms.begin(); roomptr != Rooms.end(); roomptr++ )
  {
    Room* room = (Room*)*roomptr;
    room->Save( gamedb, gameid, roomid );
    roomid++;
  }
}

void World::SaveRoom( int RoomID )
{
	Rooms.at( RoomID )->Save( gamedb, gameid, RoomID );
}
