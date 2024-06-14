using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HashCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashFunctions hashFunction;
        private string filePath = null!;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            bool hasReqSize = false;

            while (!hasReqSize)
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    hasReqSize = HasReqSize(openFileDialog.FileName, 5);
                    if (hasReqSize)
                    {
                        filePath = openFileDialog.FileName;
                        FilePathTbx.Text = filePath;
                    }
                    else
                    {
                        MessageBox.Show("The file must be at least 5MB in size.", "Hash Caclulator", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file first.", "Hash Calculator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                HashResult hashResult = null!;
                if (hashFunction == HashFunctions.SHA256)
                {
                    hashResult = new HashResult()
                    {
                        HashValue = HashCalc.H256(filePath),
                        FilePath = filePath,
                        HashFunction = hashFunction
                    };
                }
                else if (hashFunction == HashFunctions.SHA512)
                {
                    hashResult = new HashResult()
                    {
                        HashValue = HashCalc.H512(filePath),
                        FilePath = filePath,
                        HashFunction = hashFunction
                    };
                }
                App.Data.HashResults!.Add(hashResult);
            }
        }

        private bool HasReqSize(string filePath, int mbSize)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length >= mbSize * 1024 * 1024;
        }

        private void SHA256_Checked(object sender, RoutedEventArgs e)
        {
            hashFunction = HashFunctions.SHA256;
        }

        private void SHA512_Checked(object sender, RoutedEventArgs e)
        {
            hashFunction = HashFunctions.SHA512;
        }
    }
}