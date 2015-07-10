
#include "room.h"
#include "../resources.h"

Room::Room()
{
  RoomTime = 0;
  BackgroundColour = 13;
}

Room::Room( int RoomID )
{
  RoomTime = 0;
  BackgroundColour = 13;

  // Load Room Data
}

Room::~Room()
{

}

void Room::OnEnter()
{
  RoomTime = 0;
  if( Script_OnCombatantEnter != "" )
  {
    GameResources::Scripting->ExecuteAsync( Script_OnCombatantEnter );
  }
}

void Room::OnLeave()
{
  if( Script_OnCombatantLeave != "" )
  {
    GameResources::Scripting->ExecuteAsync( Script_OnCombatantLeave );
  }
}

void Room::Update()
{
  RoomTime++;
  if( Script_OnUpdate != "" )
  {
    GameResources::Scripting->ExecuteAsync( Script_OnUpdate );
  }
}

void Room::Render(int FromY, int ToY)
{
  int panelstodraw = Panels.size();

  // Always 2 seconds to render a room
  if( RoomTime < FRAMEWORK->GetFramesPerSecond() )
  {
    float c = (float)RoomTime / (float)FRAMEWORK->GetFramesPerSecond(); // % through the build
    c *= Panels.size(); // Number of panels to draw at this percentage
    panelstodraw = (int)c;
  }

  for( int idx = 0; idx < panelstodraw; idx++ )
  {
    Panel* p = Panels.at( idx );
    if( p->BackgroundAtY >= FromY && p->BackgroundAtY <= ToY )
    {
      PalettedBitmap* b = GameResources::ObjectGraphics->GetPanel( p->ObjectGraphicIndex );
      b->SetOverrides( p->ColourRemap );
      b->Draw( p->ScreenX, p->ScreenY, (p->FlipHorizontal ? ALLEGRO_FLIP_HORIZONTAL : 0) | (p->FlipVertical ? ALLEGRO_FLIP_VERTICAL : 0) );
    }
  }

}

int Room::SortPanels()
{
  return SortPanels( Panels.size() - 1 );
}

int Room::SortPanels(int Current)
{
  Panel* p = Panels.at( Current );
  std::sort( Panels.begin(), Panels.end(), Panel::SortingPredicate );
  for(int i = 0; i < Panels.size(); i++ )
  {
    if( Panels.at( i ) == p )
    {
      return i;
    }
  }
  return 0;
}
