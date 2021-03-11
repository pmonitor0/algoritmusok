#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>
#include <sys/timeb.h>

int PrimSzamE(int szam)
{
    if (szam == 2) return 1;
    else if (szam < 2 || (szam % 2) == 0) return 0;
    int i = 3, k = (int)sqrt(szam);
    int prim = 1;
    while (i <= k && prim)
    {
        if ((szam % i) == 0) prim = !prim;
        i += 2;
    }
    return prim;
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
    int n = 11003;
    int prim;
    ftime(&start);
    prim = PrimSzamE(n); //kb. 38 sec.
    ftime(&end);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    printf("%d\n", prim);
    printf("Eltelt ido: %ld.%03d masodperc\n", seconds, militm);
    return 0;
}
