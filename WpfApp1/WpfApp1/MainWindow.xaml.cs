using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
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


    public partial class MainWindow : Window
    {
        public static int N1;
        public static int E1;
        public static int D1;
        public static int lenght1;

        public static BigInteger N2;
        public static BigInteger E2;
        public static BigInteger D2;
        public static int lenght2;
        public static BigInteger[] data;

        public static  string ImageSource;
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

        private void ButtonToNoisyImage_Click(object sender, RoutedEventArgs e)
        {
                NoisyImage.Source = new BitmapImage(new Uri("C:\\Users\\Mateusz\\Desktop\\pwr6\\Emedia\\WpfApp1\\WpfApp1\\bin\\Debug\\test.jpg"));
        }

        private void ButtonToXOR_Click(object sender, RoutedEventArgs e)
        {

            JPGInformation jpg = new JPGInformation();
            XOR xor = new XOR();
            string path = @"test.jpg";
            byte[] image = System.IO.File.ReadAllBytes(ImageSource);
            int startSOF = jpg.FindSOF(image);
            int startSOS = jpg.FindSOS(image);
            int number = jpg.ReturnJPGComponentNumber(image, startSOF); 
            byte[] data = jpg.ReturnJPGData(image, startSOS, number);
            xor.GenerateKey(data.Length);
            byte[] key = xor.ReadKey();
            xor.ExclusiveOR(data, key);
            jpg.ChangeJPGData(image, startSOS, number, data);
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            File.WriteAllBytes(path, image);
        }

        private void ButtonToNotXOR_Click(object sender, RoutedEventArgs e) 
{
            JPGInformation jpg = new JPGInformation();
            XOR xor = new XOR();
            string path = @"test.jpg";
            byte[] image = System.IO.File.ReadAllBytes(path);
            int startSOF = jpg.FindSOF(image);
            int startSOS = jpg.FindSOS(image);
            int number = jpg.ReturnJPGComponentNumber(image, startSOF); 
            byte[] data = jpg.ReturnJPGData(image, startSOS, number);
            byte[] key = xor.ReadKey();
            xor.ExclusiveOR(data, key); 
            jpg.ChangeJPGData(image, startSOS, number, data);
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            File.WriteAllBytes(path, image);
}
        private void ButtonToSmallERSA_Click(object sender, RoutedEventArgs e) 
{            
            string path = @"key.txt";
            XOR xor = new XOR();
            byte[] key = xor.ReadKey();
            RSA x = new RSA();
            int p = x.findPrimaryNumber();
            int q = x.findPrimaryNumber();
            while(q == p) q = q = x.findPrimaryNumber();
            int n = x.getN(p, q);
            N1 = n;
            int phi = x.getPhi(p,q);
            int E = x.getE(phi, n);
            E1 = E;
            int D = x.getD(E, phi);
            D1 = D;
            int[] key2 = new int[key.Length];
            for(int i = 0; i<key.Length; i++)
            {
                key2[i] = (int)(key[i]);
            }
            x.RSAEncryption(key2, E, n);
            lenght1 = key2.Length;
            string key3 = " ";
            for(int i = 0; i<key2.Length; i++)
            {
                key3 += " " + (key2[i]).ToString();
            }
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            File.WriteAllText(path, key3);
}
        private void ButtonToSmallDRSA_Click(object sender, RoutedEventArgs e) 
        {
            string path = @"key.txt";
            string test = System.IO.File.ReadAllText(path);
            string[] key = test.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] key2 = new int[key.Length];
            for(int i = 0; i<key.Length; i++)
            {
                key2[i] = Convert.ToInt32(key[i]);
            }
            RSA x = new RSA();
            x.RSADescryption(key2, D1, N1);
            byte[] key3 = new byte[key2.Length];
            for(int i = 0; i<key2.Length; i++)
            {
                key3[i] = (byte)key2[i];
            }
            System.IO.File.WriteAllBytes(path, key3);
        }
        private void ButtonToBigERSA_Click(object sender, RoutedEventArgs e) {
  
            string path = @"key.txt";
            XOR xor = new XOR();
            byte[] key = xor.ReadKey();
            BigPrimeGenerator x = new BigPrimeGenerator();
            BigRSA rsa = new BigRSA();
            BigInteger p = x.getBigPrimeNumber(3);
            BigInteger q = x.getBigPrimeNumber(3);
            while(q == p) q = q = x.getBigPrimeNumber(3);
            BigInteger n = rsa.getN(p, q);
            N2 = n;
            BigInteger phi = rsa.getPhi(p,q);
            BigInteger E = rsa.getE(phi, n);
            E2 = E;
            BigInteger D = rsa.getD(E, phi);
            D2 = D;
            BigInteger[] key2 = new BigInteger[key.Length];
            for(int i = 0; i<key.Length; i++)
            {
                key2[i] = new BigInteger(key[i]);
            }
            rsa.RSAEncryption(key2, E, n);
            lenght2 = key2.Length;
            string key3 = " ";
            for(int i = 0; i<key2.Length; i++)
            {
                key3 += " " + (key2[i]).ToString();
            }
            data = new BigInteger[key2.Length];
            for (int i = 0; i < key2.Length; i++)
            {
                data[i] = (key2[i]);
            }
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            File.WriteAllText(path, key3);
            /*
            rsa.RSADescryption(key2, D, n);
            xor.ExclusiveOR(data, key); */
}
        private void ButtonToBigDRSA_Click(object sender, RoutedEventArgs e) 
{
            string path = @"key.txt";
            BigInteger[] key2 = new BigInteger[data.Length];
            for(int i = 0; i<data.Length; i++)
            {
                key2[i] = (data[i]);
            }
            BigRSA x = new BigRSA();
            x.RSADescryption(key2, D2, N2);
            string[] key3 = new string[key2.Length];
            byte[] key4 = new byte[key2.Length];
            for(int i = 0; i<key2.Length; i++)
            {
                key3[i] = key2[i].ToString();
                key4[i] = Convert.ToByte(key3[i]);
            }
            System.IO.File.WriteAllBytes(path, key4);
}


        private void ShowJPGInformations()
        {
            string path = @"test.jpg";
            byte[] image = System.IO.File.ReadAllBytes(ImageSource);
            int start = 0;
            
            JPGInformation information = new JPGInformation();
            
            start = information.FindSOF(image);

            int width = 0;
            int height = 0;
            int bitDepth = 0;
            string numberOfComponents = "";
            byte[] component;

            height = information.ReturnJPGHeight(image, start);
            width = information.ReturnJPGWidth(image, start);
            bitDepth = information.ReturnJPGBitDepth(image, start);
            numberOfComponents = information.ReturnJPGType(image, start);
            component = information.ReturnJPGComponentBytes(image, start); 

            jpg.Add(new JPGInformation{Text = ("Szerokość: " + width + "px")});  
            jpg.Add(new JPGInformation{Text = ("Wysokość: " + height + "px")});  
            jpg.Add(new JPGInformation{Text = ("Głębia bitu: " + bitDepth)});  
            jpg.Add(new JPGInformation{Text = ("Typ: " + numberOfComponents)});  
         
            for(int i = 0; i<component[0]; i++)
            {
            jpg.Add(new JPGInformation{Text = ("ID componentu: " + component[1 + i*3])});  
            jpg.Add(new JPGInformation{Text = ("Współczynnik próbkowania pionowy:" + (component[2 + i*3]&0x0F))}); 
            jpg.Add(new JPGInformation{Text = ("Współczynnik próbkowania poziomy:" + (component[2 + i*3]&0xF0>>4))});  
            jpg.Add(new JPGInformation{Text = ("Numer tabeli kwantyzacji: " + component[3 + i*3])}); 
            }
        }
    }
}
