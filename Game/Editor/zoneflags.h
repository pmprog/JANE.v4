
#pragma once

#include "../../Framework/includes.h"
#include "../resources.h"

class ZoneFlagEditor : public Stage
{

	private:
		RoomZone* workingzone;
    int selection_rampdelay;
		int selection_rampindex;
		int selection_index;		

	public:
		ZoneFlagEditor(RoomZone* EditZone);

    // Stage control
    virtual void Begin();
    virtual void Pause();
    virtual void Resume();
    virtual void Finish();
    virtual void EventOccurred(Event *e);
    virtual void Update();
    virtual void Render();
		virtual bool IsTransition();
};
