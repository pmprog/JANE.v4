
#pragma once

#include <string>
#include <list>
#include "../Framework/includes.h"
#include "my_basic.h"

class ScriptEngine {

	private:
	  std::list<ALLEGRO_THREAD*> runningthreads;

    static void* ExecuteAsyncInternal(ALLEGRO_THREAD* Thread, void* Script);
    static void RegisterFunctions( mb_interpreter_t* i );

    static int Print(const char* fmt, ...);

    // APIs
    static int Test( mb_interpreter_t* i, void** l );

	public:
		ScriptEngine();
		~ScriptEngine();

		void Execute(std::string Script);
		void ExecuteAsync(std::string Script);


};
