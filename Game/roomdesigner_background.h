
#pragma once

#include "roomdesigner_mode.h"

class RoomDesignerBackground : public RoomDesignerModule
{

	private:

	public:
		RoomDesignerBackground();

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};