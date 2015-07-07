
#include "palettedbitmapcache.h"
#include "../includes.h"

std::vector<PalettedBitmapCache*> PalettedBitmapCache::cache;

PalettedBitmapCache::PalettedBitmapCache( std::string File )
{
	Filename = File;
	Image = new PalettedBitmap( File );
	Locks = 1;
}

PalettedBitmap* PalettedBitmapCache::LoadBitmap( std::string Filename )
{
	PalettedBitmapCache* b;
	for( std::vector<PalettedBitmapCache*>::iterator i = cache.begin(); i != cache.end(); i++ )
	{
		b = (PalettedBitmapCache*)*i;
		if( b->Filename == Filename )
		{
			b->Locks++;
			return b->Image;
		}
	}

	b = new PalettedBitmapCache( Filename );
	cache.push_back( b );
	return b->Image;
}

void PalettedBitmapCache::UnloadBitmap( std::string Filename )
{
	PalettedBitmapCache* b;
	for( std::vector<PalettedBitmapCache*>::iterator i = cache.begin(); i != cache.end(); i++ )
	{
		b = (PalettedBitmapCache*)*i;
		if( b->Filename == Filename )
		{
			b->Locks--;
			if( b->Locks <= 0 )
			{
				// al_destroy_bitmap( b->Image );
				delete b->Image;
				cache.erase( i );
				return;
			}
		}
	}
}

void PalettedBitmapCache::UnloadBitmap( PalettedBitmap* Bitmap )
{
	PalettedBitmapCache* b;
	for( std::vector<PalettedBitmapCache*>::iterator i = cache.begin(); i != cache.end(); i++ )
	{
		b = (PalettedBitmapCache*)*i;
		if( b->Image == Bitmap )
		{
			b->Locks--;
			if( b->Locks <= 0 )
			{
				// al_destroy_bitmap( b->Image );
				delete b->Image;
				cache.erase( i );
				return;
			}
		}
	}
}
