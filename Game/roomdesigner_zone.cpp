
#include "roomdesigner.h"
#include "../Framework/Primitives/strings.h"

void RoomDesigner::ZoneEvent(Event *e)
{
  if( e->Type == EVENT_KEY_DOWN )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
      case ALLEGRO_KEY_INSERT:
        break;
      case ALLEGRO_KEY_DELETE:
        break;
      case ALLEGRO_KEY_PGUP:
        break;
      case ALLEGRO_KEY_PGDN:
        break;
      case ALLEGRO_KEY_LEFT:
        break;
      case ALLEGRO_KEY_RIGHT:
        break;
      case ALLEGRO_KEY_UP:
        break;
      case ALLEGRO_KEY_DOWN:
        break;
    }
  }

  if( e->Type == EVENT_KEY_UP )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
      case ALLEGRO_KEY_LEFT:
      case ALLEGRO_KEY_RIGHT:
        break;
      case ALLEGRO_KEY_UP:
      case ALLEGRO_KEY_DOWN:
        break;
    }
  }
}

void RoomDesigner::ZoneUpdate()
{

}

void RoomDesigner::ZoneRender()
{

}

void RoomDesigner::ZoneRenderStatus()
{

}
