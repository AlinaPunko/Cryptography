using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab4
{
    class Trisemus
    {
        private static string alphabet = "абвгдеёжзiйклмнопрстуўфхцчшыьэюя";
        private static char[,] table = new char[4,8];

        public static void GetNewTable (string keyWord)
        {
            bool findSame = false;
            char[] newAlpha = new char[32];
            int key = -1;
            key++;
            int beg = 0, current = key;

            for (int i = key; i < keyWord.Length + key; i++)
            {
                for (int j = key; j < keyWord.Length + key; j++)
                {
                    if (keyWord[i - key] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = keyWord[i - key];
                    current++;
                }
                findSame = false;
            }


            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < newAlpha.Length; j++)
                {
                    if (alphabet[i] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = alphabet[i];
                    current++;
                }
                findSame = false;
                if (current == newAlpha.Length)
                {
                    beg = i;
                    break;
                }
            }

            current = 0;
            for (int i = beg; i < alphabet.Length; i++)
            {
                for (int j = 0; j < newAlpha.Length; j++)
                {
                    if (alphabet[i] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = alphabet[i];
                    current++;
                }
                findSame = false;
            }

            int index = 0;
            for (int i = 0; i < 4; i++) 
            {
                for (int j = 0; j < 8; j++)
                {
                    table[i, j] = newAlpha[index++];
                }
            }
        }

        internal static string Encrypt(string message)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in message)
            {
                if (!map.ContainsKey(c))
                    map.Add(c, 1);
                else
                    map[c] += 1;
            }

            int len = message.Length;

            var orderkey = from i in map orderby i.Key select i;
            foreach (var item in orderkey)
            {
                using (StreamWriter sw = new StreamWriter("info3.log", true, Encoding.Default))
                {
                    sw.Write($"{DateTime.Now}      Количество символов {item.Key}  = {item.Value} частота {(double)item.Value / len}\n");
                }
            }
            string res = "";
            foreach (char ch in message)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (ch != table[i, j])
                            continue;
                        if (i == 3)
                        {
                            res += table[0, j];
                            break;
                        }
                        res += table[i+1, j];
                        break;
                    }
                }
            }
            return res;
        }

        internal static string Decrypt(string message)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in message)
            {
                if (!map.ContainsKey(c))
                    map.Add(c, 1);
                else
                    map[c] += 1;
            }

            int len = message.Length;

            var orderkey = from i in map orderby i.Key select i;
            foreach (var item in orderkey)
            {
                using (StreamWriter sw = new StreamWriter("info4.log", true, Encoding.Default))
                {
                    sw.Write($"{DateTime.Now}      Количество символов {item.Key}  = {item.Value} частота {(double)item.Value / len}\n");
                }
            }
            string res = "";
            foreach (char ch in message)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (ch != table[i, j])
                            continue;
                        if (i == 0)
                        {
                            res += table[3, j];
                            break;
                        }
                        res += table[i-1, j];
                        break;
                    }
                }
            }
            return res;
        }
    }
}
