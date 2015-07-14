
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
		Room* workingroom;
		RoomDesigner* designer;
		ALLEGRO_FONT* textfont;

	public:
		void Init(RoomDesigner* Designer, Room* Working, ALLEGRO_FONT* Font);
		void AddLogText(std::string Text);

		virtual void OnEvent(Event* e) = 0;
		virtual void Update() = 0;
		virtual void RenderRoom() = 0;
		virtual void RenderOverlay() = 0;

};
