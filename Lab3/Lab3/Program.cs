using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (int item in MathService.GetPrimes(2, 591))
            {
                Console.Write(item);
                Console.Write(' ');
            }
            Console.WriteLine();
            foreach (int item in MathService.GetPrimes(555, 591))
            {
                Console.Write(item);
                Console.Write(' ');
            }
            Console.WriteLine();
            Console.WriteLine(MathService.CountNOD(9, 27, 90));
            int x, y ;
            var result = MathService.Foo(3, 26);
            Console.WriteLine(result);
        }
    }
}
