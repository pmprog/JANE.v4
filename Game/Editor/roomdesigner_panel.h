
#pragma once

#include "roomdesigner_mode.h"

class RoomDesignerPanel : public RoomDesignerModule
{

	private:
    int panel_repeatdelay;
		int panel_activeindex;
    int panel_xchange;
    int panel_ychange;
    int panel_fgychange;

	public:
		RoomDesignerPanel();

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};
