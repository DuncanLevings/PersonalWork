F1::
BlockInput On
BlockInput, MouseMove
Send {Esc}
Sleep 25
Send {Down}
Send {Down}
Send {Down}
Send {Down}
Send {Enter}
Sleep 25
Send {Up}
Sleep 25
Send {Enter}
Sleep 2000
Send {Enter}
Sleep 25
Send {Enter}
BlockInput, MouseMoveOff
BlockInput Off
return 	

; doubleshot
Xbutton2::
Send {LButton down}
Send {R down}
Sleep 100
Send {WheelUp}
Sleep 100
Send {WheelDown}
Sleep 200
Send {R Up}
Send {LButton Up}
return

