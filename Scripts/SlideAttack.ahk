slideAttackActive := false
Q::
    if(not slideAttackActive)  {
    slideAttackActive := true
    Send, {Xbutton1 Down}
    Send, e
    Send, {Xbutton1 Up}
    slideAttackActive := false
    }
Return
