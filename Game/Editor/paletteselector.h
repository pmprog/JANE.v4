
#pragma once

#include "../../Framework/includes.h"
#include "../resources.h"

class PaletteSelector : public Stage
{

	private:

    int selection_rampdelay;
		int selection_rampindex;
		int selection_index;		

	public:
		int* Remap;

		PaletteSelector(int* ColourRemap);

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
