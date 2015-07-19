
#include "roomdesigner_panel.h"
#include "roomdesigner.h"
#include "panelselector.h"
#include "paletteselector.h"

RoomDesignerPanel::RoomDesignerPanel()
{
  panel_repeatdelay = 0;
  panel_xchange = 0;
  panel_ychange = 0;
  panel_fgychange = 0;
}

void RoomDesignerPanel::OnEvent(Event *e)
{
  Panel* p;
  Panel* copypanel;
  if( e->Type == EVENT_KEY_DOWN )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
      case ALLEGRO_KEY_INSERT:
        p = new Panel();
        AddLogText( "Inserted Panel " + Strings::FromNumber( workingroom->Panels.size() ) );
        workingroom->Panels.push_back( p );
        panel_activeindex = workingroom->SortPanels();
        break;
      case ALLEGRO_KEY_C:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          p = workingroom->Panels.at( panel_activeindex );
          copypanel = new Panel();
          copypanel->FlipHorizontal = p->FlipHorizontal;
          copypanel->FlipVertical = p->FlipVertical;
          copypanel->BackgroundAtY = p->BackgroundAtY;
          copypanel->ObjectGraphicIndex = p->ObjectGraphicIndex;
          copypanel->ScreenX = p->ScreenX;
          copypanel->ScreenY = p->ScreenY;
          for( int cp = 0; cp < 16; cp++ )
          {
            copypanel->ColourRemap[cp] = p->ColourRemap[cp];
          }
          AddLogText( "Copied Panel " + Strings::FromNumber( workingroom->Panels.size() ) + " from " + Strings::FromNumber( panel_activeindex ) );
          workingroom->Panels.push_back( copypanel );
          panel_activeindex = workingroom->SortPanels();
        }
        break;
      case ALLEGRO_KEY_DELETE:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          AddLogText( "Deleted Panel " + Strings::FromNumber( panel_activeindex ) );
          p = workingroom->Panels.at( panel_activeindex );
          workingroom->Panels.erase( workingroom->Panels.begin() + panel_activeindex );
          delete p;
          if( panel_activeindex >= workingroom->Panels.size() )
          {
            panel_activeindex = workingroom->Panels.size() - 1;
          }
        }
        break;
      case ALLEGRO_KEY_TAB:
      case ALLEGRO_KEY_OPENBRACE:
        if( workingroom->Panels.size() > 0 )
        {
          panel_activeindex = (panel_activeindex + 1) % workingroom->Panels.size();
        }
        break;
      case ALLEGRO_KEY_CLOSEBRACE:
        if( workingroom->Panels.size() > 0 )
        {
          panel_activeindex = (panel_activeindex + (workingroom->Panels.size() - 1)) % workingroom->Panels.size();
        }
        break;
      case ALLEGRO_KEY_LEFT:
        panel_xchange = -4;
        break;
      case ALLEGRO_KEY_RIGHT:
        panel_xchange = 4;
        break;
      case ALLEGRO_KEY_UP:
        panel_ychange = -1;
        break;
      case ALLEGRO_KEY_DOWN:
        panel_ychange = 1;
        break;
      case ALLEGRO_KEY_H:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          p = workingroom->Panels.at( panel_activeindex );
          p->FlipHorizontal = !p->FlipHorizontal;
        }
        break;
      case ALLEGRO_KEY_V:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          p = workingroom->Panels.at( panel_activeindex );
          p->FlipVertical = !p->FlipVertical;
        }
        break;
      case ALLEGRO_KEY_A:
        panel_fgychange = -1;
        break;
      case ALLEGRO_KEY_Z:
        panel_fgychange = 1;
        break;
      case ALLEGRO_KEY_S:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          panel_activeindex = workingroom->SortPanels( panel_activeindex );
        } else {
          workingroom->SortPanels();
        }
        break;
			case ALLEGRO_KEY_G:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          p = workingroom->Panels.at( panel_activeindex );
          FRAMEWORK->ProgramStages->Push( new PanelSelector( p ) );
        }
				break;
			case ALLEGRO_KEY_P:
        if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
        {
          p = workingroom->Panels.at( panel_activeindex );
          FRAMEWORK->ProgramStages->Push( new PaletteSelector( p->ColourRemap ) );
        }
				break;
    }
  }

  if( e->Type == EVENT_KEY_UP )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
      case ALLEGRO_KEY_LEFT:
      case ALLEGRO_KEY_RIGHT:
        panel_xchange = 0;
        break;
      case ALLEGRO_KEY_UP:
      case ALLEGRO_KEY_DOWN:
        panel_ychange = 0;
        break;
      case ALLEGRO_KEY_A:
      case ALLEGRO_KEY_Z:
        panel_fgychange = 0;
        break;
    }
  }
}

