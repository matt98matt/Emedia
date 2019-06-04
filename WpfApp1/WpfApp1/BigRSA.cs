using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class BigRSA
    {

        public BigInteger getN(BigInteger value1, BigInteger value2)
        {
            return value1 * value2;
        }

        public BigInteger getPhi(BigInteger value1, BigInteger value2)
        {
            return (value1 - 1) * (value2 - 1);
        }

        public BigInteger EulerMethod(BigInteger value1, BigInteger value2)
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

        public bool IsCoprime(BigInteger value1, BigInteger value2)
        {
            return EulerMethod(value1, value2) == 1;
        }

        public BigInteger getE(BigInteger phi, BigInteger n)
        {
            BigPrimeGenerator x = new BigPrimeGenerator();
            BigInteger e = x.random(0, n);
            while(e == -1) e = x.random(0, n);

            while (!(IsCoprime(e, phi)))
            {
                e = x.random(0, n);
                while (e == -1) e = x.random(0, n);
            }
            return e;
        }

        public BigInteger inverseMod(BigInteger a, BigInteger n)
        {
            BigInteger i = n;
            BigInteger v = 0;
            BigInteger d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
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

        public BigInteger getD(BigInteger e, BigInteger phi)
        {
            return inverseMod(e, phi);
        }


        public void RSAEncryption(BigInteger[] data, BigInteger e, BigInteger n)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = powMod(data[i], e, n);
            }

        }

        public void RSADescryption(BigInteger[] data, BigInteger d, BigInteger n)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = powMod(data[i], d, n);
            }
        }

        public BigInteger powMod(BigInteger a, BigInteger w, BigInteger n)
        {
            BigInteger pot, wyn, q;

            pot = a; wyn = 1;
            for (q = w; q > 0; q /= 2)
            {
                if ((q % 2) != 0) wyn = (wyn * pot) % n;
                pot = (pot * pot) % n;
            }
            return wyn;
        }

    }
}
