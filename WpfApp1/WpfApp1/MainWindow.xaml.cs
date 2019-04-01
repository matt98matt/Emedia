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

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Net.Http;
using System.Xml;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
                ObservableCollection<JPGInformation> jpg = new ObservableCollection<JPGInformation>
        {
        };

        public ObservableCollection<JPGInformation> Items
        {
            get => jpg;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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
                ShowJPGInformations();
            }
        }

        private void ShowJPGInformations()
        {
            byte[] image = System.IO.File.ReadAllBytes(ImageSource);
            
            int dana = 0;

            for(int i = 0; i<(image.Length-1); i++ ){
                if(image[i] == 0xff && (image[i+1] == 0xC0 || image[i+1] == 0xC2)){
                dana = i;
                    }
            }

            byte[] image2 = new byte[image[dana + 3]];
            for(int i = 0, j =0; i<(image2.Length); i++, j++){
                image2[j] = image[i + dana + 2];
}
            int width = 0;
            int height = 0;
            int bitDepth = 0;
            string numberOfComponents = "";

            height = image2[3]*16*16 + image2[4];
            width = image2[5]*16*16 + image2[6];
            bitDepth = image2[2];

            switch(image2[7]){
                case 0x01:
                numberOfComponents = "monochromatyczny";
                break;
                case 0x03:
                numberOfComponents = "Color YcbCr or YIQ";
                break;
                case 0x04:
                numberOfComponents = "CMYK";
                break;
                deafault:
                break;
       }

            jpg.Add(new JPGInformation{Text = ("Szerokość: " + width + "px")});  
            jpg.Add(new JPGInformation{Text = ("Wysokość: " + height + "px")});  
            jpg.Add(new JPGInformation{Text = ("Głębia bitu: " + bitDepth)});  
            jpg.Add(new JPGInformation{Text = ("Typ: " + numberOfComponents)});  
         
            for(int i = 0; i<image2[7]; i++)
{
            jpg.Add(new JPGInformation{Text = ("ID componentu: " + image2[8 + i*3])});  
            jpg.Add(new JPGInformation{Text = ("Współczynnik próbkowania pionowy:" + (image2[9 + i*3]&0x0F))}); 
            jpg.Add(new JPGInformation{Text = ("Współczynnik próbkowania poziomy:" + (image2[9 + i*3]&0xF0>>4))});  
            jpg.Add(new JPGInformation{Text = ("Numer tabeli kwantyzacji: " + image2[10 + i*3])}); 
}

            WriteToFile(image);
        }
        // https://stackoverflow.com/questions/772388/c-sharp-how-can-i-test-a-file-is-a-jpeg ??
        private void WriteToFile(byte[] image)
        {
            string path = @"C:\Users\micha\Desktop\E-Media\Images\test.txt";
            //File.WriteAllBytes(path,image);
        }
    }
}
