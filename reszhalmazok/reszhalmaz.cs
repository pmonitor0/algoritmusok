using System;
using System.Diagnostics;

namespace Reszhalmazok
{
    class Reszhalmaz
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 33;
            //int n = 3;
            int[] arr = new int[n];
            for (int i = 0; i < n; ++i) arr[i] = i + 1;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0;
            GC.Collect();
            sw.Start();
            osszesreszhalmaz(arr, n); // kb. 20 sec.
            t_1 = sw.ElapsedMilliseconds;
            Console.WriteLine("Eltelt idő: {0}", t_1);
        }

        static void osszesreszhalmaz(int[] arr, int n)
        {
            bool[] tomb = new bool[n];
            osszesreszhalmaz(arr, tomb, n);
        }

        static void osszesreszhalmaz(int[] arr, bool[] tomb, int n)
        {
            int i = 0;
            while (i < n)
            {
                tomb[i] = !tomb[i];
                if (tomb[i]) break;
                ++i;
            }
            if (i == n) return;

            /*for (int j = 0; j < n; ++j)
            {
                if (tomb[j]) Console.Write("{0} ", arr[j]);
            }
            Console.WriteLine("");*/

            osszesreszhalmaz(arr, tomb, n);
        }
    }
}
