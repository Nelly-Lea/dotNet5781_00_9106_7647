using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Device.Location;
using DO;

namespace DS
{

    public static class DataSource
    {
        public static List<Station> ListStations;
        public static List<Line> ListLines;
        public static List<Bus> ListBuses;
        public static int id = 0;
        public static List<User> ListUsers;
        public static List<AdjacentStations> ListAdjacentStations;
        public static int idAdjStation = 0;
        public static List<LineStation> ListLineStations;
        public static int idLineStation = 0;
        public static int idlinetrip = 0;
        public static List<LineTrip> ListLineTrip;
        public static List<Trip> ListTrip;
        public static int idtrip = 0;
       
        static DataSource()
        {
            InitAllLists();
           
        }
       
        static void InitAllLists()
        {
            ListStations = new List<Station>
            {
                new Station //1
                {
                    Code= 38831,
                    Name="Bar Lev School/ Ben Yehouda",
                    Latitude= 32.183921,
                    Longitude= 34.917806,
                    Address="76 Ben Yehouda Street, Kfar Sabba",
                    Area=Areas.KfarSabba,


                },
                  new Station //2
                {
                    Code= 38832,
                    Name="Herzel/ Tsomet Gilo",
                    Latitude= 31.870034,
                    Longitude=34.819541 ,
                    Address="Herzel Street, Kyriat Ekron",
                    Area=Areas.KyriatEkron,


                },
                    new Station//3
                {
                    Code= 38833,
                    Name="Anehshol/ Adayaguim",
                    Latitude= 31.984553,
                    Longitude= 34.782828,
                    Address="30 Anehshol Street, Rishon Letsion",
                    Area=Areas.RishonLetsion,


                },
                      new Station//4
                {
                    Code=38834 ,
                    Name="Frid/ Chechet Ayamim",
                    Latitude= 31.88855,
                    Longitude= 34.790904,
                    Address="9 Moshe Frid Street, Rehovot",
                     Area=Areas.Rehovot,


                },

                      new Station//5
                {
                    Code=38836 ,
                    Name="Tahanat merkazite Lod/Orada",
                    Latitude=31.956392 ,
                    Longitude=34.898098 ,
                    Address="Lod",
                     Area=Areas.Lod,


                },
                        new Station//6
                {
                    Code=38837 ,
                    Name="Hanna Avreh/ Volkni",
                    Latitude= 31.892166,
                    Longitude=34.796071 ,
                    Address="9 Hanna Avreh Street, Rehovot",
                     Area=Areas.Rehovot,


                },
                          new Station//7
                {
                    Code= 38838,
                    Name="Herzel/ Moshe Sharet",
                    Latitude= 31.857565,
                    Longitude=34.824106 ,
                    Address="20 Herzel Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,


                },
                            new Station//8
                {
                    Code=38839 ,
                    Name="Abanim/Elie Cohen",
                    Latitude=31.862305 ,
                    Longitude=34.821857 ,
                    Address="4 Abanim Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                              new Station//9
                {
                    Code= 38840,
                    Name="Weizman/Abanim",
                    Latitude=31.865085 ,
                    Longitude= 34.822237,
                    Address="11 Weizman Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                     new Station//10
                {
                    Code= 38841,
                    Name="Ahirouss/Akalanite",
                    Latitude=31.865222 ,
                    Longitude=34.818957 ,
                    Address="13 Ahirouss Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                new Station//11
                {
                    Code= 38842,
                    Name="Akalanite/Anarkiss",
                    Latitude=31.867597 ,
                    Longitude= 34.818392,
                    Address="Akalanite Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                      new Station//12
                {
                    Code= 38844,
                    Name="Elie Cohen/ Lohamey Agatahout",
                    Latitude= 31.86244,
                    Longitude= 34.827023,
                    Address="62 Elie Cohen Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                            new Station//13
                {
                    Code= 38845,
                    Name="Chivzey/ Chevet Ahim",
                    Latitude= 31.863501,
                    Longitude= 34.828702,
                    Address="51 Chivzey Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                     new Station//14
                {
                    Code= 38846,
                    Name="Chivzey/Weizman",
                    Latitude= 31.865348,
                    Longitude= 34.827102,
                    Address="31 Chivzey Street, Kyriat Ekron",
                     Area=Areas.KyriatEkron,

                },
                  new Station//15
                {
                    Code=38847 ,
                    Name="Haim Bar Lev/ Sderot Itshak Rabin",
                    Latitude= 31.977409,
                    Longitude= 34.763896,
                    Address="Hain Bar Lev Street, Rishon Letsion"
                    ,
                     Area=Areas.RishonLetsion,

                },
                        new Station//16
                {
                    Code= 38848,
                    Name="Merkaz Labriout Anefech Lev Asharon",
                    Latitude=32.300345 ,
                    Longitude=34.912708 ,
                    Address="Tsour Moshe",
                      Area=Areas.TsourMoshe,

                },
                      new Station//17
                {
                    Code=38849 ,
                    Name="Merkaz Labriout Anefech Lev Asharon",
                    Latitude= 32.301347,
                    Longitude=34.912602 ,
                    Address="Tsour Moshe",
                      Area=Areas.TsourMoshe,

                },
                     new Station//18
                {
                    Code=38852,
                    Name="Oltsman/Amada",
                    Latitude= 31.914255,
                    Longitude=34.807944 ,
                    Address="2 Haim Oltsman Street, Rehovot",
                      Area=Areas.Rehovot,

                },
                       new Station//19
                {
                    Code=38854,
                    Name="Mahaney Tserifin/Mohadon",
                    Latitude= 31.963668,
                    Longitude=34.836363 ,
                    Address="Tserifin",
                      Area=Areas.General,

                },
                        new Station//20
                {
                    Code=38855,
                    Name="Herzel/Golani",
                    Latitude= 31.856115,
                    Longitude=34.825249 ,
                    Address="4 Herzel Street, Kyriat Ekron",
                      Area=Areas.KyriatEkron,

                },
                         new Station//21
                {
                    Code=38856,
                    Name="Arotem/Adagniot",
                    Latitude=31.874963 ,
                    Longitude= 34.81249,
                    Address="3 Arotem Street, Rehovot",
                      Area=Areas.Rehovot,

                },
                  new Station//22
                {
                    Code=38859,
                    Name="Aarava",
                    Latitude=32.300035 ,
                    Longitude=34.910842 ,
                    Address="Aarava Street, Tsour Moshe",
                      Area=Areas.TsourMoshe,

                },
                      new Station//23
                {
                    Code=38860,
                    Name="Mavo Aguefen/Mored Ataana",
                    Latitude=32.305234 ,
                    Longitude=34.948647 ,
                    Address="Mavo Aguefen Street, Yanov",
                      Area=Areas.General,

                },
                      new Station//24
                {
                    Code=38861,
                    Name="Mavo Aguefen/ Aarhava",
                    Latitude=32.304022 ,
                    Longitude=34.943393 ,
                    Address="Mavo Aguefen Street, Yanov",
                      Area=Areas.General,

                },
                      new Station//25
                {
                    Code=38862,
                    Name="Aarhava Aleph",
                    Latitude= 32.302957,
                    Longitude=34.940529 ,
                    Address="Aarhava Street, Geoulim",
                      Area=Areas.Gueoulim,

                },
                      new Station//26
                {
                    Code=38863,
                    Name="Aarhava Bet",
                    Latitude=32.300264 ,
                    Longitude=34.939512 ,
                    Address="Aarhava Street, Gueoulim",
                      Area=Areas.Gueoulim,

                },
                      new Station//27
                {
                    Code=38864,
                    Name="Aarhava/Otikim",
                    Latitude= 32.298171,
                    Longitude=34.938705 ,
                    Address="Aarhava Street, Gueoulim",
                      Area=Areas.Gueoulim,

                },
                      new Station//28
                {
                    Code=38865,
                    Name="Reshout Sdot Ateoufat/ Aelia",
                    Latitude= 31.990876,
                    Longitude=34.8976 ,
                    Address="Sderot Aelia Street,Namal Teoufat Ben Gurion",
                      Area=Areas.center

                },
                      new Station//29
                {
                    Code=38866,
                    Name="Kanaf/Brosh",
                    Latitude=31.998767 ,
                    Longitude= 34.879725,
                    Address="Kanaf Street, Namal Teoufat Ben Gurion",
                    Area=Areas.center

                },
                       new Station//30
                {
                    Code=38867,
                    Name="Ahaboura/Dov Oz",
                    Latitude= 31.883019,
                    Longitude= 34.818708,
                    Address="24 Ahaboura Street,Rehovot",
                    Area=Areas.Rehovot

                },
                      new Station//31
                {
                    Code=38869,
                    Name="Bet Alevy He",
                    Latitude=32.349776 ,
                    Longitude=34.926837 ,
                    Address="105, Bet Alevy",
                          Area=Areas.General,


                },
                      new Station//32
                {
                    Code=38870,
                    Name="Arishonim/Kvish 5700",
                    Latitude= 32.352953,
                    Longitude= 34.899465,
                    Address="13 Amigdal Street, Kfar Haim",
                          Area=Areas.General,


                },
                      new Station//33
                {
                    Code=38872,
                    Name="Agaon Ben Ish Hay/Tsaalon",
                    Latitude=31.897286 ,
                    Longitude=34.775083 ,
                    Address="Rehovot",
                          Area=Areas.Rehovot,

                },
                      new Station//34
                {
                    Code=38873,
                    Name="Oukchey/Levi Eshkol",
                    Latitude= 31.883941,
                    Longitude=34.807039 ,
                    Address="4 Israel Oukchey, Rehovot",
                          Area=Areas.Rehovot,

                },
                      new Station//35
                {
                    Code=38875,
                    Name="Menouha Venahala/ Yehouda Gorodiski",
                    Latitude= 31.896762,
                    Longitude=34.816752 ,
                    Address="31 Menouha Venahala Street, Rehovot",
                          Area=Areas.Rehovot,

                },
                      new Station//36
                {
                    Code=38876,
                    Name="Gorodiski/Yehiel Faldi",
                    Latitude= 31.898463,
                    Longitude=34.823461 ,
                    Address="35 Yehouda Gorodiski Street, Rehovot",
                          Area=Areas.Rehovot,

                },
                      new Station//37
                {
                    Code=38877,
                    Name="Dereh Menahem Begin/Yaakov Hazan",
                    Latitude= 32.076535,
                    Longitude=34.904907 ,
                    Address="30 Dereh Menahem Begin, Petah Tikva",
                          Area=Areas.General

                },
                      new Station//38
                {
                    Code=38878,
                    Name="Dereh Apark/ Arav Neria",
                    Latitude= 32.299994,
                    Longitude= 34.878765,
                    Address="20 Dereh Apark Street, Netanya",
                          Area=Areas.north

                },
                      new Station//39
                {
                    Code=38879,
                    Name="Ateena/Aguefen",
                    Latitude=31.865457 ,
                    Longitude=34.859437 ,
                    Address="Ateena Street,Yatsits",
                          Area=Areas.General

                },
                      new Station//40
                {
                    Code=38880,
                    Name="Ateena/Aelon",
                    Latitude=31.866772 ,
                    Longitude=34.864555 ,
                    Address="Ateena Street, Yatsits",
                          Area=Areas.General

                },
                      new Station//41
                {
                    Code=38881,
                    Name="Dereh Aprahim/ Yasmin",
                    Latitude= 31.809325,
                    Longitude=34.784347 ,
                    Address="46 Dereh Aprahim Street,Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//42
                {
                    Code=38883,
                    Name="Itshak Rabin/Pinhas Sapir",
                    Latitude=31.80037 ,
                    Longitude=34.778239 ,
                    Address="Dereh Itshak Rabin Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//43
                {
                    Code=38884,
                    Name="Menahem Begin/ Itshak Rabin",
                    Latitude=31.799224 ,
                    Longitude=34.782985 ,
                    Address="4 Sderot Menahem Begin Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//44
                {
                    Code=38885,
                    Name="Haim Hertsog/Dolev",
                    Latitude=31.800334 ,
                    Longitude=34.785069,
                    Address="12 Haim Hertsog Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//45
                {
                    Code=38886,
                    Name="Bet Sefer Guevanim/Erez",
                    Latitude=31.802319 ,
                    Longitude=34.786735 ,
                    Address="2 Erez Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//46
                {
                    Code=38887,
                    Name="Dereh Ailanot/Elon",
                    Latitude=31.804595 ,
                    Longitude=34.786623 ,
                    Address="13 Dereh Ailanot Street, Guedera",
                          Area=Areas.Guedera,

                },
                          new Station//47
                {
                    Code=38888,
                    Name="Dereh Ailanot/ Menahem Begin",
                    Latitude=31.805041 ,
                    Longitude=34.785098 ,
                    Address="3 Dereh Ailanot Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//48
                {
                    Code=38889,
                    Name="Aatsmaout/Weizman",
                    Latitude=31.816751 ,
                    Longitude=34.782252 ,
                    Address="1 Aatsmaout Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//49
                {
                    Code=38890,
                    Name="Weizman/ Marabed Aksamim",
                    Latitude=31.816579 ,
                    Longitude=34.779753 ,
                    Address="19 Weizman Street, Guedera",
                          Area=Areas.Guedera,

                },
                      new Station//50
                {
                    Code=38891,
                    Name="Tsaala/Elmog",
                    Latitude=31.801182 ,
                    Longitude=34.787199 ,
                    Address="25 Tsaala Street, Guedera",
                          Area=Areas.Guedera,

                },
            };
            ListLines = new List<Line>
            {
                new Line//1
                {
                   // Id=++id,
                   Id=++id,
                    Code=100,
                    Area= Areas.KyriatEkron,
                    FirstStation=38838,
                    LastStation=38855,
                },
                 new Line//2
                {
                    Id=++id,
                    Code=101,
                    Area=Areas.RishonLetsion,
                    FirstStation=38833,
                    LastStation=38847,
                },
                  new Line//3
                {
                    Id=++id,
                    Code=102,
                    Area=Areas.Rehovot,
                    FirstStation=38834,
                    LastStation=38867,
                },
                      new Line//4
                {
                    Id=++id,
                    Code=103,
                    Area=Areas.Guedera,
                    FirstStation=38887,
                    LastStation=38893,
                },
                new Line//5
                {
                    Id=++id,
                    Code=104,
                    Area=Areas.KyriatEkron,
                    FirstStation=38839,
                    LastStation=38846,
                },
                    new Line//6
                {
                    Id=++id,
                    Code=105,
                    Area=Areas.TsourMoshe,
                    FirstStation=38848,
                    LastStation=38859,
                },
                    new Line//7
                {
                    Id=++id,
                    Code=106,
                    Area=Areas.Guedera,
                    FirstStation=38881,
                    LastStation=38886,
                },
                        new Line//8
                {
                    Id=++id,
                    Code=107,
                    Area=Areas.Guedera,
                    FirstStation=38885,
                    LastStation=38891,
                },
                new Line//9
                {
                    Id=++id,
                    Code=108,
                    Area=Areas.Rehovot,
                    FirstStation=38852,
                    LastStation=38856,
                },
                    new Line//10
                {
                    Id=++id,
                    Code=109,
                    Area=Areas.Gueoulim,
                    FirstStation=38862,
                    LastStation=38864,
                },
            };
            ListBuses = new List<Bus>
        {
            new Bus//1
            {
                LicenceNum=11-111-111,
                FromDate=new DateTime(2017,10,5),
                TotalTrip=180000,
                FuelRemain=1100,
                Status=0,

            },
            new Bus//2
            {
                LicenceNum=22-222-22,
                FromDate=new DateTime(2017,11,2),
                TotalTrip=200000,
                FuelRemain=1000,
                Status=0,

            },
              new Bus//3
            {
                LicenceNum=33-333-33,
                FromDate=new DateTime(2016,4,6),
                TotalTrip=150000,
                FuelRemain=500,
                Status=0,

            },
                new Bus//4
            {
                LicenceNum=44-444-44,
                FromDate=new DateTime(2014,7,20),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
            new Bus//5
            {
                LicenceNum=55-555-55,
                FromDate=new DateTime(2013,9,8),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                new Bus//6
            {
                LicenceNum=66-666-66,
                FromDate=new DateTime(2010,6,5),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                    new Bus//7
            {
                LicenceNum=77-777-77,
                FromDate=new DateTime(2015,8,18),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                 new Bus//8
            {
                LicenceNum=88-888-88,
                FromDate=new DateTime(2011,12,10),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                new Bus//9
            {
                LicenceNum=99-999-99,
                FromDate=new DateTime(2013,10,17),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                    new Bus//10
            {
                LicenceNum=10-101-10,
                FromDate=new DateTime(2014,7,3),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
            new Bus//11
            {
                LicenceNum=123-11-123,
                FromDate=new DateTime(2018,6,4),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                new Bus//12
            {
                LicenceNum=123-12-123,
                FromDate=new DateTime(2019,8,14),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                    new Bus//13
            {
                LicenceNum=123-13-123,
                FromDate=new DateTime(2020,5,5),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                new Bus//14
            {
                LicenceNum=123-14-123,
                FromDate=new DateTime(2019,4,4),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                    new Bus//15
            {
                LicenceNum=123-15-123,
                FromDate=new DateTime(2020,10,11),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                 new Bus//16
            {
                LicenceNum=123-16-123,
                FromDate=new DateTime(2018,11,15),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                 new Bus//17
            {
                LicenceNum=123-17-123,
                FromDate=new DateTime(2019,1,2),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                     new Bus//18
            {
                LicenceNum=123-18-123,
                FromDate=new DateTime(2020,12,12),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                 new Bus//19
            {
                LicenceNum=123-19-123,
                FromDate=new DateTime(2019,10,25),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },
                       new Bus//20
            {
                LicenceNum=123-20-123,
                FromDate=new DateTime(2019,10,28),
                TotalTrip=50000,
                FuelRemain=400,
                Status=0,

            },

        };
            ListUsers = new List<User>
        {
            new User
            {
                UserName="Chirly Sfez",
                Password="password",
                Admin=true,

            },
              new User
            {
                UserName="Nelly Lea Amar",
                Password="123456",
                Admin=false,

            }
        };
            ListAdjacentStations = new List<AdjacentStations>
            {
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38838,
                    Station2=38839,
                    Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38839,
                    Station2=38840,
                     Distance=2,
                    Time=new TimeSpan(0,5,0),


                },
                   new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38840,
                    Station2=38841,
                     Distance=2,
                    Time=new TimeSpan(0,5,0),


                },
                     new AdjacentStations
                {
                         id=++(idAdjStation),
                    Station1=38841,
                    Station2=38842,
                     Distance=2,
                    Time=new TimeSpan(0,5,0),


                },
              new AdjacentStations
                {
                  id=++(idAdjStation),
                    Station1=38844,
                    Station2=38845,
                     Distance=2,
                    Time=new TimeSpan(0,5,0),


                },
               new AdjacentStations
                {
                   id=++(idAdjStation),
                    Station1=38845,
                    Station2=38846,
                     Distance=2,
                    Time=new TimeSpan(0,5,0),


                },
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38846,
                    Station2=38847,
                     Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38847,
                    Station2=38855,
                      Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38833,
                    Station2=38834,
                      Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38834,
                    Station2=38836,
                      Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                    new AdjacentStations
                {
                        id=++(idAdjStation),
                    Station1=38836,
                    Station2=38837,
                      Distance=6,
                    Time=new TimeSpan(0,15,0),


                },
                     new AdjacentStations
                {
                         id=++(idAdjStation),
                    Station1=38837,
                    Station2=38838,
                      Distance=6,
                    Time=new TimeSpan(0,15,0)

                },
                      new AdjacentStations
                {
                          id=++(idAdjStation),
                    Station1=38842,
                    Station2=38847,
                          Distance=6,
                    Time=new TimeSpan(0,15,0)

                },
             new AdjacentStations
                {
                 id=++(idAdjStation),
                    Station1=38834,
                    Station2=38852,
                          Distance=6,
                    Time=new TimeSpan(0,15,0)

                },
              new AdjacentStations
                {
                  id=++(idAdjStation),
                    Station1=38852,
                    Station2=38856,
                          Distance=6,
                    Time=new TimeSpan(0,15,0)

                },
               new AdjacentStations
                {
                   id=++(idAdjStation),
                    Station1=38856,
                    Station2=38873,
                          Distance=6,
                    Time=new TimeSpan(0,15,0)

                },
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38873,
                    Station2=38875,

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38875,
                    Station2=38876,
                          Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38876,
                    Station2=38872,
                         Distance=2,
                    Time=new TimeSpan(0,5,0)

                },

                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38872,
                    Station2=38870,
                         Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                    new AdjacentStations
                {
                        id=++(idAdjStation),
                    Station1=38870,
                    Station2=38869,
                         Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                     new AdjacentStations
                {
                         id=++(idAdjStation),
                    Station1=38869,
                    Station2=38867,
                         Distance=8,
                    Time=new TimeSpan(0,20,0)

                },
                      new AdjacentStations
                {
                          id=++(idAdjStation),
                    Station1=38887,
                    Station2=38888,
                           Distance=8,
                    Time=new TimeSpan(0,20,0)

                },
                       new AdjacentStations
                {
                           id=++(idAdjStation),
                    Station1=38888,
                    Station2=38889,
                           Distance=8,
                    Time=new TimeSpan(0,20,0)

                },
                        new AdjacentStations
                {
                            id=++(idAdjStation),
                    Station1=38889,
                    Station2=38890,
                           Distance=4,
                    Time=new TimeSpan(0,10,0)

                },
                         new AdjacentStations
                {
                             id=++(idAdjStation),
                    Station1=38890,
                    Station2=38891,
                           Distance=4,
                    Time=new TimeSpan(0,10,0)

                },
                          new AdjacentStations
                {
                              id=++(idAdjStation),
                    Station1=38891,
                    Station2=38892,
                           Distance=4,
                    Time=new TimeSpan(0,10,0)

                },
             new AdjacentStations
                {
                 id=++(idAdjStation),
                    Station1=38892,
                    Station2=38898,
                           Distance=4,
                    Time=new TimeSpan(0,10,0)

                },
              new AdjacentStations
                {
                  id=++(idAdjStation),
                    Station1=38898,
                    Station2=38895,
                           Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
               new AdjacentStations
                {
                   id=++(idAdjStation),
                    Station1=38895,
                    Station2=38894,
                                Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38894,
                    Station2=38893,
                                Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38839,
                    Station2=38838,
                                Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38838,
                    Station2=38837,
                                Distance=10,
                    Time=new TimeSpan(0,25,0)

                },
                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38837,
                    Station2=38840,
                                Distance=2,
                    Time=new TimeSpan(0,5,0)

                },
                    new AdjacentStations
                {
                        id=++(idAdjStation),
                    Station1=38845,
                    Station2=38855,
                                Distance=10,
                    Time=new TimeSpan(0,25,0)

                },
                     new AdjacentStations
                {
                         id=++(idAdjStation),
                    Station1=38855,
                    Station2=38846,
                                Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                      new AdjacentStations
                {
                          id=++(idAdjStation),
                    Station1=38848,
                    Station2=38849,
                                Distance=10,
                    Time=new TimeSpan(0,25,0),

                },
                       new AdjacentStations
                {
                           id=++(idAdjStation),
                    Station1=38849,
                    Station2=38852,
                           Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                        new AdjacentStations
                {
                            id=++(idAdjStation),
                    Station1=38852,
                    Station2=38854,
                           Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                         new AdjacentStations
                {
                             id=++(idAdjStation),
                    Station1=38854,
                    Station2=38855,
                           Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                          new AdjacentStations
                {
                              id=++(idAdjStation),
                    Station1=38855,
                    Station2=38865,
                           Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                           new AdjacentStations
                {
                               id=++(idAdjStation),
                    Station1=38865,
                    Station2=38864,
                               Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
             new AdjacentStations
                {
                 id=++(idAdjStation),
                    Station1=38864,
                    Station2=38861,
                               Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
              new AdjacentStations
                {
                  id=++(idAdjStation),
                    Station1=38861,
                    Station2=38860,
                               Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
               new AdjacentStations
                {
                   id=++(idAdjStation),
                    Station1=38860,
                    Station2=38859,
                               Distance=12,
                    Time=new TimeSpan(0,30,0),

                },
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38881,
                    Station2=38883,
                               Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
             new AdjacentStations
                {
                 id=++(idAdjStation),
                    Station1=38883,
                    Station2=38884,
                               Distance=12,
                    Time=new TimeSpan(0,30,0),

                },
              new AdjacentStations
                {
                  id=++(idAdjStation),
                    Station1=38884,
                    Station2=38885,
                               Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
               new AdjacentStations
                {
                   id=++(idAdjStation),
                    Station1=38885,
                    Station2=38891,
                            Distance=2,
                    Time=new TimeSpan(0,5,0),
                },
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38891,
                    Station2=38890,
                            Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38890,
                    Station2=38889,
                            Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38889,
                    Station2=38888,
                            Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38888,
                    Station2=38887,
                            Distance=2,
                    Time=new TimeSpan(0,5,0),
                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38887,
                    Station2=38886,
                            Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38885,
                    Station2=38886,
                            Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38886,
                    Station2=38884,
                                   Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38884,
                    Station2=38883,
                                   Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38883,
                    Station2=38881,
                                   Distance=4,
                    Time=new TimeSpan(0,10,0),


                },
                    new AdjacentStations
                {
                        id=++(idAdjStation),
                    Station1=38881,
                    Station2=38880,
                                   Distance=8,
                    Time=new TimeSpan(0,20,0),


                },
                     new AdjacentStations
                {
                         id=++(idAdjStation),
                    Station1=38880,
                    Station2=38888,
                           Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
             new AdjacentStations
                {
                 id=++(idAdjStation),
                    Station1=38852,
                    Station2=38834,
                           Distance=8,
                    Time=new TimeSpan(0,20,0),

                },
               
              new AdjacentStations
                {
                  id=++(idAdjStation),
                    Station1=38834,
                    Station2=38837,
                           Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
               new AdjacentStations
                {
                   id=++(idAdjStation),
                    Station1=38837,
                    Station2=38831,
                           Distance=8,
                    Time=new TimeSpan(0,20,0),

                },
                new AdjacentStations
                {
                    id=++(idAdjStation),
                    Station1=38831,
                    Station2=38836,
                           Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38836,
                    Station2=38854,
                           Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38854,
                    Station2=38859,
                           Distance=8,
                    Time=new TimeSpan(0,20,0),

                },
                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38859,
                    Station2=38860,
                           Distance=10,
                    Time=new TimeSpan(0,25,0),

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38860,
                    Station2=38861,
                           Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38861,
                    Station2=38856,
                           Distance=8,
                    Time=new TimeSpan(0,20,0),

                },
                   new AdjacentStations
                {
                       id=++(idAdjStation),
                    Station1=38862,
                    Station2=38861,
                           Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                    new AdjacentStations
                {
                        id=++(idAdjStation),
                    Station1=38859,
                    Station2=38863,
                        Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                     new AdjacentStations
                {
                         id=++(idAdjStation),
                    Station1=38863,
                    Station2=38869,
                        Distance=2,
                    Time=new TimeSpan(0,5,0),

                },
                 new AdjacentStations
                {
                     id=++(idAdjStation),
                    Station1=38869,
                    Station2=38867,
                        Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                  new AdjacentStations
                {
                      id=++(idAdjStation),
                    Station1=38867,
                    Station2=38866,
                        Distance=10,
                    Time=new TimeSpan(0,25,0),
                },
             new AdjacentStations
                {
                 id=++(idAdjStation),
                    Station1=38866,
                    Station2=38865,
                        Distance=4,
                    Time=new TimeSpan(0,10,0),

                },
                 new AdjacentStations
                {
                          id=++(idAdjStation),
                    Station1=38842,
                    Station2=38844,
                          Distance=6,
                    Time=new TimeSpan(0,15,0)

                },
            };
            ListLineStations = new List<LineStation>
            {
                new LineStation
                {
                    Id=++(idLineStation),
                        LineId=1,
                        Station=38838,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38839,
                        
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38839,
                        LineStationIndex=2,
                        PrevStation=38838,
                        NextStation=38840,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38840,
                        LineStationIndex=3,
                        PrevStation=38839,
                        NextStation=38841,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38841,
                        LineStationIndex=4,
                        PrevStation=38840,
                        NextStation=38842,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38842,
                        LineStationIndex=5,
                        PrevStation=38841,
                        NextStation=38844,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38844,
                        LineStationIndex=6,
                        PrevStation=38842,
                        NextStation=38845,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38845,
                        LineStationIndex=7,
                        PrevStation=38844,
                        NextStation=38846,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38846,
                        LineStationIndex=8,
                        PrevStation=38845,
                        NextStation=38847,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=1,
                        Station=38847,
                        LineStationIndex=9,
                        PrevStation=38846,
                        NextStation=38855,
                },


                new LineStation
                {
                      Id=++(idLineStation),

                        LineId=1,
                        Station=38855,
                        LineStationIndex=10,
                        PrevStation=38847,
                        NextStation=-1,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38833,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38834,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38834,
                        LineStationIndex=2,
                        PrevStation=38833,
                        NextStation=38836,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38836,
                        LineStationIndex=3,
                        PrevStation=38834,
                        NextStation=38837,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38837,
                        LineStationIndex=4,
                        PrevStation=38836,
                        NextStation=38838,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38838,
                        LineStationIndex=5,
                        PrevStation=38837,
                        NextStation=38839,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38839,
                        LineStationIndex=6,
                        PrevStation=38838,
                        NextStation=38840,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38840,
                        LineStationIndex=7,
                        PrevStation=38839,
                        NextStation=38841,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38841,
                        LineStationIndex=8,
                        PrevStation=38840,
                        NextStation=38842,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38842,
                        LineStationIndex=9,
                        PrevStation=38841,
                        NextStation=38847,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=2,
                        Station=38847,
                        LineStationIndex=10,
                        PrevStation=38842,
                        NextStation=-1,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38834,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38852,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38852,
                        LineStationIndex=2,
                        PrevStation=38834,
                        NextStation=38856,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38856,
                        LineStationIndex=3,
                        PrevStation=38852,
                        NextStation=38873,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38873,
                        LineStationIndex=4,
                        PrevStation=38856,
                        NextStation=38875,
                },

                new LineStation
                {  Id=++(idLineStation),
                        LineId=3,
                        Station=38875,
                        LineStationIndex=5,
                        PrevStation=38873,
                        NextStation=38876,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38876,
                        LineStationIndex=6,
                        PrevStation=38875,
                        NextStation=38872,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38872,
                        LineStationIndex=7,
                        PrevStation=38876,
                        NextStation=38870,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38870,
                        LineStationIndex=8,
                        PrevStation=38872,
                        NextStation=38869,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38869,
                        LineStationIndex=9,
                        PrevStation=38870,
                        NextStation=38867,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=3,
                        Station=38867,
                        LineStationIndex=10,
                        PrevStation=38869,
                        NextStation=-1,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38887,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38888,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38888,
                        LineStationIndex=2,
                        PrevStation=38887,
                        NextStation=38889,
                },

                new LineStation
                {  Id=++(idLineStation),
                        LineId=4,
                        Station=38889,
                        LineStationIndex=3,
                        PrevStation=38888,
                        NextStation=38890,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38890,
                        LineStationIndex=4,
                        PrevStation=38889,
                        NextStation=38891,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38891,
                        LineStationIndex=5,
                        PrevStation=38890,
                        NextStation=38892,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38892,
                        LineStationIndex=6,
                        PrevStation=38891,
                        NextStation=38898,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38898,
                        LineStationIndex=7,
                        PrevStation=38892,
                        NextStation=38895,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38895,
                        LineStationIndex=8,
                        PrevStation=38898,
                        NextStation=38894,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38894,
                        LineStationIndex=9,
                        PrevStation=38895,
                        NextStation=38893,
                },

                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=4,
                        Station=38893,
                        LineStationIndex=10,
                        PrevStation=38894,
                        NextStation=-1,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38839,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38838,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38838,
                        LineStationIndex=2,
                        PrevStation=38839,
                        NextStation=38837,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38837,
                        LineStationIndex=3,
                        PrevStation=38838,
                        NextStation=38840,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38840,
                        LineStationIndex=4,
                        PrevStation=38837,
                        NextStation=38841,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38841,
                        LineStationIndex=5,
                        PrevStation=38840,
                        NextStation=38842,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38842,
                        LineStationIndex=6,
                        PrevStation=38841,
                        NextStation=38844,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38844,
                        LineStationIndex=7,
                        PrevStation=38842,
                        NextStation=38845,
                },
                 new LineStation
                {
                       Id=++(idLineStation),
                        LineId=5,
                        Station=38845,
                        LineStationIndex=8,
                        PrevStation=38844,
                        NextStation=38855,
                },
                   new LineStation
                {
                         Id=++(idLineStation),
                        LineId=5,
                        Station=38855,
                        LineStationIndex=9,
                        PrevStation=38845,
                        NextStation=38846,
                },
                     new LineStation
                {
                           Id=++(idLineStation),
                        LineId=5,
                        Station=38846,
                        LineStationIndex=10,
                        PrevStation=38855,
                        NextStation=-1,
                },
                       new LineStation
                {
                             Id=++(idLineStation),
                        LineId=6,
                        Station=38848,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38849,
                },
                         new LineStation
                {
                               Id=++(idLineStation),
                        LineId=6,
                        Station=38849,
                        LineStationIndex=2,
                        PrevStation=38848,
                        NextStation=38852,
                },
                           new LineStation
                {
                                 Id=++(idLineStation),
                        LineId=6,
                        Station=38852,
                        LineStationIndex=3,
                        PrevStation=38849,
                        NextStation=38854,
                },
                             new LineStation
                {
                                   Id=++(idLineStation),
                        LineId=6,
                        Station=38854,
                        LineStationIndex=4,
                        PrevStation=38852,
                        NextStation=38855,
                },
                               new LineStation
                {
                                     Id=++(idLineStation),
                        LineId=6,
                        Station=38855,
                        LineStationIndex=5,
                        PrevStation=38854,
                        NextStation=38865,
                },
                                 new LineStation
                {
                                       Id=++(idLineStation),
                        LineId=6,
                        Station=38865,
                        LineStationIndex=6,
                        PrevStation=38855,
                        NextStation=38864,
                },
              new LineStation
                {
                    Id=++(idLineStation),
                        LineId=6,
                        Station=38864,
                        LineStationIndex=7,
                        PrevStation=38865,
                        NextStation=38861,
                },
                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=6,
                        Station=38861,
                        LineStationIndex=8,
                        PrevStation=38864,
                        NextStation=38860,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=6,
                        Station=38860,
                        LineStationIndex=9,
                        PrevStation=38861,
                        NextStation=38859,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=6,
                        Station=38859,
                        LineStationIndex=10,
                        PrevStation=38860,
                        NextStation=-1,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=7,
                        Station=38881,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38883,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=7,
                        Station=38883,
                        LineStationIndex=2,
                        PrevStation=38881,
                        NextStation=38884,
                },
                      new LineStation
                {
                            Id=++(idLineStation),
                        LineId=7,
                        Station=38884,
                        LineStationIndex=3,
                        PrevStation=38883,
                        NextStation=38885,
                },
                        new LineStation
                {
                              Id=++(idLineStation),
                        LineId=7,
                        Station=38885,
                        LineStationIndex=4,
                        PrevStation=38884,
                        NextStation=38891,
                },
                          new LineStation
                {
                                Id=++(idLineStation),
                        LineId=7,
                        Station=38891,
                        LineStationIndex=5,
                        PrevStation=38885,
                        NextStation=38890,
                },
                            new LineStation
                {
                                  Id=++(idLineStation),
                        LineId=7,
                        Station=38890,
                        LineStationIndex=6,
                        PrevStation=38891,
                        NextStation=38889,
                },
                              new LineStation
                {
                                    Id=++(idLineStation),
                        LineId=7,
                        Station=38889,
                        LineStationIndex=7,
                        PrevStation=38890,
                        NextStation=38888,
                },
                                new LineStation
                {
                                      Id=++(idLineStation),
                        LineId=7,
                        Station=38888,
                        LineStationIndex=8,
                        PrevStation=38889,
                        NextStation=38887,
                },
                                  new LineStation
                {
                                        Id=++(idLineStation),
                        LineId=7,
                        Station=38887,
                        LineStationIndex=9,
                        PrevStation=38888,
                        NextStation=38886,
                },
              new LineStation
                {
                    Id=++(idLineStation),
                        LineId=7,
                        Station=38886,
                        LineStationIndex=10,
                        PrevStation=38887,
                        NextStation=-1,
                },
                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=8,
                        Station=38885,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38886,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=8,
                        Station=38886,
                        LineStationIndex=2,
                        PrevStation=38885,
                        NextStation=38884,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=8,
                        Station=38884,
                        LineStationIndex=3,
                        PrevStation=38886,
                        NextStation=38883,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=8,
                        Station=38883,
                        LineStationIndex=4,
                        PrevStation=38884,
                        NextStation=38881,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=8,
                        Station=38881,
                        LineStationIndex=5,
                        PrevStation=38883,
                        NextStation=38880,
                },
                      new LineStation
                {
                            Id=++(idLineStation),
                        LineId=8,
                        Station=38880,
                        LineStationIndex=6,
                        PrevStation=38881,
                        NextStation=38888,
                },
              new LineStation
                {
                    Id=++(idLineStation),
                        LineId=8,
                        Station=38888,
                        LineStationIndex=7,
                        PrevStation=38880,
                        NextStation=38889,
                },
                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=8,
                        Station=38889,
                        LineStationIndex=8,
                        PrevStation=38888,
                        NextStation=38890,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=8,
                        Station=38890,
                        LineStationIndex=9,
                        PrevStation=38889,
                        NextStation=38891,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=8,
                        Station=38891,
                        LineStationIndex=10,
                        PrevStation=38890,
                        NextStation=-1,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=9,
                        Station=38852,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38834,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=9,
                        Station=38834,
                        LineStationIndex=2,
                        PrevStation=38852,
                        NextStation=38837,
                },
                      new LineStation
                {
                            Id=++(idLineStation),
                        LineId=9,
                        Station=38837,
                        LineStationIndex=3,
                        PrevStation=38834,
                        NextStation=38831,
                },
                        new LineStation
                {
                              Id=++(idLineStation),
                        LineId=9,
                        Station=38831,
                        LineStationIndex=4,
                        PrevStation=38837,
                        NextStation=38836,
                },
                          new LineStation
                {
                                Id=++(idLineStation),
                        LineId=9,
                        Station=38836,
                        LineStationIndex=5,
                        PrevStation=38831,
                        NextStation=38854,
                },
              new LineStation
                {
                    Id=++(idLineStation),
                        LineId=9,
                        Station=38854,
                        LineStationIndex=6,
                        PrevStation=38836,
                        NextStation=38859,
                },
                new LineStation
                {
                      Id=++(idLineStation),
                        LineId=9,
                        Station=38859,
                        LineStationIndex=7,
                        PrevStation=38854,
                        NextStation=38860,
                },
                  new LineStation
                {
                        Id=++(idLineStation),
                        LineId=9,
                        Station=38860,
                        LineStationIndex=8,
                        PrevStation=38859,
                        NextStation=38861,
                },
                    new LineStation
                {
                          Id=++(idLineStation),
                        LineId=9,
                        Station=38861,
                        LineStationIndex=9,
                        PrevStation=38860,
                        NextStation=38856,
                },
                      new LineStation
                {
                            Id=++(idLineStation),
                        LineId=9,
                        Station=38856,
                        LineStationIndex=10,
                        PrevStation=38861,
                        NextStation=-1,
                },
                        new LineStation
                {
                              Id=++(idLineStation),
                        LineId=10,
                        Station=38862,
                        LineStationIndex=1,
                        PrevStation=-1,
                        NextStation=38861,
                },
                          new LineStation
                {
                                Id=++(idLineStation),
                        LineId=10,
                        Station=38861,
                        LineStationIndex=2,
                        PrevStation=38862,
                        NextStation=38860,
                },
                            new LineStation
                {
                                  Id=++(idLineStation),
                        LineId=10,
                        Station=38860,
                        LineStationIndex=3,
                        PrevStation=38861,
                        NextStation=38859,
                },
                              new LineStation
                {
                                    Id=++(idLineStation),
                        LineId=10,
                        Station=38859,
                        LineStationIndex=4,
                        PrevStation=38860,
                        NextStation=38863,
                },
                                new LineStation
                {
                                      Id=++(idLineStation),
                        LineId=10,
                        Station=38863,
                        LineStationIndex=5,
                        PrevStation=38859,
                        NextStation=38869,
                },
                                  new LineStation
                {
                                        Id=++(idLineStation),
                        LineId=10,
                        Station=38869,
                        LineStationIndex=6,
                        PrevStation=38863,
                        NextStation=38867,
                },
                                    new LineStation
                {
                                          Id=++(idLineStation),
                        LineId=10,
                        Station=38867,
                        LineStationIndex=7,
                        PrevStation=38869,
                        NextStation=38866,
                },
                                      new LineStation
                {
                                            Id=++(idLineStation),
                        LineId=10,
                        Station=38866,
                        LineStationIndex=8,
                        PrevStation=38867,
                        NextStation=38865,
                },
                                        new LineStation
                {
                                              Id=++(idLineStation),
                        LineId=10,
                        Station=38865,
                        LineStationIndex=9,
                        PrevStation=38866,
                        NextStation=38864,
                },
              new LineStation
                {
                    Id=++(idLineStation),
                        LineId=10,
                        Station=38864,
                        LineStationIndex=10,
                        PrevStation=38865,
                        NextStation=-1,
                },


            };
            ListLineTrip = new List<LineTrip>
        {
            new LineTrip//1
            {
                Id=++idlinetrip,
                LineId=1,
                StartAt= new TimeSpan(8,0,0),
                FinishAt=new TimeSpan(23, 0 ,0),
                Frequency=new TimeSpan(1,0,0),

            },
             new LineTrip//2
            {
                Id=++idlinetrip,
                LineId=2,
                StartAt= new TimeSpan(7,30,0),
                FinishAt=new TimeSpan(22, 30 ,0),
                Frequency=new TimeSpan(0,30,0),

            },
              new LineTrip//3
            {
                Id=++idlinetrip,
                LineId=3,
                StartAt= new TimeSpan(6,0,0),
                FinishAt=new TimeSpan(22, 0 ,0),
                Frequency=new TimeSpan(1,30,0),

            },
               new LineTrip//4
            {
                Id=++idlinetrip,
                LineId=4,
                StartAt= new TimeSpan(5,30,0),
                FinishAt=new TimeSpan(23, 0 ,0),
                Frequency=new TimeSpan(0,15,0),

            },
             new LineTrip//5
            {
                Id=++idlinetrip,
                LineId=5,
                StartAt= new TimeSpan(10,0,0),
                FinishAt=new TimeSpan(23, 30 ,0),
                Frequency=new TimeSpan(0,30,0),

            },
              new LineTrip//6
            {
                Id=++idlinetrip,
                LineId=6,
                StartAt= new TimeSpan(9,0,0),
                FinishAt=new TimeSpan(9, 0 ,0),
                Frequency=new TimeSpan(0,0,0),

            },
               new LineTrip//7
            {
                Id=++idlinetrip,
                LineId=7,
                StartAt= new TimeSpan(8,45,0),
                FinishAt=new TimeSpan(23, 15 ,0),
                Frequency=new TimeSpan(0,15,0),

            },
                new LineTrip//8
            {
                Id=++idlinetrip,
                LineId=8,
                StartAt= new TimeSpan(7,15,0),
                FinishAt=new TimeSpan(20, 0 ,0),
                Frequency=new TimeSpan(0,15,0),

            },
                 new LineTrip//9
            {
                Id=++idlinetrip,
                LineId=9,
                StartAt= new TimeSpan(8,0,0),
                FinishAt=new TimeSpan(20, 0 ,0),
                Frequency=new TimeSpan(1,0,0),

            },
                  new LineTrip//10
            {
                Id=++idlinetrip,
                LineId=10,
                StartAt= new TimeSpan(6,30,0),
                FinishAt=new TimeSpan(23, 0 ,0),
                Frequency=new TimeSpan(0,30,0),

            },
        };
            ListTrip = new List<Trip>
     {
         new Trip
         {
             Id=++idtrip,
             InStation=38854,
             OutStation=38864,
             LineId=6,
             UserName="Chirly Sfez",
             InAt=new TimeSpan(17,30,0),
             OutAt=new TimeSpan(18,0,0),//quand on calculera la distance entre 2 station a corriger
            
         
         },
          new Trip
         {
             Id=++idtrip,
             InStation=38834,
             OutStation=38841,
             LineId=2,
             UserName="Nelly Lea Amar",
             InAt=new TimeSpan(10,30,0),
             OutAt=new TimeSpan(11,45,0),//quand on calculera la distance entre 2 station a corriger
            
         
         }

     };
        }
    }
}

