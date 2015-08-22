
#include "roomzone.h"
#include "room.h"
#include "../../Framework/Primitives/strings.h"

RoomZone::RoomZone( Room* Owner )
{
	Area = new Polyhedron();
	Script_OnCombatantEnter = "";
	Script_OnCombatantLeave = "";
	WorldZ = 0;
	ClimbingZone = false;
	ClimbingUpperZ = 0;
	ClimbingRequiresItemHeld = -1;
	ClimbingFace = GameDirection::NORTH;
	DrowningZone = false;
	TransportZone = false;
	TransportMode = RoomZoneTeleportMode::INSTANT;
	TransportRoomID = 0;
	TransportScreenX = 0;
	TransportScreenY = 0;
	TransportFacing = GameDirection::UNCHANGED;
	TransportClearInput = false;
	TransportRequiresItemHeld = -1;
	RollingZone = false;

}

RoomZone::~RoomZone()
{

}

void RoomZone::Load(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void RoomZone::Save(ConfigFile* DataFile, std::string KeyPrefix)
{

}

void RoomZone::Load(SQLiteDB* Database, int GameID, int RoomID, int ZoneID)
{
	Database->LoadZone( GameID, RoomID, ZoneID, this );
}

void RoomZone::Save(SQLiteDB* Database, int GameID, int RoomID, int ZoneID)
{
	std::string Query;

	if( !Database->RowExists("SELECT * FROM `Zones` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND ZoneID = " + Strings::FromNumber( ZoneID ) + ";" ) )
	{
		Query = "INSERT INTO `Zones` ( GameID, RoomID, ZoneID, OnCombatantEnter, OnCombatantLeave, WorldZ,";
		Query += "  ClimbingZone, ClimbingUpperZ, ClimbingRequiresItemHeld, ClimbingFace, DrowningZone,";
		Query += "  TransportZone, TransportMode, TransportRoomID, TransportScreenX, TransportScreenY, TransportFacing, TransportClearInput, TransportRequiresItemHeld,";
		Query += "  RollingZone ) ";
		Query += "SELECT " + Strings::FromNumber( GameID ) + ", " + Strings::FromNumber( RoomID ) + ", " + Strings::FromNumber( ZoneID ) + ", ";
		Query += "'" + Strings::Replace( Script_OnCombatantEnter, "'", "''" ) + "', '" + Strings::Replace( Script_OnCombatantLeave, "'", "''" ) + "', " + Strings::FromNumber( WorldZ ) + ", ";
		Query += Strings::FromNumber( (ClimbingZone ? 1 : 0) ) + ", " + Strings::FromNumber( ClimbingUpperZ ) + ", " + Strings::FromNumber( ClimbingRequiresItemHeld ) + ", " + Strings::FromNumber( ClimbingFace ) + ", " + Strings::FromNumber( (DrowningZone ? 1 : 0) ) + ", ";
		Query += Strings::FromNumber( (TransportZone ? 1 : 0) ) + ", " + Strings::FromNumber( TransportMode ) + ", " + Strings::FromNumber( TransportRoomID ) + ", " + Strings::FromNumber( TransportScreenX ) + ", " + Strings::FromNumber( TransportScreenY ) + ", ";
		Query += Strings::FromNumber( TransportFacing ) + ", " + Strings::FromNumber( TransportClearInput ) + ", " + Strings::FromNumber( TransportRequiresItemHeld ) + ", ";
		Query += Strings::FromNumber( (RollingZone ? 1 : 0) ) + ";";
	} else {
		Query = "UPDATE `Zones` ";
		Query += "SET OnCombatantEnter = '" + Strings::Replace( Script_OnCombatantEnter, "'", "''" ) + "', ";
		Query += " OnCombatantLeave = '" + Strings::Replace( Script_OnCombatantLeave, "'", "''" ) + "', ";
		Query += " WorldZ = " + Strings::FromNumber( WorldZ ) + ", ";
		Query += " ClimbingZone = " + Strings::FromNumber( (ClimbingZone ? 1 : 0 ) ) + ", ";
		Query += " ClimbingUpperZ = " + Strings::FromNumber( ClimbingUpperZ ) + ", ";
		Query += " ClimbingRequiresItemHeld = " + Strings::FromNumber( ClimbingRequiresItemHeld ) + ", ";
		Query += " ClimbingFace = " + Strings::FromNumber( ClimbingFace ) + ", ";
		Query += " DrowningZone = " + Strings::FromNumber( (DrowningZone ? 1 : 0 ) ) + ", ";
		Query += " TransportZone = " + Strings::FromNumber( (TransportZone ? 1 : 0 ) ) + ", ";
		Query += " TransportMode = " + Strings::FromNumber( TransportMode ) + ", ";
		Query += " TransportRoomID = " + Strings::FromNumber( TransportRoomID ) + ", ";
		Query += " TransportScreenX = " + Strings::FromNumber( TransportScreenX ) + ", ";
		Query += " TransportScreenY = " + Strings::FromNumber( TransportScreenY ) + ", ";
		Query += " TransportFacing = " + Strings::FromNumber( TransportFacing ) + ", ";
		Query += " TransportClearInput = " + Strings::FromNumber( TransportClearInput ) + ", ";
		Query += " TransportRequiresItemHeld = " + Strings::FromNumber( TransportRequiresItemHeld ) + ", ";
		Query += " RollingZone = " + Strings::FromNumber( (RollingZone ? 1 : 0 ) ) + " ";
		Query += "WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND ZoneID = " + Strings::FromNumber( ZoneID ) + ";";
	}
	Database->ExecuteStatement( Query );

	for( int i = 0; i < Area->Points->count; i++ )
  {
    Vector2* v = (Vector2*)Area->Points->ItemAt( i );

    if( !Database->RowExists("SELECT * FROM `ZonePoints` WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND ZoneID = " + Strings::FromNumber( ZoneID ) + " AND PointID = " + Strings::FromNumber( i ) + ";" ) )
    {
      Query = "INSERT INTO `ZonePoints` ( GameID, RoomID, ZoneID, PointID, X, Y ) ";
      Query += "SELECT " + Strings::FromNumber( GameID ) + ", " + Strings::FromNumber( RoomID ) + ", " + Strings::FromNumber( ZoneID ) + ", " + Strings::FromNumber( i );
      Query += ", " + Strings::FromNumber( v->X ) + ", " + Strings::FromNumber( v->Y ) + ";";
    } else {
      Query = "UPDATE `ZonePoints` ";
      Query += "SET X = " + Strings::FromNumber( v->X ) + ", ";
      Query += " Y = " + Strings::FromNumber( v->Y ) + " ";
      Query += "WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND ZoneID = " + Strings::FromNumber( ZoneID ) + " AND PointID = " + Strings::FromNumber( i ) + ";";
    }
    Database->ExecuteStatement( Query );
  }
  Query = "DELETE FROM `ZonePoints` ";
  Query += "WHERE GameID = " + Strings::FromNumber( GameID ) + " AND RoomID = " + Strings::FromNumber( RoomID ) + " AND ZoneID = " + Strings::FromNumber( ZoneID ) + " AND PointID >= " + Strings::FromNumber( Area->Points->count ) + ";";
  Database->ExecuteStatement( Query );
}
