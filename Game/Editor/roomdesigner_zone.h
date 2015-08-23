
#pragma once

#include "roomdesigner_mode.h"

#define ZONEMODE_ZONESELECT 0
#define ZONEMODE_POINTSELECT 1
#define ZONEMODE_POINTMOVE 2

class RoomDesignerZone : public RoomDesignerModule
{

	private:
		int zone_activeindex;
		int zonepoint_activeindex;

	public:
		int CursorX;
		int CursorY;
		int CursorMove;
		bool CursorFineMove;
		bool CursorSlow;
		int CursorSlowDelay;

		int Mode;
		int ActiveZoneIndex;

		RoomDesignerZone();

		virtual void Init(RoomDesigner* Designer, Room* Working, int WorkingRoomIndex, ALLEGRO_FONT* Font);

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};
