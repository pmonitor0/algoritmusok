//Egyszeru 1D cutting(1D vagas optimalizalo) program.

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>
#include <string.h>

void QuickSort(int* arr2, int p, int r)
{
    int Low, High, MidValue;
    Low = p;
    High = r;
    MidValue = arr2[(p + r) / 2];
    do
    {
        while (arr2[Low] < MidValue) ++Low;
        while (arr2[High] > MidValue) --High;
        if (Low <= High)
        {
            int T = arr2[Low];
            arr2[Low] = arr2[High];
            arr2[High] = T;
            ++Low;
            --High;
        }
    } while (Low <= High);
    if (p < High) QuickSort(arr2, p, High);
    if (Low < r) QuickSort(arr2, Low, r);
}

void SzaldarabEsNegyzetOsszegFeltoltese(int arr[], int hossz, int szalhossz, int* szaldarab, long* nosszeg)
{
    int ig = hossz - 1, akthull = szalhossz;
    *nosszeg = 0;
    for (int i = 0; i <= ig; ++i)
    {
        if (arr[i] > akthull)
        {
            ++(*szaldarab);
            *nosszeg += akthull * akthull;
            akthull = szalhossz - arr[i];
        }
        else
        {
            akthull -= arr[i];
        }
    }
    *nosszeg += akthull * akthull;
}

void permism(int darabok[], int eredmeny[], int dbhossz, int szalhossz)
{
    int* darabok_0 = (int*)malloc(sizeof(int) * dbhossz);
    if (darabok_0 == NULL) {
        printf("Memory not allocated.\r\n");
        exit(0);
    }
    memcpy(darabok_0, darabok, sizeof(int) * dbhossz);
    QuickSort(darabok_0, 0, dbhossz - 1);
    int i, j, temp;
    int szaldarab = 600;
    long maxnegyzet = 0;
    for (;;)
    {
        int szdb = 0, nosszeg = 0;
        SzaldarabEsNegyzetOsszegFeltoltese(darabok_0, dbhossz, szalhossz, &szdb, &nosszeg);
        if (szdb < szaldarab)
        {
            memcpy(eredmeny, darabok_0, sizeof(int) * dbhossz);
            szaldarab = szdb;
            maxnegyzet = nosszeg;
        }
        else if (szdb == szaldarab)
        {
            if (nosszeg > maxnegyzet)
            {
                memcpy(eredmeny, darabok_0, sizeof(int) * dbhossz);
                szaldarab = szdb;
                maxnegyzet = nosszeg;
            }
        }
        for (i = dbhossz - 2; i >= 0 && darabok_0[i] >= darabok_0[i + 1]; i--);
        if (i < 0) break;
        for (j = dbhossz - 1; darabok_0[j] <= darabok_0[i]; j--);
        temp = darabok_0[i]; darabok_0[i] = darabok_0[j]; darabok_0[j] = temp;
        for (j = i + 1; j < dbhossz + i - j; j++)
        {
            temp = darabok_0[j]; darabok_0[j] = darabok_0[dbhossz + i - j]; darabok_0[dbhossz + i - j] = temp;
        }
    }
    free(darabok_0);
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

void cutter(char str[])
{
    int szalhossz = 0;
    int ind = 1;
    char sorok[50][20];
    int aktsor = 0, sordb = 0;
    int darabok[500];
    int dbindex = 0, dbhossz = 0;
    int i;

    struct timeb start, end;
    long seconds;
    int militm;

    time_t t = time(NULL);
    struct tm tm = *localtime(&t);
    printf("%02d:%02d:%02d\n", tm.tm_hour, tm.tm_min, tm.tm_sec);

    ftime(&start);
    char* token = strtok(str, "\n");
    while (token != NULL)
    {
        if (ind)
        {
            szalhossz = atoi(token);
            ind = 0;
        }
        else strcpy(sorok[aktsor++], token);
        token = strtok(NULL, "\n");
    }
    sordb = aktsor;
    for (aktsor = 0; aktsor < sordb; ++aktsor)
    {
        int hossz, darab;
        token = strtok(sorok[aktsor], ",");
        if (token != NULL) hossz = atoi(token);
        token = strtok(NULL, ",");
        if (token != NULL) darab = atoi(token);
        for (i = 0; i < darab; ++i)
        {
            darabok[dbindex] = hossz;
            ++dbindex;
        }
    }
    dbhossz = dbindex;
    int eredmeny[500];
    int szaldarab;
    long maxnegyzet;
    permism(darabok, eredmeny, dbhossz, szalhossz);
    SzaldarabEsNegyzetOsszegFeltoltese(eredmeny, dbhossz, szalhossz, &szaldarab, &maxnegyzet);
    ftime(&end);

    int akthull = szalhossz;
    aktsor = 0; aktsor = 1;
    printf("%d: ", aktsor);
    int osszeg = 0;
    for (i = 0; i < dbhossz; ++i)
    {
        osszeg += eredmeny[i];
        if (akthull - eredmeny[i] < 0)
        {
            printf(": %d", akthull);
            printf("\n");
            ++aktsor;
            printf("%d: ", aktsor);
            printf("%d ", eredmeny[i]);
            akthull = szalhossz - eredmeny[i];
            if (i == dbhossz - 1) printf(": %d", akthull);
        }
        else if (akthull == eredmeny[i])
        {
            printf("%d ", eredmeny[i]);
            printf(": %d", 0);
            printf("\n");
            akthull = szalhossz;
            ++aktsor;
            if (i < dbhossz - 1) printf("%d: ", aktsor);
            else if (i == dbhossz - 1) printf(": %d", 0);
        }
        else
        {
            printf("%d ", eredmeny[i]);
            akthull -= eredmeny[i];
            if (i == dbhossz - 1) printf(": %d", akthull);
        }
    }
    int min = osszeg / szalhossz;
    if ((osszeg % szalhossz) > 0) ++min;
    printf("\nElmeleti minimum szal: %d\n", min);
    seconds = timediff(&start, &end);
    militm = start.millitm;
    printf("Eltelt ido: %ld.%03d masodperc.", seconds, militm);
}

int main()
{
    char str[] = "6000\n"
        "1725, 4\n"
        "1625, 4\n"
        "1540, 3\n"
        "1500, 3\n"
        "1346, 3";
    cutter(str);
    return 0;
}
