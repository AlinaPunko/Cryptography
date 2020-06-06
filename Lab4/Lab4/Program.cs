using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string path = "text.txt";
            var encoding = Encoding.GetEncoding(1251);
            string text;


            using (StreamReader sr = new StreamReader(path, encoding))
            {
                text = sr.ReadToEnd();
            }

            string patternBY = @"[\s+\W_]";
            string target = "";

            Regex regexBY = new Regex(patternBY, RegexOptions.IgnoreCase);

            string resultBY = regexBY.Replace(text, target);


            Caesar.CreateNewAlpha("iнфарматыка", 2);
            Stopwatch watch1 = new Stopwatch();
            watch1.Start();
            string encrypt = Caesar.Encrypt(resultBY.ToLower());
            Console.WriteLine(encrypt);
            watch1.Stop();
            Console.WriteLine("Time " + watch1.ElapsedMilliseconds);

            Console.WriteLine();
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            string decrypt = Caesar.Decrypt(encrypt);
            Console.WriteLine(decrypt);
            watch2.Stop();
            Console.WriteLine("Time " + watch2.ElapsedMilliseconds);


            Trisemus.GetNewTable("алiна");
            Stopwatch watch3 = new Stopwatch();
            watch3.Start();
            string encrypt1 = Trisemus.Encrypt(resultBY.ToLower());
            Console.WriteLine(encrypt1);
            watch3.Stop();
            Console.WriteLine("Time " + watch3.ElapsedMilliseconds);
            Console.WriteLine();
            Stopwatch watch4 = new Stopwatch();
            watch4.Start();
            string decrypt1 = Trisemus.Decrypt(encrypt1);
            Console.WriteLine(decrypt1);

            watch4.Stop();
            Console.WriteLine("Time " + watch4.ElapsedMilliseconds);
        }
    }
}
