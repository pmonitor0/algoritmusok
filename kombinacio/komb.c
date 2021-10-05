#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

long long db = 0;

void kombinacio(int arr[], int n, int k)
{
    int* tomb = (int*)malloc(k * sizeof(int));
    for (int i = 0; i < k; ++i) tomb[i] = i;
    int j = k - 1, m = n - k;
    do
    {
        ++db;
        /*for (int i = 0; i < k; ++i) printf("%d ", arr[tomb[i]]);
        printf("\n");*/

        ++tomb[j];
        if (tomb[j] >= n)
        {
            while (1)
            {
                if (tomb[j] >= m + j)
                {
                    if (--j == -1) break;
                }
                else break;
            }
            if (j < 0) break;
            ++tomb[j];
            ++j;
            while (j < k)
            {
                tomb[j] = tomb[j - 1] + 1;
                ++j;
            }
            --j;
        }
    } while (1);
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
    long seconds, seconds2;
    int militm, militm2;
    int arr[500];
    int i;
    int n = 250, k = 5;
    //int n = 6, k = 4;
    for (i = 0; i < n; i++) arr[i] = i + 1;
    ftime(&start);
    kombinacio(arr, n, k); //kb. 11-12 sec.
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    return 0;
}
