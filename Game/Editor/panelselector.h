
#pragma once

#include "../../Framework/includes.h"
#include "../resources.h"
#include "../Entities/panel.h"

class PanelSelector : public Stage
{

	private:
		int top_visible;
		Panel* working;

    int selection_rampdelay;
		int selection_rampindex;
		

	public:
		

		PanelSelector(Panel* Editing);

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
