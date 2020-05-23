;; Dungeon Defenders -- Wheel Automation
;; Made by Duncan Levings

;; REQUIREMENTS:
;; Windowed mode and the host of the game.
;; All pixel cords are based on 1080x1920 resolution

;; USAGE: 
;; Press hotkey corresponding to split screen or full, do not click during process of wheel
;; If wheel detection fails, script will stop scanning pixels after 6 seconds (2 seconds attempt per column)
;; In this case you can click wheel manually

#SingleInstance Force ;No point having multiple instances, slows down PC
#IfWinActive, Dungeon Defenders ;Disables hotkeys when the game is minimized or isn't running. This doesn't terminate the script.
#NoEnv
#MaxHotkeysPerInterval 20
#Persistent ;Allows use of global variables instead of just hotkeys
SendMode Input  ;Input|Play|Event|InputThenPlay, ;Faster and more reliable compared to SendEvent
Menu, tray, Tip, DunDef - Jester Wheel

;color is opposite from what Active Window Info shows
CoordMode, Pixel, Screen

/* You can customize the hotkeys to your liking using the following
	! ALT, ^ CTRL, + SHIFT, # WIN
	<! (< left key, > right key)
	& means combine keys
	* means any ALT/CTRL/SHIFT
	~ means pass input through
	$ means can send itself without recursion
	& means combine keys
	Up means fired on release
	
	Example Hotkey: 
	<!#n Up:: ; call when n is released and left alt is down
	return
*/

;Y position to check for fullscreen
Y_F = 520
;Y position to check for 4 player splitscreen
Y_S = 270

;Fullscreen
ColumnsF := [765, 965, 1150]

;3 player splitscreen
ColumnsM := [295, 485, 675]

;4 player splitscreen
ColumnsS := [395, 485, 580]

;=============================================================================
;HEAL, Fullscreen
F1::
SendInput, 3
Sleep, 500
Heal(30, Y_F, ColumnsF)
return

;=============================================================================
;KILL, Fullscreen
F2::
SendInput, 3
Sleep, 500
Kill(30, Y_F, ColumnsF)
return

;=============================================================================
;HEAL, 3 player splitscreen, JESTER MUST BE ON LEFT
F3::
SendInput, 3
Sleep, 500
Heal(20, Y_F, ColumnsM)
return

;=============================================================================
;KILL, 3 player splitscreen, JESTER MUST BE ON LEFT
F4::
SendInput, 3
Sleep, 500
Kill(20, Y_F, ColumnsM)
return

;=============================================================================
;HEAL, 4 player splitscreen, JESTER MUST BE ON TOP LEFT
F5::
SendInput, 3
Sleep, 500
Heal(15, Y_S, ColumnsS)
return

;=============================================================================
;KILL, 4 player splitscreen, JESTER MUST BE ON TOP LEFT
F6::
SendInput, 3
Sleep, 500
Kill(15, Y_S, ColumnsS)
return

;=============================================================================

Heal(margin, y, columns)
{
	HealColor = 0x0000C2
	for index, element in columns
	{
		X1 := element - margin
		X2 := element + margin
		Y1 := y - margin
		Y2 := y + margin
		FindColor(HealColor, X1, X2, Y1, Y2)
	}
}

Kill(margin, y, columns)
{
	SwordColor = 0xD9BC6A
	CrystalColor = 0xFFE505
	for index, element in columns
	{
		color = %SwordColor%
		if index = 3
		{
			color = %CrystalColor%
		}	
		X1 := element - margin
		X2 := element + margin
		Y1 := y - margin
		Y2 := y + margin
		FindColor(color, X1, X2, Y1, Y2)
	}
}

FindColor(color, x1, x2, y1, y2)
{
	start := A_TickCount
	Loop
		{
			PixelSearch, ColorX, ColorY, %x1%, %y1%, %x2%, %y2%, %color%, 20, Fast
			If %ColorX%
			{
				MouseClick, Left
				break
			} Else {
				Sleep, 25
			}
			
			now := A_TickCount-start
			if now > 2000
			{
				break 1
			}
		}
}
