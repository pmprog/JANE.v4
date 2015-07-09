
#include "roomdesigner.h"
#include "../Framework/Primitives/strings.h"

RoomDesigner::RoomDesigner()
{
  designermode = DesignerMode::PanelMode;
  workingroom = new Room();
  for( int i = 0; i < FRAMEWORK->GetFramesPerSecond() * 2; i++ )
  {
    workingroom->Update();
  }

  textfont = al_load_ttf_font( "resources/silkscreen.ttf", 8, ALLEGRO_TTF_MONOCHROME );

  AddLogText("Welcome to the designer");

  selection_rampdelay = 0;
  selection_rampindex = 0;

  panel_repeatdelay = 0;
  panel_graphicchangedelay = 0;
  panel_graphicchange = 0;
  panel_xchange = 0;
  panel_ychange = 0;
  panel_fgychange = 0;
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
        FRAMEWORK->ShutdownFramework();
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
		}
	}

  switch( designermode )
  {
    case DesignerMode::BackgroundMode:
      BackgroundEvent(e);
      break;
    case DesignerMode::ObjectMode:
      ObjectEvent(e);
      break;
    case DesignerMode::PanelMode:
      PanelEvent(e);
      break;
    case DesignerMode::ZoneMode:
      ZoneEvent(e);
      break;
    case DesignerMode::EnemyMode:
      EnemyEvent(e);
      break;
  }
}

void RoomDesigner::Update()
{
  selection_rampdelay = (selection_rampdelay + 1) % 4;
  if( selection_rampdelay == 0 )
  {
    selection_rampindex = (selection_rampindex + 1) % 8;
  }

  switch( designermode )
  {
    case DesignerMode::BackgroundMode:
      BackgroundUpdate();
      break;
    case DesignerMode::ObjectMode:
      ObjectUpdate();
      break;
    case DesignerMode::PanelMode:
      PanelUpdate();
      break;
    case DesignerMode::ZoneMode:
      ZoneUpdate();
      break;
    case DesignerMode::EnemyMode:
      EnemyUpdate();
      break;
  }
}

void RoomDesigner::Render()
{
  std::string modename;
	al_clear_to_color( Palette::ColourPalette[workingroom->BackgroundColour] );

  workingroom->Render( 0, 200 );

  switch( designermode )
  {
    case DesignerMode::BackgroundMode:
      modename = "F1: Bkgrnd";
      BackgroundRender();
      break;
    case DesignerMode::ObjectMode:
      modename = "F1: Object";
      ObjectRender();
      break;
    case DesignerMode::PanelMode:
      modename = "F1: Panel";
      PanelRender();
      break;
    case DesignerMode::ZoneMode:
      modename = "F1: Zone";
      ZoneRender();
      break;
    case DesignerMode::EnemyMode:
      modename = "F1: Enemy";
      EnemyRender();
      break;
  }

	al_draw_bitmap( BitmapCache::LoadBitmap("resources/background.png"), 0, 0, 0 );
	BitmapCache::UnloadBitmap("resources/background.png");

	al_draw_text( textfont, Palette::ColourPalette[ Palette::RampA[selection_rampindex] ], 250, 3, ALLEGRO_ALIGN_LEFT, modename.c_str() );

  for( int i = 0; i < 5; i++ )
  {
    al_draw_text( textfont, Palette::ColourPalette[7], 4, 150 + (i * 9), ALLEGRO_ALIGN_LEFT, LogText[i].c_str() );
    //al_draw_text( textfont, Palette::ColourPalette[ Palette::RampB[selection_rampindex] ], 4, 150 + (i * 9), ALLEGRO_ALIGN_LEFT, LogText[i].c_str() );
  }

  switch( designermode )
  {
    case DesignerMode::BackgroundMode:
      BackgroundRenderStatus();
      break;
    case DesignerMode::ObjectMode:
      ObjectRenderStatus();
      break;
    case DesignerMode::PanelMode:
      PanelRenderStatus();
      break;
    case DesignerMode::ZoneMode:
      ZoneRenderStatus();
      break;
    case DesignerMode::EnemyMode:
      EnemyRenderStatus();
      break;
  }
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

void RoomDesigner::BackgroundEvent(Event *e)
{
  if( e->Type != EVENT_KEY_DOWN )
  {
    return;
  }

  if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_PGUP )
  {
    workingroom->BackgroundColour = (workingroom->BackgroundColour + 15) % 16;
  }
  if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_PGDN )
  {
    workingroom->BackgroundColour = (workingroom->BackgroundColour + 1) % 16;
  }
}

void RoomDesigner::BackgroundUpdate()
{
}

void RoomDesigner::BackgroundRender()
{
}

void RoomDesigner::BackgroundRenderStatus()
{
  al_draw_text( textfont, Palette::ColourPalette[8], 250, 16, ALLEGRO_ALIGN_LEFT, "PgUp: Prev" );
  al_draw_text( textfont, Palette::ColourPalette[8], 250, 25, ALLEGRO_ALIGN_LEFT, "PgUp: Next" );
}
