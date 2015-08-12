
#pragma once

#include "controller.h"

class KeyboardController : public Controller
{

	public:
		int Key_North = ALLEGRO_KEY_UP;
		int Key_East = ALLEGRO_KEY_RIGHT;
		int Key_South = ALLEGRO_KEY_DOWN;
		int Key_West = ALLEGRO_KEY_LEFT;
		int Key_FireButton = ALLEGRO_KEY_LCTRL;
		int Key_SwitchWeapon = ALLEGRO_KEY_SPACE;
		int Key_SwitchItem = ALLEGRO_KEY_ALT;

		KeyboardController() : Controller()
		{
			// TODO: Load Key Map
		};

		virtual void EventOccurred(Event* e)
		{
			if( e->Type == EVENT_KEY_DOWN )
			{
				if( e->Data.Keyboard.KeyCode == Key_North )
				{
					CurrentState |= ControllerStateFlags::NORTH;
				}
				if( e->Data.Keyboard.KeyCode == Key_East )
				{
					CurrentState |= ControllerStateFlags::EAST;
				}
				if( e->Data.Keyboard.KeyCode == Key_South )
				{
					CurrentState |= ControllerStateFlags::SOUTH;
				}
				if( e->Data.Keyboard.KeyCode == Key_West )
				{
					CurrentState |= ControllerStateFlags::WEST;
				}
				if( e->Data.Keyboard.KeyCode == Key_FireButton )
				{
					CurrentState |= ControllerStateFlags::FIRE;
				}
				if( e->Data.Keyboard.KeyCode == Key_SwitchWeapon )
				{
					CurrentState |= ControllerStateFlags::WEAPON;
				}
				if( e->Data.Keyboard.KeyCode == Key_SwitchItem )
				{
					CurrentState |= ControllerStateFlags::ITEM;
				}
			}
			if( e->Type == EVENT_KEY_UP )
			{
				if( e->Data.Keyboard.KeyCode == Key_North )
				{
					CurrentState &= ~ControllerStateFlags::NORTH;
				}
				if( e->Data.Keyboard.KeyCode == Key_East )
				{
					CurrentState &= ~ControllerStateFlags::EAST;
				}
				if( e->Data.Keyboard.KeyCode == Key_South )
				{
					CurrentState &= ~ControllerStateFlags::SOUTH;
				}
				if( e->Data.Keyboard.KeyCode == Key_West )
				{
					CurrentState &= ~ControllerStateFlags::WEST;
				}
				if( e->Data.Keyboard.KeyCode == Key_FireButton )
				{
					CurrentState &= ~ControllerStateFlags::FIRE;
				}
				if( e->Data.Keyboard.KeyCode == Key_SwitchWeapon )
				{
					CurrentState &= ~ControllerStateFlags::WEAPON;
				}
				if( e->Data.Keyboard.KeyCode == Key_SwitchItem )
				{
					CurrentState &= ~ControllerStateFlags::ITEM;
				}
			}
		};

};
