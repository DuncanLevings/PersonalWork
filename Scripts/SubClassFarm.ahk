#IfWinActive, ahk_class Disgaea 5 Complete

`::
{
	BlockInput On
	BlockInput, MouseMove
	
	;=========================================
	;start of match, hovering dispatch
	Select()
	
	;=========================================
	;FIRST UNIT
	Select()
	Select()
	Move("W", 3)
	
	;=========================================
	;FIRST MAGICHANGE
	HoverDispatch()
	Select()
	Select()
	Select()
	Move("W", 2)
	Select()
	SelectMAGICHANGEWeapon1()
	Select()
	
	;=========================================
	;SECOND MAGICHANGE
	HoverDispatch()
	Select()
	Select()
	Select()
	Move("W", 2)
	Select()
	SelectMAGICHANGEWeapon1()
	Select()
	
	;=========================================
	;THIRD MAGICHANGE
	HoverDispatch()
	Select()
	Select()
	Select()
	Move("W", 2)
	Select()
	SelectMAGICHANGEWeapon2()
	Select()
	
	;=========================================
	;FOURTH MAGICHANGE
	HoverDispatch()
	Select()
	Select()
	Select()
	Move("W", 2)
	Select()
	SelectMAGICHANGEWeapon2()
	Select()
	
	;=========================================
	;CAST MAIN UNIT SPELL
	HoverDispatch()
	HoverDispatch()
	Select()
	SelectSPECIAL()
	executeTurn()

	BlockInput, MouseMoveOff
	BlockInput Off
}

HoverDispatch()
{
	Send {Z Down}
	sleep, 75
	Send {Z Up}	

	sleep 75
}

Select()
{
	Send {K Down}
	sleep, 75
	Send {K Up}
	
	sleep, 75
}

Move(Key, Distance)
{
	Loop, %Distance%
	{
		Send {%Key% Down}
		sleep, 75
		Send {%Key% Up}
		sleep, 75
	}
	Select()
	
	sleep, 75
}

SelectMAGICHANGEWeapon1()
{
	;move down to magichange
	Loop, 3
	{
		Send {S Down}
		sleep, 25
		Send {S Up}
		sleep, 25
	}
	;select magichange
	Select()
	
	sleep, 25
	
	;select unit
	Select()
	
	sleep, 25
	
	;select unit to magichange onto
	Select()
	
	sleep, 150
}

SelectMAGICHANGEWeapon2()
{
	;move down to magichange
	Loop, 3
	{
		Send {S Down}
		sleep, 25
		Send {S Up}
		sleep, 25
	}
	;select magichange
	Select()
	
	sleep, 25
	
	;select unit
	Select()
	
	sleep, 25
	
	;select second weapon
	Send {S Down}
	sleep, 25
	Send {S Up}
	sleep, 25
	
	;select weapon to magichange onto
	Select()
	
	sleep, 150
}

SelectSPECIAL()
{
	;move down to special
	Loop, 2
	{
		Send {S Down}
		sleep, 25
		Send {S Up}
		sleep, 25
	}
	;select special
	Select()
	
	sleep, 25
	
	;select spell
	Send {W Down}
	sleep, 25
	Send {W Up}
	sleep, 25
	
	Select()
	Select()
}

executeTurn()
{
	Send {I Down}
	sleep, 25
	Send {I Up}
	sleep, 25
	
	Select()
}

return