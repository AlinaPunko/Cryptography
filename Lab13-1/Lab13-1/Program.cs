using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Lab13_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int xmin = 583, xmax = 620;
            int temp = xmin;
            Dictionary<int, int> xValues = new Dictionary<int, int>();
            Dictionary<int, int> yValues = new Dictionary<int, int>();
            while (temp <= xmax)
            {
                Console.WriteLine($"x = {temp} x^3-x+1 mod 751= {(temp * temp * temp - temp +1) % 751} ");
                xValues.Add(temp,( temp * temp * temp - temp + 1) % 751 );
                Console.WriteLine($"y = {temp} y^2 mod 751= {(temp * temp ) % 751} ");
                yValues.Add(temp, (temp * temp) % 751);
                temp++;
            }

            foreach (var xx in xValues.Keys)
            {
                xValues.TryGetValue(xx, out int func1);
                foreach (var yy2 in yValues.Values)
                {
                    if (func1 == yy2)
                    {
                        var xx1 = yValues.FirstOrDefault(p => p.Value == yy2).Key;
                        Console.WriteLine($"({xx}, {xx1})");
                    }
                }
            }

            int gamma, x, y;
            Console.WriteLine(" k=7 P=(59, 365) Q=(59, 386) R=(105, 382)");

            Console.WriteLine("Найдем kP");
            int gamma1 = (3*59*59 - 1)/(2*365);
            int x1 = (gamma1 * gamma1 - 59 - 59) % 751;
            int y1 = ((gamma1 * 59 - x1) -365) % 751;
            Console.WriteLine($"2P({x1}, {y1})");

            int gamma2 = (y1 - 365) / (x1 - 59) % 751;
            int x2 = (gamma2 * gamma2 - 59 - x1) % 751;
            int y2 = (gamma2 * 59 - x2 - 365) % 751;
            Console.WriteLine($"3P({x2}, {x2})");

            int gamma3 = (3 * x1 * x1 - 1) / (2 * y1);
            int x3 = (gamma3 * gamma3 - x1 - x1) % 751;
            int y3 = ((gamma3 * x1 - x3) - y1) % 751;
            Console.WriteLine($"4P({x3}, {y3})");

            int gamma4 = (y3 - y2) / (x3 - x2) % 751;
            int x4 = (gamma4 * gamma4 - x2 - x3) % 751;
            int y4 = ((gamma4 * x2 - x4) - y2) % 751;
            Console.WriteLine($"7P({x4}, {y4})");
            int x7p = x4, y7p = y4;

            Console.WriteLine("Найдем P+Q");
            gamma = ((386 - 365) / (61 - 59)) % 751;
            x = (gamma * gamma - 59 - 61) % 751;
            y = (gamma * (59 - x) - 365)% 751;
            Console.WriteLine($"gamma = {gamma}");
            Console.WriteLine($"({x}, {y})");

            Console.WriteLine("Найдем 7Q");
            gamma1 = (3 * 59 * 59 - 1) / (2 * 386);
            x1 = (gamma1 * gamma1 - 59 - 59) % 751;
            y1 = ((gamma1 * 59 - x1) - 386) % 751;
            Console.WriteLine($"2Q({x1}, {y1})");

            gamma2 = (y1 - 365) / (x1 - 59) % 751;
            x2 = (gamma2 * gamma2 - 59 - x1) % 751;
            y2 = (gamma2 * 59 - x2 - 386) % 751;
            Console.WriteLine($"3Q({x2}, {x2})");

            gamma3 = (3 * x1 * x1 - 1) / (2 * y1);
            x3 = (gamma3 * gamma3 - x1 - x1) % 751;
            y3 = ((gamma3 * x1 - x3) - y1) % 751;
            Console.WriteLine($"4Q({x3}, {y3})");

            gamma4 = (y3 - y2) / (x3 - x2) % 751;
            x4 = (gamma4 * gamma4 - x2 - x3) % 751;
            y4 = ((gamma4 * x2 - x4) - y2) % 751;
            Console.WriteLine($"7Q({x4}, {y4})");

            Console.WriteLine("Найдем 7P+7Q");
            gamma4 = (y4 -y7p) / (x4 - x7p) % 751;
            x4 = (gamma4 * gamma4 - x4 - x7p) % 751;
            y4 = ((gamma4 * x7p - x4) - y7p) % 751;
            Console.WriteLine($"7P+7Q({x4}, {y4})");

            Console.WriteLine("Найдем 7P+7Q-R");
            gamma4 = (y4 + 382) / (x4 + 105) % 751;
            x4 = (gamma4 * gamma4 - x4 + 105) % 751;
            y4 = ((gamma4 * 105 - x4) + 382) % 751;
            Console.WriteLine($"7P+7Q-R({x4}, {y4})");

            Console.WriteLine("Найдем P-Q");
            gamma = ((386 + 365) / (61 + 59)) % 751;
            x = (gamma * gamma - 59 + 61) % 751;
            y = (gamma * (59 - x) + 365) % 751;
            Console.WriteLine($"({x}, {y})");

            Console.WriteLine("Найдем P-Q+R");
            gamma = ((y - 382) / (x - 105)) % 751;
            x = (gamma * gamma - 59 + 105) % 751;
            y = (gamma * (105 - x) + 382) % 751;
            Console.WriteLine($"({x}, {y})");
        }

        string SumTwoPoints(int xP, int xQ, int yP, int yQ)
        {
            int p = 427;
            BigInteger lyambda;
            int raznX = xQ - xP;
            int raznY = yQ - yP;
            if (raznX < 0)
            {
                raznX += p;
            }
            if (raznY < 0)
            {
                raznY += p;
            }
            if (xP == 0 & yP == 0)
            {
                return xQ.ToString() + ',' + yQ.ToString();
            }
            if (xQ == 0 & yQ == 0)
            {
                return xP.ToString() + ',' + yP.ToString();
            }
            BigInteger xR = 0, yR = 0;
            if (xP == xQ && yP != yQ || (yP == 0 && yQ == 0 && xP == xQ))
            { }
            else
            {
                if (xP == xQ && yP == yQ)
                {
                    lyambda = (3 * BigInteger.Pow(xP, 2) - 1) * (Foo(2 * yP, p));
                }
                else
                {
                    lyambda = (raznY) * Foo(raznX, p);
                }
                xR = (BigInteger.Pow(lyambda, 2) - xP - xQ);
                yR = yP + lyambda * (xR - xP);
                xR = xR % p < 0 ? (xR % p) + p : xR % p;
                yR = -yR % p < 0 ? (-yR % p) + p : (-yR % p);
            }
            string Result = xR.ToString() + ',' + yR.ToString();
            return Result;
        }

        private int Foo(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }

        private int GCD(int a, int b, out int x, out int y)
        {
            int p = 427;
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
            return d % p;
        }

        string Multiply(int k, int xP, int yP)
        {
            string[] numbers = { "", "" };
            int xQ = xP;
            int yQ = yP;
            string[] result = { "" };
            string[] addend = { xQ.ToString(), yQ.ToString() };
            while (k > 0)
            {
                if ((k & 1) > 0)
                {
                    if (result.Length == 2)
                    {
                        result = SumTwoPoints(int.Parse(result[0]), int.Parse(addend[0]), int.Parse(result[1]), int.Parse(addend[1])).Split(',');
                    }
                    else
                    {
                        result = addend;
                    }
                }
                addend = SumTwoPoints(int.Parse(addend[0]), int.Parse(addend[0]), int.Parse(addend[1]), int.Parse(addend[1])).Split(',');
                k >>= 1;
            }
            return result[0] + "," + result[1];
        }
    }
}
