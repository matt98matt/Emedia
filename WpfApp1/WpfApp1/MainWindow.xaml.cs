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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string ImageSource;

        private void ButtonToImage_Click(object sender, RoutedEventArgs e)
        {
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\micha\\Desktop\\E-Media\\Images";
            openFileDialog.Filter = "jpeg files (*.jpg)|*.jpg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
            }
            if (filePath != "")
            {
                filePath = openFileDialog.FileName;
                ImageSource = filePath;
                Image.Source = new BitmapImage(new Uri(filePath));
                ToArray();
            }
        }

        private void ToArray()
        {
            byte[] image = System.IO.File.ReadAllBytes(ImageSource);
            WriteToFile(image);
        }
        // https://stackoverflow.com/questions/772388/c-sharp-how-can-i-test-a-file-is-a-jpeg ??
        private void WriteToFile(byte[] image)
        {
            string path = @"C:\Users\micha\Desktop\E-Media\Images\test.txt";
            File.WriteAllBytes(path,image);
        }
    }
}
