/* 
 * File:   main.c
 * Author: Duncan
 *
 * Created on October 9, 2018, 5:45 PM
 */

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#define N 100

void main() {
    //declaring initial bool array and values
    bool lockers[N+1] = {false};
    int studentIndex = 1;
    int isEvenOdd, i;
    
    do{
        //setting isEvenOdd to 1 (meaning odd), i to studentIndex for start point of for loop
        isEvenOdd = 1;
        i = studentIndex;
        
        //checking if current studentIndex is even or odd
        if (studentIndex % 2 == 0) {
            //if odd, setting isEvenOdd to 0, setting i to N - by studentIndex + 1 ie. 100 - 2 + 1 = 99 for 2nd student
            isEvenOdd = 0;
            i = N - studentIndex + 1;
        }
        
        for(i; isEvenOdd == 0 ? i >= 0 : i <= N;) {
            //setting boolean at current locker index to opposite its current value
            lockers[i] = !lockers[i];
            //increment or decrement i based on even or odd
            if (isEvenOdd == 0)
                i -= studentIndex;
            else
                i += studentIndex;
        }
            
        //increment studentIndex and loop while studentIndex has not reached N
        studentIndex++;
    } while(studentIndex <= N);
    
    //reusing i for print for loop
    i = 1;
    printf("Open lockers:");
    //storing open lockers in char array
    char segment[6];
    char output[N * 2];
    for(i; i <= N; i++) {
        //if current locker is open, store formated char array in segment and concatenate to output
        if(lockers[i]) {
            snprintf(segment, sizeof(segment), " %d,", i);
            strlcat(output, segment, sizeof(output));
        }
    }
    //changing last char in array to .
    output[strlen(output)-1] = '.';
    printf(output);
}
