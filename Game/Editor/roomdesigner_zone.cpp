
#include "roomdesigner_zone.h"
#include "roomdesigner.h"


RoomDesignerZone::RoomDesignerZone()
{
}

void RoomDesignerZone::OnEvent(Event *e)
{
  if( e->Type == EVENT_KEY_DOWN )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
      case ALLEGRO_KEY_PGUP:
				workingroom->BackgroundColour = (workingroom->BackgroundColour + 15) % 16;
        break;
      case ALLEGRO_KEY_PGDN:
				workingroom->BackgroundColour = (workingroom->BackgroundColour + 1) % 16;
        break;
    }
  }
}

void RoomDesignerZone::Update()
{
}

void RoomDesignerZone::RenderRoom()
{

}

void RoomDesignerZone::RenderOverlay()
{
  al_draw_text( textfont, Palette::ColourPalette[8], 250, 16, ALLEGRO_ALIGN_LEFT, "PgUp: Prev" );
  al_draw_text( textfont, Palette::ColourPalette[8], 250, 25, ALLEGRO_ALIGN_LEFT, "PgUp: Next" );
}
