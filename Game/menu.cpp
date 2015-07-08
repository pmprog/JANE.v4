
#include "menu.h"

void Menu::Begin()
{
	menuTime = 0;

}

void Menu::Pause()
{
}

void Menu::Resume()
{
}

void Menu::Finish()
{
}

void Menu::EventOccurred(Event *e)
{
	bool activateoption = false;

	if( e->Type == EVENT_KEY_DOWN )
	{
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_ESCAPE )
		{
			FRAMEWORK->ShutdownFramework();
			return;
		}
	}

}

void Menu::Update()
{
	menuTime++;
}

void Menu::Render()
{
	al_clear_to_color( Palette::ColourPalette[5] );

//  PalettedBitmap* b = new PalettedBitmap( BitmapCache::LoadBitmap("resources/ninja.png") ), 0, 0, 24, 42 );
//  b->SetOverride( 0, 9 );
//  b->Draw( 0, 0, 48, 84, 0 );
//  delete b;

  GameResources::ObjectGraphics->GetPanel( 84 )->Draw( 0, 0, 0 );

	al_draw_bitmap( BitmapCache::LoadBitmap("resources/background.png"), 0, 0, 0 );
	BitmapCache::UnloadBitmap("resources/background.png");

}

bool Menu::IsTransition()
{
	return false;
}
