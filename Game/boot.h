
#pragma once

#include "includes.h"

class BootUp : public Stage
{
	private:
		int bootBarAdjust;
		int bootBarSize;
		PalettedBitmap* logoSprite;
		int logoFadeIn;

		ALLEGRO_THREAD* loadingthread;
		static bool loadingComplete;

		void StartGame();
		void PreparePalette();
		static void* ThreadedLoad(ALLEGRO_THREAD* source, void* arg);

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
