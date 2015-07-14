
#include "roomdesigner_mode.h"
#include "roomdesigner.h"

void RoomDesignerModule::Init(RoomDesigner* Designer, Room* Working, ALLEGRO_FONT* Font)
{
	workingroom = Working;
	designer = Designer;
	textfont = Font;
}

void RoomDesignerModule::AddLogText(std::string Text)
{
	designer->AddLogText( Text );
}
