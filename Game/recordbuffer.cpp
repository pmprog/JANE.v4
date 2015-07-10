
#include "recordbuffer.h"
#include "boot.h"

void RecordBuffer::Begin()
{
}

void RecordBuffer::Pause()
{
}

void RecordBuffer::Resume()
{
}

void RecordBuffer::Finish()
{
}

void RecordBuffer::EventOccurred(Event *e)
{
	if( e->Type == EVENT_KEY_DOWN )
	{
		if( e->Data.Keyboard.KeyCode == ALLEGRO_KEY_SPACE )
		{
			delete FRAMEWORK->ProgramStages->Pop();
			FRAMEWORK->ProgramStages->Push( new BootUp() );
		}
	}
}

void RecordBuffer::Update()
{
}

void RecordBuffer::Render()
{
	al_clear_to_color( al_map_rgb(0,0,0) );
}

bool RecordBuffer::IsTransition()
{
	return false;
}
