
#include "spritesheet.h"
#include "display.h"

SpriteSheet::SpriteSheet( std::string Filename )
{
	sheet = BitmapCache::LoadBitmap( Filename );
}

SpriteSheet::SpriteSheet( std::string Filename, int FrameWidth, int FrameHeight )
{
	sheet = BitmapCache::LoadBitmap( Filename );

	for( int y = 0; y < al_get_bitmap_height( sheet ); y += FrameHeight )
	{
		for( int x = 0; x < al_get_bitmap_width( sheet ); x += FrameWidth )
		{
			AddSprite( x, y, FrameWidth, FrameHeight );
		}
	}
}

SpriteSheet::SpriteSheet( std::string Filename, int GridSize )
{
  sheet = BitmapCache::LoadBitmap( Filename );
  DepackFromGrid( GridSize );
}

SpriteSheet::~SpriteSheet()
{
	BitmapCache::UnloadBitmap( sheet );
}

ALLEGRO_BITMAP* SpriteSheet::GetSheet()
{
	return sheet;
}

int SpriteSheet::AddSprite( int FrameX, int FrameY, int FrameWidth, int FrameHeight )
{
	SpriteSheetRegion* r = (SpriteSheetRegion*)malloc( sizeof(SpriteSheetRegion) );
	r->X = FrameX;
	r->Y = FrameY;
	r->Width = FrameWidth;
	r->Height = FrameHeight;
	frames.push_back( r );
	return frames.size() - 1;
}

void SpriteSheet::DrawSprite( int FrameNumber, int ScreenX, int ScreenY )
{
	if( FrameNumber < 0 || FrameNumber >= (int)frames.size() )
	{
		return;
	}

	SpriteSheetRegion* r = frames.at( FrameNumber );
	al_draw_bitmap_region( sheet, r->X, r->Y, r->Width, r->Height, ScreenX, ScreenY, 0 );
}

void SpriteSheet::DrawSprite( int FrameNumber, int ScreenX, int ScreenY, float ScaleX, float ScaleY, Angle* Rotation )
{
	if( FrameNumber < 0 || FrameNumber >= (int)frames.size() )
	{
		return;
	}

	SpriteSheetRegion* r = frames.at( FrameNumber );
	if( (Rotation == 0 || Rotation->ToDegrees() == 0) && ScaleX == 1.0f && ScaleY == 1.0f )
	{
		al_draw_bitmap_region( sheet, r->X, r->Y, r->Width, r->Height, ScreenX, ScreenY, 0 );
	} else {
		int renderX = r->X + ((r->Width * ScaleX) / 2);
		if( Rotation == 0 )
		{
			al_draw_tinted_scaled_rotated_bitmap_region( sheet, r->X, r->Y, r->Width, r->Height, al_map_rgba(255, 255, 255, 255), 0, 0, ScreenX, ScreenY, ScaleX, ScaleY, 0, 0 );
		} else {
			al_draw_tinted_scaled_rotated_bitmap_region( sheet, r->X, r->Y, r->Width, r->Height, al_map_rgba(255, 255, 255, 255), r->Width / 2, r->Height / 2, ScreenX + ((r->Width * ScaleX) / 2), ScreenY + ((r->Height * ScaleY) / 2), ScaleX, ScaleY, Rotation->ToRadians(), 0 );
		}
	}
}

void SpriteSheet::DrawSpritePortion( int FrameNumber, int ScreenX, int ScreenY, int FrameX, int FrameY, int Width, int Height )
{
	if( FrameNumber < 0 || FrameNumber >= (int)frames.size() )
	{
		return;
	}

	SpriteSheetRegion* r = frames.at( FrameNumber );
	al_draw_bitmap_region( sheet, r->X + FrameX, r->Y + FrameY, Width, Height, ScreenX, ScreenY, 0 );
}

SpriteSheetRegion* SpriteSheet::GetFrame( int FrameNumber )
{
	return frames.at( FrameNumber );
}

int SpriteSheet::GetFrameCount()
{
	return frames.size();
}

