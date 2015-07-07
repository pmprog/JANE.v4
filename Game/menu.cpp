
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

		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_UP && menuSelection > 0 )
		{
			menuSelection--;
		}
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_DOWN && menuSelection < 4 )
		{
			menuSelection++;
		}

		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_ENTER )
		{
			activateoption = true;
		}
	}

	if( e->Type == EVENT_JOYSTICK_BUTTON_DOWN )
	{
		activateoption = true;
	}

	if( e->Type == EVENT_JOYSTICK_AXIS )
	{
		if( e->Data.Joystick.Axis == 1 )
		{
			if( e->Data.Joystick.Position < 0 && menuSelection > 0 )
			{
				menuSelection--;
			}
			if( e->Data.Joystick.Position > 0 && menuSelection < 4 )
			{
				menuSelection++;
			}
		}
	}

	if( activateoption )
	{
		switch( menuSelection )
		{
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				AUDIO->StopMusic();
				FRAMEWORK->ShutdownFramework();
				break;
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

  PalettedBitmap* b = new PalettedBitmap( BitmapCache::LoadBitmap("resources/ninja.png"), 0, 0, 24, 42 );
  b->SetOverride( 0, 9 );
  b->Draw( 0, 0, 48, 84, 0 );
  delete b;


	al_draw_bitmap( BitmapCache::LoadBitmap("resources/background.png"), 0, 0, 0 );
	BitmapCache::UnloadBitmap("resources/background.png");

}

bool Menu::IsTransition()
{
	return false;
}
