
#include "zoneflags.h"

ZoneFlagEditor::ZoneFlagEditor(RoomZone* EditZone)
{
	workingzone = EditZone;
	selection_rampdelay = 0;
	selection_rampindex = 0;
	selection_index = 0;
}

void ZoneFlagEditor::Begin()
{
}

void ZoneFlagEditor::Pause()
{
}

void ZoneFlagEditor::Resume()
{
}

void ZoneFlagEditor::Finish()
{
}

void ZoneFlagEditor::EventOccurred(Event *e)
{
	if( e->Type == EVENT_KEY_DOWN )
	{
		switch( e->Data.Keyboard.KeyCode )
		{
			case ALLEGRO_KEY_ESCAPE:
			case ALLEGRO_KEY_ENTER:
				delete FRAMEWORK->ProgramStages->Pop();
				return;

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
}

void ZoneFlagEditor::Update()
{
  selection_rampdelay = (selection_rampdelay + 1) % 4;
  if( selection_rampdelay == 0 )
  {
    selection_rampindex = (selection_rampindex + 1) % 8;
  }
}

void ZoneFlagEditor::Render()
{
	al_clear_to_color( Palette::ColourPalette[0] );

}

bool ZoneFlagEditor::IsTransition()
{
	return false;
}

