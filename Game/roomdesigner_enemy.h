
#pragma once

#include "roomdesigner_mode.h"

class RoomDesignerEnemy : public RoomDesignerModule
{

	private:

	public:
		RoomDesignerEnemy();

		virtual void OnEvent(Event* e);
		virtual void Update();
		virtual void RenderRoom();
		virtual void RenderOverlay();

};