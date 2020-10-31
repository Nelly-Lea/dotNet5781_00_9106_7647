using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_9106_7647
{
    class Program
    {
        class BUS
        {
            string numberbus;
            int day, month, year;
            int km;
            int gasoline;


        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a number");


            int caseSwitch = (int.Parse(Console.ReadLine()));
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                    break;

            }
            Console.ReadKey();
        }
    }
}

