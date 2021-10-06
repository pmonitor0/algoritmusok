using System;
using System.Diagnostics;

namespace Variaciok
{
    class Var
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 90, k = 5;
            //int n = 6, k = 5;
            int[] arr = new int[n];
            for (int i = 0; i < n; ++i) arr[i] = i + 1;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0;
            sw.Start();
            Teszt(arr, k); //kb. 25 sec.
            t_1 = sw.ElapsedMilliseconds;
            Console.WriteLine("Eltelt idő: {0}", t_1);
            Console.ReadKey();
        }

        static void Teszt(int[] arr, int k)
        {
            int[] arr2 = (int[])arr.Clone();
            Variacio(arr2, k);
        }

        static void Variacio(int[] arr, int k)
        {
            int[] tomb = new int[k];
            int n = arr.Length;
            int m = 0, j = 0, i = 0, p = k - 1;
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
                    if (i == p)
                    {
                        tomb[i] = j;


                        /*for (int index = 0; index < k; ++index) Console.Write("{0} ", arr[tomb[index] - 1]);
                        Console.WriteLine("");*/

                    }
                    else tomb[i++] = j;
                }
                else tomb[i--] = 0;
            }
        }
    }
}
