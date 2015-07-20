
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

	Script_OnUpdate = gamedb->QueryStringValue( "SELECT OnUpdate FROM `World` WHERE GameID = " + Strings::FromNumber( GameID ) + ";" );

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

	gamedb = new SQLiteDB( DataFilename );

	gamedb->ExecuteStatement("DELETE FROM `World` WHERE GameID = " + Strings::FromNumber( gameid ) + ";");
	gamedb->ExecuteStatement("INSERT INTO `World` ( GameID, GameName, OnUpdate ) SELECT " + Strings::FromNumber( gameid ) + ", 'JANE v4', '" + Strings::Replace( Script_OnUpdate, "'", "''" ) + "';");
	
  roomid = 0;
  for( std::vector<Room*>::const_iterator roomptr = Rooms.begin(); roomptr != Rooms.end(); roomptr++ )
  {
    Room* room = (Room*)*roomptr;
    room->Save( gamedb, gameid, roomid );
    roomid++;
  }
	delete gamedb;
}
