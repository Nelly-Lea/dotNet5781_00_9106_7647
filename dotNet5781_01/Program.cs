using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

// Chirly Sfez 342687647
//Nelly Lea Amar 341289106

namespace dotNet5781_01_9106_7647
{
    class Program
    {
        class BUS
        {
            private
                string numberbus;
            public string Num { get => numberbus;   // line number
                set => numberbus = value;
            }
       
            DateTime Datebegin;
           
            public DateTime Dates { get=> Datebegin; set=>Datebegin=value; }  // date of first activity

            DateTime Datetreatment;
            public DateTime Datetr { get=>Datetreatment; set=>Datetreatment=value; } //date since the last treatment
            int km;  // km depuis le tipoul
            public int Km { get => km; set => km = value; }      // number of kilometers since the last treatment
            int kmbegining;
            public int KmBegin { get=>kmbegining; set=>kmbegining=value; } // number of kilometers since the first activity
            int gasoline = 1200;
            public int Gasoline {             //gasoline rate
                get => gasoline;
                set => gasoline = value;
            }
            public
                BUS(string busnumber, DateTime date)   // BUS constructor
            {
                numberbus = busnumber;
                 Datebegin = date;
                Datetreatment = date;

            }
 
        }

        public static DateTime Setdate(int d,int m,int y)      // this function is to verify that the date is valid
        {
            while ((d < 0 || d > 31) || (m < 0 || m > 12) || y < 0 || ((m == 2) && (d > 28)))
            {
                Console.WriteLine("The date isn't valid");
                Console.WriteLine("Please enter a valid date");
                d = int.Parse(Console.ReadLine());
                m = int.Parse(Console.ReadLine());
                y = int.Parse(Console.ReadLine());

            }
            DateTime da = new DateTime(y, m, d);
            return da;
        }
        static  void Travel(string busnumber1, int Size, ref List<BUS> Listb)  //This function verifies that we can travel 
        {
            bool result=false;
            for (int i = 0; i < Size; i++)
            {
               
                result = busnumber1.Equals(Listb[i].Num);
                if (result)
                {
                    Random r = new Random();
                    int x = r.Next(3000);
                    Console.WriteLine("The number of km that you have to travel is {0}", x);
                    if (x <= Listb[i].Gasoline)
                    {
                        DateTime today = DateTime.Now;
                       
                        if ((Listb[i].Km < 20000)|| (today - Listb[i].Datetr).Days<365)
                        {
                            Console.WriteLine("its good you can travel!");
                            Listb[i].Gasoline = Listb[i].Gasoline - x;
                            Listb[i].KmBegin += x;
                            
                        }
                        else
                            Console.WriteLine("You can't travel you have to do treatment");

                    }
                    else
                        Console.WriteLine("There isn't enough gasoline");
                    break;
                }

            }
            if (!result)
            {
                Console.WriteLine("The line doesn't exist");
            }
        }
        static void Gasolortreat(ref List<BUS> listb, ref int size, string busnum) // This function verifies if we can put gasoline or if we can do a car treatment
        {
            for (int i = 0; i < size; i++)
            {
                bool result = false;
                result = busnum.Equals(listb[i].Num);
                if (result)
                {
                    Console.WriteLine("tape 1 for gasoline");
                    Console.WriteLine("tape 2 for treatment");
                    int c = int.Parse(Console.ReadLine());

                    if (c == 1)
                    {
                        if(listb[i].Gasoline!=1200)
                            listb[i].Gasoline += (1200 - listb[i].Gasoline);
                        else
                            Console.WriteLine("You have enough gasoline");
                        break;
                    }
                    if (c == 2)
                    {

                        DateTime currentDate = DateTime.Now;
                        if ((currentDate - listb[i].Datetr).Days > 365)
                        {
                            listb[i].Datetr = currentDate;
                            listb[i].Km = 0;
                        }
                        else
                            Console.WriteLine("You don't need a treatment");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You choice doesn't exist");
                        break;
                    }
                }
                else
                    Console.WriteLine("The line doesn't exist");
            }
        }
        static bool verifbusnumber(string n, DateTime d) // This function verifies if the line number is valid 
        {
            int sum=0;
            for (int j = 0; j < n.Length; j++)
            {
                for (int i = 48; i < 58; i++)
                {
                    if (n[j] == i)
                        sum++;
                }
            }
            if (d.Year < 2018)
            {
                if ((n[2] == '-') && (n[6] == '-') && (sum == 7))
                            return true;
                 return false;
              }
            if ((n[3] == '-') && (n[6] == '-') && (sum == 8))
                return true;
            return false;
        }

            static void Main(string[] args)
        {
            
           
             List<BUS> listb = new List<BUS>();
            int size = 0;
            Console.WriteLine("Please enter a number");
            Console.WriteLine("1 to add a bus line");
            Console.WriteLine("2 to travel");
            Console.WriteLine("3 to get some fuel or to do car treatment");
            Console.WriteLine("4 to show all the complete bus line");
            Console.WriteLine("5 to exit");
            string caseSwitch = " ";
            caseSwitch = Console.ReadLine();
            bool success;
            int number;
            do
            {
               
                success = (int.TryParse(caseSwitch, out number));


                switch (number)
                {
                    case 1:
                        Console.WriteLine("Enter the bus line");

                        string busnumber = Console.ReadLine();
                        Console.WriteLine("Enter the day of begining of activity");
                        int d = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the month");
                        int m = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the year");
                        int y = int.Parse(Console.ReadLine());
                       
                        DateTime date = Setdate(d, m, y);
                        while (verifbusnumber(busnumber, date) == false)
                        {
                            Console.WriteLine("The number of the bus is not valid");
                            Console.WriteLine("Please enter a new bus number" );
                            busnumber = Console.ReadLine();
                        }
                        BUS bus = new BUS(busnumber, date);
                       
                        listb.Add(bus);
                        size++;
                        break;
                    case 2:
                        
                        
                        Console.WriteLine("Enter the number of the line");
                        string busnumber1  = Console.ReadLine();
                        Travel(busnumber1, size, ref listb);
                        

                        break;
                    case 3:
                        Console.WriteLine("Enter the number of the line");
                        string busnumber2 = Console.ReadLine();
                        Gasolortreat(ref listb, ref size, busnumber2);

                       
                        break;
                    case 4:
                        for(int i=0;i<size;i++)
                        {
                            Console.WriteLine("line bus is {0}", listb[i].Num);
                            
                            Console.WriteLine("number of kilometers since the start of the activty is {0}", listb[i].KmBegin);
                            
                        }
                        break;
                    default:
                        Console.WriteLine("this case doesn't exist");
                        break;
                     

                }
                
                Console.WriteLine(" Please enter a choice to restart or / if you want to finish enter 5");
                caseSwitch=Console.ReadLine();
                success = int.TryParse(caseSwitch, out number);

            } while (success&&(number !=5));
            Console.WriteLine("FINISH");
            Console.ReadKey();
        }
    }
}

