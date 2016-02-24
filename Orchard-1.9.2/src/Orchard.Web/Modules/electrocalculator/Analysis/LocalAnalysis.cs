using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using electrocalculator.Models;
namespace electrocalculator.Analysis
{
    public class LocalAnalysis
    {
        CreateLinearEquations leq;
        public int Node { get; set; }
        public resistor Resistor { get; set; }
        public capacitor Capacitor { get; set; }
        public inductor Inductor { get; set; }

        public void AnalysisTheCircute()
        {
            leq = new CreateLinearEquations(Node);
            if(Resistor != null)
            {
                double[] resistorvalues = Resistor.ResistorsValue.ToArray();
                resistorvalues = pushArrayRightSide.pushArray(resistorvalues, resistorvalues.Count());
                int[,] resistornodes = nodesArray(Resistor.ResistorsPositiveNodes.ToArray(),
                                                    Resistor.ResistorsNegativeNodes.ToArray(),
                                                    Resistor.ResistorsNegativeNodes.ToArray().Count()
                                                  );
                leq.createArray(resistorvalues, resistornodes, resistorvalues.Count(), 'r');
               
            }
            if (this.Capacitor != null)
            {
                double[] capacitorvalues = Capacitor.CapacitorsValue.ToArray();
                capacitorvalues = pushArrayRightSide.pushArray(capacitorvalues,capacitorvalues.Count());
                int[,] capacitornodes = nodesArray(Capacitor.CapacitorsPositiveNodes.ToArray(),
                                                    Capacitor.CapacitorsNegativeNodes.ToArray(),
                                                    Capacitor.CapacitorsNegativeNodes.ToArray().Count()
                                                   );
                leq.createArray(capacitorvalues,capacitornodes, capacitorvalues.Count(), 'c');
            }

            if (this.Inductor != null)
            {
                double[] inductorvalues = Inductor.IductorsValue.ToArray();
                inductorvalues = pushArrayRightSide.pushArray(inductorvalues,inductorvalues.Count());
                int[,] inductornodes = nodesArray(Inductor.IductorsPositiveNodes.ToArray(),
                                                    Inductor.IductorsNegativeNodes.ToArray(),
                                                    Inductor.IductorsNegativeNodes.ToArray().Count()
                                                   );

                leq.createArray(inductorvalues,inductornodes, inductorvalues.Count(), 'l');

            }
            
        }

        public double[] Answer(int kp,int km)
        {
           double[] result= leq.getAnswer(kp, km);
            return result;
        }

        private int[,] nodesArray(int[] positive, int[] negative,int nn)
        {
           // int n=(nn-1);
            int[,] nodes =new int[nn+1,2];

            for (int s = 1; s <= nn; s++)  // changed from for (int s = 0; s < n; s++)
            {
                for (int ss = 0; ss < 2; ss++)
                {

                    if (ss == 0) nodes[s, ss] = positive[s-1];
                    if (ss == 1) nodes[s, ss] = negative[s-1];


                }
            }

            return nodes;
        }
    }

    public  static class pushArrayRightSide
    {
        public static double[] pushArray(double[] input, int i)
        {
            double[] output = new double[i+1];
            for(int z=1,x=0; z<= i && x < i; z++,x++)
            {
                output[z] = input[x];
            }

            return output;
        }
    }

   
}