using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class XOR
    {

        public string path = @"key.txt";

        public void ExclusiveOR(byte[] data, byte[] key )
        {
            for(int i = 0; i< data.Length; i++)
            {
                data[i] ^= key[i];
            }
        }

        public void GenerateKey(int length)
        {
            Random random = new Random();
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            byte[] key = new byte[length];
            random.NextBytes(key);
            File.WriteAllBytes(path, key);
        }

        public byte[] ReadKey()
        {
            return  System.IO.File.ReadAllBytes(path);
        }
    }
}
