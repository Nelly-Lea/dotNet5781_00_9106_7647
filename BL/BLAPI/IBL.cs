using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BO;


namespace BLAPI
{
    public interface IBL
    {
        List<BO.Areas> GetAreas();
       
        #region Line
        void AddLine(int code, BO.Station FirstStation, BO.Station LastStation);
        IEnumerable<BO.Station> AddLineFirst(int code, BO.Areas area, BO.Station firstStation);
       IEnumerable<BO.Line> GetAllLines();
        void DeleteLine(int id);
        void UpdateLine(BO.Line Line, BO.LineStation DeletedStation, BO.Station NewStation);
        void UpdateLineCode(BO.Line Line, int code);
        #endregion Line
        #region LineStation
        IEnumerable<BO.LineStation> GetLineStation(int LineId);
        IEnumerable<BO.LineStation> GetAllLinesStation(int lineid);
        void RemoveLineStation(BO.Line Line, int code);
        void AddLineStation(BO.Line Line, BO.Station Station);
        BO.ShowStationsLine ShowStations(BO.Line line);

        #endregion LineStation
        #region Station
        BO.ShowStations ShowBusStations();
        //List<int> GetAllLineInStation(int index);
        List<BO.Line> GetAllLineInStation(int index);
        List<string> GetAllLastStationInLine(int index);
        IEnumerable<BO.Station> ShowStationArea(BO.Line line);
        IEnumerable<BO.Station> GetStationWithArea(BO.Areas area);
        void DeleteStation(int code);
        void UpdateStation(BO.Station StationToUpdate, string name, string address);
        void AddStation(int code, string name, double longitude, double latitude, string address, BO.Areas area);

        #endregion Station
        #region AdjacentStations
        void AddAdjacentStations(BO.AdjacentStations AdjacentStation);
        void UpdateTimeAndDistanceAdjStations(BO.AdjacentStations AdjacentStation);
        #endregion AdjacentStations
        #region User
        void CheckUserWorker(string UserName, string password);
        void CheckPassword(string password, string password1);
        void AddUser(string username, string password, bool admin);

        void CheckUserPassenger(string UserName, string password);
   #endregion User
        //Add Person to Course
        //get all courses for student
        //etc...
        //#region Student
        //BO.Student GetStudent(int id);
        //IEnumerable<BO.Student> GetAllStudents();
        //IEnumerable<BO.ListedPerson> GetStudentIDNameList();

        //IEnumerable<BO.Student> GetStudentsBy(Predicate<BO.Student> predicate);

        //void UpdateStudentPersonalDetails(BO.Student student);

        //void DeleteStudent(int id);

        //#endregion

        //#region StudentInCourse
        //void AddStudentInCourse(int perID, int courseID, float grade = 0);
        //void UpdateStudentGradeInCourse(int perID, int courseID, float grade);
        //void DeleteStudentInCourse(int perID, int courseID);

        //#endregion

        //#region Course
        //IEnumerable<BO.Course> GetAllCourses();
        //#endregion

        //#region StudentCourse
        //IEnumerable<BO.StudentCourse> GetAllCoursesPerStudent(int id);
        //#endregion

    }
}
