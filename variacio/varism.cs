using System;
using System.Diagnostics;

namespace Variaciok
{
    class VarIsm
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 120, k = 5;
            //int n = 4, k = 3;
            int[] arr = new int[n];
            for (int i = 0; i < n; ++i) arr[i] = i + 1;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0, t_2 = 0, t_3 = 0;
            GC.Collect();
            sw.Start();
            IsmVariacio(arr, k); //kb. 35 sec.
            t_1 = sw.ElapsedMilliseconds;
            GC.Collect();
            t_2 = sw.ElapsedMilliseconds;
            IsmVar(arr, k); //kb. 42 sec.
            t_3 = sw.ElapsedMilliseconds;
            Console.WriteLine("Eltelt idő: {0}", t_1);
            Console.WriteLine("Eltelt idő: {0}", t_3 - t_2);
        }

        static void IsmVariacio(int[] arr, int k)
        {
            int n = arr.Length;
            int[] tomb = new int[k];
            for (int i = 0; i < k; ++i) tomb[i] = 0;
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
            } while (true);
        }

        static void IsmVar(int[] arr, int k)
        {
            int n = arr.Length;
            int[] tomb = new int[k + 1];
            int i;
            for (i = 1; i <= k; i++) tomb[i] = 1;
            tomb[k] = 0;
            i = k;
            while (true)
            {
                if (tomb[i] == n)
                {
                    i--;
                    if (i == 0) return;
                }
                else
                {
                    tomb[i]++;
                    while (i < k)
                    {
                        i++;
                        tomb[i] = 1;
                    }

                    /*for (int j = 1; j <= k; j++) Console.Write("{0} ", arr[tomb[j] - 1]);
                    Console.WriteLine("");*/
                }
            }
        }
    }
}
