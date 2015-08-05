
#include "roomdesigner_zone.h"
#include "roomdesigner.h"


RoomDesignerZone::RoomDesignerZone()
{
	CursorX = 120;
	CursorY = 72;
	CursorMove = 0;
	CursorFineMove = false;
	zone_activeindex = 0;

	Mode = ZONEMODE_ZONESELECT;
}

void RoomDesignerZone::Init(RoomDesigner* Designer, Room* Working, ALLEGRO_FONT* Font)
{
	RoomDesignerModule::Init( Designer, Working, Font );
	CursorX = 120;
	CursorY = 72;
	CursorMove = 0;
	CursorFineMove = false;
	zone_activeindex = 0;

	Mode = ZONEMODE_ZONESELECT;
}

void RoomDesignerZone::OnEvent(Event *e)
{
  if( e->Type == EVENT_KEY_DOWN )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
			case ALLEGRO_KEY_LEFT:
				CursorMove = CursorMove | 8;
				break;
			case ALLEGRO_KEY_UP:
				CursorMove = CursorMove | 1;
				break;
			case ALLEGRO_KEY_DOWN:
				CursorMove = CursorMove | 4;
				break;
			case ALLEGRO_KEY_RIGHT:
				CursorMove = CursorMove | 2;
				break;
			case ALLEGRO_KEY_M:
				CursorFineMove = !CursorFineMove;
				AddLogText( (CursorFineMove ? "Movement: Fine" : "Movement : Ninja") );
				break;
			case ALLEGRO_KEY_ENTER:
				Mode = 1 - Mode;
				break;
			case ALLEGRO_KEY_INSERT:

				switch( Mode )
				{
					case ZONEMODE_ZONESELECT:
						workingroom->Zones.push_back( new RoomZone( workingroom ) );
						zone_activeindex = workingroom->Zones.size() - 1;
						Mode = ZONEMODE_POINTSELECT;
						break;

					case ZONEMODE_POINTSELECT:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							workingroom->Zones.at( zone_activeindex )->Area->Points->AddToEnd( new Vector2( CursorX, CursorY ) );
						}
						break;
				}


				break;
    }
  }

  if( e->Type == EVENT_KEY_UP )
  {
    switch( e->Data.Keyboard.KeyCode )
    {
			case ALLEGRO_KEY_LEFT:
				CursorMove = CursorMove & ~8;
				break;
			case ALLEGRO_KEY_UP:
				CursorMove = CursorMove & ~1;
				break;
			case ALLEGRO_KEY_DOWN:
				CursorMove = CursorMove & ~4;
				break;
			case ALLEGRO_KEY_RIGHT:
				CursorMove = CursorMove & ~2;
				break;
    }
  }
}

void RoomDesignerZone::Update()
{
	if( Mode == ZONEMODE_POINTSELECT )
	{
		if( CursorFineMove )
		{
			// Fine position control
			if( (CursorMove & 1) != 0 )
			{
				CursorY--;
			}
			if( (CursorMove & 2) != 0 )
			{
				CursorX++;
			}
			if( (CursorMove & 4) != 0 )
			{
				CursorY++;
			}
			if( (CursorMove & 8) != 0 )
			{
				CursorX--;
			}

		} else {
			// Move as ninja
			if( (CursorMove & 3) != 0 )
			{
				CursorX += 4;
			}
			if( (CursorMove & 12) != 0 )
			{
				CursorX -= 4;
			}
			if( (CursorMove & 9) != 0 )
			{
				CursorY--;
			}
			if( (CursorMove & 6) != 0 )
			{
				CursorY++;
			}
		}
	}
}

void RoomDesignerZone::RenderRoom()
{
	// TODO: Draw zones
  RoomZone* workingzone;
	int activevertcount = 0;
	float* activeverts = nullptr;

  for( int zoneindex = 0; zoneindex < workingroom->Zones.size(); zoneindex++ )
  {
    workingzone = workingroom->Zones.at( zoneindex );

		int vertcount = workingzone->Area->Points->count;
		int vertbuffersize = vertcount * 2 * sizeof(float);
		float* verts = (float*)malloc( vertbuffersize );
		
		int writeindex = 0;
		for( int vertindex = 0; vertindex < workingzone->Area->Points->count; vertindex++ )
		{
			verts[writeindex] = ((Vector2*)workingzone->Area->Points->ItemAt( vertindex ))->X;
			verts[writeindex + 1] = ((Vector2*)workingzone->Area->Points->ItemAt( vertindex ))->Y;
			writeindex += 2;
		}

		if( vertcount == 1 )
		{
			al_draw_filled_ellipse( verts[0], verts[1], 8, 2, Palette::ColourPalette[ Palette::RampGrayDark[designer->GetRampIndex()] ] );
		} else if( vertcount > 1 ) {
			al_draw_polygon( verts, vertcount, ALLEGRO_LINE_JOIN_MITER, Palette::ColourPalette[ Palette::RampGrayDark[designer->GetRampIndex()] ], 1, 1.0f );
		}

		if( zoneindex == zone_activeindex )
		{
			activevertcount = vertcount;
			activeverts = (float*)malloc( vertbuffersize );
			memcpy( (void*)activeverts, (void*)verts, vertbuffersize );
		}

		free( (void*)verts );
  }

	if( Mode == ZONEMODE_POINTSELECT )
	{
		// TODO: Draw cursor
		al_draw_filled_ellipse( CursorX, CursorY, 8, 2, Palette::ColourPalette[ Palette::RampRed[designer->GetRampIndex()] ]  );
	}

	// TODO: Colour ramp the selected zone
	if( activeverts != nullptr )
	{
		al_draw_polygon( activeverts, activevertcount, ALLEGRO_LINE_JOIN_MITER, Palette::ColourPalette[ Palette::RampRed[designer->GetRampIndex()] ], 1, 1.0f );
		free( (void*)activeverts );
	}
}

void RoomDesignerZone::RenderOverlay()
{
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 34, ALLEGRO_ALIGN_LEFT, "[: Prev" );
	al_draw_text( textfont, Palette::ColourPalette[8], 250, 43, ALLEGRO_ALIGN_LEFT, "]: Next" );

	if( Mode == ZONEMODE_ZONESELECT )
	{
		al_draw_text( textfont, Palette::ColourPalette[8], 250, 16, ALLEGRO_ALIGN_LEFT, "INS: New Z" );
		al_draw_text( textfont, Palette::ColourPalette[8], 250, 25, ALLEGRO_ALIGN_LEFT, "DEL: Delete Z" );

		al_draw_text( textfont, Palette::ColourPalette[8], 250, 43, ALLEGRO_ALIGN_LEFT, "ENT: Edit" );
	} else {
		al_draw_text( textfont, Palette::ColourPalette[8], 250, 16, ALLEGRO_ALIGN_LEFT, "INS: New Pnt" );
		al_draw_text( textfont, Palette::ColourPalette[8], 250, 25, ALLEGRO_ALIGN_LEFT, "DEL: Del Pnt" );

		al_draw_text( textfont, Palette::ColourPalette[8], 250, 43, ALLEGRO_ALIGN_LEFT, "ENT: Commit" );
		al_draw_text( textfont, Palette::ColourPalette[8], 250, 52, ALLEGRO_ALIGN_LEFT, "M: MoveMode" );
		al_draw_text( textfont, Palette::ColourPalette[8], 250, 61, ALLEGRO_ALIGN_LEFT, "F: Flags" );
	}

}
