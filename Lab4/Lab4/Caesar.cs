using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab4
{
    class Caesar
    {
        private static string alpha = "абвгдеёжзiйклмнопрстуўфхцчшыьэюя";
        private static char[] newAlpha = new char[32];

        public static string Encrypt(string Message)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in Message)
            {
                if (!map.ContainsKey(c))
                    map.Add(c, 1);
                else
                    map[c] += 1;
            }

            int len = Message.Length;

            var orderkey = from i in map orderby i.Key select i;
            foreach (var item in orderkey)
            {
                using (StreamWriter sw = new StreamWriter("info1.log", true, Encoding.Default))
                {
                    sw.Write($"{DateTime.Now}      Количество символов {item.Key}  = {item.Value} частота {(double)item.Value/len}\n");
                }
            }
            string res = "";
            foreach (char ch in Message)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (ch != alpha[i])
                        continue;
                    res += newAlpha[i];
                    break;
                }
            }
            return res;
        }

        public static string Decrypt(string Message)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in Message)
            {
                if (!map.ContainsKey(c))
                    map.Add(c, 1);
                else
                    map[c] += 1;
            }

            int len = Message.Length;

            var orderkey = from i in map orderby i.Key select i;
            foreach (var item in orderkey)
            {
                using (StreamWriter sw = new StreamWriter("info2.log", true, Encoding.Default))
                {
                    sw.Write($"{DateTime.Now}      Количество символов {item.Key}  = {item.Value}  частота {(double)(item.Value/len)}\n");
                }
            }
            string res = "";
            foreach (char ch in Message)
            {
                for (int i = 0; i < newAlpha.Length; i++)
                {
                    if (ch != newAlpha[i])
                        continue;
                    res += alpha[i];
                    break;
                }
            }
            return res;
        }

        public static void CreateNewAlpha(string keyWord, int key) 
        {
            bool findSame = false;
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


            for (int i = 0; i < alpha.Length; i++)
            {
                for (int j = 0; j < newAlpha.Length; j++)
                {
                    if (alpha[i] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = alpha[i];
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
            for (int i = beg; i < alpha.Length; i++)
            {
                for (int j = 0; j < newAlpha.Length; j++)
                {
                    if (alpha[i] == newAlpha[j])
                    {
                        findSame = true;
                        break;
                    }
                }
                if (!findSame)
                {
                    newAlpha[current] = alpha[i];
                    current++;
                }
                findSame = false;
            }
        }

        public static string GetNewAlpha()
        {
            string strNewAlpha = new string(newAlpha);
            return strNewAlpha;
        }
    }
}
