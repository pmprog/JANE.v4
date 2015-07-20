
#include "world.h"
#include "../../Framework/Primitives/strings.h"
#include "room.h"

World::World( std::string Filename, int GameID )
{
  std::string workingkey;

  DataFilename = Filename;
	gameid = GameID;

	//gamedb = new SQLiteDB( Filename.c_str() );
	//if( gamedb->ValidDB )
	//{
	//	// Load from SQLiteDB

	//} else {
	//	// Data file isn't an SQLiteDB

		DataFile = new ConfigFile( Filename );

		int roomcount = DataFile->GetQuickIntegerValue( "World.RoomCount", 0 );
		Script_OnUpdate.clear();
		Script_OnUpdate.append( *DataFile->GetQuickStringValue( "World.OnUpdate", "" ) );

		for( int roomid = 0; roomid < roomcount; roomid++ )
		{
			Room* r = new Room();
			workingkey = "Room." + Strings::FromNumber(roomid);
			r->Load( DataFile, workingkey );
			Rooms.push_back( r );
		}
	//}

}

World::~World()
{
}

void World::Save()
{
  std::string workingkey;

  DataFile->SetIntegerValue("World.RoomCount", Rooms.size() );
  DataFile->SetStringValue( "World.OnUpdate", &Script_OnUpdate );

  int roomid = 0;
  for( std::vector<Room*>::const_iterator roomptr = Rooms.begin(); roomptr != Rooms.end(); roomptr++ )
  {
    Room* room = (Room*)*roomptr;
    workingkey = "Room." + Strings::FromNumber(roomid);
    room->Save( DataFile, workingkey );
    roomid++;
  }

  DataFile->Save( DataFilename );

	// DB only version

	gamedb = new SQLiteDB( "resources/jane4.gamedb" );

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
