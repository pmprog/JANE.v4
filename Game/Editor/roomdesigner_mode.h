
#pragma once

#include "../includes.h"
#include "../resources.h"

#ifndef Room
class Room;
#endif
#ifndef RoomDesigner
class RoomDesigner;
#endif

class RoomDesignerModule
{

	protected:
	  int workingroomindex;
		Room* workingroom;
		RoomDesigner* designer;
		ALLEGRO_FONT* textfont;

	public:
		virtual void Init(RoomDesigner* Designer, Room* Working, int WorkingRoomIndex, ALLEGRO_FONT* Font);
		void AddLogText(std::string Text);

		virtual void OnEvent(Event* e) = 0;
		virtual void Update() = 0;
		virtual void RenderRoom() = 0;
		virtual void RenderOverlay() = 0;

};
