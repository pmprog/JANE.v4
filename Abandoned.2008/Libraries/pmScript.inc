extrn	LoadScriptFile:proc
extrn	LoadScriptMemory:proc
extrn	ResetVM:proc
extrn	Restart:proc
extrn	RunScript:proc
extrn	StepInto:proc
extrn	ClearScript:proc

;;; Callback Proc
; sampleCallback		STDCALL, vmScriptHandle:DWORD, vmMessage:DWORD, vmRegisterIndex:DWORD, ptrParameter:DWORD


VM_CALLBACK_UNKNOWN_ERR		equ	00h
VM_CALLBACK_HOST_REQUEST	equ	01h
VM_CALLBACK_HOST_SET		equ	02h
VM_CALLBACK_HOST_READONLY	equ	03h
VM_CALLBACK_CODE_BREAK		equ	04h
VM_CALLBACK_CODE_END		equ	05h

VM_CALLBACK_BREAK_INDEX_UNKNOWN_ERR		equ	00h
VM_CALLBACK_BREAK_INDEX_INVALIDOPCODE		equ	01h
VM_CALLBACK_BREAK_INDEX_STACKEMPTY		equ	02h
VM_CALLBACK_BREAK_INDEX_STACKFULL		equ	03h
VM_CALLBACK_BREAK_INDEX_PCOOB			equ	04h
VM_CALLBACK_BREAK_INDEX_VAROOB			equ	05h
VM_CALLBACK_BREAK_INDEX_OVERFLOW		equ	06h


VM_FLAG_ZERO		equ	01h
VM_FLAG_BELOW		equ	02h
VM_FLAG_END		equ	40h
VM_FLAG_BREAK		equ	80h



;;;;;;;;;;;;;;;;;;;;;;;;;;; LoadScriptFile
;				In:	ptrFilename (DWORD) - Script to load
;					StackSize (DWORD) - Maximum calls allowed in stack
;					ptrCallback (DWORD) - Pointer to callback function in host
;				Out:	ScriptHandle (DWORD) - Pointer to script memory block
;				Desc:	Allocates memory to new script, sets up registers
;					and stack

;;;;;;;;;;;;;;;;;;;;;;;;;;; LoadScriptMemory
;				In:	ptrCodeBlock (DWORD) - Script block
;					CodeLength (DWORD) - Length of script block
;					StackSize (DWORD) - Maximum calls allowed in stack
;					ptrCallback (DWORD) - Pointer to callback function in host
;				Out:	ScriptHandle (DWORD) - Pointer to script memory block
;				Desc:	Allocates memory to new script, sets up registers
;					and stack

;;;;;;;;;;;;;;;;;;;;;;;;;;; ResetVM
;				In:	Nothing
;				Out:	Nothing
;				Desc:	Destroys all scripts

;;;;;;;;;;;;;;;;;;;;;;;;;;; ClearScript
;				In:	hndScript (DWORD) - Script Handle
;				Out:	Nothing
;				Desc:	Removes script from memory

;;;;;;;;;;;;;;;;;;;;;;;;;;; Restart
;				In:	hndScript (DWORD) - Script Handle (pointer to memory block)
;				Out:	Nothing
;				Desc:	Rebuilds the stack, resets registers etc.

;;;;;;;;;;;;;;;;;;;;;;;;;;; RunScript
;				In:	hndScript (DWORD) - Script Handle (pointer to memory block)
;				Out:	Nothing
;				Desc:	Executes script until a break is hit (END opcode)