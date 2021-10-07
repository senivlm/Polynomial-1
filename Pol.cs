using System;
using System.Collections.Generic;
using System.Text;

namespace Polynomial
{
    class Pol
    {
        double number;
        int degree;

        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }


        public int Degree
        {
            get
            {
                return degree;
            }
            set
            {
                degree = value;
            }
        }

        public Pol(double number, int degree)
        {
            this.number = number;
            this.degree = degree;
        }

        public Pol(Pol pol)
        {
            number = pol.number;
            degree = pol.degree;
        }

        public override string ToString()
        {
            string str = "";
            if (degree == 0)
                str += number;
            else
                str += number + "*x^" + degree;
            return str;
        }

        public static Pol operator + (Pol first, Pol second)
        {
            Pol rez = new Pol(0, 0);
            if (first.degree == second.degree)
            {
                rez.number = first.number + second.number;
                rez.degree = first.degree;
            }
            return rez;
        }

        public static Pol operator -(Pol first, Pol second)
        {
            Pol rez = new Pol(0, 0);
            if (first.degree == second.degree)
            {
                rez.number = first.number - second.number;
                rez.degree = first.degree;
            }
            return rez;
        }

        public static Pol operator *(Pol first, Pol second)
        {
            Pol rez = new Pol(0, 0);
            rez.number = first.number * second.number;
            rez.degree = first.degree + second.degree;

            return rez;
        }

        public static Pol operator *(Pol first, int num)
        {
            Pol rez = new Pol(0, 0);
            rez.number = first.number * num;
            rez.degree = first.degree;

            return rez;
        }
    }
}
