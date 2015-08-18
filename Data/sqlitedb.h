
#pragma once

#include "sqlite3.h"
#include "../Framework/includes.h"

#ifndef Room
class Room;
#endif
#ifndef Panel
class Panel;
#endif
#ifndef RoomZone
class RoomZone;
#endif

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



		void LoadRoom(int GameID, int RoomID, Room* EditRoom);
		void LoadPanel(int GameID, int RoomID, int PanelID, Panel* EditPanel);
		void LoadZone(int GameID, int RoomID, int ZoneID, RoomZone* EditZone);

};

