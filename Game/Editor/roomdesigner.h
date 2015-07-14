
#pragma once

#include "../includes.h"
#include "../resources.h"
#include "../Entities/room.h"
#include "roomdesigner_mode.h"
#include "../../Framework/Primitives/strings.h"

class RoomDesigner : public Stage
{
	private:
		Room* workingroom;
		ALLEGRO_FONT* textfont;
		RoomDesignerModule* designermodes[5];
		int designermode;
		std::string LogText[5];

    int selection_rampdelay;
		int selection_rampindex;

  public:

    enum DesignerMode
    {
      BackgroundMode,
      PanelMode,
      ZoneMode,
      ObjectMode,
      EnemyMode,

      ModeCount
    };

    RoomDesigner();
    ~RoomDesigner();

		void AddLogText(std::string Text);
		int GetRampIndex();

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
