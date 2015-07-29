
#pragma once

#include "roomdesigner_mode.h"

#define ZONEMODE_ZONESELECT 0
#define ZONEMODE_POINTSELECT 1

class RoomDesignerZone : public RoomDesignerModule
{

	private:
		int zone_activeindex;

	public:
		int CursorX;
		int CursorY;
		int CursorMove;
		bool CursorFineMove;

		int Mode;
		int ActiveZoneIndex;

		RoomDesignerZone();

		virtual void Init(RoomDesigner* Designer, Room* Working, ALLEGRO_FONT* Font);

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};