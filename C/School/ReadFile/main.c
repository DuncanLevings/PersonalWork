#include <stdio.h>
#include <stdlib.h>
#include <string.h>

const unsigned int MAX_SIZE=100;
typedef unsigned int uint;

// This function will be used to swap "pointers".
void swap(char** , char** );

// Bubble sort function here.
void bubbleSort(char**, uint);

// Read quotes from quotes.txt file file and add them to array. Adjust the size as well!
// Note: size should reflect the number of quotes in the array/quotes.txt file!
void read_in(char**, uint*);

// Print the quotes using array of pointers.
void print_out(char**, uint);

// Save the sorted quotes in the output.txt file
void write_out(char**, uint);

// Free memory!
void free_memory(char**, uint);

int main() {

    // Create array of pointers. Each pointer should point to heap memory where
    // each quote is kept. I.e. arr[0] points to quote N1 saved on the heap.
    char *arr[MAX_SIZE];

    // Number of quotes in the file quotes.txt. Must be adjusted when the file is read!
    uint size=MAX_SIZE; 
    
    read_in(arr, &size);

    printf("--- Input:\n");
    print_out(arr, size);
    
    bubbleSort(arr, size);
    
    printf("--- Output:\n");    
    print_out(arr, size);
    write_out(arr, size);
    
    free_memory(arr, size);
    
    return (0);
}

//swap pointers
void swap(char** ptr1, char** ptr2) {
    char* temp = *ptr1;
    *ptr1 = *ptr2;
    *ptr2 = temp;
}

//bubble sort for quotes
void bubbleSort(char** arr, uint n) {
    int i, j, ret;
    for (i = 0; i < n - 1; i++) {
        for (j = 0; j < n - i - 1; j++) {
            if (strlen(arr[j]) > strlen(arr[j + 1]))
                swap(&arr[j], &arr[j + 1]);
            else if (strlen(arr[j]) == strlen(arr[j + 1])) {
                //when two strings are equal length check lexicographically
                ret = strcoll(arr[j], arr[j + 1]);
                if (ret > 0)
                    swap(&arr[j], &arr[j + 1]);
                else
                    swap(&arr[j + 1], &arr[j]);
            }
        }
    }
}

//reads quotes from file, allocates heap memory and sets pointer array
void read_in(char** arr, uint* size) {
    FILE *quotes;
    char str[1000];
    register int i = 0;
    
    // Program exits if file pointer returns NULL.    
    if ((quotes = fopen("quotes.txt", "r")) == NULL) {
        perror("open() for quotes.txt");
        exit(1);
    }

    // reads text until newline 
    while (!feof(quotes)) {
        if (i > MAX_SIZE - 1)
        {
            printf("MAX_SIZE of %d was reached! no more lines from quotes.txt will being read.\n", MAX_SIZE);
            break;
        }
        //reading until new line
        fscanf(quotes, "%[^\n]\n", str);
        
        //clearing newline and returns
        str[strcspn(str, "\n")] = '\0';
        str[strcspn(str, "\r")] = '\0'; 
        *size = strlen(str) + 1; 

        //allocate memory for each quote
        arr[i] = (char *) calloc (*size, sizeof (char));
        
        if (arr[i]==NULL) {
            printf("Cannot allocate memory for %p characters!\n", size);
            exit(1);
        }
        
        //copying the str quote onto heap and setting pointer from arr
        strncpy(arr[i], str, *size);
        
        //clearing str array
        memset(&str[0], 0, sizeof(str));

        i++;
    }

    fclose(quotes);
    
    //setting size to max # of quotes from file
    *size = i;
}

//printing out strings from pointer array
void print_out(char** arr, uint size) {
    for (int i = 0; i < size; i++) {
        printf("%s\n", arr[i]);
    }
}

//writes out quotes to output from pointer array
void write_out(char** arr, uint size) {
    FILE *fwp;

    if ((fwp = fopen("output.txt", "w")) == NULL) {
        perror("open() for output.txt");
        exit(1);         
    }   
    
    //writes to file
    for (int i = 0; i < size; i++) {
        fprintf(fwp, "%s\n", arr[i]);
    }
    
    fclose(fwp);
}

//frees memory of all pointers in pointer array
void free_memory(char** arr, uint size) {
    for (int i = 0; i < size; i++) {
        free(arr[i]);
    }
}
