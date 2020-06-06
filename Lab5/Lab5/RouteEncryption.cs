using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab5
{
    class RouteEncryption
    {
        private int s;
        private int k;
        char[,] table;
        public void SetKey(int countString, int lengthMessage)
        {
            s = countString;
            k = (lengthMessage - 1) / countString + 1;
        }
        public string Encrypt(string input)
        {
            var map1 = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (!map1.ContainsKey(c))
                    map1.Add(c, 1);
                else
                    map1[c] += 1;
            }

            int len1 = input.Length;

            var orderkey1 = from i in map1 orderby i.Key select i;
            foreach (var item in orderkey1)
            {
                using (StreamWriter sw1 = new StreamWriter("info1.log", true, Encoding.Default))
                {
                    sw1.Write($"{DateTime.Now}      Количество символов {item.Key}  = {item.Value} частота {(double)item.Value / len1}\n");
                }
            }
            int l = 0;
            string result = "";
            table = new char[k, s];
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    if (l < input.Length)
                    {
                        table[i, j] = input[l];
                        l++;
                    }
                    else
                    {
                        table[i, j] = ' ';
                    }
                }
            }
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    result += table[j, i];
                }
            }

            var map2 = new Dictionary<char, int>();
            foreach (char c in result)
            {
                if (!map2.ContainsKey(c))
                    map2.Add(c, 1);
                else
                    map2[c] += 1;
            }

            int len2 = result.Length;

            var orderkey2 = from i in map2 orderby i.Key select i;
            foreach (var item in orderkey2)
            {
                using (StreamWriter sw2 = new StreamWriter("info2.log", true, Encoding.Default))
                {
                    sw2.Write($"{DateTime.Now}      Количество символов {item.Key}  = {item.Value} частота {(double)item.Value / len2}\n");
                }
            }

            return result;
        }
        public string Decrypt(string output)
        {
            int p = 0;
            string result = "";
            table = new char[k, s];
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (p < output.Length)
                    {
                        table[j, i] = output[p];
                        p++;
                    }
                    else
                    {
                        table[j, i] = ' ';
                    }

                }
            }
            for (int i = 0; i < k; i++) 
            {
                for (int j = 0; j < s; j++)
                {
                    result += table[i, j];
                }

            }
            return result;
        }
    }
}
