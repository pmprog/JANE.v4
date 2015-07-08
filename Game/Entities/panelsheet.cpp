
#include "panelsheet.h"

PanelSheet::PanelSheet()
{
}

PanelSheet::PanelSheet( std::string Filename )
{
  AddFromFile( Filename );
}

PanelSheet::~PanelSheet()
{
  while( panels.size() > 0 )
  {
    PalettedBitmap* b = panels.back();
    delete b;
    panels.pop_back();
  }
}


int PanelSheet::GetCount()
{
  return panels.size();
}

PalettedBitmap* PanelSheet::GetPanel( int Index )
{
  return panels.at( Index );
}

void PanelSheet::AddFromFile( std::string Filename )
{
  int pcount = 0;

  SpriteSheet* tmp = new SpriteSheet( Filename, 8 );
  pcount = tmp->GetFrameCount();

#ifdef WRITE_LOG
  fprintf( FRAMEWORK->LogFile, "PanelSheet: Depacked %d images from %s\n", pcount, Filename.c_str() );
#endif

  for( int idx = 0; idx < pcount; idx++ )
  {
    SpriteSheetRegion* rgn = tmp->GetFrame( idx );
    //panels->AddToEnd( (void*) );
    panels.push_back( new PalettedBitmap( tmp->GetSheet(), rgn->X, rgn->Y, rgn->Width, rgn->Height ) );
  }

  delete tmp;
}
