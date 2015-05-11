
#pragma once

#include "includes.h"
#include "../Framework/Display/spritesheet.h"

class Menu : public Stage
{
	private:
		int menuTime;
		int menuSelection;

		ALLEGRO_COLOR menuSelectedColour;
		ALLEGRO_COLOR menuItemColour;

		Animation* ninjaanim;
		Vector2i* ninjapos;

  public:

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