void SpriteSheet::DepackFromGrid( int GridSize )
{

  // Create a grid to indicate populated cells
  int gridw = (al_get_bitmap_width(sheet) / GridSize);
  int gridh = (al_get_bitmap_height(sheet) / GridSize);
  gridalloc_size = GridSize;

  char* grid = (char*)malloc( gridw * gridh * sizeof(char) );
  memset( (void*)grid, 0, gridw * gridh * sizeof(char) );

  PackedARGB8888* pixelcolour;
  ALLEGRO_COLOR pixeltranslated;

  ALLEGRO_LOCKED_REGION* rgn = al_lock_bitmap( sheet, ALLEGRO_PIXEL_FORMAT_ABGR_8888_LE, ALLEGRO_LOCK_READONLY );
  for( int gy = 0; gy < gridh; gy++ )
  {
    for( int gx = 0; gx < gridw; gx++ )
    {

      for( int y = 0; y < GridSize; y++ )
      {
        for( int x = 0; x < GridSize; x++ )
        {
          int curx = (gx * GridSize) + x;
          int cury = (gy * GridSize) + y;
          PackedARGB8888* pxRow = (PackedARGB8888*)((char*)rgn->data + (cury * rgn->pitch));
          pixelcolour = &pxRow[curx];
          if( pixelcolour->a != 0 )
          {
            grid[ (gy * gridw) + gx ] = 1;
            break;
          }
        }
        if( grid[ (gy * gridw) + gx ] != 0 )
        {
          break;
        }
      }

    }
  }

  al_unlock_bitmap( sheet );

  /*
    Grid is
      0 = Empty Tile
      1 = Populated (not assigned)
      2 = Current Sprite (working)
      3 = Allocated
  */

  for( int gy = 0; gy < gridh; gy++ )
  {
    for( int gx = 0; gx < gridw; gx++ )
    {
      if( grid[ (gy * gridw) + gx ] == 1 )
      {
        ExtractSpriteFrom( grid, gridw, gridh, gx, gy );
#ifdef WRITE_LOG
        fprintf( FRAMEWORK->LogFile, "SpriteSheet: Depacked (%d, %d, %d, %d)\n", gridalloc_minx, gridalloc_miny, gridalloc_maxx, gridalloc_maxy );
#endif
      }
    }
  }



  free( (void*)grid );
}

void SpriteSheet::ExtractSpriteFrom( char* Grid, int GridW, int GridH, int StartX, int StartY )
{
  // Know where we start
	gridalloc_minx = StartX;
	gridalloc_miny = StartY;
	gridalloc_maxx = StartX;
  gridalloc_maxy = StartY;

  // Follow the grid around for adjactent [1] cells
  AllocateGrid( Grid, GridW, GridH, StartX, StartY );

  // Add sprite to sheet
  AddSprite( gridalloc_minx * gridalloc_size, gridalloc_miny * gridalloc_size, ((gridalloc_maxx - gridalloc_minx) + 1) * gridalloc_size, ((gridalloc_maxy - gridalloc_miny) + 1) * gridalloc_size );
}

void SpriteSheet::AllocateGrid( char* Grid, int GridW, int GridH, int StartX, int StartY )
{
  // Allocate tile
  Grid[ (StartY * GridW) + StartX ] = 2;
  if( StartX < gridalloc_minx )
  {
    gridalloc_minx = StartX;
  }
  if( StartY < gridalloc_miny )
  {
    gridalloc_miny = StartY;
  }
  if( StartX > gridalloc_maxx )
  {
    gridalloc_maxx = StartX;
  }
  if( StartY > gridalloc_maxy )
  {
    gridalloc_maxy = StartY;
  }

  for( int y = -1; y < 2; y++ )
  {
    for( int x = -1; x < 2; x++ )
    {
      if( y != 0 || x != 0 )
      {

        if( StartX + x >= 0 && StartX + x < GridW && StartY + y >= 0 && StartY + y < GridH )
        {
          // Adjacent tile exists, is it for allocating?
          if( Grid[ ((StartY + y) * GridW) + (StartX + x) ] == 1 )
          {
            AllocateGrid( Grid, GridW, GridH, StartX + x, StartY + y );
          }
        }
      }
    }
  }
}

ALLEGRO_BITMAP* SpriteSheet::ExtractFrame( int FrameNumber )
{
	if( FrameNumber < 0 || FrameNumber >= (int)frames.size() )
	{
		return nullptr;
	}

	SpriteSheetRegion* r = frames.at( FrameNumber );
	ALLEGRO_BITMAP* frame = al_create_bitmap( r->Width, r->Height );

  ALLEGRO_BITMAP* tmp = DISPLAY->GetCurrentTarget();
  DISPLAY->SetTarget( frame );
	al_draw_bitmap_region( sheet, r->X, r->Y, r->Width, r->Height, 0, 0, 0 );
	DISPLAY->SetTarget( tmp );

	return frame;
}
