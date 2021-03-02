using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Cutter
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "6000" + Environment.NewLine +
                "1725, 4" + Environment.NewLine +
                "1625, 4" + Environment.NewLine +
                "1540, 3" + Environment.NewLine +
                "1500, 3" + Environment.NewLine +
                "1346, 3";
            Stopwatch sw = new Stopwatch();
            long t_1 = 0;
            Cutting ct = new Cutting(str);
            Console.WriteLine(DateTime.Now);
            sw.Start();
            ct.Manipulal();
            t_1 = sw.ElapsedMilliseconds;
            Console.Write(ct.ToString());
            Console.Write("Eltelt idő: {0}", t_1);
        }
    }

    public class Cutting
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(int[] dest, int[] src, uint count);
        static int intSize = Marshal.SizeOf(typeof(int));

        int[] forras;
        int szalhossz;
        int[] Eredmeny;
        int Szaldarab, Maxnegyzet, Osszeg;
        int[,] KettodArr;

        public Cutting(string text)
        {
            forras = ReadFromString(text, ref szalhossz);
            Array.Sort(forras);
        }

        public void Manipulal()
        {
            int n = forras.Length;
            Eredmeny = new int[n];
            Array.Copy(forras, Eredmeny, n);
            SzaldarabEsNegyzetOsszegFeltoltese(forras, n, szalhossz, ref Szaldarab, ref Maxnegyzet);
            Manipulal(forras);
            SzaldarabEsNegyzetOsszegFeltoltese(Eredmeny, Eredmeny.Length, szalhossz, ref Szaldarab, ref Maxnegyzet);
            int darab = Eredmeny.Length;
            Osszeg = 0;
            for (int i = 0; i < darab; ++i) Osszeg += Eredmeny[i];
        }

        void SzaldarabEsNegyzetOsszegFeltoltese(int[] arr, int hossz, int szalhossz, ref int szaldarab, ref int negyzetosszeg)
        {
            int ig = hossz - 1, akthull = szalhossz;
            negyzetosszeg = 0;
            for (int i = 0; i <= ig; ++i)
            {
                if (arr[i] > akthull)
                {
                    ++szaldarab;
                    negyzetosszeg += akthull * akthull;
                    akthull = szalhossz - arr[i];
                }
                else
                {
                    akthull -= arr[i];
                }
            }
            negyzetosszeg += akthull * akthull;
        }

        void Manipulal(int[] tomb)
        {
            int size = tomb.Length;
            QuickSort(tomb, 0, size - 1);
            //Array.Sort(tomb);
            int n = tomb.Length;
            int i, j, temp;
            uint mashossz = (uint)(size * intSize);
            while (true)
            {
                int szdb = 0, maxn = 0;
                SzaldarabEsNegyzetOsszegFeltoltese(tomb, size, szalhossz, ref szdb, ref maxn);
                if (szdb < Szaldarab)
                {
                    CopyMemory(Eredmeny, tomb, mashossz);
                    Szaldarab = szdb;
                    Maxnegyzet = maxn;
                }
                else if (szdb == Szaldarab)
                {
                    if (maxn > Maxnegyzet)
                    {
                        CopyMemory(Eredmeny, tomb, mashossz);
                        Szaldarab = szdb;
                        Maxnegyzet = maxn;
                    }
                }

                for (i = n - 2; i >= 0 && tomb[i] >= tomb[i + 1]; --i) ;
                if (i < 0) break;
                for (j = n - 1; tomb[j] <= tomb[i]; --j) ;
                temp = tomb[i]; tomb[i] = tomb[j]; tomb[j] = temp;
                for (j = i + 1; j < n + i - j; ++j)
                {
                    temp = tomb[j]; tomb[j] = tomb[n + i - j]; tomb[n + i - j] = temp;
                }
            }

        }

        int[,] ConvertTomb1dToTomb2d(int[] tomb, int szalhossz)
        {
            int[,] arr = new int[1 + tomb.Length, 2 + tomb.Length];
            int aktsor = 0;
            arr[aktsor, 0] = 0;
            arr[aktsor, 1] = szalhossz;
            arr[1 + aktsor, 1] = szalhossz;
            for (int i = 0; i < tomb.Length; ++i)
            {
                if (arr[aktsor, 1] >= tomb[i])
                {
                    arr[aktsor, 1] -= tomb[i];
                    arr[aktsor, 2 + arr[aktsor, 0]] = tomb[i];
                    ++arr[aktsor, 0];
                }
                else
                {
                    ++aktsor;
                    arr[aktsor, 1] = szalhossz - tomb[i];
                    arr[aktsor, 2] = tomb[i];
                    arr[aktsor, 0] = 1;
                }
            }
            arr[1 + aktsor, 1] = szalhossz;
            Szaldarab = aktsor;
            return arr;
        }

        void RendezHulladekCsokkeno2D(int[,] tomb2D, int sordarab)
        {
            int mintempindex;
            for (int i = 0; i < sordarab - 1; ++i)
            {
                mintempindex = i;
                for (int j = 1 + i; j < sordarab; ++j)
                {
                    if (tomb2D[j, 1] > tomb2D[mintempindex, 1])
                    {
                        mintempindex = j;
                    }
                }
                if (mintempindex > i)
                {
                    //csere i,mintempindex
                    SzalCsere2D(tomb2D, i, mintempindex);
                }
            }
        }

        void SzalCsere2D(int[,] Tomb2D, int szal1, int szal2)
        {
            int mashossz = Tomb2D[szal1, 0];
            if (Tomb2D[szal2, 0] > Tomb2D[szal1, 0]) mashossz = Tomb2D[szal2, 0];
            int tmp;
            for (int i = 0; i < mashossz; ++i)
            {
                tmp = Tomb2D[szal1, 2 + i];
                Tomb2D[szal1, 2 + i] = Tomb2D[szal2, 2 + i];
                Tomb2D[szal2, 2 + i] = tmp;
            }
            tmp = Tomb2D[szal1, 0];
            Tomb2D[szal1, 0] = Tomb2D[szal2, 0];
            Tomb2D[szal2, 0] = tmp;
            tmp = Tomb2D[szal1, 1];
            Tomb2D[szal1, 1] = Tomb2D[szal2, 1];
            Tomb2D[szal2, 1] = tmp;
        }

        void SzalRendez2D(int[,] tomb2d, int sordarab)
        {
            for (int i = 0; i < sordarab; ++i)
            {
                int[] arr = new int[tomb2d[i, 0]];
                int k = 2;
                int jmax = arr.Length;
                for (int j = 0; j < jmax; ++j, ++k) arr[j] = tomb2d[i, k];
                Array.Sort(arr); Array.Reverse(arr);
                k = 2;
                for (int j = 0; j < jmax; ++j, ++k) tomb2d[i, k] = arr[j];
            }
        }

        static int[] ReadFromString(string text, ref int szalhossz)
        {
            string[] strt2 = text.Split(Environment.NewLine.ToCharArray());
            if (!int.TryParse(strt2[0], out szalhossz)) throw new ApplicationException("A szál hossza nem szám!!!");
            int[] tomb = new int[50];
            int aktindex = 0;
            for (int i = 1; i < strt2.Length; ++i)
            {
                if (strt2[i] != "")
                {
                    string[] strt3 = strt2[i].Split(',');
                    if (strt3.Length < 2) throw new ApplicationException("Méretet és darabszámot is meg kell adni \",\"(vessző)-vel elválasztva!!!");
                    if (strt3.Length > 2) throw new ApplicationException("Csak egy méretet és darabszámot lehet egy sorban megadni!!!");
                    int meret, darab;
                    if (!int.TryParse(strt3[0], out meret)) throw new ApplicationException("Az egyik méret nem szám!!!");
                    if (!int.TryParse(strt3[1], out darab)) throw new ApplicationException("Az egyik darabszám nem szám!!!");
                    if (aktindex + darab > tomb.Length) Array.Resize(ref tomb, aktindex + darab + 50);
                    for (int j = 0; j < darab; ++j) tomb[aktindex++] = meret;
                }
            }
            Array.Resize(ref tomb, aktindex);
            return tomb;
        }

        static void QuickSort(int[] arr2, int p, int r)
        {//quicksort
            int Low, High;
            int MidValue;
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

        string Sorstringbe(int[,] arr, int index)
        {
            StringBuilder s = new StringBuilder();
            int jmax = 2 + arr[index, 0];
            for (int j = 2; j < jmax; ++j)
            {
                s.Append(arr[index, j].ToString().PadLeft(4, ' ') + " ");
            }
            s.Append(": " + arr[index, 1].ToString().PadLeft(4, ' '));
            return s.ToString();
        }


        public override string ToString()
        {
            KettodArr = ConvertTomb1dToTomb2d(Eredmeny, szalhossz);
            RendezHulladekCsokkeno2D(KettodArr, 1 + Szaldarab);
            SzalRendez2D(KettodArr, 1 + Szaldarab);
            StringBuilder s = new StringBuilder();
            int sormax = 1 + Szaldarab;
            int osszhull = 0;
            int hullnegyzet = 0;
            int[] indexek = new int[2 + Szaldarab];
            int ind = 0;
            indexek[ind] = 0;
            ++ind;
            osszhull += KettodArr[0, 1];
            hullnegyzet += (KettodArr[0, 1] * KettodArr[0, 1]);
            for (int i = 1; i < sormax; ++i)
            {
                osszhull += KettodArr[i, 1];
                hullnegyzet += (KettodArr[i, 1] * KettodArr[i, 1]);
                if (KettodArr[i, 1] != KettodArr[i - 1, 1])
                {
                    indexek[ind] = i;
                    ++ind;
                }
            }
            indexek[ind] = sormax;

            string[] strt = new string[1 + Szaldarab];
            int strtind = 0; ;
            for (ind = 0; indexek[ind] < sormax; ++ind)
            {
                int sormaxi = indexek[1 + ind];
                Array.Resize(ref strt, sormaxi - indexek[ind]);
                strtind = 0;
                int i;
                for (i = indexek[ind]; i < sormaxi; ++i)
                {
                    strt[strtind] = Sorstringbe(KettodArr, i);
                    ++strtind;
                }
                Array.Sort(strt); Array.Reverse(strt);

                sormaxi = strt.Length;
                int akt = 0;
                string[] erdm = new string[sormaxi];
                int[] db = new int[sormaxi];
                for (i = 0; i < sormaxi; ++i)
                {
                    if (erdm[akt] == null)
                    {
                        erdm[akt] = strt[i];
                        db[akt] = 1;
                    }
                    else
                    {
                        if (strt[i] == erdm[akt]) ++db[akt];
                        else
                        {
                            ++akt;
                            erdm[akt] = strt[i];
                            db[akt] = 1;
                        }
                    }
                }
                for (i = 0; i < sormaxi; ++i)
                {
                    if (erdm[i] == null) break;
                    s.AppendLine(db[i].ToString().PadLeft(3, ' ') + " db " + erdm[i]);
                }
            }

            s.AppendLine(Environment.NewLine + "Összesen " + Eredmeny.Length.ToString().PadLeft(4, ' ') + " darab " + (1 + Szaldarab).ToString().PadLeft(4, ' ') + " szálban.");
            s.AppendLine("Hulladék: " + osszhull.ToString().PadLeft(4, ' ') + " Összeg: " + Osszeg.ToString().PadLeft(4, ' '));
            s.AppendLine("Négyzetösszeg: " + hullnegyzet.ToString().PadLeft(10, ' '));
            int min = Osszeg / szalhossz;
            if ((Osszeg % szalhossz) > 0) ++min;
            s.AppendLine("Elméleti minimum szál: " + min.ToString().PadLeft(4, ' '));
            return s.ToString();
        }
    }
}
