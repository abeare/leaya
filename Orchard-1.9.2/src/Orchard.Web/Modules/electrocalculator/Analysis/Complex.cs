using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace electrocalculator.Analysis
{
    public class Complex
    {

            double real;
            double imaginary;

            public Complex(double real, double imaginary)
            {
                this.real = real;
                this.imaginary = imaginary;
            }



            public double Real
            {
                get
                {
                    return (real);
                }
                set
                {
                    real = value;
                }
            }

            public double Imaginary
            {
                get
                {
                    return (imaginary);
                }
                set
                {
                    imaginary = value;
                }
            }

            //public override string ToString()
            //{
            //    return(String.Format("({0}, {1}i)", real, imaginary));
            //}

            public static bool operator ==(Complex c1, Complex c2)
            {
                if ((c1.real == c2.real) &&
                (c1.imaginary == c2.imaginary))
                    return (true);
                else
                    return (false);
            }

            public static bool operator !=(Complex c1, Complex c2)
            {
                return (!(c1 == c2));
            }

            public override bool Equals(object o2)
            {
                Complex c2 = (Complex)o2;

                return (this == c2);
            }

            public override int GetHashCode()
            {
                return (real.GetHashCode() ^ imaginary.GetHashCode());
            }



            public static Complex operator +(Complex c1, Complex c2)
            {
                return (new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary));
            }

            public static Complex operator -(Complex c1, Complex c2)
            {
                return (new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary));
            }

            // product of two complex numbers
            public static Complex operator *(Complex c1, Complex c2)
            {
                return (new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary,
                c1.real * c2.imaginary + c2.real * c1.imaginary));
            }

            // quotient of two complex numbers
            public static Complex operator /(Complex c1, Complex c2)
            {
                if ((c2.real == 0.0) &&
                (c2.imaginary == 0.0))
                    throw new DivideByZeroException("Can't divide by zero Complex number");

                double newReal =
                (c1.real * c2.real + c1.imaginary * c2.imaginary) /
                (c2.real * c2.real + c2.imaginary * c2.imaginary);
                double newImaginary =
                (c2.real * c1.imaginary - c1.real * c2.imaginary) /
                (c2.real * c2.real + c2.imaginary * c2.imaginary);

                return (new Complex(newReal, newImaginary));
            }



            public static Complex operator /(double g, Complex c1)
            {
                if ((c1.real == 0.0) &&
                (c1.imaginary == 0.0))
                    throw new DivideByZeroException("Can't divide by zero Complex number");

                double newReal = c1.real * c1.real + c1.imaginary * c1.imaginary;
                return (new Complex(g * c1.real / newReal, -g * c1.imaginary / newReal));
            }


            public static Complex operator *(double g, Complex c1)
            {
                return (new Complex(c1.real * g, g * c1.imaginary));
            }

            public static Complex operator *(Complex c1, double m)
            {
                return (new Complex(c1.real * m, m * c1.imaginary));
            }

            //public static Complex operator /(double n, double m)
            //{
            //    return (new Complex(n/ m,0.0));
            //}

            public static double abs(Complex c1)
            {
                return Math.Sqrt(c1.real * c1.real + c1.imaginary * c1.imaginary);
            }

            public static double arg(Complex z)
            { return Math.Atan2(z.Imaginary, z.Real); }

        }
    
}