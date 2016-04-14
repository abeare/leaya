using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using electrocalculator.Analysis;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace electrocalculator.Analysis
{
    public class CreateLinearEquations
    {
        Complex[,] w;
        Complex s = new Complex(0, 2 * 3.141593 * 0.16);
        Complex ss = new Complex(1, 0);
        double frq = 2 * 3.141593 * 1; // w=2pif   
        int _nodeInCome;

        public CreateLinearEquations(int node)
        {
            _nodeInCome = node;

            w = new Complex[node + 1, node + 1];

            for (int j = 0; j <= node; j++)  // changed form  for (int j = 0; j <= node; j++)
            {

                for (int i = 0; i <= node; i++) // changed form  int i = 0
                {

                    w[j, i] = new Complex(0, 0);
                }

            }
        }
        public void createArray(double[] val, int[,] Nodes, int nodee, char ch)
        {
            //sozdat massiv
            nodee = nodee - 1;
            int ii, jj, g;
            for (int kd = 1; kd <= nodee; kd++)
                for (int l = 0; l <= 1; l++)
                {
                    ii = Nodes[kd, l];
                    if (ii == 0) continue;
                    for (int m = 0; m <= 1; m++)
                    {
                        jj = Nodes[kd, m];
                        if (jj == 0) continue;
                        g = (1 - 2 * l) * (1 - 2 * m);
                        switch (ch)
                        {
                            case 'r': w[ii, jj] = w[ii, jj] + g / (ss * val[kd]); break;
                            case 'c': w[ii, jj] = w[ii, jj] + g * s * val[kd]; break;
                            case 'l': w[ii, jj] = w[ii, jj] + g / (s * val[kd]); break;
                            default: throw new Exception("no way");
                        }


                    }

                }

            
         
        }
        public double[] getAnswer(int kpInput,int kmInput)
        {
            form_s();

            //localanlysis
            Servicegauss.Complexdata[] ccn = ArrayTransform.manyToOne(w, _nodeInCome);
            w = ArrayTransform.oneToMany(ccn, _nodeInCome);
            GaussMethod.gauss_c(w, _nodeInCome);

            int km = kmInput;
            int kp = kpInput;
            double kum, kua;
            Complex ku, ri;
            ri = w[0, 1] - w[0, 0];
            ku = (w[0, kp] - w[0, km]) / ri;
            kum = Complex.abs(ku);
            kua = Complex.arg(ku) * (180 / 3.1416);




            double[] result = new double[2];
            result[0] = kum;
            result[1] = kua;
            return result;

        }
        protected void form_s()
        {
            int lp = 1;
            int lm = 0;
            Complex complex = new Complex(0, 0);
            for (int i = 1; i < _nodeInCome; i++)
                w[i, 0] = complex;
            if (lp != 0)
                w[lp, 0] = new Complex(-1, 0);
            if (lm != 0)
                w[lm, 0] = new Complex(1, 0);
        }
    }

  
}