void RoomDesignerPanel::Update()
{
  Panel* p;

  panel_repeatdelay = (panel_repeatdelay + 1) % 3;
  if( panel_repeatdelay != 0 )
  {
    return;
  }

  if( panel_xchange != 0 )
  {
    if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
    {
      p = workingroom->Panels.at( panel_activeindex );
      p->ScreenX += panel_xchange;
      panel_activeindex = workingroom->SortPanels( panel_activeindex );
    }
  }
  if( panel_ychange != 0 )
  {
    if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
    {
      p = workingroom->Panels.at( panel_activeindex );
      p->ScreenY += panel_ychange;
      panel_activeindex = workingroom->SortPanels( panel_activeindex );
    }
  }
  //
  if( panel_fgychange != 0 )
  {
    if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
    {
      p = workingroom->Panels.at( panel_activeindex );
      p->BackgroundAtY += panel_fgychange;
      if( p->BackgroundAtY < 0 )
      {
        p->BackgroundAtY = 0;
      }
      if( p->BackgroundAtY > 200 )
      {
        p->BackgroundAtY = 200;
      }
      panel_activeindex = workingroom->SortPanels( panel_activeindex );
    }
  }
}

void RoomDesignerPanel::RenderRoom()
{
  Panel* p;

  if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
  {
    p = workingroom->Panels.at( panel_activeindex );
    PalettedBitmap* b = GameResources::ObjectGraphics->GetPanel( p->ObjectGraphicIndex );
    al_draw_rectangle( p->ScreenX - 3, p->ScreenY - 4, p->ScreenX + b->GetWidth() + 4, p->ScreenY + b->GetHeight() + 3, Palette::ColourPalette[ Palette::RampRed[designer->GetRampIndex()] ], 1 );
    al_draw_line( 0, p->BackgroundAtY, 320, p->BackgroundAtY, Palette::ColourPalette[ Palette::RampRed[designer->GetRampIndex()] ], 3 );
  }
}

void RoomDesignerPanel::RenderOverlay()
{
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 16, ALLEGRO_ALIGN_LEFT, "INS: New" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 25, ALLEGRO_ALIGN_LEFT, "DEL: Delete" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 34, ALLEGRO_ALIGN_LEFT, "C: Copy" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 43, ALLEGRO_ALIGN_LEFT, "[: Prev" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 52, ALLEGRO_ALIGN_LEFT, "]: Next" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 61, ALLEGRO_ALIGN_LEFT, "H/V: Flip" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 70, ALLEGRO_ALIGN_LEFT, "A/Z: BkgY" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 79, ALLEGRO_ALIGN_LEFT, "S: Sort" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 88, ALLEGRO_ALIGN_LEFT, "G: Graphic" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 97, ALLEGRO_ALIGN_LEFT, "P: Palette" );

	Panel* p;
  if( panel_activeindex < workingroom->Panels.size() && panel_activeindex >= 0 )
  {
    p = workingroom->Panels.at( panel_activeindex );
    GameResources::ObjectGraphics->GetPanel( p->ObjectGraphicIndex )->Draw( 160, 150, 48, 45, 0 );
  }
}
