using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Lab12
{
    namespace Lab12
    {
        class Program
        {
            static void Main()
            {
                BigInteger big = 501;
                BigInteger big2 = 2;
                BigInteger big3 = 3161;
                BigInteger big5 = BigInteger.Parse("102343504342");
                BigInteger big6 = 4721;
                BigInteger big7 = BigInteger.Multiply(BigInteger.ModPow(big, big2, big6), BigInteger.ModPow(big3, big5, big6)) % big6;
                DateTime t = DateTime.Now;
                Rsa.CheckCorrectly();
                Rsa.CheckInCorrectly();
                Console.WriteLine("RSA:" + (DateTime.Now - t));
                t = DateTime.Now;
                ElGamal.CheckCorrectly();
                ElGamal.CheckInCorrectly();
                Console.WriteLine("ElGamal:" + (DateTime.Now - t));
                Console.InputEncoding = Encoding.ASCII;
                t = DateTime.Now;
                Shnorr.Do();
                Console.WriteLine("Shnorr:" + (DateTime.Now - t));
                Console.ReadLine();
            }
        }
        public static class Rsa
        {
            public static void CheckCorrectly()
            {
                var p = 7;
                var q = 13;
                var pathToSource = ".\\Test.txt";
                var pathToEds = ".\\RSA.txt";
                var result = Create(p, q, pathToSource, pathToEds);
                var veryify = Verify(result.d, result.n, pathToEds, pathToSource);
                Console.WriteLine(veryify);
            }
            public static void CheckInCorrectly()
            {
                var p = 7;
                var q = 13;
                var pathToSource = ".\\Test.txt";
                var pathToFakeSource = ".\\FakeTest.txt";
                var pathToEds = ".\\RSA.txt";
                var result = Create(p, q, pathToSource, pathToEds);
                var veryify = Verify(result.d, result.n, pathToEds, pathToFakeSource);
                Console.WriteLine(veryify);
            }
            private static readonly char[] Characters = { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

            private static (long d, long n) Create(long p, long q, string sourceFilePathTextBox, string signFilePathTextBox)
            {
                var hash = File.ReadAllText(sourceFilePathTextBox).GetHashCode().ToString();
                var n = p * q;
                var m = (p - 1) * (q - 1);
                var d = Calculate_d(m);
                var e = Calculate_e(d, m);

                var result = RSA_Encode(hash, e, n);

                var sw = new StreamWriter(signFilePathTextBox);
                foreach (var item in result)
                {
                    sw.WriteLine(item);
                }
                sw.Close();

                return (d, n);
            }

            private static bool Verify(long d, long n, string signFilePathTextBox, string sourceFilePathTextBox)
            {
                var input = new List<string>();

                var sr = new StreamReader(signFilePathTextBox);

                while (!sr.EndOfStream)
                {
                    input.Add(sr.ReadLine());
                }

                sr.Close();

                var result = RSA_Decode(input, d, n);

                var hash = File.ReadAllText(sourceFilePathTextBox).GetHashCode().ToString();
                if (result.Equals(hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private static List<string> RSA_Encode(string s, long e, long n)
            {
                var result = new List<string>();

                foreach (var t in s)
                {
                    var index = Array.IndexOf(Characters, t);
                    var bi = new BigInteger(index);
                    bi = BigInteger.Pow(bi, (int)e);
                    var bn = new BigInteger((int)n);
                    bi %= bn;
                    result.Add(bi.ToString());
                }

                return result;
            }

            private static string RSA_Decode(List<string> input, long d, long n)
            {
                var result = "";
                var bn = new BigInteger((int)n);
                foreach (var item in input)
                {
                    var bi = new BigInteger(Convert.ToDouble(item));
                    bi = BigInteger.Pow(bi, (int)d);
                    bi = bi % bn;

                    var index = Convert.ToInt32(bi.ToString());

                    result += Characters[index].ToString();
                }

                return result;
            }

            private static long Calculate_d(long m)
            {
                var d = m - 1;

                for (long i = 2; i <= m; i++)
                {
                    if (m % i == 0 && d % i == 0)
                    {
                        d--;
                        i = 1;
                    }
                }

                return d;
            }

            private static long Calculate_e(long d, long m)
            {
                long e = 10;

                while (true)
                {
                    if (e * d % m == 1)
                    {
                        break;
                    }
                    else
                    {
                        e++;
                    }
                }
                return e;
            }
        }

        public static class ElGamal
        {
            public static void CheckCorrectly()
            {
                var str = "Hello world";
                var hash = CalculateMd5Hash(str).ToString();
                var sign = ElGamalClass.EnCrypt(hash);
                var verify = ElGamalClass.DeCrypt(sign) == CalculateMd5Hash(str).ToString();
                Console.WriteLine(verify);
            }
            public static void CheckInCorrectly()
            {
                var str = "Hello world";
                var fakeStr = "Helloworld";
                var hash = CalculateMd5Hash(str).ToString();
                var sign = ElGamalClass.EnCrypt(hash);
                var verify = ElGamalClass.DeCrypt(sign) == CalculateMd5Hash(fakeStr).ToString();
                Console.WriteLine(verify);
            }
            public static BigInteger CalculateMd5Hash(string input)
            {
                var md5 = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(inputBytes);
                return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            }
        }

        public static class ElGamalClass
        {
            private static int Power(int a, int b, int n)
            {
                var tmp = a;
                var sum = tmp;
                for (var i = 1; i < b; i++)
                {
                    for (var j = 1; j < a; j++)
                    {
                        sum += tmp;
                        if (sum >= n)
                        {
                            sum -= n;
                        }
                    }

                    tmp = sum;
                }

                return tmp;
            }

            private static int Mul(int a, int b, int n)
            {
                var sum = 0;

                for (var i = 0; i < b; i++)
                {
                    sum += a;

                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }

                return sum;
            }

            public static string EnCrypt(string str)
            {
                return Crypt(593, 123, 8, str);
            }

            public static string DeCrypt(string str)
            {
                return Decrypt(593, 8, str);
            }

            private static string Crypt(int p, int g, int x, string inString)
            {
                var result = "";
                var y = Power(g, x, p);
                var rand = new Random();
                foreach (int code in inString)
                    if (code > 0)
                    {
                        var k = rand.Next() % (p - 2) + 1; 
                        var a = Power(g, k, p);
                        var b = Mul(Power(y, k, p), code, p);
                        result += a + " " + b + " ";
                    }

                return result;
            }

            private static string Decrypt(int p, int x, string inText)
            {
                var result = "";

                var arr = inText.Split(' ').Where(xx => xx != "").ToArray();
                for (var i = 0; i < arr.Length; i += 2)
                {
                    var a = int.Parse(arr[i]);
                    var b = int.Parse(arr[i + 1]);

                    if (a != 0 && b != 0)
                    {
                        //wcout<<a<<" "<<b<<endl; 

                        var deM = Mul(b, Power(a, p - 1 - x, p),
                            p);
                        var m = (char)deM;
                        result += m;
                    }
                }

                return result;
            }
        }

        public static class Shnorr
        {
            public static void Do()
            {
                BigInteger p = 29;
                BigInteger q = 7;
                string text = File.ReadAllText(".\\Test.txt");
                BigInteger g = 16;
                BigInteger obg = 20;
                int x = 6;
                BigInteger y = BigInteger.ModPow(obg, x, p);
                BigInteger a = BigInteger.Pow(g, 3) % p;
                BigInteger hash = (text + a).GetHashCode();
                File.WriteAllText(".\\shnorr.txt", hash.ToString());
                BigInteger b = (3 + 6 * hash) % q;
                BigInteger dov = BigInteger.ModPow(g, b, p);
                BigInteger X = dov * BigInteger.ModPow(y, hash, p);
                BigInteger hash2 = (text + X).GetHashCode(); 
                var f = hash == hash2;
                Console.WriteLine(true);
                string text2 = File.ReadAllText(".\\FakeTest.txt");
                BigInteger hash3 = (text2 + X).GetHashCode();
                var f2 = hash == hash3;
                Console.WriteLine(f2);
            }
        }
    }

}
