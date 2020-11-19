using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_9106_7647
{
   
    class Program
    {
       static  public bool verifBusLineNumber(Collectionbusline Cbl, int val)//verifies if the bus line exists
        {
            try
            {
                if (Cbl.collec.Exists((x => x.buslinenumber == val)))
                    return true;
                else
                {
                    throw new System.ArgumentException("The bus doesn't exists");
                    return false;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }


        }
        static public void verifchoice(int choice)//verify if the choice exists
        {
            try
            {
                if ((choice != 1) && (choice != 2))
                {
                    throw new System.ArgumentOutOfRangeException("the choice is not good");
                }
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static public void VerifStationExist(Collectionbusline Cbl,int stationk)//verify if the station exists or is contained in bus line
        {
           
                if ((stationk < 0) || (stationk > 39))
                {
                    throw new System.ArgumentOutOfRangeException("the station doesn't exists");
               
                }
                bool flag = false;
                foreach (var item in Cbl.collec)
                {

                    for (int i = 0; i < item.stations.Count(); i++)
                    {

                        if ((item.stations[i].busstationkey == stationk))
                        {
                            flag = true;
                            

                        }

                    }
                }
            if (!flag)
            {
                throw new System.ArgumentOutOfRangeException("there isn't bus line that contains this station");
                
            }
           
            
        }
           
        static public void verifthereis2busline(Collectionbusline CBL,int stationk)// verify if there are already 2 buses with the same line number 
        {
            int count = 0;
            foreach (var item in CBL.collec)
            {
                if (item.buslinenumber == stationk)
                    count++;
                
            }
            if(count==2)
                throw new System.ArgumentOutOfRangeException("there is already 2 buses with this number line");
        }
        
        static void Main(string[] args)
        {
            Random random = new Random();
            List<busstation> BS = new List<busstation>();
            for (int i = 0; i < 40; i++)
            {
                BS.Add(new busstation(i,random));//List bus stations
            }
           
            busstationline bsl1 = new busstationline(BS[0].busstationkey,random);
            busstationline bsl2 = new busstationline(BS[30].busstationkey, random);
            busstationline bsl3 = new busstationline(BS[2].busstationkey, random);
            busstationline bsl4 = new busstationline(BS[15].busstationkey, random);
            busstationline bsl5 = new busstationline(BS[27].busstationkey, random);
            busstationline bsl6 = new busstationline(BS[18].busstationkey, random);
            busstationline bsl7 = new busstationline(BS[34].busstationkey, random);
            busstationline bsl8 = new busstationline(BS[17].busstationkey, random);
            busstationline bsl9 = new busstationline(BS[39].busstationkey, random);
            busstationline bsl10 = new busstationline(BS[1].busstationkey, random);
            busstationline bsl11 = new busstationline(BS[9].busstationkey, random);

            busstationline bsl12 = new busstationline(BS[0].busstationkey, random);
           
            busstationline bsl13 = new busstationline(BS[16].busstationkey, random);
            busstationline bsl14 = new busstationline(BS[23].busstationkey, random);
            busstationline bsl15 = new busstationline(BS[32].busstationkey, random);
            busstationline bsl16 = new busstationline(BS[31].busstationkey, random);
            busstationline bsl17 = new busstationline(BS[5].busstationkey, random);
            busstationline bsl18 = new busstationline(BS[13].busstationkey, random);
            busstationline bsl19 = new busstationline(BS[22].busstationkey, random);
            busstationline bsl20 = new busstationline(BS[3].busstationkey, random);
            busstationline bsl21 = new busstationline(BS[4].busstationkey, random);

            busline BL1 = new busline(50, BS[0], BS[2], random);
            BL1.addstation(ref bsl1);
            BL1.addstation(ref bsl2);
            BL1.addstation(ref bsl3);

            busline BL2 = new busline(51, BS[15], BS[18], random);
            BL2.addstation(ref bsl4); BL2.addstation(ref bsl10); BL2.addstation(ref bsl1); BL2.addstation(ref bsl5); BL2.addstation(ref bsl6);

            busline BL3 = new busline(52, BS[34], BS[39], random);


            BL3.addstation(ref bsl7); BL3.addstation(ref bsl8); BL3.addstation(ref bsl11); BL3.addstation(ref bsl9);

            busline BL4 = new busline(53, BS[9], BS[7], random);
            BL4.addstation(ref bsl9); BL4.addstation(ref bsl8); BL3.addstation(ref bsl11); BL3.addstation(ref bsl7);
            busline BL5 = new busline(54, BS[31], BS[4], random);
            BL5.addstation(ref bsl16);
            BL5.addstation(ref bsl21);


            busline BL6 = new busline(55, BS[23], BS[32], random);
            BL6.addstation(ref bsl14);
            BL6.addstation(ref bsl20);
            BL6.addstation(ref bsl15);

            busline BL7 = new busline(56, BS[9], BS[7], random);
            BL7.addstation(ref bsl14);
            BL7.addstation(ref bsl15);
            busline BL8 = new busline(57, BS[18], BS[31], random);
            BL8.addstation(ref bsl6);
            BL8.addstation(ref bsl17);
            BL8.addstation(ref bsl16);

            busline BL9 = new busline(58, BS[5], BS[3], random);
            BL9.addstation(ref bsl17);
            BL9.addstation(ref bsl20);

            busline BL10 = new busline(59, BS[27], BS[32], random);
            BL10.addstation(ref bsl5);
            BL10.addstation(ref bsl15);


            Collectionbusline CBL = new Collectionbusline();  //List of buses line
            CBL.collec.Add(BL1); CBL.collec.Add(BL2); CBL.collec.Add(BL3); CBL.addbusline(BL4) ; 
            CBL.collec.Add(BL5); CBL.collec.Add(BL6); CBL.collec.Add(BL7); CBL.addbusline(BL8);
            CBL.collec.Add(BL9); CBL.collec.Add(BL10); 
            Console.WriteLine("Please enter a choice");
            Console.WriteLine("tape 1 to add bus line/station");
            Console.WriteLine("tape 2 to remove bus line/station");
            Console.WriteLine("tape 3 to search");
            Console.WriteLine("tape 4 to print");
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
                        Console.WriteLine("Tape 1 if you want to add a new bus line");
                        Console.WriteLine("Tape 2 if you want to add a new station to a bus line");
                        int choice;
                        choice = (int.Parse(Console.ReadLine()));
                        verifchoice(choice);
                        if (choice == 1)
                        {
                            Console.WriteLine("Enter a bus key");
                            int num = (int.Parse(Console.ReadLine()));
                            try
                            {
                                verifthereis2busline(CBL, num);

                                busline b = new busline(num, random);
                                CBL.addbusline(b);
                            }
                            catch(ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        if (choice == 2)
                        {
                            Console.WriteLine("In which line would you want to add a new station ? ");
                            int kav = (int.Parse(Console.ReadLine()));

                            if (verifBusLineNumber(CBL, kav))

                            {

                                foreach (var item in CBL.collec)
                                {
                                    if (item.buslinenumber == kav)
                                    {
                                        Console.WriteLine("Enter a bus station key between 0 and 39");
                                        int key = (int.Parse(Console.ReadLine()));
                                        try
                                        {

                                            for (int i = 0; i < item.stations.Count(); i++)
                                            {
                                                if ((item.stations[i].busstationkey == key)||(key<0) ||(key>39))
                                                    {
                                                        throw new System.ArgumentException("the station doesn't exists or this bus line already contains this station");
                                                    }
                                                }

                                            item.addstation(key, random, BS);
                                        }
                                        catch (ArgumentException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                    }
                                    

                                }
                            }


                        }

                        break;

                    case 2:
              
                        Console.WriteLine("Tape 1 to remove a bus line");
                        Console.WriteLine("Tape 2 to remove a station from bus line");
                        int choice2 = (int.Parse(Console.ReadLine()));
                        verifchoice(choice2);
                        if (choice2 == 1)
                        {
                            Console.WriteLine("Enter a bus key that you want to remove");
                            int kav2 = (int.Parse(Console.ReadLine()));
                            if (verifBusLineNumber(CBL, kav2))

                            {

                                foreach (var item in CBL.collec)
                                {
                                    if (item.buslinenumber == kav2)
                                    {
                                        CBL.removebusline(item);
                                        break;
                                    }
                                }

                            }

                        }
                        if (choice2 == 2)
                        {
                            Console.WriteLine("Enter the bus line that you want to remove a station from it");
                            int kav2 = (int.Parse(Console.ReadLine()));
                            if (verifBusLineNumber(CBL, kav2))

                            {
                                Console.WriteLine("Enter the station key that you want to remove");
                                int stationk = (int.Parse(Console.ReadLine()));

                                foreach (var item in CBL.collec)
                                {
                                    if (item.buslinenumber == kav2)
                                    {
                                        try
                                        {
                                            int index = item.stations.FindIndex(x => x.busstationkey == stationk);
                                            if(index==-1)
                                                throw new System.ArgumentOutOfRangeException("the station doesn't exists or this bus line already contains this station");
                                            item.substation(item.stations[index]);
                                        }
                                        catch(ArgumentOutOfRangeException e)
                                        {
                                            Console.WriteLine(e.Message);

                                        }
                                    }

                                }
                            }
                        }
                       
                        break;
                   case 3:
                        Console.WriteLine("Tape 1 to know which buses stop at a station");
                        Console.WriteLine("Tape 2 to print all possibilities travel between 2 stations");
                        int choice3 = (int.Parse(Console.ReadLine()));
                        verifchoice(choice3);
                        if (choice3 == 1)
                        {
                            try
                            {
                                Console.WriteLine("Enter the bus station key");
                                int stationk = (int.Parse(Console.ReadLine()));
                                VerifStationExist(CBL, stationk);
                                List<busline> lb = new List<busline>();
                                lb = CBL.BusThatStopInThisStation(stationk);
                                foreach (var item in lb)
                                {
                                    item.printbusline();
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                               
                            }

                        }
                        if (choice3 == 2)
                        {
                            try
                            {
                                Console.WriteLine("Enter the first station key");
                                int stationk1 = (int.Parse(Console.ReadLine()));
                                VerifStationExist(CBL, stationk1);
                                Console.WriteLine("Enter the second station key");
                                int stationk2 = (int.Parse(Console.ReadLine()));
                                VerifStationExist(CBL, stationk2);
                                busstation b1 = new busstation(stationk1);
                                busstation b2 = new busstation(stationk2);

                                Collectionbusline c = new Collectionbusline();
                                c = CBL;
                                c.SortTimeBus(b1, b2);

                                Collectionbusline c1 = new Collectionbusline();
                                c1.collec = c.listwithsubrout(b1, b2, random);//this list help us to print just one time stations
                                foreach (var item in c1.collec)
                                {
                                    if (item.stations.Count() != 0)
                                        item.printbusline();
                                    for (int i = 0; i < item.stations.Count(); i++)
                                        Console.WriteLine(item.stations[i].ToString());
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                                
                            }
                        } 
                        
              
                        break;
                    case 4:
                        Console.WriteLine("tape 1 if you want to print all the bus lines");
                        Console.WriteLine("tape 2 if you want to print all the stations");
                        int choice4 = (int.Parse(Console.ReadLine()));
                        verifchoice(choice4);
                        if (choice4 == 1)
                        {
                            foreach (var item in CBL.collec)
                            {
                                if (item.firststation == null || item.laststation == null)
                                    item.printbusline();
                                else
                                   Console.WriteLine(item.ToString());
                            }

                        }
                        if (choice4 == 2)
                        {

                            List<busline> lb = new List<busline>();
                            List<int> listInt = new List<int>();
                            foreach (var item in CBL.collec)
                            {
                                for (int i = 0; i < item.stations.Count(); i++)
                                {
                                    lb = CBL.BusThatStopInThisStation(item.stations[i].busstationkey);

                                    if (!listInt.Exists(x => x == item.stations[i].busstationkey))
                                    {
                                        Console.WriteLine("station: {0}", item.stations[i].busstationkey);
                                        for (int j = 0; j < lb.Count(); j++)
                                        {
                                            Console.WriteLine(lb[j].ToString(), "\n\r");

                                        }
                                        Console.WriteLine("\r ");

                                    }
                                    listInt.Add(item.stations[i].busstationkey);
                                }

                            }
                        }
                        break;
                    default:
                        Console.WriteLine("ERROR please enter a new number");
                        break;

                }

                Console.WriteLine(" Please enter a choice to restart or / if you want to finish enter 5");
                caseSwitch = Console.ReadLine();
                success = int.TryParse(caseSwitch, out number);

            } while (success && (number != 5));
        }
    }
}
