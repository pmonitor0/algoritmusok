using System;
using System.Diagnostics;

namespace Kombinaciok
{
    class Kombism
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 250, k = 5;
            int[] arr = new int[n];
            for (int i = 0; i < n; ++i) arr[i] = i + 1;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0;
            sw.Start();
            IsmKombinacio(arr, k); //kb. 12 sec.
            t_1 = sw.ElapsedMilliseconds;
            Console.WriteLine("Eltelt idő: {0}", t_1);
        }

        static void IsmKombinacio(int[] arr, int k)
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
                    --j;
                    while (j > -1)
                    {
                        if (tomb[j] == n - 1)
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
                        tomb[j] = tomb[j - 1];
                        ++j;
                    }
                    --j;
                }
            } while (true);
        }
    }
}
