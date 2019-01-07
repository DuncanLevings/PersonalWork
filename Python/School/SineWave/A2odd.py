"""
Title: Multiple Sine Wave Plotter with Turtle (version even numbers)
Name: Duncan Levings
Description:
Draws multiple sine waves using user inputed amplitude and fequency value, sums them together for squared sine wave
"""

import turtle;
import math;

"""
====createTurtle(turtle color or list, turtle pen color or list, amount of turtles, pensize, speed, shape)====
sets all attributes to turtle or a list of turtles, uses defaults for parameters
"""
def createTurtle(t_color = None, t_penColor = None, amt_of_turtles = None, t_penSize = 1, t_speed = 10, t_shape = 'turtle'):
    #if t_color is not set, default to black
    if t_color == None:
        t_color = 'black'

    #if t_penColor is not set, default to black
    if t_penColor == None:
        t_penColor = 'black';

    #checks if t_color is a list
    if type(t_color) == list:
        countColor = 0;
        #countMax to pevent index out of bounds when assigning the colors
        countMax = len(t_color) - 1;
        isList = True;
    else:
        isList = False;

    #checks if t_penColor is a list
    if type(t_penColor) == list:
        countPenColor = 0;
        #countPenMax to pevent index out of bounds when assigning the colors
        countPenMax = len(t_penColor) - 1;
        isPenList = True;
    else:
        isPenList = False;

    #if amt_of_turtles is not set, default to 1
    if amt_of_turtles == None:
        amt_of_turtles = 1;

    turtList = [];
    #for loop range using amt_of_turtles
    for i in range(amt_of_turtles):
        a_turtle = turtle.Turtle();
        a_turtle.shape(t_shape);

        #if t_color was a list, assign current turtle color to index of t_color[countColor], otherwise assign t_color
        if isList:
            a_turtle.color(t_color[countColor]);
            #if countColor has exceeded countMax, reset countColor to 0, otherwise increment by 1
            if (countColor >= countMax):
                countColor = 0;
            else:
                countColor += 1;
        else:
            a_turtle.color(t_color);

        #if t_penColor was a list, assign current turtle color to index of t_penColor[countPenColor], otherwise assign t_penColor
        if isPenList:
            a_turtle.pencolor(t_penColor[countPenColor]);
            #if countPenColor has exceeded countPenMax, reset countPenColor to 0, otherwise increment by 1
            if (countPenColor >= countPenMax):
                countPenColor = 0;
            else:
                countPenColor += 1;
        else:
            a_turtle.pencolor(t_penColor);

        a_turtle.pensize(t_penSize);
        a_turtle.speed(t_speed);
        turtList.append(a_turtle);
    
    #return 1 turtle or the list of turtles
    if amt_of_turtles == 1:
        return turtList[0];
    else:
        return turtList;

"""
====createScreen(screen color)====
assigns wn to turtle class screen object, assigns a color to it
"""
#creates the screen object and assigns a color to it
def createScreen(color):
    wn = turtle.Screen();
    wn.bgcolor(color);

"""
====setTurtlePosition(turtle object or list, x cordinate, y cordinate)====
uses t to determine if you passed one turtle or a list of turtles, puts pen up to avoid unwanted
drawing, sets new turtle position, sets pen back down
"""
def setTurtlePosition(t, x = 0, y = 0):
    if type(t) == list:
        for i in t:
            i.pu();
            i.setpos(x, y);
            i.pd();
    else:
        t.pu();
        t.setpos(x, y);
        t.pd();

"""
====exit()====
calls exitonclick function from turtle class
"""
def exit():
    print('exiting turtle...');
    turtle.exitonclick();

"""
create a dictonary of turtles and the releated harmonic values
"""
def createTurtDictonary(turtList):
    dictList = {};
    #setting this value to 1 results in odd numbers for harmonic, set to 2 for even number harmonic
    harmonic = 1;
    for i in turtList:
        dictList[i] = harmonic;
        harmonic += 2;

    return dictList; 

"""
draws a sine curve using amplitude for the wave length and
fequency for the amount of wave cycles based on wave length.
"""
def sinCurve(dict, sumTurt, amplitude, frequency):
    a = 2 * math.pi * frequency
    sumY = 0;
    for x in range(amplitude + 1):
        for turt_key, har_val in dict.items():
            y = (amplitude / har_val) * math.sin((x / amplitude) * (a * har_val));
            sumY = sumY + y;
            #can do y * -1 to get same reversed output as example in assignment doc
            turt_key.goto(x, y)
        sumTurt.goto(x, sumY);
        sumY = 0;

#========================= MAIN =========================
amp = 100;
feq = 1;

#sets screen background color
createScreen('lightyellow');

#scales the world coordinates to better center the sine wave
#setworldcoordinates(lower left x, lower left y, upper right x, upper right y)
turtle.setworldcoordinates(-10, (amp + 30) * -1, amp + 15, amp + 30);

#creates the turtle object(s)
turtColor = ['lightblue', 'lightgreen', 'red'];
turtPenColor = ['blue', 'green', 'red'];
turtList = createTurtle(turtColor, turtPenColor, 3);
#main sine wave turtle
sum_wave_turt = createTurtle('black', 'black', 1, 3);

#creates a dictonary of turtles and harmonics
turtDict = createTurtDictonary(turtList);

#draws the sine waves
sinCurve(turtDict, sum_wave_turt, amp, feq);

#exits the program on click
exit();
