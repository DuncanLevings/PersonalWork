#IfWinActive, ahk_class Disgaea 5 Complete

`::
{
	BlockInput On
	BlockInput, MouseMove
	
	;=========================================
	;start of match, hovering dispatch
	Select()
	
	;=========================================
	;FIRST SACRIFICE
	Select()
	Select()
	Move("W", 4)
	Select()
	SelectATTACK()
	
	;=========================================
	;SECOND SACRIFICE
	HoverDispatch()
	Select()
	Select()
	Select()
	MoveDiagonal("W", 3, "A", 1)
	Select()
	SelectATTACK()
	
	;=========================================
	;FIRST UNIT
	HoverDispatch()
	Select()
	Select()
	Select()
	Move("W", 1)
	
	;=========================================
	;SECOND UNIT
	HoverDispatch()
	Select()
	Select()
	Select()
	MoveDiagonal("D", 1, "W", 1)
	
	;=========================================
	;THIRD UNIT
	HoverDispatch()
	Select()
	Select()
	Select()
	Move("D", 1)
	
	;=========================================
	;FOURTH UNIT
	HoverDispatch()
	Select()
	Select()
	SelectMAGICBOOST()
	
	;=========================================
	;CAST OVERLOAD AND END TURN
	executeTurn()
	sleep, 1500
	HoverDispatch()
	Select()
	SelectOVERLOAD()
	Select()
	sleep, 250
	endTurn()

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

MoveDiagonal(Key1, Distance1, Key2, Distance2)
{
	Loop, %Distance1%
	{
		Send {%Key1% Down}
		sleep, 75
		Send {%Key1% Up}
		sleep, 75
	}
	Loop, %Distance2%
	{
		Send {%Key2% Down}
		sleep, 75
		Send {%Key2% Up}
		sleep, 75
	}
	Select()
	
	sleep, 75
}

SelectMAGICBOOST()
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
	Select()
	
	;select AOE
	Send {D Down}
	sleep, 25
	Send {D Up}
	
	
	sleep, 25
	
	Select()
	Select()
}

SelectATTACK()
{
	;move down to attack
	Send {S Down}
	sleep, 25
	Send {S Up}
	sleep, 25
	
	;select attak
	Select()
	
	sleep, 25

	Select()
}

SelectOVERLOAD()
{
	;move down to overload
	Loop, 3
	{
		Send {W Down}
		sleep, 25
		Send {W Up}
		sleep, 25
	}
	;select overload
	Select()
	Select()
	
	sleep, 250
}

executeTurn()
{
	Send {I Down}
	sleep, 25
	Send {I Up}
	sleep, 25
	
	Select()
}

endTurn()
{
	Send {I Down}
	sleep, 25
	Send {I Up}
	sleep, 25
	
	Send {S Down}
	sleep, 25
	Send {S Up}
	sleep, 25
	
	Select()
}

return