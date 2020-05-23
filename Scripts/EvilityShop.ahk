#IfWinActive, ahk_class Disgaea 5 Complete

`::
{
	BlockInput On
	BlockInput, MouseMove

	Select()

	BlockInput, MouseMoveOff
	BlockInput Off
}

Select()
{
Loop, 250
	{
		Send {K Down}
		sleep, 25
		Send {K Up}
		sleep, 25
		Send {K Down}
		sleep, 25
		Send {K Up}
		sleep, 300
	}
}

return