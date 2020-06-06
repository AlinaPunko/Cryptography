using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Org.BouncyCastle.Math;

namespace Lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        List<BigInteger> testowa = new List<BigInteger>();
        MHKey key = new MHKey();

        class MHKey
        {
            public List<BigInteger> e { get; set; }
            public MHPrivateKey privateKey { get; set; }
            public MHKey()
            {
                generator gen = new generator(8);
                BigInteger w = gen.w;
                e = new List<BigInteger>();
                for (int i = 0; i < gen.a.Count; i++)
                {
                    e.Add(w.Multiply(gen.a.ElementAt(i)).Mod(gen.n)); // w*a Mod n ..
                }

                BigInteger w1 = BigInteger.ValueOf(1);
                BigInteger one = BigInteger.ValueOf(2);
                while (one.IntValue != 1)
                {
                    w1 = w1.Add(BigInteger.ValueOf(1)).Mod(gen.n);
                    one = w.Multiply(w1).Mod(gen.n);

                }
                privateKey = new MHPrivateKey(gen.a, w.ModInverse(gen.n), gen.n);
            }
            public string getList()
            {
                string str = "";
                foreach (BigInteger elem in this.e)
                {
                    str += elem + ",";
                }
                return str;
            }
            public void swap(int n, int m)
            {
                BigInteger tmp = (BigInteger)e.ElementAt(m);
                e.Insert(m, e.ElementAt(n));
                e.Insert(n, tmp);
            }

            public void permutation()
            {
                Random rnd = new Random();
                int tmp, tmp2;

                for (int i = 0; i < e.Count * e.Count; i++)
                {
                    tmp = Math.Abs(rnd.Next() % e.Count);
                    tmp2 = Math.Abs(rnd.Next() % e.Count);
                    swap(tmp, tmp2);
                }
            }

            public List<BigInteger> encipher(String plain)
            {
                String binary;
                BigInteger tmp;
                List<BigInteger> enciphered = new List<BigInteger>();
                char[] charArray = plain.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    tmp = BigInteger.ValueOf(0);
                    binary = charToBinary(charArray[i]);
                    char one = '1';
                    for (int j = binary.Length - 1; j > -1; j--)
                    {
                        if (binary.ElementAt(j) == one)
                        {
                            tmp = tmp.Add((BigInteger)e.ElementAt(7 - j));
                        }
                    }
                    enciphered.Add(tmp);
                }
                return enciphered;
            }

            public String decipher(List<BigInteger> cipher, MHPrivateKey key)
            {
                String decrypted = "";
                BigInteger temp = BigInteger.ValueOf(0);
                int tmp = 0;

                BigInteger bits = BigInteger.ValueOf(0);

                for (int i = 0; i < cipher.Count; i++)
                {
                    temp = cipher.ElementAt(i);
                    int bitlen = temp.BitLength;
                    int ff = 0;
                    while (bitlen < (int)Math.Pow(2, ff))
                    {
                        ff++;
                    }
                    if (ff > bitlen)
                        bitlen = ff;

                    for (int j = 0; j < bitlen; j++)
                    {
                        if (temp.Mod(BigInteger.ValueOf(2)).CompareTo(BigInteger.ValueOf(1)) == 0)
                        {
                            bits = bits.Add(key.w1.Multiply(BigInteger.ValueOf((long)Math.Pow(2, j))));
                        }
                        temp = temp.ShiftRight(1);
                    }
                    bits = bits.Mod(key.n);
                    List<BigInteger> list = (List<BigInteger>)key.a;
                    BigInteger temper;

                    int k = key.a.Count - 1;
                    while (k >= 0)
                    {
                        temper = list.ElementAt(k);
                        if (bits.CompareTo(temper) > -1)
                        {
                            tmp += (int)Math.Pow(2, k);
                            bits = bits.Subtract(temper);
                        }
                        k--;
                    }
                    decrypted += (binaryToChar(Convert.ToString(tmp, 2))).ToString();

                    bits = BigInteger.ValueOf(0);
                    tmp = 0;
                }
                return decrypted;
            }
            public String charToBinary(char ch)
            {
                String chBin = Convert.ToString((int)ch, 2).PadLeft(8, '0');


                return chBin;
            }
            char binaryToChar(String binStr)
            {
                char[] temp = binStr.ToCharArray();
                int sum = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    sum += (Int32.Parse(char.ToString(temp[i])) << (temp.Length - i - 1));
                }

                return (char)sum;
            }


            public BigInteger getGCD(BigInteger bd1, BigInteger bd2)
            {
                BigInteger bigger = (bd1.CompareTo(bd2) > 0) ? bd1 : bd2;
                BigInteger smaller = (bd1.CompareTo(bd2) < 0) ? bd1 : bd2;
                BigInteger gcd = smaller;
                while (!BigInteger.Zero.Equals(smaller))
                {
                    gcd = smaller;
                    smaller = bigger.Mod(smaller);
                    bigger = gcd;
                }
                return gcd;
            }

        }

        public class generator
        {
            public List<BigInteger> a { get; set; }
            public BigInteger n { get; set; }
            public BigInteger w { get; set; }

            public BigInteger getGCD(BigInteger bd1, BigInteger bd2)
            {
                BigInteger bigger = (bd1.CompareTo(bd2) > 0) ? bd1 : bd2;
                BigInteger smaller = (bd1.CompareTo(bd2) < 0) ? bd1 : bd2;
                BigInteger gcd = smaller;
                while (!BigInteger.Zero.Equals(smaller))
                {
                    gcd = smaller;
                    smaller = bigger.Mod(smaller);
                    bigger = gcd;
                }
                return gcd;
            }
            public generator(int k)
            {

                a = new List<BigInteger>();
                Random rnd = new Random();
                BigInteger tmp;
                BigInteger sum;
                sum = BigInteger.ValueOf(0);
                for (int i = 0; i < k; i++)
                {
                    tmp = BigInteger.ValueOf((long)(Math.Pow(2, k + i)));
                    a.Add(tmp);
                    sum = sum.Add(tmp);
                }
                n = sum.Add(BigInteger.ValueOf(1));
                w = BigInteger.ValueOf(467);
                while (this.getGCD(n, w).CompareTo(BigInteger.ValueOf(1)) != 0)
                {
                    w.Add(BigInteger.ValueOf(1));
                }

            }

            public BigInteger getListElement(int i)
            {

                BigInteger tmp = a.ElementAt(i);
                return tmp;
            }
            String getAll()
            {
                String tmp = "";
                for (int i = 0; i < a.Count; i++)
                    tmp += "" + a.ElementAt(i) + ",";
                tmp += ";" + w;
                tmp += ";" + n;

                return tmp;
            }
        }

        public class MHPrivateKey
        {
            public List<BigInteger> a { get; set; }
            public BigInteger w1 { get; set; }
            public BigInteger n { get; set; }

            public MHPrivateKey()
            {
                this.a = null;
                this.w1 = null;
                this.n = null;
            }

            public MHPrivateKey(List<BigInteger> a, BigInteger w1, BigInteger n)
            {
                this.a = a;
                this.w1 = w1;
                this.n = n;
            }


            public String getAll()
            {
                String tmp = "a[] = " + a.ElementAt(0);
                for (int i = 1; i < a.Count; i++)
                    tmp += "," + a.ElementAt(i);
                tmp += "\n  w^1 = " + w1;
                tmp += "\n  n = " + n;

                return tmp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            tbPrivateKey.Text = key.privateKey.getAll();
            tbPublicKey.Text = key.getList();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            var cipherBigIntegers = key.encipher(tbMessage.Text);
            string ciphertext = "";
            int i = 0;
            foreach (BigInteger elem in cipherBigIntegers)
            {
                ciphertext += elem.ToString();
                i++;
                if (cipherBigIntegers.Count != i)
                    ciphertext += " ";
            }
            tbMessage.Text = ciphertext;

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            List<BigInteger> list = new List<BigInteger>();
            string str = ""; 
            var txt = tbMessage.Text;

            foreach (char elem in tbMessage.Text)
            {
                if (elem != ' ')
                    str += elem;
                else
                {
                    list.Add(new BigInteger(str));
                    str = "";
                }
            }
            list.Add(new BigInteger(str));

            tbMessage.Text = key.decipher(list, key.privateKey);

        }
    }
}
