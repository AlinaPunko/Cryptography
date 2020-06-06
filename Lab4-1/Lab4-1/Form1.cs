using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                textBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            string Ex2 = textBox2.Text;
            int cols1 = (int)Math.Ceiling(Math.Sqrt(Ex2.Length));
            int rows1 = (int)Math.Ceiling(Ex2.Length / (double)cols1);
            dataGridView2.RowCount = rows1;
            dataGridView2.ColumnCount = cols1;

            for (int j = 0; j < rows1; j++)
                for (int i = 0; i < cols1; i++)
                    if (Ex2.Length > j * cols1 + i) dataGridView2[i, j].Value = Ex2[j * cols1 + i];
                    else dataGridView1[i, j].Value = '+';
            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < cols1; i++)
                for (int j = 0; j < rows1; j++)
                    sb1.Append(dataGridView2[i, j].Value.ToString());
            string str1 = sb1.ToString();
            string str = "";
            for (int i = 0; i < str1.Length; i++)
            {
                if (char.IsLetter(str1[i]))
                    str = str + str1[i];
                else
                {
                    str = str;
                }
            }
            textBox3.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            int cols = (int)Math.Ceiling(Math.Sqrt(s.Length));
            int rows = (int)Math.Ceiling(s.Length / (double)cols);
            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = cols;
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    if (s.Length > i * rows + j) dataGridView1[i, j].Value = s[i * rows + j];
                    else dataGridView1[i, j].Value = '+';
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < cols; i++)
                    sb.Append(dataGridView1[i, j].Value.ToString());
            textBox2.Text = sb.ToString();

        }
    }
}
