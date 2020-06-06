using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            RouteEncryption routeEncryption = new RouteEncryption();
            ManyEncryption manyEncryption = new ManyEncryption();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string path = "text.txt";
            var encoding = Encoding.GetEncoding(1251);
            string text;


            using (StreamReader sr = new StreamReader(path, encoding))
            {
                text = sr.ReadToEnd();
            }

            Stopwatch watch1 = new Stopwatch();
            watch1.Start();
            routeEncryption.SetKey(5, text.Length);
            string encrypt = routeEncryption.Encrypt(text);
            Console.WriteLine(encrypt);
            watch1.Stop();
            Console.WriteLine("Time " + watch1.ElapsedMilliseconds);

            Console.WriteLine();
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            string decrypt = routeEncryption.Decrypt(encrypt);
            Console.WriteLine(decrypt);
            watch2.Stop();
            Console.WriteLine("Time " + watch2.ElapsedMilliseconds);

            Stopwatch watch3 = new Stopwatch();
            watch3.Start();
            manyEncryption.SetKey("алiна", "пунько");
            string encrypt1 = manyEncryption.Encrypt(text);
            Console.WriteLine(encrypt);
            watch3.Stop();
            Console.WriteLine("Time " + watch3.ElapsedMilliseconds);

            Console.WriteLine();
            Stopwatch watch4 = new Stopwatch();
            watch4.Start();
            string decrypt1 = manyEncryption.Decrypt(encrypt1);
            Console.WriteLine(decrypt);
            watch4.Stop();
            Console.WriteLine("Time " + watch4.ElapsedMilliseconds);
        }
    }
}
