using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab10
{
    public partial class Form1 : Form
    {
        private UnicodeEncoding Converter = new UnicodeEncoding();
        Random random = new Random();
        //Create byte arrays to hold original, encrypted, and decrypted data.
        byte[] encryptedData;
        byte[] decryptedData;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32(593);
            int g = Convert.ToInt32(123);
            int x = Convert.ToInt32(8);
            crypt(p, g, x, txtBIn.Text);
            decrypt(p, x, txtBCrypt.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] dataToEncrypt = Converter.GetBytes(txtBIn.Text);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {

                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                var t = RSA.ExportParameters(false);
                encryptedData = RSAEncrypt(dataToEncrypt, t, false);
                this.txtBCrypt.Text = Converter.GetString(encryptedData);

                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                var d = RSA.ExportParameters(true);
                decryptedData = RSADecrypt(encryptedData, d, false);
                this.txtBDecrypt.Text = Converter.GetString(decryptedData);

            }
        }

        private int Rand()//Ф-я получения случайного числа
        {
            return random.Next();
        }
        int power(int a, int b, int n) // a^b mod n - возведение a в степень b по модулю n
        {
            int tmp = a;
            int sum = tmp;
            for (int i = 1; i < b; i++)
            {
                for (int j = 1; j < a; j++)
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
        int mul(int a, int b, int n) // a*b mod n - умножение a на b по модулю n
        {
            int sum = 0;
            for (int i = 0; i < b; i++)
            {
                sum += a;
                if (sum >= n)
                {
                    sum -= n;
                }
            }
            return sum;
        }
        void crypt(int p, int g, int x, string strIn)
        {
            txtBCrypt.Text = "";
            int y = power(g, x, p);
            txtBPublicKey.Text = "Открытый ключ (p,g,y) = (" + p + "," + g + "," + y + ")";
            txtBSecretKey.Text = "Закрытый ключ x = " + x;
            if (strIn.Length > 0)
            {
                char[] temp = new char[strIn.Length - 1];
                temp = strIn.ToCharArray();
                for (int i = 0; i <= strIn.Length - 1; i++)
                {
                    int m = (int)temp[i];
                    if (m > 0)
                    {
                        int k = Rand() % (p - 2) + 1; // 1 < k < (p-1)
                        int a = power(g, k, p);
                        int b = mul(power(y, k, p), m, p);
                        txtBCrypt.Text = txtBCrypt.Text + a + " " + b + " ";
                    }
                }
            }
        }
        void decrypt(int p, int x, string strIn)
        {
            if (strIn.Length > 0)
            {
                txtBDecrypt.Text = "";
                string[] strA = strIn.Split(' ');
                if (strA.Length > 0)
                {
                    for (int i = 0; i < strA.Length - 1; i += 2)
                    {
                        char[] a = new char[strA[i].Length];
                        char[] b = new char[strA[i + 1].Length];
                        int ai = 0;
                        int bi = 0;
                        a = strA[i].ToCharArray();
                        b = strA[i + 1].ToCharArray();
                        for (int j = 0; (j < a.Length); j++)
                        {
                            ai = ai * 10 + (int)(a[j] - 48);
                        }
                        for (int j = 0; (j < b.Length); j++)
                        {
                            bi = bi * 10 + (int)(b[j] - 48);
                        }
                        if ((ai != 0) && (bi != 0))
                        {
                            int deM = mul(bi, power(ai, p - 1 - x, p), p);// m=b*(a^x)^(-1)mod p =b*a^(p-1-x)mod p
                            char m = (char)deM;
                            txtBDecrypt.Text = txtBDecrypt.Text + m;
                        }
                    }
                }

            }
        }

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}
