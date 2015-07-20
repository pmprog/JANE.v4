
#include "boot.h"
#include "resources.h"
#include "menu.h"
#include "../Framework/Primitives/strings.h"

bool BootUp::loadingComplete = false;

void BootUp::Begin()
{
  PreparePalette();
	DISPLAY->SetTitle("JANE - Just Another Ninja Engine - Version 4");
	bootBarSize = 0;
	bootBarAdjust = (DISPLAY->GetWidth() / (FRAMEWORK->GetFramesPerSecond() * 1.75f));

  logoFadeOut = false;
	logoSprite = PalettedBitmapCache::LoadBitmap("resources/pmprog.png");
  logoSprite->BeginOverrides();
  logoSprite->SetOverride( 2, 0 );
  logoSprite->SetOverride( 8, 0 );
  logoSprite->SetOverride( 9, 0 );
  logoSprite->SetOverride( 10, 0 );
  logoSprite->SetOverride( 11, 0 );
  logoSprite->EndOverrides();
	logoFadeIn = 0;

	loadingthread = al_create_thread( ThreadedLoad, nullptr );
	al_start_thread( loadingthread );
}

void BootUp::Pause()
{
}

void BootUp::Resume()
{
}

void BootUp::Finish()
{
  PalettedBitmapCache::UnloadBitmap( logoSprite );
}

void BootUp::EventOccurred(Event *e)
{
	if( e->Type == EVENT_KEY_DOWN )
	{
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_ESCAPE )
		{
			delete FRAMEWORK->ProgramStages->Pop();
		} else if( loadingComplete ) {
			// StartGame();
			logoFadeOut = true;
		}
	}
}

void BootUp::Update()
{
  if( logoFadeOut )
  {
    logoFadeIn--;
  } else {
    logoFadeIn++;
  }

	if( logoFadeIn > 40 )
  {
    logoSprite->ClearOverrides();
  } else if( logoFadeIn > 37 ) {
    logoSprite->BeginOverrides();
    logoSprite->SetOverride( 2, 12 );
    logoSprite->SetOverride( 8, 12 );
    logoSprite->SetOverride( 9, 12 );
    logoSprite->SetOverride( 10, 1 );
    logoSprite->SetOverride( 11, 12 );
    logoSprite->EndOverrides();
  } else if( logoFadeIn > 34 ) {
    logoSprite->BeginOverrides();
    logoSprite->SetOverride( 2, 8 );
    logoSprite->SetOverride( 8, 8 );
    logoSprite->SetOverride( 9, 8 );
    logoSprite->SetOverride( 10, 10 );
    logoSprite->SetOverride( 11, 8 );
    logoSprite->EndOverrides();
  } else if( logoFadeIn > 31 ) {
    logoSprite->BeginOverrides();
    logoSprite->SetOverride( 2, 2 );
    logoSprite->SetOverride( 8, 2 );
    logoSprite->SetOverride( 9, 2 );
    logoSprite->SetOverride( 10, 8 );
    logoSprite->SetOverride( 11, 2 );
    logoSprite->EndOverrides();
  } else if( logoFadeIn > 28 ) {
    logoSprite->SetOverride( 10, 2 );
  } else if( logoFadeIn > 25 ) {
    logoSprite->SetOverride( 10, 9 );
  } else {
    logoSprite->BeginOverrides();
    logoSprite->SetOverride( 2, 0 );
    logoSprite->SetOverride( 8, 0 );
    logoSprite->SetOverride( 9, 0 );
    logoSprite->SetOverride( 10, 0 );
    logoSprite->SetOverride( 11, 0 );
    logoSprite->EndOverrides();
  }

	if( bootBarSize < DISPLAY->GetWidth() )
	{
		bootBarSize += bootBarAdjust;
	}

	// Only allow completion when all resources are loaded
	if( loadingComplete && bootBarSize >= DISPLAY->GetWidth() && !logoFadeOut )
	{
		// StartGame();
		logoFadeOut = true;
		logoFadeIn = 50;
	}

	if( logoFadeOut && logoFadeIn <= 0 )
  {
    StartGame();
  }
}

void BootUp::Render()
{
	al_clear_to_color( al_map_rgb( 0, 0, 0 ) );

	int logox = (DISPLAY->GetWidth() - logoSprite->GetWidth()) / 2;
	int logoy = (DISPLAY->GetHeight() - logoSprite->GetHeight()) / 2;

	logoSprite->Draw( logox, logoy, 0 );

	int xPos = (DISPLAY->GetWidth() / 2) - (bootBarSize / 2);
	int yPos = DISPLAY->GetHeight() - 12;
	int colidx = ( logoSprite->GetOverride( 10 ) == 0 && !logoFadeOut ? 2 : logoSprite->GetOverride( 10 ) );
	al_draw_filled_rectangle( xPos, yPos, xPos + bootBarSize, yPos + 4, Palette::ColourPalette[ colidx ] ); // 2] );
}

void BootUp::StartGame()
{
	delete Framework::System->ProgramStages->Pop();
	Framework::System->ProgramStages->Push( new Menu() );
}

bool BootUp::IsTransition()
{
	return false;
}

void* BootUp::ThreadedLoad( ALLEGRO_THREAD*, void* )
{
	// Load all resources for caching
	ALLEGRO_BITMAP* bkg = BitmapCache::LoadBitmap("resources/background.png");
	Palette::ApplyColourOverrides( bkg );
	GameResources::GameOverlay = new PalettedBitmap( bkg );
	BitmapCache::UnloadBitmap("resources/background.png");
	Palette::ApplyColourOverrides( BitmapCache::LoadBitmap("resources/ninja.png") );
	Palette::ApplyColourOverrides( BitmapCache::LoadBitmap("resources/ln1_1.png") );
	GameResources::ObjectGraphics = new PanelSheet( "resources/ln1_1.png" );
	Palette::ApplyColourOverrides( BitmapCache::LoadBitmap("resources/ln1_2.png") );
	GameResources::ObjectGraphics->AddFromFile( "resources/ln1_2.png" );
	Palette::ApplyColourOverrides( BitmapCache::LoadBitmap("resources/ln1_3.png") );
	GameResources::ObjectGraphics->AddFromFile( "resources/ln1_3.png" );
	Palette::ApplyColourOverrides( BitmapCache::LoadBitmap("resources/ln1_4.png") );
	GameResources::ObjectGraphics->AddFromFile( "resources/ln1_4.png" );

  GameResources::GameDataFile = new ConfigFile( "resources/jane.cfg" );
  GameResources::Scripting = new ScriptEngine();

  GameResources::GameWorld = new World( "resources/jane4.game", 1 );

	loadingComplete = true;
	return nullptr;
}

void BootUp::PreparePalette()
{
  char paletteconfig[100];
  std::string paletteconfigname;

  Palette::InitPalette();

  // Load colour overrides from settings file
  for( int col = 0; col < 16; col++ )
  {
    sprintf((char*)&paletteconfig, "Video.Palette.%d", col);
    paletteconfigname.clear();
    paletteconfigname.append( (char*)&paletteconfig );
    if( FRAMEWORK->Settings->KeyExists(paletteconfigname) && FRAMEWORK->Settings->GetArraySize(paletteconfigname) == 3 )
    {
      Palette::ColourPalette[col] = al_map_rgb( FRAMEWORK->Settings->GetQuickIntegerValue(paletteconfigname, 0, 0), FRAMEWORK->Settings->GetQuickIntegerValue(paletteconfigname, 1, 0), FRAMEWORK->Settings->GetQuickIntegerValue(paletteconfigname, 2, 0) );
    }
  }
}
