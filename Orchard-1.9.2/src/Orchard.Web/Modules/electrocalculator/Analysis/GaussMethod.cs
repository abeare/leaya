using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace electrocalculator.Analysis
{
    public static class GaussMethod
    {
        public static void gauss_c(Complex[,] w, int n)
        {
           
            n = n - 1;
            Complex c, d, t;
            Complex cn = new Complex(0, 0);
            int i, j, k, l;
            //Прямой ход
            for (k = 1; k < n; k++)
            {  //Перестановка строк по максимальному значению
                l = k;
                for (i = k + 1; i <= n; i++)
                    if (Complex.abs(w[i, k]) > Complex.abs(w[l, k]))
                        l = i;
                if (l != k)
                    for (j = 0; j <= n; j++)
                        if (j == 0 || j >= k)
                        {
                            t = w[k, j];
                            w[k, j] = w[l, j];
                            w[l, j] = t;
                        }  //Конец перестановки строк
                int kk = k;
                d = 1 / w[k, k];
                for (i = k + 1; i <= n; i++)
                {
                    if (w[i, k] == cn) continue;
                    c = w[i, k] * d;
                    for (j = k + 1; j <= n; j++)
                        if (w[k, j] != cn) w[i, j] -= c * w[k, j];
                    if (w[k, 0] != cn) w[i, 0] -= c * w[k, 0];
                }
            }
            //Обратный ход
            w[0, n] = -1 * (w[n, 0] / w[n, n]);//?????-1*(
            for (i = n - 1; i >= 1; i--)
            {
                t = w[i, 0];
                for (j = i + 1; j <= n; j++)
                    t += w[i, j] * w[0, j];
                w[0, i] = -1 * (t / w[i, i]);//????
            }


        }

    }
}