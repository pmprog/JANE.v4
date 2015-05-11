
#include "scriptengine.h"

ScriptEngine::ScriptEngine()
{
	mb_init();
}

ScriptEngine::~ScriptEngine()
{
	mb_dispose();
}

void ScriptEngine::Execute(std::string script)
{
	mb_interpreter_t* i = nullptr;

	mb_open( &i );

	mb_load_string( i, script.c_str() );
	mb_run( i );

	mb_close( &i );
}