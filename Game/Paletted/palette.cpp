
#include "palette.h"

ALLEGRO_COLOR Palette::SourceColourPalette[16] = { COLOUR_BLACK, COLOUR_WHITE, COLOUR_RED, COLOUR_CYAN, COLOUR_PURPLE, COLOUR_GREEN, COLOUR_BLUE, COLOUR_YELLOW,
        COLOUR_ORANGE, COLOUR_BROWN, COLOUR_PINK, COLOUR_DARK_GRAY, COLOUR_MID_GRAY, COLOUR_LIGHT_GREEN, COLOUR_LIGHT_BLUE, COLOUR_LIGHT_GRAY };

ALLEGRO_COLOR Palette::ColourPalette[16] = { COLOUR_BLACK, COLOUR_WHITE, COLOUR_RED, COLOUR_CYAN, COLOUR_PURPLE, COLOUR_GREEN, COLOUR_BLUE, COLOUR_YELLOW,
        COLOUR_ORANGE, COLOUR_BROWN, COLOUR_PINK, COLOUR_DARK_GRAY, COLOUR_MID_GRAY, COLOUR_LIGHT_GREEN, COLOUR_LIGHT_BLUE, COLOUR_LIGHT_GRAY };

void Palette::InitPalette()
{
  SourceColourPalette[0] = COLOUR_BLACK;
  SourceColourPalette[1] = COLOUR_WHITE;
  SourceColourPalette[2] = COLOUR_RED;
  SourceColourPalette[3] = COLOUR_CYAN;
  SourceColourPalette[4] = COLOUR_PURPLE;
  SourceColourPalette[5] = COLOUR_GREEN;
  SourceColourPalette[6] = COLOUR_BLUE;
  SourceColourPalette[7] = COLOUR_YELLOW;
  SourceColourPalette[8] = COLOUR_ORANGE;
  SourceColourPalette[9] = COLOUR_BROWN;
  SourceColourPalette[10] = COLOUR_PINK;
  SourceColourPalette[11] = COLOUR_DARK_GRAY;
  SourceColourPalette[12] = COLOUR_MID_GRAY;
  SourceColourPalette[13] = COLOUR_LIGHT_GREEN;
  SourceColourPalette[14] = COLOUR_LIGHT_BLUE;
  SourceColourPalette[15] = COLOUR_LIGHT_GRAY;

  for( int c = 0; c < 16; c++ )
  {
    ColourPalette[c] = SourceColourPalette[c];
  }
}

void Palette::ApplyColourOverrides( ALLEGRO_BITMAP* Image )
{
  PackedARGB8888* pixelcolour;
  ALLEGRO_COLOR pixeltranslated;

  int imgW = al_get_bitmap_width( Image );
  int imgH = al_get_bitmap_height( Image );
  ALLEGRO_LOCKED_REGION* rgn = al_lock_bitmap( Image, ALLEGRO_PIXEL_FORMAT_ABGR_8888_LE, ALLEGRO_LOCK_READWRITE );
  for( int y = 0; y < imgH; y++ )
  {
    for( int x = 0; x < imgW; x++ )
    {
      PackedARGB8888* pxRow = (PackedARGB8888*)((char*)rgn->data + (y * rgn->pitch));
      pixelcolour = &pxRow[x];
      pixeltranslated = al_map_rgba( pixelcolour->r, pixelcolour->g, pixelcolour->b, pixelcolour->a );
      for( int c = 0; c < 16; c++ )
      {
        // Only check against colours where the palette has changed
        if( ColourPalette[c].r != SourceColourPalette[c].r || ColourPalette[c].g != SourceColourPalette[c].g || ColourPalette[c].b != SourceColourPalette[c].b || ColourPalette[c].a != SourceColourPalette[c].a )
        {
          if( pixeltranslated.r == SourceColourPalette[c].r && pixeltranslated.g == SourceColourPalette[c].g && pixeltranslated.b == SourceColourPalette[c].b && pixeltranslated.a == SourceColourPalette[c].a )
          {
            pixelcolour->r = (ColourPalette[c].r * 255);
            pixelcolour->g = (ColourPalette[c].g * 255);
            pixelcolour->b = (ColourPalette[c].b * 255);
            break;
          }
        }
      }
    }
  }
  al_unlock_bitmap( Image );

}
