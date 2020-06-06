using System;
using System.Collections.Generic;

namespace Lab5
{
    class ManyEncryption
    {
        private int[] key = null;
        private int[] key2 = null;
        char[,,] table;
        public void SetKey(string _key, string _key2)
        {
            Dictionary<int, char> matrix = new Dictionary<int, char>();
            int[] codedStr = new int[_key.Length];
            char[] alphabet = "абвгдеёжзiйклмнопрстуўфхцчшыьэюя".ToCharArray();

            int counter = 1;
            bool found = false;
            foreach (char с in alphabet)
            {
                for (int i = 0; i < _key.Length; i++)
                {
                    if (_key[i] == с)
                    {
                        codedStr[i] = counter;
                        if (matrix.ContainsKey(counter) == false)
                        {
                            matrix.Add(counter, _key[i]);
                        }
                        found = true;
                    }
                }

                if (found)
                {
                    counter++;
                    found = false;
                }
            }

            counter = 1;
            codedStr = new int[_key2.Length];
            found = false;
            key = new int[] { 1, 4, 5, 3, 2 };
            foreach (char с in alphabet)
            {
                for (int i = 0; i < _key2.Length; i++)
                {
                    if (_key2[i] == с)
                    {
                        codedStr[i] = counter;
                        if (matrix.ContainsKey(counter) == false)
                        {
                            matrix.Add(counter, _key2[i]);
                        }
                        found = true;
                    }
                }

                if (found)
                {
                    counter++;
                    found = false;
                }
            }

            key2 = codedStr;
        }
        public string Encrypt(string input)
        {
            string result = "";
            int length = 0;
            int countTable = input.Length / (key.Length * key2.Length) + 1;
            table = new char[countTable, key.Length, key2.Length];
            for (int i = 0; i < countTable; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    for (int k = 0; k < key2.Length; k++)
                    {
                        if (length < input.Length)
                        {
                            table[i, j, k] = input[length];
                            length++;
                        }
                        else
                        {
                            table[i, j, k] = ' ';
                        }
                    }
                }
            }
            char temp;
            int index, index2 = 0,
            number = 1, temp2;
            int[] massusingkeys = new int[key2.Length];
            for (int f = 0; f < key2.Length; f++)
            {
                massusingkeys[f] = 999;
            }
            for (int i = 0; i < key2.Length; i++)
            {
                index = Array.IndexOf(key2, number);
                for (int j = 0; j < countTable; j++)
                {
                    for (int k = 0; k < key.Length; k++)
                    {
                        if (Array.IndexOf(massusingkeys, index) == -1)
                        {
                            temp = table[j, k, index];
                            table[j, k, index] = table[j, k, index2];
                            table[j, k, index2] = temp;
                        }
                    }
                    if (Array.IndexOf(massusingkeys, index) == -1)
                    {
                        temp2 = key2[index];
                        key2[index] = key2[index2];
                        key2[index2] = temp2;
                    }
                }
                massusingkeys[i] = number - 1;
                number = number + 1;
                index2++;
            }
            index2 = 0;
            number = 1;
            massusingkeys = new int[key2.Length];
            for (int f = 0; f < key2.Length; f++)
            {
                massusingkeys[f] = 999;
            }
            for (int i = 0; i < key.Length; i++)
            {
                index = Array.IndexOf(key, Convert.ToInt32(number));
                for (int j = 0; j < countTable; j++)
                {
                    for (int k = 0; k < key2.Length; k++)
                    {
                        if (Array.IndexOf(massusingkeys, index) == -1)
                        {
                            temp = table[j, index, k];
                            table[j, index, k] = table[j, index2, k];
                            table[j, index2, k] = temp;
                        }
                    }
                    if (Array.IndexOf(massusingkeys, index) == -1)
                    {
                        temp2 = key[index];
                        key[index] = key[index2];
                        key[index2] = temp2;
                    }
                }
                massusingkeys[i] = number - 1;
                number = number + 1;
                index2++;
            }
            for (int i = 0; i < countTable; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    for (int k = 0; k < key2.Length; k++)
                    {
                        result += table[i, j, k];
                    }
                }
            }
            return result;
        }
        public string Decrypt(string output)
        {
            string result = "";
            int length = 0;
            int countTable = (output.Length - 1) / (key.Length * key2.Length) + 1;
            table = new char[countTable, key.Length, key2.Length];
            for (int i = 0; i < countTable; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    for (int k = 0; k < key2.Length; k++)
                    {
                        if (length < output.Length)
                        {
                            table[i, j, k] = output[length];
                            length++;
                        }
                        else
                        {
                            table[i, j, k] = ' ';
                        }
                    }
                }
            }
            char temp;
            int index, index2 = 0,
            number = 1;
            int[] massusingkeys = new int[key2.Length];
            for (int f = 0; f < key2.Length; f++)
            {
                massusingkeys[f] = 999;
            }
            for (int i = 0; i < key2.Length; i++)
            {
                index = Array.IndexOf(key2, number);
                for (int j = 0; j < countTable; j++)
                {
                    for (int k = 0; k < key.Length; k++)
                    {
                        if (Array.IndexOf(massusingkeys, index2) == -1)
                        {
                            temp = table[j, k, index];
                            table[j, k, index] = table[j, k, index2];
                            table[j, k, index2] = temp;
                        }
                    }
                }
                massusingkeys[i] = index;
                number = number + 1;
                index2++;
            }
            index2 = 0;
            number = 1;
            massusingkeys = new int[key2.Length];
            for (int f = 0; f < key2.Length; f++)
            {
                massusingkeys[f] = 999;
            }
            for (int i = 0; i < key.Length; i++)
            {
                index = Array.IndexOf(key, Convert.ToInt32(number));
                for (int j = 0; j < countTable; j++)
                {
                    for (int k = 0; k < key2.Length; k++)
                    {
                        if (Array.IndexOf(massusingkeys, index2) == -1)
                        {
                            temp = table[j, index, k];
                            table[j, index, k] = table[j, index2, k];
                            table[j, index2, k] = temp;
                        }
                    }
                }
                massusingkeys[i] = index;
                number = number + 1;
                index2++;
            }
            for (int i = 0; i < countTable; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    for (int k = 0; k < key2.Length; k++)
                    {
                        result += table[i, j, k];
                    }
                }
            }
            return result;
        }
    }
}
