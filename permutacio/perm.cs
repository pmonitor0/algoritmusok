using System;
using System.Diagnostics;

namespace Permutaciok
{
    class Permutal
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 13;
            int[] arr = new int[n];
            for (int i = 0; i < n; ++i) arr[i] = i + 1;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0;
            sw.Start();
            Teszt(arr);
            t_1 = sw.ElapsedMilliseconds;
            
            Console.WriteLine("Teszt: {0}", t_1);
        }

        static void Teszt(int[] arr)
        {
            int[] arr2 = (int[])arr.Clone();
            int n = arr2.Length;
            int i, j, temp;
            for (i = 0; i < n; ++i) arr2[i] = i + 1;
            while (true)
            {
                /*for (i = 0; i < n; ++i) Console.Write("{0} ", arr[i]);
                Console.WriteLine("");*/

                for (i = n - 2; i >= 0 && arr2[i] > arr2[i + 1]; --i) ;
                if (i < 0) break;
                for (j = n - 1; arr2[j] < arr2[i]; --j) ;
                temp = arr2[i]; arr2[i] = arr2[j]; arr2[j] = temp;
                for (j = i + 1; j < n + i - j; ++j)
                {
                    temp = arr2[j]; arr2[j] = arr2[n + i - j]; arr2[n + i - j] = temp;
                }
            }
        }
    }
}
