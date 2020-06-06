using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnigmaMachine machine = new EnigmaMachine();
            if (!Regex.IsMatch(Decrypted.Text, @"^[a-zA-Z ]+$"))
            {
                Decrypted.Text = "Only letters A-Z is allowed, try again: ";
            }
            else
            {
                machine.SetSettings(Ring.Text.ToCharArray(), StartPos.Text.ToCharArray(), Rotors.Text, Reflector.Text[0]);
                if (Plugs.Text != "")
                {
                    string[] plugs = Plugs.Text.Split(' ');
                    foreach (string plug in plugs)
                    {
                        char[] p = plug.ToCharArray();
                        machine.AddPlug(p[0], p[1]);
                    }
                }
                Decrypted.Text = Decrypted.Text.Replace(" ", "").ToUpper();
                Encrypted.Text = machine.RunEnigma(Decrypted.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EnigmaMachine machine = new EnigmaMachine();
            if (!Regex.IsMatch(Encrypted.Text, @"^[a-zA-Z ]+$"))
            {
                Encrypted.Text = "Only letters A-Z is allowed, try again: ";
            }
            else
            {
                machine.SetSettings(Ring.Text.ToCharArray(), StartPos.Text.ToCharArray(), Rotors.Text, Reflector.Text[0]);
                if (Plugs.Text != "")
                {
                    string[] plugs = Plugs.Text.Split(' ');
                    foreach (string plug in plugs)
                    {
                        char[] p = plug.ToCharArray();
                        machine.AddPlug(p[0], p[1]);
                    }
                }
                Encrypted.Text = Encrypted.Text.Replace(" ", "").ToUpper();
                Decrypted.Text = machine.RunEnigma(Encrypted.Text);
            }
        }
    }
}
