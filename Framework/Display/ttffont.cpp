
#include "ttffont.h"

TTFFont::TTFFont( std::string Filename, int Size )
{
	fontobject = al_load_ttf_font( Filename.c_str(), Size, 0 );
	fontheight = al_get_font_line_height( fontobject );
}

TTFFont::~TTFFont()
{
	al_destroy_font( fontobject );
}

void TTFFont::DrawString( int X, int Y, std::string Text, FontHAlign::FontHorizontalAlignment Alignment, ALLEGRO_COLOR Colour )
{
	int xpos = X;
	int textlen = 0;
	int flags = 0;

	switch( Alignment )
	{
		case FontHAlign::LEFT:
			flags = ALLEGRO_ALIGN_LEFT;
			break;
		case FontHAlign::CENTRE:
			flags = ALLEGRO_ALIGN_CENTRE;
			break;
		case FontHAlign::RIGHT:
			flags = ALLEGRO_ALIGN_RIGHT;
			break;
	}

	al_draw_text( fontobject, Colour, xpos, Y, flags, Text.c_str() );
}

int TTFFont::GetFontHeight()
{
	return fontheight;
}

int TTFFont::GetFontWidth( std::string Text )
{
	return al_get_text_width( fontobject, Text.c_str() );
}
