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
        #region User
          bool CheckUserWorker(string UserName, string password);
        #endregion User
        #region Line
       IEnumerable<BO.Line> GetAllLines();
        void DeleteLine(int id);
        #endregion Line
        #region LineStation
        IEnumerable<BO.LineStation> GetLineStation(int LineId);
        IEnumerable<BO.LineStation> GetAllLinesStation(int lineid);
        void RemoveLineStation(BO.Line Line, int code);
        #endregion LineStation

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
