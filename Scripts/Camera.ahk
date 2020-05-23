#IfWinActive, RuneScape

;camera movement
Xbutton1::MButton

;target cycle
Tab::` 

;alt tab
<!Tab::Tab

;shift S without accidently doing S keybind
<+s::
BlockInput On
Send <+s
BlockInput Off
return

#IfWinActive