#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

long long db = 0;

void osszesreszhalmaz(int arr[], int tomb[], int n)
{
    int i = 0;
    while (i < n)
    {
        tomb[i] = !tomb[i];
        if (tomb[i]) break;
        ++i;
    }
    if (i == n) return;

    /*for (int j = 0; j < n; ++j)
    {
        if (tomb[j]) printf("%d ", arr[j]);
    }
    printf("\n");*/

    osszesreszhalmaz(arr, tomb, n);
}

void teszt_2(int arr[], int n)
{
    int tomb[50];
    int i;
    for (i = 0; i < n; i++) tomb[i] = 0;
    osszesreszhalmaz(arr, tomb, n);
}

void teszt_1(int arr[], int n)
{
    int tomb[50];
    int darab = 0;
    for (int i = 0; i < n; ++i) tomb[i] = 0;
    do
    {
        int i = 0;
        while (i < n)
        {
            tomb[i] = !tomb[i];
            if (tomb[i]) break;
            ++i;
        }
        if (i == n) break;

        ++db;
        /*for (int j = 0; j < n; ++j)
        {
            if (tomb[j]) printf("%d ", arr[j]);
        }
        printf("\n");*/
    } while (1);
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
    long seconds, seconds2;
    int militm, militm2;
    int arr[500];
    int i, n = 33;
    for (i = 0; i < n; i++) arr[i] = i + 1;
    ftime(&start);
    teszt_1(arr, n); //kb. 17-18 sec.
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    ftime(&start);
    teszt_2(arr, n); //kb. 18-19 sec.
    ftime(&end);
    seconds2 = timediff(&start, &end);
    militm2 = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds2, militm2);
    return 0;
}
