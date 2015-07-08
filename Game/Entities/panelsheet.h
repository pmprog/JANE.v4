
#pragma once

#include "../includes.h"

class PanelSheet
{
  private:
    std::vector<PalettedBitmap*> panels;

  public:
    PanelSheet();
    PanelSheet( std::string Filename );
    ~PanelSheet();

    int GetCount();
    PalettedBitmap* GetPanel( int Index );

    void AddFromFile( std::string Filename );
};
