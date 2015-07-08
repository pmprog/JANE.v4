
#include "palettedbitmap.h"
#include "../includes.h"

PalettedBitmap::PalettedBitmap( std::string Filename )
{
  ALLEGRO_BITMAP* image = BitmapCache::LoadBitmap( Filename );
  Palette::ApplyColourOverrides( image ); // Make sure we're in the game's palette
  ConvertBitmap( image, 0, 0, al_get_bitmap_width( image ), al_get_bitmap_height( image ) );
  BitmapCache::UnloadBitmap( image );
}

PalettedBitmap::PalettedBitmap( int Width, int Height )
{
  Setup( Width, Height );
}

PalettedBitmap::PalettedBitmap( ALLEGRO_BITMAP* Copy )
{
  ConvertBitmap( Copy, 0, 0, al_get_bitmap_width( Copy ), al_get_bitmap_height( Copy ) );
}

PalettedBitmap::PalettedBitmap( ALLEGRO_BITMAP* PartialCopy, int X, int Y, int W, int H )
{
  ConvertBitmap( PartialCopy, X, Y, W, H );
}

void PalettedBitmap::Setup( int Width, int Height )
{
  width = Width;
  height = Height;
  indicies = (char*)malloc( width * height * sizeof(char) );
  memset( (void*)indicies, -1, width * height * sizeof(char) );
  batchoverrides = false;

  prerendered = nullptr;
  ClearOverrides();
  PrerenderImage();
}

void PalettedBitmap::ConvertBitmap( ALLEGRO_BITMAP* Source, int X, int Y, int W, int H )
{

  Setup( W, H );

  PackedARGB8888* pixelcolour;
  ALLEGRO_COLOR pixeltranslated;

  ALLEGRO_LOCKED_REGION* rgn = al_lock_bitmap( Source, ALLEGRO_PIXEL_FORMAT_ABGR_8888_LE, ALLEGRO_LOCK_READONLY );
  for( int y = 0; y < height; y++ )
  {
    for( int x = 0; x < width; x++ )
    {
      PackedARGB8888* pxRow = (PackedARGB8888*)((char*)rgn->data + ((Y + y) * rgn->pitch));
      pixelcolour = &pxRow[(X + x)];
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
  al_unlock_bitmap( Source );

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
  al_draw_bitmap( prerendered, X, Y, Flags );
}

void PalettedBitmap::Draw(int X, int Y, int W, int H, int Flags)
{
  al_draw_scaled_bitmap( prerendered, 0, 0, width, height, X, Y, W, H, Flags );
}

void PalettedBitmap::DrawPartial(int SourceX, int SourceY, int SourceW, int SourceH, int DestinationX, int DestinationY, int Flags)
{
  al_draw_bitmap_region( prerendered, SourceX, SourceY, SourceW, SourceH, DestinationX, DestinationY, Flags );
}

void PalettedBitmap::DrawPartial(int SourceX, int SourceY, int SourceW, int SourceH, int DestinationX, int DestinationY, int DestinationW, int DestinationH, int Flags)
{
  ALLEGRO_BITMAP* subbmp = al_create_sub_bitmap( prerendered, SourceX, SourceY, SourceW, SourceH );
  al_draw_scaled_bitmap( subbmp, 0, 0, SourceW, SourceH, DestinationX, DestinationY, DestinationW, DestinationH, Flags );
  al_destroy_bitmap( subbmp );
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
  if( width == 0 || height == 0 )
  {
    return;
  }
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
      int indx = (int)indicies[ (y * width) + x ];
      if( indx < 0 )
      {
        pixelcolour->r = 0;
        pixelcolour->g = 0;
        pixelcolour->b = 0;
        pixelcolour->a = 0;
      } else {
        int colindex = colouroverrides[indx];
        pixelcolour->r = Palette::ColourPalette[colindex].r * 255;
        pixelcolour->g = Palette::ColourPalette[colindex].g * 255;
        pixelcolour->b = Palette::ColourPalette[colindex].b * 255;
        pixelcolour->a = Palette::ColourPalette[colindex].a * 255;
      }
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

void PalettedBitmap::Save( std::string Filename )
{
  al_save_bitmap( Filename.c_str(), prerendered );
}

