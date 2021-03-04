#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

long long db = 0;

void Variacio(int arr[], int n, int k)
{
    int* tomb = (int*)calloc(k, sizeof(int));
    int m = 0, j = 0, i = 0;
    while (i >= 0 && i < k)
    {
        j = tomb[i] + 1;
        m = 0;
        while (j <= n && m < i)
        {
            m = 0;
            while (m < i && j != tomb[m]) ++m;
            if (m < i) ++j;
        }
        if (j <= n)
        {
            if (i == k - 1)
            {
                tomb[i] = j;
                ++db;

                /*for (int index = 0; index < k; ++index) printf("%d ", arr[tomb[index] - 1]);
                printf("\n");*/

            }
            else
            {
                tomb[i] = j;
                ++i;
            }
        }
        else
        {
            tomb[i] = 0;
            --i;
        }
    }
    free(tomb);
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
    int arr[500];
    int n = 90, k = 5;
    int i;
    for (i = 0; i < n; ++i) arr[i] = i + 1;
    ftime(&start);
    Variacio(arr, n, k); //Kb. 20-21 sec.
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    printf("Variacio: %ld.%03d masodperc.\n", seconds, militm);
}
