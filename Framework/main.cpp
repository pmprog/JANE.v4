
#include "framework.h"

#ifdef PANDORA
#include <signal.h>
#endif

int main( int argc, char* argv[] )
{
  Framework* fw = new Framework( 320, 200, 60, false );
  fw->Run();
  delete fw;

#ifdef PANDORA
	// Hacky close on the Pandora :(
	raise(SIGKILL);
#endif

  return 0;
}
