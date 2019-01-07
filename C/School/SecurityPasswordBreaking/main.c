#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#define FLUSH stdin=freopen(NULL,"r",stdin)

#define SIZE 200

void brute_force (char*, int);
void cryptanalysis (char* str);

int main() {
   int shift;
   char str[SIZE+2]; // reserve 2 extra characters
   char str2[SIZE+2];
   
   char err;
   
   do {
        err = 0;

        printf("Please enter a string up to %d characters: ", SIZE);
        fgets(str, sizeof (str), stdin);
        FLUSH;
        str[strcspn(str, "\n")] = 0;
        if (strlen(str) > SIZE || strlen(str) < 1) {
            printf("Incorrect Input! Please try again!\n");
            err = 1;
        }
    } while (err);  

   printf("\nBrute force attack:\n");
   for (shift = 1; shift < 26; shift++) {
       strcpy(str2, str);
       brute_force(str2, 26 - shift);
       printf("shift = %d. %s\n", shift, str2); 
   }
   
   strcpy(str2, str);
   printf("\nFrequency Analysis:\n\n");
   cryptanalysis(str2);
   return 0;
}

void brute_force (char* str, int shift) {
    int i=0;
    
    while(str[i]!='\0') {
        if(str[i]>='a' && str[i]<='z') {
            // character 'a'== 65
            str[i]=(str[i]+shift-'a')%26 + 'a';
        }
        else if (str[i]>='A' && str[i]<='Z') {
            // character 'A' == 97
            str[i]=(str[i]+shift-'A')%26 + 'A';
        }

        i++;
    }
}

void cryptanalysis (char* str) {
    int characters[128] = {0};
    int i, j;
    char char_freq[3];
    int char_freq_count[3];
    
    for(i = 0; str[i] != '\0'; i++) {
        if ((int)str[i] != 32)
            characters[(int)str[i]]++;
    }
    
    int max = 0; int counter = 0; int index = 0;
    while (counter < 3) {
        for(i = 0; i < 128; i++)
        {
            if(characters[i] != 0)
            {
                if(characters[i] >= max) {
                    max = characters[i];
                    index = i;
                }
            }
        }
        char_freq[counter] = (char)index;
        char_freq_count[counter] = max;
        max = 0;
        characters[index] = 0;
        counter++;
    }
    
    int shift;
    char letters[4] = {'e', 't', 'o', 'a'};
    char str3[SIZE+2];
    
    for(i = 0; i < 3; i++){
        strcpy(str3, str);
        printf("Character: %c appears %d times\n", char_freq[i], char_freq_count[i]);
        shift = 0;
        
        for(j = 0; j < 4; j++) {
            shift = (char_freq[i] - 96) - (letters[j] - 96);

            if(shift > 0 && shift < 27) { 
                brute_force(str3, 26 - shift);
                printf("Trying %c -> %c (shift=%d): %s\n", char_freq[i], letters[j], shift, str3);
            }
        }
        printf("\n");
    }
}
