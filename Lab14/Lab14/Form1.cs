using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BitArray ByteToBit(byte src)
        {
            BitArray bitArray = new BitArray(8);
            bool st = false;
            for (int i = 0; i < 8; i++)
            {
                if ((src >> i & 1) == 1)
                {
                    st = true;
                }
                else st = false;
                bitArray[i] = st;
            }
            return bitArray;
        }

        private byte BitToByte(BitArray scr)
        {
            byte num = 0;
            for (int i = 0; i < scr.Count; i++)
                if (scr[i] == true)
                    num += (byte)Math.Pow(2, i);
            return num;
        }

        /*Проверяет, зашифрован ли файл,  возвраещает true, если символ в первом пикслеле равен / иначе false */
        private bool isEncryption(Bitmap scr)
        {
            byte[] rez = new byte[1];
            Color color = scr.GetPixel(0, 0);
            BitArray colorArray = ByteToBit(color.R); //получаем байт цвета и преобразуем в массив бит
            BitArray messageArray = ByteToBit(color.R); ;//инициализируем результирующий массив бит
            messageArray[0] = colorArray[0];
            messageArray[1] = colorArray[1];

            colorArray = ByteToBit(color.G);//получаем байт цвета и преобразуем в массив бит
            messageArray[2] = colorArray[0];
            messageArray[3] = colorArray[1];
            messageArray[4] = colorArray[2];

            colorArray = ByteToBit(color.B);//получаем байт цвета и преобразуем в массив бит
            messageArray[5] = colorArray[0];
            messageArray[6] = colorArray[1];
            messageArray[7] = colorArray[2];
            rez[0] = BitToByte(messageArray); //получаем байт символа, записанного в 1 пикселе
            string m = Encoding.GetEncoding(1251).GetString(rez);
            if (m == "/")
            {
                return true;
            }
            else return false;
        }

        /*Записыает количество символов для шифрования в первые биты картинки */
        private void WriteCountText(int count, Bitmap src)
        {
            byte[] CountSymbols = Encoding.GetEncoding(1251).GetBytes(count.ToString());
            for (int i = 0; i < 3; i++)
            {
                BitArray bitCount = ByteToBit(CountSymbols[i]); //биты количества символов
                Color pColor = src.GetPixel(0, i + 1); //1, 2, 3 пикселы
                BitArray bitsCurColor = ByteToBit(pColor.R); //бит цветов текущего пикселя
                bitsCurColor[0] = bitCount[0];
                bitsCurColor[1] = bitCount[1];
                byte nR = BitToByte(bitsCurColor); //новый бит цвета пиксея

                bitsCurColor = ByteToBit(pColor.G);//бит бит цветов текущего пикселя
                bitsCurColor[0] = bitCount[2];
                bitsCurColor[1] = bitCount[3];
                bitsCurColor[2] = bitCount[4];
                byte nG = BitToByte(bitsCurColor);//новый цвет пиксея

                bitsCurColor = ByteToBit(pColor.B);//бит бит цветов текущего пикселя
                bitsCurColor[0] = bitCount[5];
                bitsCurColor[1] = bitCount[6];
                bitsCurColor[2] = bitCount[7];
                byte nB = BitToByte(bitsCurColor);//новый цвет пиксея

                Color nColor = Color.FromArgb(nR, nG, nB); //новый цвет из полученных битов
                src.SetPixel(0, i + 1, nColor); //записали полученный цвет в картинку
            }
        }

        /*Читает количество символов для дешифрования из первых бит картинки*/
        private int ReadCountText(Bitmap src)
        {
            byte[] rez = new byte[3]; //массив на 3 элемента, т.е. максимум 999 символов шифруется
            for (int i = 0; i < 3; i++)
            {
                Color color = src.GetPixel(0, i + 1); //цвет 1, 2, 3 пикселей 
                BitArray colorArray = ByteToBit(color.R); //биты цвета
                BitArray bitCount = ByteToBit(color.R); ; //инициализация результирующего массива бит
                bitCount[0] = colorArray[0];
                bitCount[1] = colorArray[1];

                colorArray = ByteToBit(color.G);
                bitCount[2] = colorArray[0];
                bitCount[3] = colorArray[1];
                bitCount[4] = colorArray[2];

                colorArray = ByteToBit(color.B);
                bitCount[5] = colorArray[0];
                bitCount[6] = colorArray[1];
                bitCount[7] = colorArray[2];
                rez[i] = BitToByte(bitCount);
            }
            string m = Encoding.GetEncoding(1251).GetString(rez);
            return Convert.ToInt32(m, 10);
        }

        /* Открыть файл для шифрования */
        private void button1_Click(object sender, EventArgs e)
        {
            string FilePic;
            string FileText;
            OpenFileDialog dPic = new OpenFileDialog();
            dPic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (dPic.ShowDialog() == DialogResult.OK)
            {
                FilePic = dPic.FileName;
            }
            else
            {
                return;
            }

            FileStream rFile;
            try
            {
                rFile = new FileStream(FilePic, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap bPic = new Bitmap(rFile);

            OpenFileDialog dText = new OpenFileDialog();
            dText.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (dText.ShowDialog() == DialogResult.OK)
            {
                FileText = dText.FileName;
            }
            else
            {
                return;
            }

            FileStream rText;
            try
            {
                rText = new FileStream(FileText, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BinaryReader bText = new BinaryReader(rText, Encoding.ASCII);

            List<byte> bList = new List<byte>();
            while (bText.PeekChar() != -1)
            { //считали весь текстовый файл для шифрования в лист байт
                bList.Add(bText.ReadByte());
            }
            int CountText = bList.Count; // в CountText - количество в байтах текста, который нужно закодировать
            bText.Close();
            rFile.Close();

            //проверяем, поместиться ли исходный текст в картинке
            if (CountText > (bPic.Width * bPic.Height))
            {
                MessageBox.Show("Выбранная картинка мала для размещения выбранного текста", "Информация", MessageBoxButtons.OK);
                return;
            }

            //проверяем, может быть картинка уже зашифрована
            if (isEncryption(bPic))
            {
                MessageBox.Show("Файл уже зашифрован", "Информация", MessageBoxButtons.OK);
                return;
            }

            byte[] Symbol = Encoding.GetEncoding(1251).GetBytes("/");
            BitArray ArrBeginSymbol = ByteToBit(Symbol[0]);
            Color curColor = bPic.GetPixel(0, 0);
            BitArray tempArray = ByteToBit(curColor.R);
            tempArray[0] = ArrBeginSymbol[0];
            tempArray[1] = ArrBeginSymbol[1];
            byte nR = BitToByte(tempArray);

            tempArray = ByteToBit(curColor.G);
            tempArray[0] = ArrBeginSymbol[2];
            tempArray[1] = ArrBeginSymbol[3];
            tempArray[2] = ArrBeginSymbol[4];
            byte nG = BitToByte(tempArray);

            tempArray = ByteToBit(curColor.B);
            tempArray[0] = ArrBeginSymbol[5];
            tempArray[1] = ArrBeginSymbol[6];
            tempArray[2] = ArrBeginSymbol[7];
            byte nB = BitToByte(tempArray);

            Color nColor = Color.FromArgb(nR, nG, nB);
            bPic.SetPixel(0, 0, nColor);
            //то есть в первом пикселе будет символ /, который говорит о том, что картика зашифрована

            WriteCountText(CountText, bPic); //записываем количество символов для шифрования

            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == bList.Count)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(bList[index]);
                    colorArray[0] = messageArray[0]; //меняем
                    colorArray[1] = messageArray[1]; // в нашем цвете биты
                    byte newR = BitToByte(colorArray);

                    colorArray = ByteToBit(pixelColor.G);
                    colorArray[0] = messageArray[2];
                    colorArray[1] = messageArray[3];
                    colorArray[2] = messageArray[4];
                    byte newG = BitToByte(colorArray);

                    colorArray = ByteToBit(pixelColor.B);
                    colorArray[0] = messageArray[5];
                    colorArray[1] = messageArray[6];
                    colorArray[2] = messageArray[7];
                    byte newB = BitToByte(colorArray);

                    Color newColor = Color.FromArgb(newR, newG, newB);
                    bPic.SetPixel(i, j, newColor);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            pictureBox1.Image = bPic;

            string sFilePic;
            SaveFileDialog dSavePic = new SaveFileDialog();
            dSavePic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (dSavePic.ShowDialog() == DialogResult.OK)
            {
                sFilePic = dSavePic.FileName;
            }
            else
            {
                sFilePic = "";
                return;
            };

            FileStream wFile;
            try
            {
                wFile = new FileStream(sFilePic, FileMode.Create); //открываем поток на запись результатов
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bPic.Save(wFile, System.Drawing.Imaging.ImageFormat.Bmp);
            wFile.Close(); //закрываем поток
        }

        /*Открыть файл для дешифрования */
        private void button2_Click(object sender, EventArgs e)
        {
            string FilePic;
            OpenFileDialog dPic = new OpenFileDialog();
            dPic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (dPic.ShowDialog() == DialogResult.OK)
            {
                FilePic = dPic.FileName;
            }
            else
            {
                return;
            }

            FileStream rFile;
            try
            {
                rFile = new FileStream(FilePic, FileMode.Open); //открываем поток
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap bPic = new Bitmap(rFile);
            if (!isEncryption(bPic))
            {
                MessageBox.Show("В файле нет зашифрованной информации", "Информация", MessageBoxButtons.OK);
                return;
            }

            int countSymbol = ReadCountText(bPic); //считали количество зашифрованных символов
            byte[] message = new byte[countSymbol];
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == message.Length)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(pixelColor.R); ;
                    messageArray[0] = colorArray[0];
                    messageArray[1] = colorArray[1];

                    colorArray = ByteToBit(pixelColor.G);
                    messageArray[2] = colorArray[0];
                    messageArray[3] = colorArray[1];
                    messageArray[4] = colorArray[2];

                    colorArray = ByteToBit(pixelColor.B);
                    messageArray[5] = colorArray[0];
                    messageArray[6] = colorArray[1];
                    messageArray[7] = colorArray[2];
                    message[index] = BitToByte(messageArray);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            string strMessage = Encoding.GetEncoding(1251).GetString(message);

            string sFileText;
            SaveFileDialog dSaveText = new SaveFileDialog();
            dSaveText.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (dSaveText.ShowDialog() == DialogResult.OK)
            {
                sFileText = dSaveText.FileName;
            }
            else
            {
                sFileText = "";
                return;
            };

            FileStream wFile;
            try
            {
                wFile = new FileStream(sFileText, FileMode.Create); //открываем поток на запись результатов
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла на запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StreamWriter wText = new StreamWriter(wFile, Encoding.Default);
            wText.Write(strMessage);
            MessageBox.Show("Текст записан в файл", "Информация", MessageBoxButtons.OK);
            wText.Close();
            wFile.Close(); //закрываем поток

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
