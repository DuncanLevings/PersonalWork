#IfWinActive, ahk_exe Terraria.exe
#If !GetKeyState("NumLock","T")
LShift::
if getkeystate("A")
{
	Send, {D Down}
	Sleep, 10
	Send, {D Up}
	Sleep, 10
	Send, {D Down}
	Sleep, 10
	Send, {D Up}
}
else
{
	Send, {A Down}
	Sleep, 10
	Send, {A Up}
	Sleep, 10
	Send, {A Down}
	Sleep, 10
	Send, {A Up}
}
Return
#If
#IfWinActive
