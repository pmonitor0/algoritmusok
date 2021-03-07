#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

long long db = 0;

void IsmVariacio(int arr[], int n, int k)
{
    int* tomb = (int*)calloc(k, sizeof(int));
    int i;
    int j = k - 1;
    do
    {
        ++db;
        /*for (int i = 0; i < k; ++i) printf("%d ", arr[tomb[i]]);
        printf("\n");*/

        ++tomb[j];
        if (tomb[j] > n - 1)
        {
            while (j > -1)
            {
                if (tomb[j] >= n - 1)
                {
                    --j;
                    if (j == -1) return;
                }
                else break;
            }
            ++tomb[j];
            ++j;
            while (j < k)
            {
                tomb[j] = 0;
                ++j;
            }
            --j;
        }
    } while (1);
    free(tomb);
}

void IsmVar(int arr[], int n, int k)
{
    int tomb[50], i;
    for (i = 1; i <= k; i++) tomb[i] = 1;
    tomb[k] = 0;
    i = k;
    while (1) {
        if (tomb[i] == n) {
            i--;
            if (i == 0) break;
        }
        else {
            tomb[i]++;
            while (i < k) {
                i++;
                tomb[i] = 1;
            }

            ++db;
            /*int j;
            for (j = 1; j <= k; j++) printf("%d ", arr[tomb[j] - 1]);
            printf("\n");*/
        }
    }
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
    int i, n = 120, k = 5;
    for (i = 0; i < n; i++) arr[i] = i + 1;
    ftime(&start);
    IsmVariacio(arr, n, k); //kb 36 sec.
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    db = 0;
    ftime(&start);
    IsmVar(arr, n, k); //kb. 30 sec.
    ftime(&end);
    seconds2 = timediff(&start, &end);
    militm2 = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds2, militm2);
    return 0;
}
