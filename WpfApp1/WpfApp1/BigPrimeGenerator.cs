using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class BigPrimeGenerator
    {
        public BigInteger getBigNumber(int lenght)
        {
            byte[] number = new byte[lenght];
            Random rnd = new Random();
            rnd.NextBytes(number);
            BigInteger a = new BigInteger(number);
            return a;
        }

        public BigInteger random(BigInteger start, BigInteger finish)
        {
            byte[] helper = finish.getBytes();
            Random rnd = new Random();
            int lenght = rnd.Next(1, helper.Length);
            byte[] helper2 = new byte[lenght];
            rnd.NextBytes(helper2);
            BigInteger a = new BigInteger(helper2);
            if (a > finish || a < start) {
                return -1;
            }
            return a;
        }

        public BigInteger getBigPrimeNumber(int lenght)
        {
            BigInteger p = 1, d, s, a, x;
            int j;
            bool t = true;
            int flag = 0;
            while (t == false || flag == 0)
            {
                t = true;
                flag = 1;
                p = getBigNumber(lenght);
                while (p % 2 == 0) { p = getBigNumber(lenght); }
                d = p - 1;
                s = 0;
                while (d % 2 == 0)
                {
                    s += 1;
                    d /= 2;
                }
                for (int i = 0; i < 20; i++)
                {
                    a = random(2, p - 2);
                    while (a == -1) a = random(2, p - 2);
                    x = powMod(a, d, p);
                    if ((x == 1) || (x == p - 1)) continue;
                    for (j = 1; (j < s) && (x != p - 1); j++)
                    {
                        x = multiplyMod(x, x, p);
                        if (x == 1)
                        {
                            t = false; break;
                        }
                    }
                    if (!t) break;
                    if (x != (p - 1))
                    {
                        t = false; break;
                    }
                }
            }
            return p;
        }

        public BigInteger multiplyMod(BigInteger a, BigInteger b, BigInteger n)
        {
            BigInteger m, w;

            w = 0;
            for (m = 1; m != 0; m <<= 1)
            {
                if ((b & m) != 0) w = (w + a) % n;
                a = (a << 1) % n;
            }
            return w;
        }

        public BigInteger powMod(BigInteger a, BigInteger e, BigInteger n)
        {
            BigInteger m, p, w;

            p = a; w = 1;
            for (m = 1; m != 0; m <<= 1)
            {
                if ((e & m) != 0) w = multiplyMod(w, p, n);
                p = multiplyMod(p, p, n);
            }
            return w;
        }
    }
}
