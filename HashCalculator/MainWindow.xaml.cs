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
using System.Text.Json;

namespace HashCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashFunctions hashFunction;
        private string filePath = null!;
        private HashResult? selectedHashResult;

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

        private bool IsHashCalculated(string path, HashFunctions hash)   
        {
            foreach (HashResult hashResult in App.Data.HashResults!)
            {
                if (hashResult.FilePath == path && hashResult.HashFunction==hash)
                {
                    return true;
                }
            }
            return false;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file first.", "Hash Calculator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (!IsHashCalculated(filePath, hashFunction))
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
                else
                {
                    MessageBox.Show("The hash for this file has already been calculated.", "Hash Calculator", MessageBoxButton.OK, MessageBoxImage.Information);
                }                
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.Data.Load();
            if (selectedHashResult != null)
            {
                FileNameLbl.Text = new FileInfo(selectedHashResult.FilePath!).Name;
                FilePathLbl.Text = selectedHashResult.FilePath;
                FileSizeLbl.Text = new FileInfo(selectedHashResult.FilePath!).Length.ToString();
                HashValueLbl.Text = selectedHashResult.HashValue;
            }
            else
            {
                FileNameLbl.Text = "No file selected";
                FilePathLbl.Text = "";
                FileSizeLbl.Text = "";
                HashValueLbl.Text = "";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Data.Save();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {            
            Clipboard.SetText(((sender as Button)?.DataContext as HashResult)!.HashValue);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            HashResult? hashResult = (sender as Button)?.DataContext as HashResult;
            App.Data.HashResults!.Remove(hashResult!);
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            if(App.Data.HashResults!.Count == 0)
            {
                MessageBox.Show("There are no hash results to erase.", "Hash Calculator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to erase all hash results?", "Hash Calculator", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    File.WriteAllText("hashResults.json", "[]");
                    App.Data.HashResults!.Clear();
                    App.Data.Load();
                }
            }
        }

        private void BackUpButtton_Click(object sender, RoutedEventArgs e)
        {
            if(App.Data.HashResults!.Count == 0)
            {
                MessageBox.Show("There are no hash results to back up.", "Hash Calculator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON files (*.json)|*.json";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string json = JsonSerializer.Serialize(App.Data.HashResults);
                    File.WriteAllText(saveFileDialog.FileName, json);
                }
            }
        }

        private void HashResultsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView? listView = sender as ListView;
            selectedHashResult = listView!.SelectedItem as HashResult;
            if (selectedHashResult != null)
            {
                FileInfo fileInfo = new FileInfo(selectedHashResult.FilePath!);
                FileNameLbl.Text = fileInfo.Name;
                FilePathLbl.Text = selectedHashResult.FilePath;
                FileSizeLbl.Text = fileInfo.Length.ToString();
                HashValueLbl.Text= selectedHashResult.HashValue;
            }
            else
            {
                FileNameLbl.Text = "No file selected";
                FilePathLbl.Text = "";
                FileSizeLbl.Text = "";
                HashValueLbl.Text = "";
            }
        }

        private void CoppyHashValue_Click(object sender, RoutedEventArgs e)
        {
            if(selectedHashResult != null)
            {
                Clipboard.SetText(selectedHashResult.HashValue);
            }
        }
    }
}