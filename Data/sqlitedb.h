
#pragma once

#include "sqlite3.h"
#include "../Framework/includes.h"

class SQLiteDB
{

	private:
		sqlite3* database;
		std::string filename;

		static int callback(void*,int,char**,char**);

	public:
		bool ValidDB;

		SQLiteDB( std::string Filename );
		~SQLiteDB();

		void ExecuteStatement( std::string Statement );
		int QueryIntegerValue( std::string Statement );
		std::string QueryStringValue( std::string Statement );
		bool RowExists( std::string Statement );

};

