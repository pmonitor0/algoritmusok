#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

long long db = 0;

void ismkomb(int arr[], int n, int k)
{
    int* tomb = (int*)malloc(k * sizeof(int));
    for (int i = 0; i < k; ++i) tomb[i] = 0;
    int j = k - 1, m = n - 1;
    do
    {
        ++db;
        /*for (int i = 0; i < k; ++i) printf("%d ", arr[tomb[i]]);
        printf("\n");*/

        ++tomb[j];
        if (tomb[j] >= n)
        {
            --j;
            while (j > -1)
            {
                if (tomb[j] == m)
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
                tomb[j] = tomb[j - 1];
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
    long seconds;
    int militm;
    int arr[500];
    int i;
    //int n = 4, k = 3;
    int n = 250, k = 5;
    for (i = 0; i < n; i++) arr[i] = i + 1;
    ftime(&start);
    ismkomb(arr, n, k); //kb. 16-17 sec;
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    return 0;
}
