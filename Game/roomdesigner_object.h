
#pragma once

#include "roomdesigner_mode.h"

class RoomDesignerObject : public RoomDesignerModule
{

	private:

	public:
		RoomDesignerObject();

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};