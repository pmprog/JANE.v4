
#include "panelselector.h"

PanelSelector::PanelSelector(Panel* Editing)
{
	selection_rampindex = 0;
	selection_rampdelay = 0;
	working = Editing;
	top_visible = working->ObjectGraphicIndex - (working->ObjectGraphicIndex % 6);
}

void PanelSelector::Begin()
{
}

void PanelSelector::Pause()
{
}

void PanelSelector::Resume()
{
}

void PanelSelector::Finish()
{
}

void PanelSelector::EventOccurred(Event *e)
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
				working->ObjectGraphicIndex--;
				break;

			case ALLEGRO_KEY_UP:
				working->ObjectGraphicIndex -= 6;
				break;

			case ALLEGRO_KEY_PGUP:
				working->ObjectGraphicIndex -= 24;
				break;

			case ALLEGRO_KEY_RIGHT:
				working->ObjectGraphicIndex++;
				break;

			case ALLEGRO_KEY_DOWN:
				working->ObjectGraphicIndex += 6;
				break;

			case ALLEGRO_KEY_PGDN:
				working->ObjectGraphicIndex += 24;
				break;
		}

		if( working->ObjectGraphicIndex < 0 )
		{
			working->ObjectGraphicIndex = 0;
		}
		if( working->ObjectGraphicIndex >= GameResources::ObjectGraphics->GetCount() )
		{
			working->ObjectGraphicIndex = GameResources::ObjectGraphics->GetCount() - 1;
		}
		if( working->ObjectGraphicIndex < top_visible )
		{
			top_visible = working->ObjectGraphicIndex - (working->ObjectGraphicIndex % 6);
		}
		while( working->ObjectGraphicIndex >= top_visible + 24 )
		{
			top_visible += 6;
		}
	}
}

void PanelSelector::Update()
{
  selection_rampdelay = (selection_rampdelay + 1) % 4;
  if( selection_rampdelay == 0 )
  {
    selection_rampindex = (selection_rampindex + 1) % 8;
  }
}

void PanelSelector::Render()
{
	al_clear_to_color( Palette::ColourPalette[14] );

	for(int y = 0; y < 5; y++ )
	{
		al_draw_line( 0, (y + 1) * 48, 288,  (y + 1) * 48, Palette::ColourPalette[6], 1 );
		for( int x = 0; x < 6; x++ )
		{
			al_draw_line( (x + 1) * 48, y * 48, (x + 1) * 48,  (y + 1) * 48, Palette::ColourPalette[6], 1 );
			int curitem = top_visible + (y * 6) + x;

			if( GameResources::ObjectGraphics->GetCount() > curitem )
			{
				PalettedBitmap* p = GameResources::ObjectGraphics->GetPanel( curitem );
				p->ClearOverrides();
				//float pscale = Maths::Min( (p->GetWidth() <= 46 ? 1.0f : p->GetWidth() / 46.0f), (p->GetHeight() <= 46 ? 1.0f : p->GetHeight() / 46.0f) );
				float pscale = Maths::Min( 46.0f / (float)p->GetWidth(), 46.0f / (float)p->GetHeight() );
				if( curitem == working->ObjectGraphicIndex )
				{
					al_draw_filled_rectangle( x * 48, y * 48, (x+1) * 48, (y+1) * 48, Palette::ColourPalette[ Palette::RampRed[selection_rampindex] ]);
				}
				p->Draw( (x * 48) + 1, (y * 48) + 1, p->GetWidth() * pscale, p->GetHeight() * pscale, 0 );
			}
		}
	}

}

bool PanelSelector::IsTransition()
{
	return false;
}

