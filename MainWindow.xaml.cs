using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace DES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DES_Algorithm.Encoding("0123456789ABCDEF", "133457799BBCDFF1");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Bin files (*.bin)|*.bin"
            };
            if (openFileDialog.ShowDialog() == true) inputTextBox.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
