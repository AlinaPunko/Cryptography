using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button3.Text = @"RC4 Decrypt";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            int[] result = congruential(1);
            foreach (int x in result)
            {
                richTextBox1.Text += x + " ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
            string[] s = richTextBox4.Text.Split(' ');
            byte[] key = new byte[s.Length * 2];
            for (int i = 0; i < s.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(int.Parse(s[i]));
                key[i * 2] = temp[0];
                key[i * 2 + 1] = temp[1];
            }
            RC4 rc4 = new RC4(key);
            string text = richTextBox2.Text;
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(text[i]);
                result[i] = BitConverter.ToChar(rc4.Encode(temp, 2), 0);
                richTextBox3.Text += result[i].ToString();
            }
        }

        int[] congruential(int x) // функция генерации псевдослучайных чисел
        {
            int[] result = new int[7875];
            int n = 7875, // генерация псевдослучайных чисел в 
                //диапазоне значений от 0 до 7875 (выбирается случайно n > 0)
                a = 421, // множитель (выбирается случайно 0 <= a <= n)
                c = 1663; // инкрементирующее значение (выбирается случайно 0 <= c <= mn
            for (int i = 0; i < n; i++)
            {
                x = ((a * x) + c) % n; // формула линейного конгруэнтного метода генерации псевдослучайных чисел
                result[i] = x;
            }
            return result.Take(100).ToArray();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            richTextBox2.Text = "";
            string[] s = richTextBox4.Text.Split(' ');
            byte[] key = new byte[s.Length * 2];
            for (int i = 0; i < s.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(int.Parse(s[i]));
                key[i * 2] = temp[0];
                key[i * 2 + 1] = temp[1];
            }
            RC4 rc4 = new RC4(key);
            string text = richTextBox3.Text;
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(text[i]);
                result[i] = BitConverter.ToChar(rc4.Decode(temp, 2), 0);
                richTextBox2.Text += result[i];
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class RC4
    {
        byte[] S = new byte[256];

        int x = 0;
        int y = 0;

        public RC4(byte[] key)
        {
            init(key);
        }

        // Key-Scheduling Algorithm 
        // Алгоритм ключевого расписания 
        private void init(byte[] key)
        {
            int keyLength = key.Length;

            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;
                S.Swap(i, j);
            }
        }

        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] data = dataB.Take(size).ToArray();

            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ keyItem());
            }

            return cipher;
        }
        public byte[] Decode(byte[] dataB, int size)
        {
            return Encode(dataB, size);
        }

        // Pseudo-Random Generation Algorithm 
        // Генератор псевдослучайной последовательности 
        private byte keyItem()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            S.Swap(x, y);

            return S[(S[x] + S[y]) % 256];
        }
    }

    static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
