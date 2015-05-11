
#pragma once

#include <string>
#include "my_basic.h"

class ScriptEngine {

	private:

	public:
		ScriptEngine();
		~ScriptEngine();

		void Execute(std::string script);


};