
#include "room.h"
#include "../resources.h"
#include "../../Framework/Primitives/strings.h"

Room::Room()
{
  SetDefaults();
}

void Room::SetDefaults()
{
  RoomTime = 0;
  BackgroundColour = 13;
  Enemy = nullptr;
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

void Room::Load(ConfigFile* DataFile, std::string KeyPrefix)
{
  int counter;
  BackgroundColour = DataFile->GetQuickIntegerValue( KeyPrefix + ".BackgroundColour", 13 );
  counter = DataFile->GetQuickIntegerValue( KeyPrefix + ".PanelCount", 0 );
  for( int i = 0; i < counter; i++ )
  {
    Panel* p = new Panel();
    p->Load( DataFile, KeyPrefix + ".Panel" + Strings::FromNumber( i ) );
    Panels.push_back( p );
  }
  counter = DataFile->GetQuickIntegerValue( KeyPrefix + ".ZoneCount", 0 );
  for( int i = 0; i < counter; i++ )
  {
    RoomZone* z = new RoomZone( this );
    z->Load( DataFile, KeyPrefix + ".Zone" + Strings::FromNumber( i ) );
    Zones.push_back( z );
  }
  Script_OnCombatantEnter.clear();
  Script_OnCombatantEnter.append( *DataFile->GetQuickStringValue( KeyPrefix + ".OnEnter", ""  ) );
  Script_OnCombatantLeave.clear();
  Script_OnCombatantLeave.append( *DataFile->GetQuickStringValue( KeyPrefix + ".OnLeave", ""  ) );
  Script_OnUpdate.clear();
  Script_OnUpdate.append( *DataFile->GetQuickStringValue( KeyPrefix + ".OnUpdate", "" ) );
  if( DataFile->KeyExists( KeyPrefix + ".Enemy" ) )
  {
    Enemy = new Combatant();
    Enemy->Load( DataFile, KeyPrefix + ".Enemy" );
  }
}

void Room::Save(ConfigFile* DataFile, std::string KeyPrefix)
{
  DataFile->SetIntegerValue( KeyPrefix + ".BackgroundColour", BackgroundColour );
  DataFile->SetIntegerValue( KeyPrefix + ".PanelCount", Panels.size() );
  for(int i = 0; i < Panels.size(); i++ )
  {
    Panels.at( i )->Save( DataFile, KeyPrefix + ".Panel" + Strings::FromNumber( i ) );
  }
  DataFile->SetIntegerValue( KeyPrefix + ".ZoneCount", Zones.size() );
  for(int i = 0; i < Zones.size(); i++ )
  {
    Zones.at( i )->Save( DataFile, KeyPrefix + ".Zone" + Strings::FromNumber( i ) );
  }
  DataFile->SetStringValue( KeyPrefix + ".OnEnter", &Script_OnCombatantEnter );
  DataFile->SetStringValue( KeyPrefix + ".OnLeave", &Script_OnCombatantLeave );
  DataFile->SetStringValue( KeyPrefix + ".OnUpdate", &Script_OnUpdate );
  if( Enemy != nullptr )
  {
    Enemy->Save( DataFile, KeyPrefix + ".Enemy" );
  }
}
