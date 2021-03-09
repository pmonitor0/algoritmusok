using System;
using System.Diagnostics;

namespace Particiok
{
    class Particio
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 118;
            //int n = 5;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0, t_2 = 0, t_3 = 0, t_4 = 0, t_5 = 0;
            GC.Collect();
            sw.Start();
            Teszt_1(n); // kb. 43 sec.
            t_1 = sw.ElapsedMilliseconds;
            GC.Collect();
            Console.WriteLine("");
            t_2 = sw.ElapsedMilliseconds;
            Teszt_2(n); // kb. 51 sec.
            t_3 = sw.ElapsedMilliseconds;
            GC.Collect();
            Console.WriteLine("");
            t_4 = sw.ElapsedMilliseconds;
            Teszt_3(n); // kb. 26 sec.
            t_5 = sw.ElapsedMilliseconds;
            Console.WriteLine("Eltelt idő: {0}", t_1);
            Console.WriteLine("Eltelt idő: {0}", t_3 - t_2);
            Console.WriteLine("Eltelt idő: {0}", t_5 - t_4);
        }

        static void Teszt_1(int n)
        {
            int[] arr = new int[n + 1];
            particiok_1(arr, n, n, 1);
        }

        static void Teszt_2(int n)
        {
            int[] arr = new int[n + 1];
            arr[0] = n;
            particiok_2(arr, n, 1);
        }

        static void Teszt_3(int n)
        {
            int[] arr = new int[n];
            arr[0] = n;
            particiok_3(arr, n);
        }

        static void particiok_1(int[] arr, int szam, int aktosszeg, int j)
        {
            if (aktosszeg < 0) return;
            else if(aktosszeg == 0)
            {
                /*for (int i = 1; i < j; ++i) Console.Write("{0} ", arr[i]);
                Console.WriteLine("");*/
                return;
            }
            for (int i = 1; i <= szam; ++i)
            {
                arr[j] = i;
                particiok_1(arr, i, aktosszeg - i, j + 1);
            }
        }

        static void particiok_2(int[] arr, int osszeg, int j)
        {
            if (j == 0) return;
            if (osszeg <= arr[j - 1])
            {
                arr[j] = osszeg;

                /*int i, k;
                for (i = 1, k = 0; k + arr[i] < arr[0]; k += arr[i++]) Console.Write("{0} ", arr[i]);
                Console.WriteLine("{0}", arr[i]);*/
            }
            else
            {
                arr[j] = arr[j - 1] + 1;
            }

            while (--arr[j] > 0)
            {
                particiok_2(arr, osszeg - arr[j], j + 1);
            }
        }

        static void particiok_3(int[] arr, int osszeg)
        {
            int j = 0, k = -1, m;

            arr[j] = osszeg + 1;
            while (j >= 0)
            {
                --arr[j]; ++k;
                m = arr[j];
                while (k > 0)
                {
                    ++j;
                    arr[j] = (k <= m) ? k : m;
                    k -= arr[j];
                }

                /*int i;
                for (i = 0; i < j; ++i) Console.Write("{0} ", arr[i]);
                Console.WriteLine("{0}", arr[i]);*/

                while ((j >= 0) && (arr[j] <= 1))
                {
                    k += arr[j];
                    --j;
                }
            }
        }
    }
}
