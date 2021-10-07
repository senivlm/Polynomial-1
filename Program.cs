using System;

namespace Polynomial
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial pol_1 = new Polynomial(4);
            Polynomial pol_2 = new Polynomial(5);
            Polynomial rez = pol_1.Add(pol_2);
            Console.WriteLine(pol_1.ToString());
            Console.WriteLine(pol_2.ToString());
            Console.WriteLine("\n{0}", rez.ToString());
            rez = pol_1.Mult(pol_2);
            Console.WriteLine("\n{0}", rez.ToString());
            rez = pol_1.Subtract(pol_2);
            Console.WriteLine("\n{0}", rez.ToString());
        }
    }
}
