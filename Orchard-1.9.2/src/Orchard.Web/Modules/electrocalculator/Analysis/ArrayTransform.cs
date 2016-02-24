using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace electrocalculator.Analysis
{
    public static class ArrayTransform
    {
        public static Servicegauss.Complexdata[] manyToOne(Complex[,] c, int n)
        {
           
            Complex[] mat = new Complex[c.Length];
            int k = 0;
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    mat[k] = new Complex(c[i, j].Real, c[i, j].Imaginary);
                    k++;
                }
            }

            Servicegauss.Complexdata[] comdata = new Servicegauss.Complexdata[mat.Length];

            for (int z = 0; z < mat.Length; z++)
            {
                comdata[z] = new Servicegauss.Complexdata() { Real = (double)mat[z].Real, Imaginary = (double)mat[z].Imaginary };

            }
            return comdata;
        }

        public static Complex[,] oneToMany(Servicegauss.Complexdata[] c, int n)
        {
            Complex[] com = new Complex[c.Length];

            for (int z = 0; z < c.Length; z++)
            {
                com[z] = new Complex(c[z].Real, c[z].Imaginary);

            }

            Complex[,] mat = new Complex[n + 1, n + 1];
            int k = 0;
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    mat[i, j] = new Complex(com[k].Real, com[k].Imaginary);
                    k++;
                }
            }
            return mat;
        }

    }

    public class Complexdata
    {

        public double Real
        {
            get;
            set;
        }

        public double Imaginary
        {
            get;
            set;
        }
    }
}