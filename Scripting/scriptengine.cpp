
#include "scriptengine.h"
#include "../Framework/framework.h"
#include <stdarg.h>

ScriptEngine::ScriptEngine()
{
	mb_init();
}

ScriptEngine::~ScriptEngine()
{
	mb_dispose();
}

void ScriptEngine::ExecuteAsync(std::string Script)
{
  ALLEGRO_THREAD* thread;
  const char* scriptlink = Script.c_str();
	thread = al_create_thread( ScriptEngine::ExecuteAsyncInternal, (void*)scriptlink );
	al_start_thread( thread );

  runningthreads.push_back( thread );
}

void* ScriptEngine::ExecuteAsyncInternal(ALLEGRO_THREAD* Thread, void* Script)
{
	mb_interpreter_t* i = nullptr;

	mb_open( &i );

	RegisterFunctions( i );

	mb_load_string( i, ((const char*)Script) );
	mb_run( i );

	mb_close( &i );

	return nullptr;
}

void ScriptEngine::Execute(std::string Script)
{
	mb_interpreter_t* i = nullptr;

	mb_open( &i );

	RegisterFunctions( i );

	mb_load_string( i, Script.c_str() );
	mb_run( i );

	mb_close( &i );
}

void ScriptEngine::RegisterFunctions( mb_interpreter_t* i )
{
	mb_set_printer( i, ScriptEngine::Print );

	// Register APIs here
	mb_register_func( i, "JANE", ScriptEngine::Test );
}

int ScriptEngine::Print(const char* fmt, ...)
{
  char buffer[1024];
  va_list argptr;

  va_start( argptr, fmt );
  vsnprintf( buffer, sizeof(buffer), fmt, argptr );
  va_end( argptr );

#ifdef WRITE_LOG
  fprintf( FRAMEWORK->LogFile, "Script Output: %s\n", buffer );
#endif

  // TODO: Handle any "PRINT" statements from scripts
  return MB_FUNC_OK;
}

int ScriptEngine::Test( mb_interpreter_t* i, void** l )
{
  // n = JANE(v)
  //   v (int)
  //   n (int) is set to v
  int result = MB_FUNC_OK;
  int v;

  mb_assert( i && l );

  mb_check(mb_attempt_open_bracket(i, l));
  // Get me values from inside the brackets
  mb_check(mb_pop_int(i, l, &v));
  mb_check(mb_attempt_close_bracket(i, l));

  // Return v
  mb_check(mb_push_int(i, l, v));

  return result;
}
