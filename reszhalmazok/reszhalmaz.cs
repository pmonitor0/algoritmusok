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
            long t_1 = 0, t_2 = 0, t_3 = 0;
            GC.Collect();
            sw.Start();
            Teszt_1(arr, n); // kb. 19-20 sec.
            t_1 = sw.ElapsedMilliseconds;
            GC.Collect();
            t_2 = sw.ElapsedMilliseconds;
            Teszt_2(arr, n); // kb. 18-19 sec.
            t_3 = sw.ElapsedMilliseconds;
            Console.WriteLine("Eltelt idő: {0}", t_1);
            Console.WriteLine("Eltelt idő: {0}", t_3 - t_2);
        }

        static void Teszt_1(int[] arr, int n)
        {
            bool[] tomb = new bool[n];
            int darab = 0;
            for (int i = 0; i < n; ++i) tomb[i] = false;
            do
            {
                int i = 0;
                while (i < n)
                {
                    tomb[i] = !tomb[i];
                    if (tomb[i]) break;
                    ++i;
                }
                if (i == n) break;
                /*for (int j = 0; j < n; ++j)
                {
                    if (tomb[j]) Console.Write("{0} ", arr[j]);
                }
                Console.WriteLine("");*/
                ++darab;
            } while (true);
        }

        static void Teszt_2(int[] arr, int n)
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
