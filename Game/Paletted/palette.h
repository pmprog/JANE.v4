
#pragma once

#include <allegro5/allegro_color.h>
#include "../../Framework/Display/display.h"

#define COLOUR_BLACK        al_map_rgb(   0,   0,   0 )
#define COLOUR_WHITE        al_map_rgb( 255, 255, 255 )
#define COLOUR_RED          al_map_rgb( 104,  55,  43 )
#define COLOUR_CYAN         al_map_rgb( 112, 164, 178 )
#define COLOUR_PURPLE       al_map_rgb( 111,  61, 134 )
#define COLOUR_GREEN        al_map_rgb(  88, 141,  67 )
#define COLOUR_BLUE         al_map_rgb(  53,  40, 121 )
#define COLOUR_YELLOW       al_map_rgb( 184, 199, 111 )
#define COLOUR_ORANGE       al_map_rgb( 111,  79,  37 )
#define COLOUR_BROWN        al_map_rgb(  67,  57,   0 )
#define COLOUR_PINK         al_map_rgb( 154, 103,  89 )
#define COLOUR_DARK_GRAY    al_map_rgb(  68,  68,  68 )
#define COLOUR_MID_GRAY     al_map_rgb( 108, 108, 108 )
#define COLOUR_LIGHT_GREEN  al_map_rgb( 154, 210, 132 )
#define COLOUR_LIGHT_BLUE   al_map_rgb( 108,  94, 181 )
#define COLOUR_LIGHT_GRAY   al_map_rgb( 149, 149, 149 )


class Palette
{

  private:
    static ALLEGRO_COLOR SourceColourPalette[16];


  public:

    static ALLEGRO_COLOR ColourPalette[16];

    static const int RampBlue[8];
    static const int RampBlueDark[8];
    static const int RampRed[8];
    static const int RampRedDark[8];
    static const int RampGray[8];
    static const int RampGrayDark[8];

    static void InitPalette();
    static void ApplyColourOverrides(ALLEGRO_BITMAP* Image);

};


