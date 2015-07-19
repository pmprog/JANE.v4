
#include "panel.h"

Panel::Panel()
{
  ObjectGraphicIndex = 0;
  ScreenX = 0;
  ScreenY = 0;
  FlipHorizontal = false;
  FlipVertical = false;
  BackgroundAtY = 0;
  for( int i = 0; i < 16; i++ )
  {
    ColourRemap[i] = i;
  }
}

bool Panel::SortingPredicate(const Panel* A, const Panel* B)
{
  if( A->BackgroundAtY == B->BackgroundAtY )
  {
    // If ForegroundY is the same, the lesser is futher up the screen
    return ( A->ScreenY < B->ScreenY );
  } else if( A->BackgroundAtY < B->BackgroundAtY ) {
    // If ForegroundY greater (harder for combatants to be in front), then it's more "background"
    return true;
  }
  return false;
}


void Panel::Load(ConfigFile* DataFile, std::string KeyPrefix)
{
  ObjectGraphicIndex = DataFile->GetQuickIntegerValue( KeyPrefix, 0, ObjectGraphicIndex );
  for( int i = 0; i < 16; i++ )
  {
    ColourRemap[i] = DataFile->GetQuickIntegerValue( KeyPrefix, i + 1, ColourRemap[i] );
  }
  ScreenX = DataFile->GetQuickIntegerValue( KeyPrefix, 17, ScreenX );
  ScreenY = DataFile->GetQuickIntegerValue( KeyPrefix, 18, ScreenY );
  FlipHorizontal = DataFile->GetQuickBooleanValue( KeyPrefix, 19, FlipHorizontal );
  FlipVertical = DataFile->GetQuickBooleanValue( KeyPrefix, 20, FlipVertical );
  BackgroundAtY = DataFile->GetQuickIntegerValue( KeyPrefix, 21, BackgroundAtY );

}

void Panel::Save(ConfigFile* DataFile, std::string KeyPrefix)
{
  DataFile->SetIntegerValue( KeyPrefix, 0, ObjectGraphicIndex );
  for( int i = 0; i < 16; i++ )
  {
    DataFile->SetIntegerValue( KeyPrefix, i + 1, ColourRemap[i] );
  }
  DataFile->SetIntegerValue( KeyPrefix, 17, ScreenX );
  DataFile->SetIntegerValue( KeyPrefix, 18, ScreenY );
  DataFile->SetBooleanValue( KeyPrefix, 19, FlipHorizontal );
  DataFile->SetBooleanValue( KeyPrefix, 20, FlipVertical );
  DataFile->SetIntegerValue( KeyPrefix, 21, BackgroundAtY );
}
