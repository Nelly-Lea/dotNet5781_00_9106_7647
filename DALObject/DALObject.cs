using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DLAPI;
namespace DL
{
    sealed class DALObject : IDL
    {
        #region singelton
        static readonly DALObject instance = new DALObject();
        static DALObject() { }// static ctor to ensure instance init is done just before first usage
        DALObject() { } // default => private
        public static DALObject Instance { get => instance; }// The public Instance property to use
        #endregion
        //Implement IDL methods, CRUD
        #region Station

        public DO.Station GetStation(int code)
        {
            DO.Station stat = DataSource.ListStations.Find(p => p.Code ==code);

            if (stat != null)
                return stat.Clone();
            else
                throw new DO.BadStationCodeException(code, $"bad station code: {code}");
        }
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();
        }
        public IEnumerable<DO.Station> GetAllPersonsBy(Predicate<DO.Station> predicate)
        {
            throw new NotImplementedException();
        }
        public void AddPerson(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code) != null)
                throw new DO.BadStationCodeException(station.Code, "Duplicate station code");
            DataSource.ListStations.Add(station.Clone());
        }

        public void DeletePerson(int code)
        {
            DO.Station stat = DataSource.ListStations.Find(p => p.Code == code);

            if (stat != null)
            {
                DataSource.ListStations.Remove(stat);
            }
            else
                throw new DO.BadStationCodeException(code, $"bad station code: {code}");
        }

        public void UpdatePerson(DO.Station station)
        {
            DO.Station stat = DataSource.ListStations.Find(p => p.Code == station.Code);

            if (stat != null)
            {
                DataSource.ListStations.Remove(stat);
                DataSource.ListStations.Add(station.Clone());
            }
            else
                throw new DO.BadStationCodeException(station.Code, $"bad station code: {station.Code}");
        }

        public void UpdatePerson(int id, Action<DO.Station> update)
        {
            throw new NotImplementedException();
        }
        #endregion Station

        //#region Student
        //public DO.Student GetStudent(int id)
        //{
        //    DO.Student stu = DataSource.ListStudents.Find(p => p.ID == id);
        //    try { Thread.Sleep(2000); } catch (ThreadInterruptedException e) { }
        //    if (stu != null)
        //        return stu.Clone();
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        //}
        //public void AddStudent(DO.Student student)
        //{
        //    if (DataSource.ListStudents.FirstOrDefault(s => s.ID == student.ID) != null)
        //        throw new DO.BadPersonIdException(student.ID, "Duplicate student ID");
        //    if (DataSource.ListPersons.FirstOrDefault(p => p.ID == student.ID) == null)
        //        throw new DO.BadPersonIdException(student.ID, "Missing person ID");
        //    DataSource.ListStudents.Add(student.Clone());
        //}
        //public IEnumerable<DO.Student> GetAllStudents()
        //{
        //    return from student in DataSource.ListStudents
        //           select student.Clone();
        //}
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    return from student in DataSource.ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        //public IEnumerable<object> GetStudentListWithSelectedFields(Func<DO.Student, object> generate)
        //{
        //    return from student in DataSource.ListStudents
        //           select generate(student);
        //}
        //public void UpdateStudent(DO.Student student)
        //{
        //    DO.Student stu = DataSource.ListStudents.Find(p => p.ID == student.ID);
        //    if (stu != null)
        //    {
        //        DataSource.ListStudents.Remove(stu);
        //        DataSource.ListStudents.Add(student.Clone());
        //    }
        //    else
        //        throw new DO.BadPersonIdException(student.ID, $"bad student id: {student.ID}");
        //}

        //public void UpdateStudent(int id, Action<DO.Student> update)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteStudent(int id)
        //{
        //    DO.Student stu = DataSource.ListStudents.Find(p => p.ID == id);

        //    if (stu != null)
        //    {
        //        DataSource.ListStudents.Remove(stu);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        //}
        //#endregion Student

        //#region StudentInCourse
        //public IEnumerable<DO.StudentInCourse> GetStudentsInCourseList(Predicate<DO.StudentInCourse> predicate)
        //{
        //    //option A - not good!!! 
        //    //produces final list instead of deferred query and does not allow proper cloning:
        //    // return DataSource.listStudInCourses.FindAll(predicate);

        //    // option B - ok!!
        //    //Returns deferred query + clone:
        //    //return DataSource.listStudInCourses.Where(sic => predicate(sic)).Select(sic => sic.Clone());

        //    // option c - ok!!
        //    //Returns deferred query + clone:
        //    return from sic in DataSource.ListStudInCourses
        //           where predicate(sic)
        //           select sic.Clone();
        //}
        //public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        //{
        //    if (DataSource.ListStudInCourses.FirstOrDefault(cis => (cis.PersonId == perID && cis.CourseId == courseID)) != null)
        //        throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is already registered to course ID");
        //    DO.StudentInCourse sic = new DO.StudentInCourse() { PersonId = perID, CourseId = courseID, Grade = grade };
        //    DataSource.ListStudInCourses.Add(sic);
        //}

        //public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        //{
        //    DO.StudentInCourse sic = DataSource.ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

        //    if (sic != null)
        //    {
        //        sic.Grade = grade;
        //    }
        //    else
        //        throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");
        //}

        //public void DeleteStudentInCourse(int perID, int courseID)
        //{
        //    DO.StudentInCourse sic = DataSource.ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

        //    if (sic != null)
        //    {
        //        DataSource.ListStudInCourses.Remove(sic);
        //    }
        //    else
        //        throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");
        //}
        //public void DeleteStudentFromAllCourses(int perID)
        //{
        //    DataSource.ListStudInCourses.RemoveAll(p => p.PersonId == perID);
        //}

        //#endregion StudentInCourse

        //#region Course
        //public DO.Course GetCourse(int id)
        //{
        //    return DataSource.ListCourses.Find(c => c.ID == id).Clone();
        //}

        //public IEnumerable<DO.Course> GetAllCourses()
        //{
        //    return from course in DataSource.ListCourses
        //           select course.Clone();
        //}

        //#endregion Course

        //#region Lecturer
        //public IEnumerable<DO.LecturerInCourse> GetLecturersInCourseList(Predicate<DO.LecturerInCourse> predicate)
        //{
        //    //Returns deferred query + clone:
        //    return from sic in DataSource.ListLectInCourses
        //           where predicate(sic)
        //           select sic.Clone();
        //}
        //#endregion
    }
}
