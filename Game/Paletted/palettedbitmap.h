
#pragma once

#include <string>
#include "../../Framework/Display/display.h"

class PalettedBitmap
{

  private:
    int width;
    int height;
    char* indicies;
    int colouroverrides[16];
    bool batchoverrides;
    ALLEGRO_BITMAP* prerendered;

    void Setup( int Width, int Height );
    void ConvertBitmap( ALLEGRO_BITMAP* Source, int X, int Y, int W, int H );
    void PrerenderImage();

  public:
    PalettedBitmap( std::string Filename );
    PalettedBitmap( int Width, int Height );
    PalettedBitmap( ALLEGRO_BITMAP* Copy );
    PalettedBitmap( ALLEGRO_BITMAP* PartialCopy, int X, int Y, int W, int H );
    ~PalettedBitmap();

    void ClearOverrides();
    void BeginOverrides();
    void EndOverrides();
    void SetOverride(int ColourIndex, int NewColourIndex);
    void SetOverrides(int ColourSwap[16]);
    int GetOverride(int ColourIndex);

    void Draw(int X, int Y, int Flags);
    void Draw(int X, int Y, int W, int H, int Flags);
    void DrawPartial(int SourceX, int SourceY, int SourceW, int SourceH, int DestinationX, int DestinationY, int Flags);
    void DrawPartial(int SourceX, int SourceY, int SourceW, int SourceH, int DestinationX, int DestinationY, int DestinationW, int DestinationH, int Flags);

    int GetWidth();
    int GetHeight();

    void Save( std::string Filename );
};
