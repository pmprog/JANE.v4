
#include "roomdesigner_background.h"
#include "roomdesigner.h"


RoomDesignerBackground::RoomDesignerBackground()
{
}

void RoomDesignerBackground::OnEvent(Event *e)
{
  if( e->Type == EVENT_KEY_DOWN )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
      case ALLEGRO_KEY_OPENBRACE:
				workingroom->BackgroundColour = (workingroom->BackgroundColour + 15) % 16;
        break;
      case ALLEGRO_KEY_CLOSEBRACE:
				workingroom->BackgroundColour = (workingroom->BackgroundColour + 1) % 16;
        break;
    }
  }
}

void RoomDesignerBackground::Update()
{
}

void RoomDesignerBackground::RenderRoom()
{

}

void RoomDesignerBackground::RenderOverlay()
{
  al_draw_text( textfont, Palette::ColourPalette[8], 250, 16, ALLEGRO_ALIGN_LEFT, "[: Prev" );
  al_draw_text( textfont, Palette::ColourPalette[8], 250, 25, ALLEGRO_ALIGN_LEFT, "]: Next" );
}
