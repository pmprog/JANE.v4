
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
