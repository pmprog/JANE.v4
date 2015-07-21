
#include "sqlitedb.h"
#include "../Framework/Primitives/strings.h"
#include "../Game/Entities/room.h"
#include "../Game/Entities/panel.h"

SQLiteDB::SQLiteDB( std::string Filename )
{
	ValidDB = false;
	filename = Filename;
	if( sqlite3_open( filename.c_str(), &database ) != SQLITE_OK )
	{
		database = nullptr;
		return;
	}
	ValidDB = true;
}

SQLiteDB::~SQLiteDB()
{
	if( database != nullptr )
	{
		sqlite3_close( database );
	}
}

void SQLiteDB::ExecuteStatement( std::string Statement )
{
	if( !ValidDB )
	{
		return;
	}

	sqlite3_exec( database, Statement.c_str(), nullptr, nullptr, nullptr );

}

int SQLiteDB::QueryIntegerValue( std::string Statement )
{
	if( !ValidDB )
	{
		return -1;
	}

	int returnval = 0;
	sqlite3_stmt *cmd;
	const char *tail;

	sqlite3_prepare( database, Statement.c_str(), Statement.length(), &cmd, &tail );

	if( sqlite3_step(cmd) == SQLITE_ROW )
	{
		returnval = sqlite3_column_int( cmd, 0 );
	}

	sqlite3_finalize( cmd );

	return returnval;
}

std::string SQLiteDB::QueryStringValue( std::string Statement )
{
	if( !ValidDB )
	{
		return "";
	}
	
	std::string returnval;
	sqlite3_stmt *cmd;
	const char *tail;

	sqlite3_prepare( database, Statement.c_str(), Statement.length(), &cmd, &tail );

	if( sqlite3_step(cmd) == SQLITE_ROW )
	{
		returnval.append( (const char*)sqlite3_column_text( cmd, 0 ) );
	}

	sqlite3_finalize( cmd );
	return returnval;
}

bool SQLiteDB::RowExists( std::string Statement )
{
	if( !ValidDB )
	{
		return false;
	}
	
	bool returnval = false;
	sqlite3_stmt *cmd;
	const char *tail;

	sqlite3_prepare( database, Statement.c_str(), Statement.length(), &cmd, &tail );

	if( sqlite3_step(cmd) == SQLITE_ROW )
	{
		returnval = true;
	}

	sqlite3_finalize( cmd );
	return returnval;
}

void SQLiteDB::LoadRoom(int GameID, int RoomID, Room* EditRoom)
{
	if( !ValidDB )
	{
		return;
	}
	
	std::string returnval;
	sqlite3_stmt *cmd;
	const char *tail;

	std::string Statement = "SELECT * FROM `Rooms` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + ";";

	sqlite3_prepare( database, Statement.c_str(), Statement.length(), &cmd, &tail );

	if( sqlite3_step(cmd) == SQLITE_ROW )
	{
		EditRoom->BackgroundColour = sqlite3_column_int( cmd, 2 );
		EditRoom->Script_OnCombatantEnter.clear();
		EditRoom->Script_OnCombatantEnter.append( (const char*)sqlite3_column_text( cmd, 3 ) );
		EditRoom->Script_OnCombatantLeave.clear();
		EditRoom->Script_OnCombatantLeave.append( (const char*)sqlite3_column_text( cmd, 4 ) );
		EditRoom->Script_OnUpdate.clear();
		EditRoom->Script_OnUpdate.append( (const char*)sqlite3_column_text( cmd, 5 ) );
	}

	sqlite3_finalize( cmd );
}

void SQLiteDB::LoadPanel(int GameID, int RoomID, int PanelID, Panel* EditPanel)
{
	if( !ValidDB )
	{
		return;
	}
	
	std::string returnval;
	sqlite3_stmt *cmd;
	const char *tail;

	std::string Statement = "SELECT * FROM `Panels` p LEFT JOIN `ColourRemaps` c ON p.RemapID = c.RemapID WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND PanelID = " + Strings::FromNumber( PanelID ) + ";";

	sqlite3_prepare( database, Statement.c_str(), Statement.length(), &cmd, &tail );

	if( sqlite3_step(cmd) == SQLITE_ROW )
	{
		EditPanel->ObjectGraphicIndex = sqlite3_column_int( cmd, 3 );
		EditPanel->ScreenX = sqlite3_column_int( cmd, 5 );
		EditPanel->ScreenY = sqlite3_column_int( cmd, 6 );
		EditPanel->FlipHorizontal = (sqlite3_column_int( cmd, 7 ) != 0);
		EditPanel->FlipVertical = (sqlite3_column_int( cmd, 8 ) != 0);
		EditPanel->BackgroundAtY = sqlite3_column_int( cmd, 9 );
		EditPanel->Visible = (sqlite3_column_int( cmd, 10 ) != 0);

		for( int i = 0; i < 16; i++ )
		{
			EditPanel->ColourRemap[i] = sqlite3_column_int( cmd, 12 + i );
		}
	}

	sqlite3_finalize( cmd );
}
