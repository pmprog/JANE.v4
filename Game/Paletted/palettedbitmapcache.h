
#pragma once

#include "palettedbitmap.h"
#include <vector>

class PalettedBitmapCache
{

	private:
		static std::vector<PalettedBitmapCache*> cache;

		PalettedBitmapCache( std::string File );


	public:
		std::string Filename;
		PalettedBitmap* Image;
		int Locks;


		static PalettedBitmap* LoadBitmap( std::string Filename );
		static void UnloadBitmap( std::string Filename );
		static void UnloadBitmap( PalettedBitmap* Bitmap );

};
