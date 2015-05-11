
#include "menu.h"

void Menu::Begin()
{
	menuTime = 0;
	menuSelection = 0;

	menuSelectedColour = al_map_rgb( 255, 255, 0 );
	menuItemColour = al_map_rgb( 220, 220, 220 );

	ninjapos = new Vector2i( 0, 0 );
	ninjaanim = new Animation( new SpriteSheet( "resources/ninja.png", 80, 100 ), true, 3 );
	ninjaanim->AddFrame( 28 + 7 );
	ninjaanim->AddFrame( 28 + 6 );
	ninjaanim->AddFrame( 28 + 5 );
	ninjaanim->AddFrame( 28 + 4 );
	ninjaanim->AddFrame( 28 + 3 );
	ninjaanim->AddFrame( 28 + 2 );
	ninjaanim->AddFrame( 28 + 1 );
	ninjaanim->AddFrame( 28 + 0 );

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
	ninjapos->X = (ninjapos->X + 4) % DISPLAY->GetWidth();
	ninjapos->Y = (ninjapos->Y + 1) % DISPLAY->GetHeight();
	ninjaanim->Update();
}

void Menu::Render()
{
	al_clear_to_color( al_map_rgb( 255, 255, 255 ) );

	ninjaanim->DrawFrame( ninjapos->X, ninjapos->Y );

	// al_draw_text( fntTitle, ( menuSelection == 0 ? menuSelectedColour : menuItemColour ), DISPLAY->GetWidth() - 20, 310, ALLEGRO_ALIGN_RIGHT, "Arcade" );
	// al_draw_text( fntTitle, ( menuSelection == 1 ? menuSelectedColour : menuItemColour ), DISPLAY->GetWidth() - 20, 340, ALLEGRO_ALIGN_RIGHT, "Network" );
	// al_draw_text( fntTitle, ( menuSelection == 2 ? menuSelectedColour : menuItemColour ), DISPLAY->GetWidth() - 20, 370, ALLEGRO_ALIGN_RIGHT, "Demo" );
	// al_draw_text( fntTitle, ( menuSelection == 3 ? menuSelectedColour : menuItemColour ), DISPLAY->GetWidth() - 20, 400, ALLEGRO_ALIGN_RIGHT, "Settings" );
	// al_draw_text( fntTitle, ( menuSelection == 4 ? menuSelectedColour : menuItemColour ), DISPLAY->GetWidth() - 20, 430, ALLEGRO_ALIGN_RIGHT, "Quit" );
}

bool Menu::IsTransition()
{
	return false;
}
