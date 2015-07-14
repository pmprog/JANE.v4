
#pragma once

#include "roomdesigner_mode.h"

class RoomDesignerZone : public RoomDesignerModule
{

	private:

	public:
		RoomDesignerZone();

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};