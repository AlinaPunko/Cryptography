using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public static class MathService
    {
        public static int CountNOD(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    int tmp = a;
                    a = b;
                    b = tmp;
                }
                b -= a;
            }
            return a;
        }

        public static int CountNOD(int a, int b, int c)
        {
            return CountNOD(CountNOD(a,b),c);
        }

        public static List<int> GetPrimes (int min, int max)
        {
            if (max <= 0)
                return null;
            List<int> result = new List<int>();
            for (int i = min; i <= max; i++)
                if (IsSimple(i))
                {
                    result.Add(i);
                }
            return result;
        }

        public static int Foo(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }

        public static int GCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
        public static bool IsSimple(double x)
        {
            double sqrtX = Math.Sqrt(x);
            for (int i = 2; i <= sqrtX; i++)
                if (x % i == 0)
                    return false;
            return true;
        }
    }
}
