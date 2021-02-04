using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
//using BO;
//using BO;

namespace BL
{
     public class BLImp : IBL //internal pas publiccccc
    {
        IDL dl = DLFactory.GetDL();
        #region Line 
       public  BO.ShowStationsLine ShowStations(BO.Line line)// pas public!!!
        {
            BO.ShowStationsLine stationslineBO = new BO.ShowStationsLine();
            BO.Station stationBO = new BO.Station();



            IEnumerable<DO.Line> ListofLines = dl.GetAllLines();
            IEnumerable<DO.Line> linesDO = (IEnumerable<DO.Line>)ListofLines.Where(x => x.Id == line.Id);
            int currentstation;
            int id = linesDO.FirstOrDefault().Id;
            currentstation = linesDO.FirstOrDefault().FirstStation;
            IEnumerable<DO.LineStation> linestationsDO = dl.GetAllLineStations();
            IEnumerable<DO.LineStation> linestationDO;
            while (currentstation!= linesDO.FirstOrDefault().LastStation)
            {
                //DO.LineStation linestationDO = dl.GetLineStation(id);

                 linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == currentstation));
                DO.Station stationDO = dl.GetStation(linestationDO.FirstOrDefault().Station);
                stationBO.Address = stationDO.Address;
                stationBO.Code = stationDO.Code;
                stationBO.Name= stationDO.Name;
                stationBO.Latitude= stationDO.Latitude;
                stationBO.Longitude = stationDO.Longitude;
                stationslineBO.ListStat.Add(stationBO);
                int nextstation = linestationDO.FirstOrDefault().NextStation;
                IEnumerable<DO.AdjacentStations> adjstationsDO = dl.GetAllAdjacentStations();
                IEnumerable<DO.AdjacentStations> adjstationDO = adjstationsDO.Where(x => (x.Station1 == currentstation) && (x.Station2 == nextstation));
                stationslineBO.ListDistances.Add(adjstationDO.FirstOrDefault().Distance);
                stationslineBO.ListTimes.Add(adjstationDO.FirstOrDefault().Time);
                currentstation = nextstation;
                linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == nextstation));
            }
            linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == linesDO.FirstOrDefault().LastStation));
            DO.Station stationDO2 = dl.GetStation(linestationDO.FirstOrDefault().Station);
            stationBO.Address = stationDO2.Address;
            stationBO.Code = stationDO2.Code;
            stationBO.Name = stationDO2.Name;
            stationBO.Latitude = stationDO2.Latitude;
            stationBO.Longitude = stationDO2.Longitude;
            stationslineBO.ListStat.Add(stationBO);
            return stationslineBO;

        }

        public BO.ShowStations ShowBusStations()
        {
            BO.ShowStations ss = new BO.ShowStations();
            List<BO.Station> listStationBO=new List<BO.Station>();
            IEnumerable<DO.Station> listStationDO = dl.GetAllStations();
            
            foreach(var item in listStationDO)
            {
                BO.Station StationBO = new BO.Station();
                StationBO.Code = item.Code;
                StationBO.Name = item.Name;
                StationBO.Address = item.Address;
                StationBO.Longitude = item.Longitude;
                StationBO.Latitude = item.Latitude;
                listStationBO.Add(StationBO);

            }

            ss.stations = listStationBO;
            List<BO.AdjacentStations> listAdjStationBO = new List<BO.AdjacentStations>();
            IEnumerable<DO.AdjacentStations> listAdjStationDO = dl.GetAllAdjacentStations();
           
            foreach (var item in listAdjStationDO)
            {
                BO.AdjacentStations AdjStationBO = new BO.AdjacentStations();
                AdjStationBO.Distance = item.Distance;
                AdjStationBO.id = item.id;
                AdjStationBO.Station1 = item.Station1;
                AdjStationBO.Station2 = item.Station2;
                AdjStationBO.Time = item.Time;
                listAdjStationBO.Add(AdjStationBO);

            }
            ss.adjStations = listAdjStationBO;
            int codeLine,lineId,codeLastStation;
            List<int> listinterieurcode = new List<int>();
            List<string> listinterieurname = new List<string>();

            IEnumerable<DO.LineStation> listLineStationDO=dl.GetAllLineStations();
            //int i = 0;
            foreach(var item in listStationBO)
            {
                codeLine = item.Code;
                listinterieurcode = new List<int>();
                listinterieurname = new List<string>();
                foreach (var item1 in listLineStationDO)
                {

                    if(item1.Station==codeLine)
                    {
                        lineId = item1.LineId;
                        
                       
                        foreach (var item2 in dl.GetAllLines())
                        {
                            if (item2.Id == lineId)
                            {

                                //ss.linesNumbers[i] = new List<int>();
                                //ss.linesNumbers[i].Add(item2.Code);
                               
                                listinterieurcode.Add(item2.Code);
                                codeLastStation = item2.LastStation;
                                foreach(var item3 in dl.GetAllStations())
                                {
                                    if (item3.Code == codeLastStation)
                                    {
                                        //ss.lastStationNames[i].Add(item3.Name);
                                        
                                        listinterieurname.Add(item3.Name);
                                    }
                                }
                            }
                                
                            
                        }


                    }
                    
                }
                // i++;
                ss.linesNumbers.Add(listinterieurcode);
                ss.lastStationNames.Add(listinterieurname);
               
              
            }
            return ss;


        }
        public void AddLine(int code,BO.Areas area, int firstation, int laststation)
        {
            DO.Line line = new DO.Line();
            line.Area =(DO.Areas) area;
            line.Code = code;
            line.FirstStation = firstation;
            line.LastStation = laststation;
            line.Id = dl.Countplus();
            line.CountStation = 2;
            dl.AddLine(line);

            DO.LineStation lineStation1 = new DO.LineStation();
            lineStation1.LineId = line.Id;
            lineStation1.Station = line.FirstStation;
            lineStation1.LineStationIndex = 1;
            lineStation1.PrevStation = -1;
            lineStation1.NextStation = laststation;

            DO.LineStation lineStation2 = new DO.LineStation();
            lineStation2.LineId = line.Id;
            lineStation2.Station = line.LastStation;
            lineStation1.LineStationIndex = 2;
            lineStation1.PrevStation = line.FirstStation;
            lineStation1.NextStation = -1;

            dl.AddLineStation(lineStation1);
            dl.AddLineStation(lineStation2);

            DO.AdjacentStations adjacentStation = new DO.AdjacentStations();

            adjacentStation.Distance = (dl.CalculateDist(dl.GetStation(firstation), dl.GetStation(laststation)));
            //vitesse=30km/h=>500m/min
            double time = adjacentStation.Distance / 500;
            int h = (int)time / 60;
            int m = (int)time % 60;
            adjacentStation.Time = new TimeSpan(h, m, 0);
            dl.AddAdjacentStations(adjacentStation);
        }
        // FONCTION UPDATE
        public void DeleteLine(int id)
        {
            
            dl.DeleteLine(id);
            dl.DeleteLineStation(id);
            dl.DeleteLineTrip(id);
            
        }
        #endregion Line
        #region AdjacentStation
        void UpdateTimeAndDistance(BO.AdjacentStations AdjacentStation)
        {
            DO.Station station1 = new DO.Station();
            DO.Station station2 = new DO.Station();
            foreach(var item in dl.GetAllStations())
            {
                if (item.Code == AdjacentStation.Station1)
                    station1 = item;
                if (item.Code == AdjacentStation.Station2)
                    station2 = item;
            }
            double distance = dl.CalculateDist(station1, station2);
            AdjacentStation.Distance = distance;
            //vitesse=30km/h=>500m/min
            double time = AdjacentStation.Distance / 500;
            int h = (int)time / 60;
            int m = (int)time % 60;
            AdjacentStation.Time = new TimeSpan(h, m, 0);
            DO.AdjacentStations adjacentStationsDO = new DO.AdjacentStations();
            adjacentStationsDO.Distance = AdjacentStation.Distance;
            adjacentStationsDO.id = AdjacentStation.id;
            adjacentStationsDO.Station1 = AdjacentStation.Station1;
            adjacentStationsDO.Station2 = AdjacentStation.Station2;
            adjacentStationsDO.Time = AdjacentStation.Time;
            dl.UpdateAdjacentStations(adjacentStationsDO);

        }
        #endregion AdjacentStation
        #region Station
        void AddStation(int code, string name,double longitude,double latitude,string address)
        {
            DO.Station station = new DO.Station();
            station.Code = code;
            station.Name = name;
            station.Longitude = longitude;
            station.Latitude = latitude;
            station.Address = address;
            dl.AddStation(station);
        }
        #endregion Station

        //#region Student
        //BO.Student studentDoBoAdapter(DO.Student studentDO)
        //{
        //    BO.Student studentBO = new BO.Student();
        //    DO.Person personDO;
        //    int id = studentDO.ID;
        //    try
        //    {
        //        personDO = dl.GetPerson(id);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }
        //    personDO.CopyPropertiesTo(studentBO);
        //    //studentBO.ID = personDO.ID;
        //    //studentBO.BirthDate = personDO.BirthDate;
        //    //studentBO.City = personDO.City;
        //    //studentBO.Name = personDO.Name;
        //    //studentBO.HouseNumber = personDO.HouseNumber;
        //    //studentBO.Street = personDO.Street;
        //    //studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

        //    studentDO.CopyPropertiesTo(studentBO);
        //    //studentBO.StartYear = studentDO.StartYear;
        //    //studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
        //    //studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

        //    studentBO.ListOfCourses = from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
        //                              let course = dl.GetCourse(sic.CourseId)
        //                              select course.CopyToStudentCourse(sic);
        //    //new BO.StudentCourse()
        //    //{
        //    //    ID = course.ID,
        //    //    Number = course.Number,
        //    //    Name = course.Name,
        //    //    Year = course.Year,
        //    //    Semester = (BO.Semester)(int)course.Semester,
        //    //    Grade = sic.Grade
        //    //};

        //    return studentBO;
        //}

        //public BO.Student GetStudent(int id)
        //{
        //    DO.Student studentDO;
        //    try
        //    {
        //        studentDO = dl.GetStudent(id);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
        //    }
        //    return studentDoBoAdapter(studentDO);
        //}

        //public IEnumerable<BO.Student> GetAllStudents()
        //{
        //    //return from item in dl.GetStudentListWithSelectedFields( (stud) => { return GetStudent(stud.ID); } )
        //    //       let student = item as BO.Student
        //    //       orderby student.ID
        //    //       select student;
        //    return from studentDO in dl.GetAllStudents()
        //           orderby studentDO.ID
        //           select studentDoBoAdapter(studentDO);
        //}
        //public IEnumerable<BO.Student> GetStudentsBy(Predicate<BO.Student> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<BO.ListedPerson> GetStudentIDNameList()
        //{
        //    return from item in dl.GetStudentListWithSelectedFields((studentDO) =>
        //    {
        //        try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
        //        return new BO.ListedPerson() { ID = studentDO.ID, Name = dl.GetPerson(studentDO.ID).Name };
        //    })
        //           let studentBO = item as BO.ListedPerson
        //           //orderby student.ID
        //           select studentBO;
        //}

        //public void UpdateStudentPersonalDetails(BO.Student student)
        //{
        //    //Update DO.Person            
        //    DO.Person personDO = new DO.Person();
        //    student.CopyPropertiesTo(personDO);
        //    try
        //    {
        //        dl.UpdatePerson(personDO);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }

        //    //Update DO.Student            
        //    DO.Student studentDO = new DO.Student();
        //    student.CopyPropertiesTo(studentDO);
        //    try
        //    {
        //        dl.UpdateStudent(studentDO);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }

        //}

        //public void DeleteStudent(int id)
        //{
        //    try
        //    {
        //        dl.DeletePerson(id);
        //        dl.DeleteStudent(id);
        //        dl.DeleteStudentFromAllCourses(id);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
        //    }
        //}

        //#endregion

        //#region StudentIn Course
        //public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        //{
        //    try
        //    {
        //        dl.AddStudentInCourse(perID, courseID, grade);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //}

        //public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        //{
        //    try
        //    {
        //        dl.UpdateStudentGradeInCourse(perID, courseID, grade);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //}

        //public void DeleteStudentInCourse(int perID, int courseID)
        //{
        //    try
        //    {
        //        dl.DeleteStudentInCourse(perID, courseID);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //}
        //#endregion

        //#region Course

        //BO.Course courseDoBoAdapter(DO.Course courseDO)
        //{
        //    BO.Course courseBO = new BO.Course();
        //    int id = courseDO.ID;
        //    courseDO.CopyPropertiesTo(courseBO);

        //    courseBO.Lecturers = from lic in dl.GetLecturersInCourseList(lic => lic.CourseId == id)
        //                         let course = dl.GetCourse(lic.CourseId)
        //                         select (BO.CourseLecturer)course.CopyPropertiesToNew(typeof(BO.CourseLecturer));
        //    return courseBO;
        //}
        //public IEnumerable<BO.Course> GetAllCourses()
        //{
        //    return from crsDO in dl.GetAllCourses()
        //           select courseDoBoAdapter(crsDO);
        //}

        //public IEnumerable<BO.StudentCourse> GetAllCoursesPerStudent(int id)
        //{
        //    return from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
        //           let course = dl.GetCourse(sic.CourseId)
        //           select course.CopyToStudentCourse(sic);
        //}

        //#endregion


    }
}
