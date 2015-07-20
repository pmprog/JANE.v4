
#include "sqlitedb.h"

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
