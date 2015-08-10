
#include "roomdesigner_zone.h"
#include "roomdesigner.h"


RoomDesignerZone::RoomDesignerZone()
{
	CursorX = 120;
	CursorY = 72;
	CursorMove = 0;
	CursorFineMove = false;
	CursorSlow = false;
	CursorSlowDelay = 0;

	zone_activeindex = 0;
	zonepoint_activeindex = 0;

	Mode = ZONEMODE_ZONESELECT;
}

void RoomDesignerZone::Init(RoomDesigner* Designer, Room* Working, ALLEGRO_FONT* Font)
{
	RoomDesignerModule::Init( Designer, Working, Font );
	CursorX = 120;
	CursorY = 72;
	CursorMove = 0;
	CursorFineMove = false;
	CursorSlow = false;
	CursorSlowDelay = 0;

	zone_activeindex = 0;
	zonepoint_activeindex = 0;

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

			case ALLEGRO_KEY_OPENBRACE:
				switch( Mode )
				{
					case ZONEMODE_ZONESELECT:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex > 0 )
						{
							zone_activeindex--;
						} else {
							zone_activeindex = 0;
						}
						break;
					case ZONEMODE_POINTSELECT:
					case ZONEMODE_POINTMOVE:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							RoomZone* z = workingroom->Zones.at( zone_activeindex );
							if( z->Area->Points->count > 0 && z->Area->Points->count > zonepoint_activeindex && zonepoint_activeindex > 0)
							{
								zonepoint_activeindex--;
							} else {
								zonepoint_activeindex = z->Area->Points->count - 1;
							}
							Vector2* v = (Vector2*)z->Area->Points->ItemAt( zonepoint_activeindex );
							CursorX = v->X;
							CursorY = v->Y;
						} else {
							zonepoint_activeindex = 0;
						}
						break;
				}
				break;
			case ALLEGRO_KEY_CLOSEBRACE:
				switch( Mode )
				{
					case ZONEMODE_ZONESELECT:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex + 1 && zone_activeindex >= 0 )
						{
							zone_activeindex++;
						} else {
							zone_activeindex = workingroom->Zones.size() - 1;
						}
						break;
					case ZONEMODE_POINTSELECT:
					case ZONEMODE_POINTMOVE:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							RoomZone* z = workingroom->Zones.at( zone_activeindex );
							if( z->Area->Points->count > 0 && z->Area->Points->count > zonepoint_activeindex + 1 && zonepoint_activeindex >= 0)
							{
								zonepoint_activeindex++;
							} else {
								zonepoint_activeindex = 0;
							}
							Vector2* v = (Vector2*)z->Area->Points->ItemAt( zonepoint_activeindex );
							CursorX = v->X;
							CursorY = v->Y;
						} else {
							zonepoint_activeindex = 0;
						}
						break;
				}
				break;

			case ALLEGRO_KEY_ENTER:
				switch( Mode )
				{
					case ZONEMODE_ZONESELECT:
						Mode = ZONEMODE_POINTMOVE;
						break;
					case ZONEMODE_POINTSELECT:
					case ZONEMODE_POINTMOVE:
						Mode = ZONEMODE_ZONESELECT;
						break;
				}
				break;

			case ALLEGRO_KEY_LSHIFT:
			case ALLEGRO_KEY_RSHIFT:
				CursorSlow = true;
				CursorSlowDelay = 0;
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
					case ZONEMODE_POINTMOVE:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							workingroom->Zones.at( zone_activeindex )->Area->Points->AddToEnd( new Vector2( CursorX, CursorY ) );
							zonepoint_activeindex = workingroom->Zones.at( zone_activeindex )->Area->Points->count - 1;
							Mode = ZONEMODE_POINTMOVE;
						}
						break;
				}
				break;

			case ALLEGRO_KEY_DELETE:
				switch( Mode )
				{
					case ZONEMODE_ZONESELECT:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							RoomZone* z = workingroom->Zones.at( zone_activeindex );
							workingroom->Zones.erase( workingroom->Zones.begin() + zone_activeindex );
							delete z;
							if( zone_activeindex >= workingroom->Zones.size() )
							{
								zone_activeindex = workingroom->Zones.size() - 1;
							}
						}
						break;

					case ZONEMODE_POINTSELECT:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							delete workingroom->Zones.at( zone_activeindex )->Area->Points->last->itemData;
							workingroom->Zones.at( zone_activeindex )->Area->Points->RemoveLast();
						}
						break;

					case ZONEMODE_POINTMOVE:
						if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
						{
							RoomZone* z = workingroom->Zones.at( zone_activeindex );
							if( z->Area->Points->count > 0 && z->Area->Points->count > zonepoint_activeindex && zonepoint_activeindex >= 0)
							{
								delete z->Area->Points->ItemAt( zonepoint_activeindex );
								z->Area->Points->RemoveAt( zonepoint_activeindex );
								if( zonepoint_activeindex >= z->Area->Points->count )
								{
									zonepoint_activeindex = z->Area->Points->count - 1;
								}
							}
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
			case ALLEGRO_KEY_LSHIFT:
			case ALLEGRO_KEY_RSHIFT:
				CursorSlow = false;
				break;
    }
  }
}

