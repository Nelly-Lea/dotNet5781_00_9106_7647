using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_9106_7647
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9106();
            Welcome7647();
            Console.ReadKey();
           



        }
        static partial void Welcome7647();
        private static void Welcome9106()
        {
            Console.Write("Enter your name: ");
            string name = System.Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }

}
