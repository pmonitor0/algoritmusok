using System;
using System.Diagnostics;

namespace Kombinaciok
{
    class Kombinal
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            //int n = 6, k = 4;
            int n = 250, k = 5;
            int[] arr = new int[n];
            for (int i = 0; i < n; ++i) arr[i] = i + 1;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0, t_2 = 0, t3 = 0;
            sw.Start();
            Teszt_1(arr, k); //kb. 11-12 sec.
            t_1 = sw.ElapsedMilliseconds;
            Teszt_2(arr, k); // kb. 20-22 sec.
            t_2 = sw.ElapsedMilliseconds;
            Console.WriteLine("Teszt_1: {0}", t_1);
            Console.WriteLine("Teszt_2: {0}", t_2 - t_1);
        }

        static void Teszt_1(int[] arr, int k)
        {
            int n = arr.Length;
            int[] tomb = new int[k];
            for (int i = 0; i < k; ++i) tomb[i] = i;
            int j = k - 1;
            do
            {
                /*for (int i = 0; i < k; ++i) Console.Write("{0} ", arr[tomb[i]]);
                Console.WriteLine("");*/

                ++tomb[j];
                if (tomb[j] > n - 1)
                {
                    while (j > -1)
                    {
                        if (tomb[j] >= n - k + j)
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
                        tomb[j] = tomb[j - 1] + 1;
                        ++j;
                    }
                    --j;
                }
            } while (true);
        }

        static void Teszt_2(int[] arr, int k)
        {
            int n = arr.Length;
            int[] tomb = new int[k];
            int j = 0;
            bool ind = true;
            while (Kombinacio(tomb, n, k, ref j, ref ind))
            {
                /*for (int i = 0; i < k; ++i) Console.Write("{0} ", arr[tomb[i]]);
                Console.WriteLine("");*/
            }
        }

        static bool Kombinacio(int[] tomb, int n, int k, ref int j, ref bool ind)
        {
            if (ind)
            {
                j = k - 1;
                for (int i = 0; i < k; ++i) tomb[i] = i;
                ind = false;
                return true;
            }
            else
            {
                ++tomb[j];
                if (tomb[j] > n - 1)
                {
                    while (j > -1)
                    {
                        if (tomb[j] >= n - k + j)
                        {
                            --j;
                            if (j == -1) return false;
                        }
                        else break;
                    }
                    ++tomb[j];
                    ++j;
                    while (j < k)
                    {
                        tomb[j] = tomb[j - 1] + 1;
                        ++j;
                    }
                    --j;
                }
                return true;
            }
        }
    }
}
