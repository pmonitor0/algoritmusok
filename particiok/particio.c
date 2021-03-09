#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

long long db = 0;

void particiok_1(int arr[], int szam, int aktosszeg, int j)
{
    if (aktosszeg < 0) return;
    else if (aktosszeg == 0)
    {
        ++db;
        /*for (int i = 1; i < j; ++i) printf("%d ", arr[i]);
        printf("\n");*/
        return;
    }
    int i;
    for (i = 1; i <= szam; ++i)
    {
        arr[j] = i;
        particiok_1(arr, i, aktosszeg - i, j + 1);
    }
}

void particiok_2(int arr[], int osszeg, int j) {

    if (osszeg <= arr[j - 1]) {
        int i, k;

        arr[j] = osszeg;
        ++db;
        /*for (i = 1, k = 0; k + arr[i] < arr[0]; k += arr[i++]) printf("%d ", arr[i]);
        printf("%d\n", arr[i]);*/
    }
    else {
        arr[j] = arr[j - 1] + 1;
    }

    while (--arr[j] > 0) particiok_2(arr, osszeg - arr[j], j + 1);
}

void particiok_3(int arr[], int osszeg) {
    int j, k, m;

    j = 0; k = -1; arr[j] = osszeg + 1;
    while (j >= 0) {
        arr[j]--; k++;
        m = arr[j];
        while (k > 0) {
            j++;
            arr[j] = (k <= m) ? k : m;
            k -= arr[j];
        }
        ++db;

        /*int i;
        for (i = 0; i < j; i++) printf("%d ", arr[i]);
        printf("%d\n", arr[i]);*/

        while ((j >= 0) && (arr[j] <= 1))
        {
            k += arr[j];
            j--;
        }
    }
}

void teszt_1(int n)
{
    int arr[500];
    particiok_1(arr, n, n, 1);
}

void teszt_2(int n)
{
    int tomb[500];
    int i;
    tomb[0] = n;
    particiok_2(tomb, n, 1);
}

void teszt_3(int n)
{
    int arr[500];
    particiok_3(arr, n);
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
    long seconds, seconds2, seconds3;
    int militm, militm2, militm3;
    int n = 118;
    //int n = 5;
    ftime(&start);
    teszt_1(n); //kb. 38 sec.
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    ftime(&start);
    teszt_2(n); //kb. 33 sec.
    ftime(&end);
    seconds2 = timediff(&start, &end);
    militm2 = start.millitm;
    ftime(&start);
    teszt_3(n); //kb. 17 sec.
    ftime(&end);
    seconds3 = timediff(&start, &end);
    militm3 = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds2, militm2);
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds3, militm3);
    return 0;
}
