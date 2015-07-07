
#include "palettedbitmap.h"
#include "../includes.h"

PalettedBitmap::PalettedBitmap( std::string Filename )
{
  ALLEGRO_BITMAP* b = BitmapCache::LoadBitmap( Filename );
  Palette::ApplyColourOverrides( b ); // Make sure we're in the game's palette

  Setup( al_get_bitmap_width( b ), al_get_bitmap_height( b ) );

  PackedARGB8888* pixelcolour;
  ALLEGRO_COLOR pixeltranslated;

  ALLEGRO_LOCKED_REGION* rgn = al_lock_bitmap( b, ALLEGRO_PIXEL_FORMAT_ABGR_8888_LE, ALLEGRO_LOCK_READONLY );
  for( int y = 0; y < height; y++ )
  {
    for( int x = 0; x < width; x++ )
    {
      PackedARGB8888* pxRow = (PackedARGB8888*)((char*)rgn->data + (y * rgn->pitch));
      pixelcolour = &pxRow[x];
      pixeltranslated = al_map_rgba( pixelcolour->r, pixelcolour->g, pixelcolour->b, pixelcolour->a );
      for( int c = 0; c < 16; c++ )
      {
        if( pixeltranslated.r == Palette::ColourPalette[c].r && pixeltranslated.g == Palette::ColourPalette[c].g && pixeltranslated.b == Palette::ColourPalette[c].b && pixeltranslated.a == Palette::ColourPalette[c].a )
        {
          indicies[ (y * width) + x ] = c;
          break;
        }
      }
    }
  }
  al_unlock_bitmap( b );

  BitmapCache::UnloadBitmap( b );

  PrerenderImage();
}

PalettedBitmap::PalettedBitmap( int Width, int Height )
{
  Setup( Width, Height );
}

void PalettedBitmap::Setup( int Width, int Height )
{
  width = Width;
  height = Height;
  indicies = (char*)malloc( width * height * sizeof(char) );
  memset( (void*)indicies, 0, width * height * sizeof(char) );
  batchoverrides = false;

  prerendered = nullptr;
  ClearOverrides();
  PrerenderImage();
}

PalettedBitmap::~PalettedBitmap()
{
  free( (void*)indicies );
  al_destroy_bitmap( prerendered );
}

void PalettedBitmap::ClearOverrides()
{
  for(int c = 0; c < 16; c++ )
  {
    colouroverrides[c] = c;
  }
  PrerenderImage();
}

void PalettedBitmap::SetOverride(int ColourIndex, int NewColourIndex)
{
  colouroverrides[ColourIndex] = NewColourIndex;
  if( !batchoverrides )
  {
    PrerenderImage();
  }
}

int PalettedBitmap::GetOverride(int ColourIndex)
{
  return colouroverrides[ColourIndex];
}

void PalettedBitmap::Draw(int X, int Y, int Flags)
{
  al_draw_bitmap( prerendered, X, Y, 0 );
}

void PalettedBitmap::Draw(int X, int Y, int W, int H, int Flags)
{

}

int PalettedBitmap::GetWidth()
{
  return width;
}

int PalettedBitmap::GetHeight()
{
  return height;
}

void PalettedBitmap::PrerenderImage()
{
  if( prerendered == nullptr )
  {
    prerendered = al_create_bitmap( width, height );
  }

  PackedARGB8888* pixelcolour;


  ALLEGRO_LOCKED_REGION* rgn = al_lock_bitmap( prerendered, ALLEGRO_PIXEL_FORMAT_ABGR_8888_LE, ALLEGRO_LOCK_READWRITE );
  for( int y = 0; y < height; y++ )
  {
    for( int x = 0; x < width; x++ )
    {
      PackedARGB8888* pxRow = (PackedARGB8888*)((char*)rgn->data + (y * rgn->pitch));
      pixelcolour = &pxRow[x];
      int colindex = colouroverrides[indicies[ (y * width) + x ]];
      pixelcolour->r = Palette::ColourPalette[colindex].r * 255;
      pixelcolour->g = Palette::ColourPalette[colindex].g * 255;
      pixelcolour->b = Palette::ColourPalette[colindex].b * 255;
      pixelcolour->a = Palette::ColourPalette[colindex].a * 255;
    }
  }
  al_unlock_bitmap( prerendered );
}

void PalettedBitmap::BeginOverrides()
{
  batchoverrides = true;
}

void PalettedBitmap::EndOverrides()
{
  batchoverrides = false;
  PrerenderImage();
}
