using System;
using System.Diagnostics;

namespace Primszamok
{
    class Prim
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            int n = 11003;
            Stopwatch sw = new Stopwatch();
            long t_1 = 0;
            bool prim;
            sw.Start();
            prim = PrimSzamE(n); // kb. 43 sec.
            t_1 = sw.ElapsedMilliseconds;
            GC.Collect();
            Console.WriteLine(prim);
            Console.WriteLine("Eltelt idő: {0}", t_1);
        }

        static bool PrimSzamE(int szam)
        {
            if (szam == 2) return true;
            else if (szam < 2 || (szam % 2) == 0) return false;
            int i = 3, k = (int)Math.Sqrt(szam);
            bool prim = true;
            while (i <= k && prim)
            {
                if ((szam % i) == 0) prim = !prim;
                i += 2;
            }
            return prim;
        }
    }
}
