
#pragma once

#include "includes.h"
#include "resources.h"
#include "./Entities/room.h"

class RoomDesigner : public Stage
{
	private:
		Room* workingroom;
		ALLEGRO_FONT* textfont;
		int designermode;
		std::string LogText[5];

    int selection_rampdelay;
		int selection_rampindex;

    int panel_repeatdelay;
    int panel_graphicchangedelay;
    int panel_graphicchange;
		int panel_activeindex;
    int panel_xchange;
    int panel_ychange;
    int panel_fgychange;

		void AddLogText(std::string Text);

		void BackgroundEvent(Event *e);
		void PanelEvent(Event *e);
		void ZoneEvent(Event *e);
		void ObjectEvent(Event *e);
		void EnemyEvent(Event *e);

		void BackgroundUpdate();
		void PanelUpdate();
		void ZoneUpdate();
		void ObjectUpdate();
		void EnemyUpdate();

		void BackgroundRender();
		void PanelRender();
		void ZoneRender();
		void ObjectRender();
		void EnemyRender();

		void BackgroundRenderStatus();
		void PanelRenderStatus();
		void ZoneRenderStatus();
		void ObjectRenderStatus();
		void EnemyRenderStatus();

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
