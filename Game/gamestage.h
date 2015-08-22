
#pragma once

#include "includes.h"
#include "resources.h"
#include "./Entities/combatant.h"

class GameStage : public Stage
{

	private:
		Combatant* ninja;
		int curroom;

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
