using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
         
        //public static List<Person> ListPersons;
        //public static List<Course> ListCourses;
        //public static List<Student> ListStudents;
        //public static List<Lecturer> ListLecturers;
        //public static List<LecturerInCourse> ListLectInCourses;
        //public static List<StudentInCourse> ListStudInCourses;

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
                    Address="76 Ben Yehouda Street, Kfar Sabba"


                },
                  new Station //2
                {
                    Code= 38832,
                    Name="Herzel/ Tsomet Gilo",
                    Latitude= 31.870034,
                    Longitude=34.819541 ,
                    Address="Herzel Street, Kyriat Ekron"


                },
                    new Station//3
                {
                    Code= 38833,
                    Name="Anehshol/ Adayaguim",
                    Latitude= 31.984553,
                    Longitude= 34.782828,
                    Address="30 Anehshol Street, Rishon Letsion"


                },
                      new Station//4
                {
                    Code=38834 ,
                    Name="Frid/ Chechet Ayamim",
                    Latitude= 31.88855,
                    Longitude= 34.790904,
                    Address="9 Moshe Frid Street, Rehovot"


                },

                      new Station//5
                {
                    Code=38836 ,
                    Name="Tahanat merkazite Lod/Orada",
                    Latitude=31.956392 ,
                    Longitude=34.898098 ,
                    Address="Lod"


                },
                        new Station//6
                {
                    Code=38837 ,
                    Name="Hanna Avreh/ Volkni",
                    Latitude= 31.892166,
                    Longitude=34.796071 ,
                    Address="9 Hanna Avreh Street, Rehovot"


                },
                          new Station//7
                {
                    Code= 38838,
                    Name="Herzel/ Moshe Sharet",
                    Latitude= 31.857565,
                    Longitude=34.824106 ,
                    Address="20 Herzel Street, Kyriat Ekron"


                },
                            new Station//8
                {
                    Code=38839 ,
                    Name="Abanim/Elie Cohen",
                    Latitude=31.862305 ,
                    Longitude=34.821857 ,
                    Address="4 Abanim Street, Kyriat Ekron"

                },
                              new Station//9
                {
                    Code= 38840,
                    Name="Weizman/Abanim",
                    Latitude=31.865085 ,
                    Longitude= 34.822237,
                    Address="11 Weizman Street, Kyriat Ekron"

                },
                     new Station//10
                {
                    Code= 38841,
                    Name="Ahirouss/Akalanite",
                    Latitude=31.865222 ,
                    Longitude=34.818957 ,
                    Address="13 Ahirouss Street, Kyriat Ekron"

                },
                new Station//11
                {
                    Code= 38842,
                    Name="Akalanite/Anarkiss",
                    Latitude=31.867597 ,
                    Longitude= 34.818392,
                    Address="Akalanite Street, Kyriat Ekron"

                },
                      new Station//12
                {
                    Code= 38844,
                    Name="Elie Cohen/ Lohamey Agatahout",
                    Latitude= 31.86244,
                    Longitude= 34.827023,
                    Address="62 Elie Cohen Street, Kyriat Ekron"

                },
                            new Station//13
                {
                    Code= 38845,
                    Name="Chivzey/ Chevet Ahim",
                    Latitude= 31.863501,
                    Longitude= 34.828702,
                    Address="51 Chivzey Street, Kyriat Ekron"

                },
                     new Station//14
                {
                    Code= 38846,
                    Name="Chivzey/Weizman",
                    Latitude= 31.865348,
                    Longitude= 34.827102,
                    Address="31 Chivzey Street, Kyriat Ekron"

                },
                  new Station//15
                {
                    Code=38847 ,
                    Name="Haim Bar Lev/ Sderot Itshak Rabin",
                    Latitude= 31.977409,
                    Longitude= 34.763896,
                    Address="Hain Bar Lev Street, Rishon Letsion"

                },
                        new Station//16
                {
                    Code= 38848,
                    Name="Merkaz Labriout Anefech Lev Asharon",
                    Latitude=32.300345 ,
                    Longitude=34.912708 ,
                    Address="Tsour Moshe"

                },
                      new Station//17
                {
                    Code=38849 ,
                    Name="Merkaz Labriout Anefech Lev Asharon",
                    Latitude= 32.301347,
                    Longitude=34.912602 ,
                    Address="Tsour Moshe"

                },
                     new Station//18
                {
                    Code=38852,
                    Name="Oltsman/Amada",
                    Latitude= 31.914255,
                    Longitude=34.807944 ,
                    Address="2 Haim Oltsman Street, Rehovot"

                },
                       new Station//19
                {
                    Code=38854,
                    Name="Mahaney Tserifin/Mohadon",
                    Latitude= 31.963668,
                    Longitude=34.836363 ,
                    Address="Tserifin"

                },
                        new Station//20
                {
                    Code=38855,
                    Name="Herzel/Golani",
                    Latitude= 31.856115,
                    Longitude=34.825249 ,
                    Address="4 Herzel Street, Kyriat Ekron"

                },
                         new Station//21
                {
                    Code=38856,
                    Name="Arotem/Adagniot",
                    Latitude=31.874963 ,
                    Longitude= 34.81249,
                    Address="3 Arotem Street, Rehovot"

                },
                  new Station//22
                {
                    Code=38859,
                    Name="Aarava",
                    Latitude=32.300035 ,
                    Longitude=34.910842 ,
                    Address="Aarava Street, Tsour Moshe"

                },
                      new Station//23
                {
                    Code=38860,
                    Name="Mavo Aguefen/Mored Ataana",
                    Latitude=32.305234 ,
                    Longitude=34.948647 ,
                    Address="Mavo Aguefen Street, Yanov"

                },
                      new Station//24
                {
                    Code=38861,
                    Name="Mavo Aguefen/ Aarhava",
                    Latitude=32.304022 ,
                    Longitude=34.943393 ,
                    Address="Mavo Aguefen Street, Yanov"

                },
                      new Station//25
                {
                    Code=38862,
                    Name="Aarhava Aleph",
                    Latitude= 32.302957,
                    Longitude=34.940529 ,
                    Address="Aarhava Street, Geoulim"

                },
                      new Station//26
                {
                    Code=38863,
                    Name="Aarhava Bet",
                    Latitude=32.300264 ,
                    Longitude=34.939512 ,
                    Address="Aarhava Street, Gueoulim"

                },
                      new Station//27
                {
                    Code=38864,
                    Name="Aarhava/Otikim",
                    Latitude= 32.298171,
                    Longitude=34.938705 ,
                    Address="Aarhava Street, Gueoulim"

                },
                      new Station//28
                {
                    Code=38865,
                    Name="Reshout Sdot Ateoufat/ Aelia",
                    Latitude= 31.990876,
                    Longitude=34.8976 ,
                    Address="Sderot Aelia Street,Namal Teoufat Ben Gurion"

                },
                      new Station//29
                {
                    Code=38866,
                    Name="Kanaf/Brosh",
                    Latitude=31.998767 ,
                    Longitude= 34.879725,
                    Address="Kanaf Street, Namal Teoufat Ben Gurion"

                },
                       new Station//30
                {
                    Code=38867,
                    Name="Ahaboura/Dov Oz",
                    Latitude= 31.883019,
                    Longitude= 34.818708,
                    Address="24 Ahaboura Street,Rehovot"

                },
                      new Station//31
                {
                    Code=38869,
                    Name="Bet Alevy He",
                    Latitude=32.349776 ,
                    Longitude=34.926837 ,
                    Address="105, Bet Alevy"

                },
                      new Station//32
                {
                    Code=38870,
                    Name="Arishonim/Kvish 5700",
                    Latitude= 32.352953,
                    Longitude= 34.899465,
                    Address="13 Amigdal Street, Kfar Haim"

                },
                      new Station//33
                {
                    Code=38872,
                    Name="Agaon Ben Ish Hay/Tsaalon",
                    Latitude=31.897286 ,
                    Longitude=34.775083 ,
                    Address="Rehovot"

                },
                      new Station//34
                {
                    Code=38873,
                    Name="Oukchey/Levi Eshkol",
                    Latitude= 31.883941,
                    Longitude=34.807039 ,
                    Address="4 Israel Oukchey, Rehovot"

                },
                      new Station//35
                {
                    Code=38875,
                    Name="Menouha Venahala/ Yehouda Gorodiski",
                    Latitude= 31.896762,
                    Longitude=34.816752 ,
                    Address="31 Menouha Venahala Street, Rehovot"

                },
                      new Station//36
                {
                    Code=38876,
                    Name="Gorodiski/Yehiel Faldi",
                    Latitude= 31.898463,
                    Longitude=34.823461 ,
                    Address="35 Yehouda Gorodiski Street, Rehovot"

                },
                      new Station//37
                {
                    Code=38877,
                    Name="Dereh Menahem Begin/Yaakov Hazan",
                    Latitude= 32.076535,
                    Longitude=34.904907 ,
                    Address="30 Dereh Menahem Begin, Petah Tikva"

                },
                      new Station//38
                {
                    Code=38878,
                    Name="Dereh Apark/ Arav Neria",
                    Latitude= 32.299994,
                    Longitude= 34.878765,
                    Address="20 Dereh Apark Street, Netanya"

                },
                      new Station//39
                {
                    Code=38879,
                    Name="Ateena/Aguefen",
                    Latitude=31.865457 ,
                    Longitude=34.859437 ,
                    Address="Ateena Street,Yatsits"

                },
                      new Station//40
                {
                    Code=38880,
                    Name="Ateena/Aelon",
                    Latitude=31.866772 ,
                    Longitude=34.864555 ,
                    Address="Ateena Street, Yatsits"

                },
                      new Station//41
                {
                    Code=38881,
                    Name="Dereh Aprahim/ Yasmin",
                    Latitude= 31.809325,
                    Longitude=34.784347 ,
                    Address="46 Dereh Aprahim Street,Guedera"

                },
                      new Station//42
                {
                    Code=38883,
                    Name="Itshak Rabin/Pinhas Sapir",
                    Latitude=31.80037 ,
                    Longitude=34.778239 ,
                    Address="Dereh Itshak Rabin Street, Guedera"

                },
                      new Station//43
                {
                    Code=38884,
                    Name="Menahem Begin/ Itshak Rabin",
                    Latitude=31.799224 ,
                    Longitude=34.782985 ,
                    Address="4 Sderot Menahem Begin Street, Guedera"

                },
                      new Station//44
                {
                    Code=38885,
                    Name="Haim Hertsog/Dolev",
                    Latitude=31.800334 ,
                    Longitude=34.785069,
                    Address="12 Haim Hertsog Street, Guedera"

                },
                      new Station//45
                {
                    Code=38886,
                    Name="Bet Sefer Guevanim/Erez",
                    Latitude=31.802319 ,
                    Longitude=34.786735 ,
                    Address="2 Erez Street, Guedera"

                },
                      new Station//46
                {
                    Code=38887,
                    Name="Dereh Ailanot/Elon",
                    Latitude=31.804595 ,
                    Longitude=34.786623 ,
                    Address="13 Dereh Ailanot Street, Guedera"

                },
                          new Station//47
                {
                    Code=38888,
                    Name="Dereh Ailanot/ Menahem Begin",
                    Latitude=31.805041 ,
                    Longitude=34.785098 ,
                    Address="3 Dereh Ailanot Street, Guedera"

                },
                      new Station//48
                {
                    Code=38889,
                    Name="Aatsmaout/Weizman",
                    Latitude=31.816751 ,
                    Longitude=34.782252 ,
                    Address="1 Aatsmaout Street, Guedera"

                },
                      new Station//49
                {
                    Code=38890,
                    Name="Weizman/ Marabed Aksamim",
                    Latitude=31.816579 ,
                    Longitude=34.779753 ,
                    Address="19 Weizman Street, Guedera"

                },
                      new Station//50
                {
                    Code=38891,
                    Name="Tsaala/Elmog",
                    Latitude=31.801182 ,
                    Longitude=34.787199 ,
                    Address="25 Tsaala Street, Guedera"

                },
            };
            ListLines = new List<Line>
            {
                new Line//1
                {
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
        //    ListPersons = new List<Person>
        //    {
        //        new Person
        //        {
        //            Name = "David",
        //            ID = 36,
        //            Street = "Harekefet",
        //            HouseNumber = 44,
        //            City = "Tel-Aviv",
        //            PersonalStatus = PersonalStatus.MARRIED,
        //            BirthDate =  DateTime.ParseExact("24.03.85", "dd.MM.yy", null)
        //        },

        //        new Person
        //        {
        //            Name = "Yossi",
        //            ID = 23,
        //            Street = "Moshe Dayan",
        //            HouseNumber = 145,
        //            City = "Jerusalem",
        //            PersonalStatus = PersonalStatus.SINGLE,
        //            BirthDate =  DateTime.ParseExact("27.12.95", "dd.MM.yy", null)
        //        },

        //        new Person
        //        {
        //            Name = "Roni",
        //            ID = 15,
        //            Street = "Dayan",
        //            HouseNumber = 33,
        //            City = "Petach Tikva",
        //            PersonalStatus = PersonalStatus.MARRIED,
        //            BirthDate =  DateTime.ParseExact("14.11.97", "dd.MM.yy", null)
        //        },

        //        new Person
        //        {
        //            Name = "Shira",
        //            ID = 3,
        //            Street = "Moshe",
        //            HouseNumber = 33,
        //            City = "Eilat",
        //            PersonalStatus = PersonalStatus.SINGLE,
        //            BirthDate =  DateTime.ParseExact("24.08.99", "dd.MM.yy", null)
        //        },

        //        new Person
        //        {
        //            Name = "Gila",
        //            ID = 67,
        //            Street = "Marom",
        //            HouseNumber = 56,
        //            City = "Givataiim",
        //            PersonalStatus = PersonalStatus.MARRIED,
        //            BirthDate =  DateTime.ParseExact("23.12.77", "dd.MM.yy", null)
        //        }


        //    };


        //    ListStudents = new List<Student>
        //    {
        //        new Student
        //        {
        //            ID = 36,
        //            StartYear = 2018,
        //            Status = StudentStatus.ACTIVE,
        //            Graduation = StudentGraduate.BSC
        //        },
        //        new Student
        //        {
        //            ID = 23,
        //            StartYear = 2017,
        //            Status = StudentStatus.FINISHED,
        //            Graduation = StudentGraduate.PHD
        //        },
        //        new Student
        //        {
        //            ID = 15,
        //            StartYear = 2013,
        //            Status = StudentStatus.FINISHED,
        //            Graduation = StudentGraduate.BA
        //        }

        //    };

        //    ListCourses = new List<Course>
        //    {
        //        new Course
        //        {
        //            ID = 1,
        //            Number = 153007,
        //            Name = "MiniProject with Windows Systems",
        //            LectureHours = 3,
        //            PracticeHours = 1,
        //            CreditPoint = 3,
        //            Year = 2010,
        //            Semester = Semester.A
        //        },

        //        new Course
        //        {
        //            ID = 2,
        //            Number = 15005,
        //            Name = "Intro to CS",
        //            LectureHours = 3,
        //            PracticeHours = 2,
        //            CreditPoint = 4,
        //            Year = 2011,
        //            Semester = Semester.B
        //        },

        //        new Course
        //        {
        //            ID = 3,
        //            Number = 15004,
        //            Name = "Data Structure 1",
        //            LectureHours = 3,
        //            PracticeHours = 1,
        //            CreditPoint = 4,
        //            Year = 2014,
        //            Semester = Semester.A
        //        },

        //        new Course
        //        {
        //            ID = 4,
        //            Number = 15006,
        //            Name = "Data Structure 2",
        //            LectureHours = 3,
        //            PracticeHours = 1,
        //            CreditPoint = 4,
        //            Year = 2014,
        //            Semester = Semester.B
        //        }


        //    };

        //    ListStudInCourses = new List<StudentInCourse>
        //    {
        //        new StudentInCourse
        //        {
        //            CourseId = 1,
        //            Grade = 100,
        //            PersonId = 36
        //        },
        //        new StudentInCourse
        //        {
        //            CourseId = 2,
        //            Grade = 100,
        //            PersonId = 36
        //        },
        //        new StudentInCourse
        //        {
        //            CourseId = 3,
        //            Grade = 100,
        //            PersonId = 23
        //        },
        //        new StudentInCourse
        //        {
        //            CourseId = 3,
        //            Grade = 100,
        //            PersonId = 15
        //        }
        //    };

        //    List<Lecturer> ListLecturers = new List<Lecturer>
        //    {
        //        new Lecturer
        //        {
        //            ID = 3,
        //            Status = LecturerStatus.STUFF,
        //            Position = LecturerPosition.PROFESSOR,
        //            SeniorStuff  = true,
        //            JuniorStuff = false,
        //            AdjunctStuff = false
        //        },


        //        new Lecturer
        //        {
        //            ID = 67,
        //            Status = LecturerStatus.SABBATICAL,
        //            Position = LecturerPosition.SENIOR_LECTURER,
        //            SeniorStuff  = false,
        //            JuniorStuff = true,
        //            AdjunctStuff = false
        //        }
        //    };

        //    ListLectInCourses = new List<LecturerInCourse>
        //    {
        //        new LecturerInCourse
        //        {
        //            CourseId = 1,
        //            PersonId = 3,
        //            Status = CourseLectureStatus.LECTURER,
        //            GroupsAmount = 2
        //        },
        //        new LecturerInCourse
        //        {
        //            CourseId = 3,
        //            PersonId = 67,
        //            Status = CourseLectureStatus.PRACTITIONER,
        //            GroupsAmount = 1
        //        },
        //        new LecturerInCourse
        //        {
        //            CourseId = 3,
        //            PersonId = 67,
        //            Status = CourseLectureStatus.LECTURER,
        //            GroupsAmount =3
        //        }
        //    };

        }
    }
}

