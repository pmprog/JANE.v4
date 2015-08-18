
#include "menu.h"
#include "Editor/roomdesigner.h"
#include "debugstage.h"

bool Menu::CheatActive = false;

void Menu::Begin()
{
	menutime = 0;
  janelogo = new PalettedBitmap( "resources/jane4.png" );
  theeyes = new PalettedBitmap( "resources/theeyes.png" );
  SetLogoColours();
	textfont = al_load_ttf_font( "resources/silkscreen.ttf", 8, ALLEGRO_TTF_MONOCHROME );
	AUDIO->PlayMusic( "resources/Paul Hannay (Feekzoid) - Last_Ninja_4_intro [1].ogg", true );
}

void Menu::Pause()
{
  GameResources::GameOverlay->ClearOverrides();
}

void Menu::Resume()
{
	AUDIO->PlayMusic( "resources/Paul Hannay (Feekzoid) - Last_Ninja_4_intro [1].ogg", true );
}

void Menu::Finish()
{
  delete janelogo;
  delete theeyes;
	al_destroy_font( textfont );
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
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_F9 )
    {
      FRAMEWORK->ProgramStages->Push( new DebugStage() );
      return;
    }
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_F10 )
    {
      FRAMEWORK->ProgramStages->Push( new RoomDesigner() );
      return;
    }
    if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_F12 )
    {
      CheatActive = true;
    }
	}

}

void Menu::Update()
{
	menutime++;
	SetLogoColours();

  if( CheatActive )
  {
    GameResources::GameOverlay->SetOverride( 6, Palette::RampBlueDark[(menutime / 4) % 8] );
    GameResources::GameOverlay->SetOverride( 14, Palette::RampBlue[(menutime / 4) % 8] );
  }
}

void Menu::Render()
{
	al_clear_to_color( Palette::ColourPalette[0] );

  theeyes->Draw( 0, 4, 0 );
  janelogo->Draw( (240 - janelogo->GetWidth()) / 2, 90, 0 );

	GameResources::GameOverlay->Draw( 0, 0, 0 );

	al_draw_text( textfont, Palette::ColourPalette[7], 6, 150, ALLEGRO_ALIGN_LEFT, "By Marq Watkin" );
	al_draw_text( textfont, Palette::ColourPalette[7], 6, 159, ALLEGRO_ALIGN_LEFT, "   Polymath Programming" );
	al_draw_text( textfont, Palette::ColourPalette[7], 6, 168, ALLEGRO_ALIGN_LEFT, "http://www.pmprog.co.uk" );
	al_draw_text( textfont, Palette::ColourPalette[7], 6, 186, ALLEGRO_ALIGN_LEFT, "Version 0.1" );

}

bool Menu::IsTransition()
{
	return false;
}

void Menu::SetLogoColours()
{
  // Jane Logo
  if( menutime < 20 )
  {
    janelogo->SetOverride( 9, 0 );
    janelogo->SetOverride( 2, 0 );
    janelogo->SetOverride( 10, 0 );
    janelogo->SetOverride( 7, 0 );
  } else if ( menutime < 22 ) {
    janelogo->SetOverride( 9, 1 );
  } else if ( menutime < 24 ) {
    janelogo->SetOverride( 9, 7 );
  } else if ( menutime < 26 ) {
    janelogo->SetOverride( 9, 10 );
  } else if ( menutime < 28 ) {
    janelogo->SetOverride( 9, 2 );
  } else if ( menutime < 30 ) {
    janelogo->SetOverride( 9, 9 );
  } else if ( menutime < 44 ) {
  } else if ( menutime < 46 ) {
    janelogo->SetOverride( 2, 1 );
  } else if ( menutime < 48 ) {
    janelogo->SetOverride( 2, 7 );
  } else if ( menutime < 50 ) {
    janelogo->SetOverride( 2, 10 );
  } else if ( menutime < 52 ) {
    janelogo->SetOverride( 2, 2 );
  } else if ( menutime < 66 ) {
  } else if ( menutime < 68 ) {
    janelogo->SetOverride( 10, 1 );
  } else if ( menutime < 70 ) {
    janelogo->SetOverride( 10, 7 );
  } else if ( menutime < 72 ) {
    janelogo->SetOverride( 10, 10 );
  } else if ( menutime < 86 ) {
  } else if ( menutime < 88 ) {
    janelogo->SetOverride( 7, 1 );
  } else if ( menutime < 90 ) {
    janelogo->SetOverride( 7, 7 );
  }
}
