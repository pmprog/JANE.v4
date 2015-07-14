
#include "roomdesigner.h"
#include "../Framework/Primitives/strings.h"
#include "roomdesigner_background.h"
#include "roomdesigner_panel.h"
#include "roomdesigner_object.h"
#include "roomdesigner_zone.h"
#include "roomdesigner_enemy.h"

RoomDesigner::RoomDesigner()
{
	designermodes[DesignerMode::BackgroundMode] = new RoomDesignerBackground();
	designermodes[DesignerMode::PanelMode] = new RoomDesignerPanel();
	designermodes[DesignerMode::ZoneMode] = new RoomDesignerZone();
	designermodes[DesignerMode::ObjectMode] = new RoomDesignerObject();
	designermodes[DesignerMode::EnemyMode] = new RoomDesignerEnemy();

  workingroom = new Room();
  for( int i = 0; i < FRAMEWORK->GetFramesPerSecond() * 2; i++ )
  {
    workingroom->Update();
  }

	for(int i = 0; i < DesignerMode::ModeCount; i++ )
	{
		designermodes[i]->Init( this, workingroom, textfont );
	}
	designermode = DesignerMode::PanelMode;

  textfont = al_load_ttf_font( "resources/silkscreen.ttf", 8, ALLEGRO_TTF_MONOCHROME );

  AddLogText("Welcome to the designer");

  selection_rampdelay = 0;
  selection_rampindex = 0;


}

RoomDesigner::~RoomDesigner()
{
  al_destroy_font( textfont );
}

void RoomDesigner::Begin()
{
}

void RoomDesigner::Pause()
{
}

void RoomDesigner::Resume()
{
}

void RoomDesigner::Finish()
{
}

void RoomDesigner::EventOccurred(Event *e)
{
	bool activateoption = false;

	if( e->Type == EVENT_KEY_DOWN )
	{
		switch( e->Data.Keyboard.KeyCode )
		{
		  case ALLEGRO_KEY_ESCAPE:
        delete FRAMEWORK->ProgramStages->Pop();
        return;
      case ALLEGRO_KEY_F1:
        designermode = (designermode + 1) % DesignerMode::ModeCount;
        switch( designermode )
        {
          case DesignerMode::BackgroundMode:
            AddLogText("Mode change to { Background }");
            break;
          case DesignerMode::ObjectMode:
            AddLogText("Mode change to { Object }");
            break;
          case DesignerMode::PanelMode:
            AddLogText("Mode change to { Panel }");
            break;
          case DesignerMode::ZoneMode:
            AddLogText("Mode change to { Zone }");
            break;
          case DesignerMode::EnemyMode:
            AddLogText("Mode change to { Enemy }");
            break;
        }
        return;
      case ALLEGRO_KEY_R:
        workingroom->OnEnter();
        break;
		}
	}

	designermodes[designermode]->OnEvent(e);
}

void RoomDesigner::Update()
{
  workingroom->Update();

  selection_rampdelay = (selection_rampdelay + 1) % 4;
  if( selection_rampdelay == 0 )
  {
    selection_rampindex = (selection_rampindex + 1) % 8;
  }

  designermodes[designermode]->Update();
}

void RoomDesigner::Render()
{
  std::string modename;
	al_clear_to_color( Palette::ColourPalette[workingroom->BackgroundColour] );

  workingroom->Render( 0, 200 );

	designermodes[designermode]->RenderRoom();
  switch( designermode )
  {
    case DesignerMode::BackgroundMode:
      modename = "F1: Bkgrnd";
      break;
    case DesignerMode::ObjectMode:
      modename = "F1: Object";
      break;
    case DesignerMode::PanelMode:
      modename = "F1: Panel";
      break;
    case DesignerMode::ZoneMode:
      modename = "F1: Zone";
      break;
    case DesignerMode::EnemyMode:
      modename = "F1: Enemy";
      break;
  }

	GameResources::GameOverlay->Draw( 0, 0, 0 );
	al_draw_filled_rectangle( 6, 150, 208, 195, Palette::ColourPalette[0] );

	al_draw_text( textfont, Palette::ColourPalette[ Palette::RampA[selection_rampindex] ], 250, 3, ALLEGRO_ALIGN_LEFT, modename.c_str() );

  for( int i = 0; i < 5; i++ )
  {
    al_draw_text( textfont, Palette::ColourPalette[7], 6, 150 + (i * 9), ALLEGRO_ALIGN_LEFT, LogText[i].c_str() );
    //al_draw_text( textfont, Palette::ColourPalette[ Palette::RampB[selection_rampindex] ], 4, 150 + (i * 9), ALLEGRO_ALIGN_LEFT, LogText[i].c_str() );
  }

  designermodes[designermode]->RenderOverlay();
}

bool RoomDesigner::IsTransition()
{
	return false;
}

void RoomDesigner::AddLogText(std::string Text)
{
  LogText[0] = LogText[1];
  LogText[1] = LogText[2];
  LogText[2] = LogText[3];
  LogText[3] = LogText[4];
  LogText[4] = Text;
}

int RoomDesigner::GetRampIndex()
{
	return selection_rampindex;
}
