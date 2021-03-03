#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

void perm(int n)
{
    int a[50];
    int i, j, temp;
    long long db = 0;
    for (i = 0; i < n; i++) a[i] = i + 1;

    for (;;)
    {
        ++db;
        /*for (i = 0; i < n; i++) printf("%d ", a[i]);
        printf("\n");*/

        for (i = n - 2; i >= 0 && a[i] > a[i + 1]; i--);
        if (i < 0) break;
        for (j = n - 1; a[j] < a[i]; j--);
        temp = a[i]; a[i] = a[j]; a[j] = temp;
        for (j = i + 1; j < n + i - j; j++)
        {
            temp = a[j]; a[j] = a[n + i - j]; a[n + i - j] = temp;
        }
    }
    printf("%llu\r\n", db);
}

long timediff_0(struct timeb* start, struct timeb* end)
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

void teszt()
{
    struct timeb start, end;
    long seconds;
    int militm;
    ftime(&start);
    perm(13);
    ftime(&end);
    seconds = timediff_0(&start, &end);
    militm = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc.\n", seconds, militm);
}

int main()
{
    teszt();
    return 0;
}
