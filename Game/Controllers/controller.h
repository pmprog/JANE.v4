
#pragma once

#include "../../Framework/includes.h"
#include "../resources.h"

class Controller
{

	public:
		enum ControllerStateFlags
		{
			NONE = 0,
			NORTH = 1,
			EAST = 2,
			SOUTH = 4,
			WEST = 8,
			FIRE = 16,
			WEAPON = 32,
			ITEM = 64
		};

	private:
		bool Locked;
		int LockedState;

	protected:
		int CurrentState;

	public:

		Controller()
		{
			Locked = false;
			CurrentState = ControllerStateFlags::NONE;
			LockedState = ControllerStateFlags::NONE;
		};

		void LockControls()
		{
			Locked = true;
			LockedState = CurrentState;
		};

		virtual void EventOccurred(Event* e)
		{
			// Null device
		};

		void UnlockControls()
		{
			Locked = false;
			LockedState = CurrentState;
		}

		int GetState()
		{
			if( Locked )
			{
				return LockedState;
			}
			return CurrentState;
		}

};
