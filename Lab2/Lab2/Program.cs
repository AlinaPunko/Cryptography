using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Shannon shannon = new Shannon();

            string path1 = "text.txt";
            string path = "text1.txt";

            string text;
            string text1;

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }

            using (StreamReader sr1 = new StreamReader(path1, Encoding.Default))
            {
                text1 = sr1.ReadToEnd();
            }

            string patternRU = @"[A-Za-z0-9\s+\W_]";
            string patternEN = @"[А-Яа-я0-9\s+\W_]";
            string target = "";

            Regex regexRU = new Regex(patternRU, RegexOptions.IgnoreCase);
            Regex regexEN = new Regex(patternEN);

            string resultRU = regexRU.Replace(text, target);
            string resultEN = regexEN.Replace(text1, target);

            StringBuilder builder = new StringBuilder();
            foreach (char a in resultRU)
                builder.Append(Convert.ToString(a, 2));

            Console.WriteLine("Binary Энторопия по Шеннону = " + shannon.ShannonEntropy(builder.ToString()));
            Console.WriteLine("RUS Энтропия по Шеннону фразы = " + shannon.ShannonEntropy(resultRU.ToLower()));
            Console.WriteLine("ENG Энтропия по Шеннону фразы = " + shannon.ShannonEntropy(resultEN.ToLower()));

            String myName = "Punko Alina Andreevna";
            string patternName = @"\s+";
            Regex regexName = new Regex(patternName);
            string resulName = regexName.Replace(myName, target);
            double shann = shannon.ShannonEntropy(resultEN.ToLower());
            Console.WriteLine($"Количество информации в ФИО {shannon.AmountOfInformation(resulName, shann)}");
            Console.WriteLine(resulName);
            byte[] bytes = Encoding.ASCII.GetBytes(resulName);
            string ASCII = "";
            foreach (var b in bytes)
                ASCII += b;

            Console.WriteLine("ASCII: Кол-во инф-ции в ФИО " + shannon.AmountOfInformation(ASCII, shann));

            Console.WriteLine("С условной вероятностью ошибки 0,1 " + shannon.AmountOfInformationWithMistake(shann, resulName, 0.9));
            Console.WriteLine("С условной вероятностью ошибки 0,5 " + shannon.AmountOfInformationWithMistake(shann, resulName, 0.5));
            Console.WriteLine("С условной вероятностью ошибки 1 " + shannon.AmountOfInformationWithMistake(shann, resulName, 1));

            Console.ReadLine();
        }
    }
}
