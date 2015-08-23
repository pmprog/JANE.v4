
#include "roomdesigner_mode.h"
#include "roomdesigner.h"

void RoomDesignerModule::Init(RoomDesigner* Designer, Room* Working, int WorkingRoomIndex, ALLEGRO_FONT* Font)
{
  workingroomindex = WorkingRoomIndex;
	workingroom = Working;
	designer = Designer;
	textfont = Font;
}

void RoomDesignerModule::AddLogText(std::string Text)
{
	designer->AddLogText( Text );
}
