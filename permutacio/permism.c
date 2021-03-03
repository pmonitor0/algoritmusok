#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>
#include <string.h>

void QuickSort(char* arr2, int p, int r)
{
    int Low, High;
    char MidValue;
    Low = p;
    High = r;
    MidValue = arr2[(p + r) / 2];
    do
    {
        while (arr2[Low] < MidValue) ++Low;
        while (arr2[High] > MidValue) --High;
        if (Low <= High)
        {
            char T = arr2[Low];
            arr2[Low] = arr2[High];
            arr2[High] = T;
            ++Low;
            --High;
        }
    } while (Low <= High);
    if (p < High) QuickSort(arr2, p, High);
    if (Low < r) QuickSort(arr2, Low, r);
}


void teszt(char str[])
{
    int n = strlen(str);
    char* str2 = (char*)malloc(sizeof(char) * (n + 1));
    if (str2 == NULL) {
        printf("Memory not allocated.\r\n");
        exit(0);
    }
    strcpy(str2, str);
    QuickSort(str2, 0, n - 1);
    int i, j;
    char temp;
    long db = 0;
    for (;;)
    {
        ++db;
        //printf("%s\n", str);

        for (i = n - 2; i >= 0 && str2[i] >= str2[i + 1]; i--);
        if (i < 0) break;
        for (j = n - 1; str2[j] <= str2[i]; j--);
        temp = str2[i]; str2[i] = str2[j]; str2[j] = temp;
        for (j = i + 1; j < n + i - j; j++)
        {
            temp = str2[j]; str2[j] = str2[n + i - j]; str2[n + i - j] = temp;
        }
    }
    printf("%ld\r\n", db);
    free(str2);
}

long timediff(struct timeb* start, struct timeb* end)
{
    long seconds;
    seconds = (long)(end->time - start->time);
    start->millitm = end->millitm - start->millitm;
    if (0 > start->millitm) {
        start->millitm += 1000;
        seconds--;
    }
    return seconds;
}

int main()
{
    struct timeb start, end;
    long seconds;
    int militm;
    char str[] = "abcdanananaabcdanana";
    //char str[] = "ACBC";
    ftime(&start);
    teszt(str);
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    return 0;
}
