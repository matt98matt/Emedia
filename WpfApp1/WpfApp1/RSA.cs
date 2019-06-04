using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class RSA
    {
        public int getN(int value1, int value2) {
            return value1 * value2;
        }

        public int getPhi(int value1, int value2)
        {
            return (value1- 1) * (value2 - 1);
        }

        public int EulerMethod(int value1, int value2)
        {
            while (value1 != 0 && value2 != 0)
            {
                if (value1 > value2)
                    value1 %= value2;
                else
                    value2 %= value1;
            }
            return (value1 > value2) ? value1 : value2;
        }

        public bool IsCoprime(int value1, int value2)
        {
            return EulerMethod(value1, value2) == 1;
        }

        public int getE(int phi, int n)
        {
            Random rnd = new Random();
            int e = rnd.Next(0, n);
  
            while (!(IsCoprime(e, phi)))
            {
                e = rnd.Next(0, n);
            }
            return e;
        }

        public int inverseMod(int a, int n)
        {
            int i = n;
            int v = 0;
            int d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

        public int getD(int e, int phi)
        {
            return inverseMod(e, phi);
        }

        public int findPrimaryNumber()
        {
            Random rnd = new Random();
            int candidat = rnd.Next(0, 100);
            while(!isPrime(candidat)) {candidat = rnd.Next(0, 100);}
            return candidat;
        }

        public bool isPrime(int n)
        {
            if (n < 2)
                return false; 

            for (int i = 2; i * i <= n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        public void RSAEncryption(int[] data, int e, int n)
        {
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = powMod(data[i], e, n); 
            }
            
        }

        public void RSADescryption(int[] data, int d, int n)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = powMod(data[i], d, n);
            }
        }

        public int powMod(int a, int w, int n)
        {
            int pot,wyn,q;

            pot = a; wyn = 1;
            for(q = w; q > 0; q /= 2)
            {
            if((q % 2) != 0) wyn = (wyn * pot) % n;
            pot = (pot * pot) % n;
            }
            return wyn;
            }


    }
}
