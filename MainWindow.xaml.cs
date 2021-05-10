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
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin"
            };
            if (openFileDialog.ShowDialog() == true) inputTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            if (binRadioButton.IsChecked == false)
                binRadioButton.IsChecked = true;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputTextBox.Text) && !string.IsNullOrWhiteSpace(keyTextBox.Text))
            {
                if (keyTextBox.Text.Length == 16)
                {
                    if (binRadioButton.IsChecked == true)
                    {
                        outputTextBox.Text = DES_Algorithm.Encoding(inputTextBox.Text, keyTextBox.Text, true);
                    }
                    else
                    {
                        outputTextBox.Text = DES_Algorithm.Encoding(inputTextBox.Text, keyTextBox.Text, false);
                    }
                }
                else
                {
                    outputTextBox.Text = "Key must be 16 characters long";
                }
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputTextBox.Text) && !string.IsNullOrWhiteSpace(keyTextBox.Text) && keyTextBox.Text.Length == 16)
            {
                if (keyTextBox.Text.Length == 16)
                {
                    if (binRadioButton.IsChecked == true)
                    {
                        outputTextBox.Text = DES_Algorithm.Decoding(inputTextBox.Text, keyTextBox.Text, true);
                    }
                    else
                    {
                        outputTextBox.Text = DES_Algorithm.Decoding(inputTextBox.Text, keyTextBox.Text, false);
                    }

                }
                else
                {
                    outputTextBox.Text = "Key must be 16 characters long";
                }
            }
        }
    }
}
