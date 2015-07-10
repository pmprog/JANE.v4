
#pragma once

#include "includes.h"
#include "resources.h"


class Menu : public Stage
{
	private:
		int menutime;
		PalettedBitmap* janelogo;
		PalettedBitmap* theeyes;

		void SetLogoColours();

  public:
    static bool CheatActive;
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
