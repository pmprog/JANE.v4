
#include "paletteselector.h"

PaletteSelector::PaletteSelector(int* ColourRemap)
{
	Remap = ColourRemap;
	selection_rampdelay = 0;
	selection_rampindex = 0;
	selection_index = 0;
}

void PaletteSelector::Begin()
{
}

void PaletteSelector::Pause()
{
}

void PaletteSelector::Resume()
{
}

void PaletteSelector::Finish()
{
}

void PaletteSelector::EventOccurred(Event *e)
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
				selection_index = (selection_index + 15) % 16;
				break;

			case ALLEGRO_KEY_RIGHT:
				selection_index = (selection_index + 1) % 16;
				break;

			case ALLEGRO_KEY_UP:
				Remap[selection_index] = (Remap[selection_index] + 15) % 16;
				break;

			case ALLEGRO_KEY_DOWN:
				Remap[selection_index] = (Remap[selection_index] + 1) % 16;
				break;
		}
	}
}

void PaletteSelector::Update()
{
  selection_rampdelay = (selection_rampdelay + 1) % 4;
  if( selection_rampdelay == 0 )
  {
    selection_rampindex = (selection_rampindex + 1) % 8;
  }
}

void PaletteSelector::Render()
{
	al_clear_to_color( Palette::ColourPalette[0] );

	// x = 96
	// w = 12

	int curx = 64;
	for( int col = 0; col < 16; col++ )
	{

		al_draw_filled_rectangle( curx, 32, curx + 12, 48, Palette::ColourPalette[ Palette::RampGray[ (col + 1) % 8 ] ] );
		al_draw_filled_rectangle( curx + 2, 34, curx + 10, 46, Palette::ColourPalette[col] );

		if( col == selection_index )
		{
			al_draw_filled_rectangle( curx, 64, curx + 12, 80, Palette::ColourPalette[ Palette::RampGray[selection_rampindex] ] );
		} else {
			al_draw_filled_rectangle( curx, 64, curx + 12, 80, Palette::ColourPalette[ Palette::RampGray[ (col + 1) % 8 ] ] );
		}
		al_draw_filled_rectangle( curx + 2, 66, curx + 10, 78, Palette::ColourPalette[ Remap[col] ] );

		curx += 12;
	}

}

bool PaletteSelector::IsTransition()
{
	return false;
}