void RoomDesignerZone::Update()
{
	if( Mode == ZONEMODE_POINTSELECT || Mode == ZONEMODE_POINTMOVE )
	{
		int proceedmove = true;
		if( CursorSlow )
		{
			CursorSlowDelay = (CursorSlowDelay + 1) % 6;
			proceedmove = ( CursorSlowDelay == 0 );
		}

		if( proceedmove )
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

		if( Mode == ZONEMODE_POINTMOVE )
		{
			if( workingroom->Zones.size() > 0 && workingroom->Zones.size() > zone_activeindex && zone_activeindex >= 0 )
			{
				RoomZone* z = workingroom->Zones.at( zone_activeindex );
				if( z->Area->Points->count > 0 && z->Area->Points->count > zonepoint_activeindex && zonepoint_activeindex >= 0)
				{
					Vector2* v = (Vector2*)z->Area->Points->ItemAt( zonepoint_activeindex );
					v->X = CursorX;
					v->Y = CursorY;
				}
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

			if( vertindex == zonepoint_activeindex && Mode == ZONEMODE_POINTMOVE )
			{
				al_draw_ellipse( verts[writeindex], verts[writeindex + 1], 8, 2, Palette::ColourPalette[ Palette::RampGrayDark[designer->GetRampIndex()] ], 1 );
			}

			writeindex += 2;
		}

		if( vertcount == 1 )
		{
			al_draw_ellipse( verts[0], verts[1], 4, 1, Palette::ColourPalette[ Palette::RampGrayDark[designer->GetRampIndex()] ], 1 );
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
		al_draw_ellipse( CursorX, CursorY, 8, 2, Palette::ColourPalette[ Palette::RampRed[designer->GetRampIndex()] ] , 2 );
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
	al_draw_text( textfont, Palette::ColourPalette[8], 160, 34, ALLEGRO_ALIGN_LEFT, "[: Prev" );
	al_draw_text( textfont, Palette::ColourPalette[8], 160, 43, ALLEGRO_ALIGN_LEFT, "]: Next" );

	if( Mode == ZONEMODE_ZONESELECT )
	{
		al_draw_text( textfont, Palette::ColourPalette[8], 160, 16, ALLEGRO_ALIGN_LEFT, "INS: New Zone" );
		al_draw_text( textfont, Palette::ColourPalette[8], 160, 25, ALLEGRO_ALIGN_LEFT, "DEL: Delete Zone" );

		al_draw_text( textfont, Palette::ColourPalette[8], 160, 43, ALLEGRO_ALIGN_LEFT, "ENT: Edit" );
	} else {
		al_draw_text( textfont, Palette::ColourPalette[8], 160, 16, ALLEGRO_ALIGN_LEFT, "INS: New Pnt" );
		al_draw_text( textfont, Palette::ColourPalette[8], 160, 25, ALLEGRO_ALIGN_LEFT, "DEL: Del Pnt" );

		al_draw_text( textfont, Palette::ColourPalette[8], 160, 43, ALLEGRO_ALIGN_LEFT, "ENT: Commit" );
		al_draw_text( textfont, Palette::ColourPalette[8], 160, 52, ALLEGRO_ALIGN_LEFT, "M: MoveMode" );
		al_draw_text( textfont, Palette::ColourPalette[8], 160, 61, ALLEGRO_ALIGN_LEFT, "F: Flags" );
	}

}
