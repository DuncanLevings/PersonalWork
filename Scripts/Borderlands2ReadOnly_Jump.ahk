F1::
BlockInput On
BlockInput, MouseMove
Send {Esc}
Sleep 100
Send {Control Down}
Send {End Down}
Sleep 50
Send {Control Up}
Send {End Up}
Sleep 2000
Send {Enter}
Sleep 25
Send {Enter}
Sleep 6500
Send {Numpad4}
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

