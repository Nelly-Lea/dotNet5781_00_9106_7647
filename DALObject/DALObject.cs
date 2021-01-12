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
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
           
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code) != null)
                throw new DO.BadStationCodeException(station.Code, "Duplicate station code");
            DataSource.ListStations.Add(station.Clone());
        }

        public void DeleteStation(int code)
        {
            DO.Station stat = DataSource.ListStations.Find(p => p.Code == code);

            if (stat != null)
            {
                DataSource.ListStations.Remove(stat);
            }
            else
                throw new DO.BadStationCodeException(code, $"bad station code: {code}");
        }

        public void UpdateStation(DO.Station station)
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

        //public void UpdateStation(int id, Action<DO.Station> update)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion Station
        #region Line
        public DO.Line GetLine(int id)
        {
            DO.Line line = DataSource.ListLines.Find(p => p.Id == id);
           
            if (line != null)
                return line.Clone();
            else
                throw new DO.BadLineIdException(id, $"bad line id: {id}");
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(s => s.Id == line.Id) != null)
                throw new DO.BadLineIdException(line.Id, "Duplicate line ID");
           
            DataSource.ListLines.Add(line.Clone());
        }
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();
        }
        public IEnumerable<DO.Line> GetAllLineBy(Predicate<DO.Line> predicate)
        {
            return from line in DataSource.ListLines
                   where predicate(line)
                   select line.Clone();
        }
        public void DeleteLine(int id)
        {
            DO.Line line = DataSource.ListLines.Find(p => p.Id == id);

            if (line != null)
            {
                DataSource.ListLines.Remove(line);
            }
            else
                throw new DO.BadLineIdException(id, $"bad line id: {id}");
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line lines = DataSource.ListLines.Find(p => p.Code == line.Code);

            if (lines != null)
            {
                DataSource.ListLines.Remove(lines);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new DO.BadStationCodeException(line.Code, $"bad line id: {line.Code}");
        }


        #endregion Line
        #region LineTrip
        public DO.LineTrip GetLineTrip(int id)
        {
            DO.LineTrip linetrip = DataSource.ListLineTrip.Find(p => p.Id == id);

            if (linetrip != null)
                return linetrip.Clone();
            else
                throw new DO.BadLineTripIdException(id, $"bad linetrip id: {id}");
        }
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            return from linetrip in DataSource.ListLineTrip
                   select linetrip.Clone();
        }
        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate)
        {
            return from linetrip in DataSource.ListLineTrip
                   where predicate(linetrip)
                   select linetrip.Clone();
        }
        public void AddLineTrip(DO.LineTrip linetrip)
        {
            if (DataSource.ListLineTrip.FirstOrDefault(p => p.Id == linetrip.Id) != null)
                throw new DO.BadLineTripIdException(linetrip.Id, "Duplicate linetrip ID");
            DataSource.ListLineTrip.Add(linetrip.Clone());
        }

        public void DeleteLineTrip(int id)
        {
            DO.LineTrip linetrip = DataSource.ListLineTrip.Find(p => p.Id == id);

            if (linetrip != null)
            {
                DataSource.ListLineTrip.Remove(linetrip);
            }
            else
                throw new DO.BadLineTripIdException(id, $"bad linetrip id: {id}");
        }

        public void UpdateLineTrip(DO.LineTrip linetrip)
        {
            DO.LineTrip linetrips = DataSource.ListLineTrip.Find(p => p.Id == linetrip.Id);

            if (linetrips != null)
            {
                DataSource.ListLineTrip.Remove(linetrips);
                DataSource.ListLineTrip.Add(linetrip.Clone());
            }
            else
                throw new DO.BadLineTripIdException(linetrip.Id, $"bad linetrip id: {linetrip.Id}");
        }
        #endregion LineTrip
        #region LineStation
        public DO.LineStation GetLineStation(int id)
        {
            DO.LineStation linestation = DataSource.ListLineStations.Find(p => (p.LineId == id));

            if (linestation != null)
                return linestation.Clone();
            else
                throw new DO.BadLineStationIdException(id, $"bad stationline id: {id}");
        }
        public IEnumerable<DO.LineStation> GetAlllinestations()
        {
            return from linestation in DataSource.ListLineStations
                   select linestation.Clone();
        }
        public IEnumerable<DO.LineStation> GetAllLinestationsBy(Predicate<DO.LineStation> predicate)
        {
            return from linestation in DataSource.ListLineStations
                   where predicate(linestation)
                   select linestation.Clone();
        }
        public void AddLineStation(DO.LineStation linestation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(p => p.Id == linestation.Id) != null)
                throw new DO.BadLineStationIdException(linestation.Id, "Duplicate linestation ID");
            DataSource.ListLineStations.Add(linestation.Clone());
        }

        public void DeleteLineStation(int id)
        {
            DO.LineStation linestation = DataSource.ListLineStations.Find(p => p.Id == id);

            if (linestation != null)
            {
                DataSource.ListLineStations.Remove(linestation);
            }
            else
                throw new DO.BadLineStationIdException(id, $"bad linestation id: {id}");
        }

        public void UpdateLineStation(DO.LineStation linestation)
        {
            DO.LineStation linestations = DataSource.ListLineStations.Find(p => p.Id == linestation.Id);

            if (linestations != null)
            {
                DataSource.ListLineStations.Remove(linestations);
                DataSource.ListLineStations.Add(linestation.Clone());
            }
            else
                throw new DO.BadLineStationIdException(linestation.Id, $"bad linestation id: {linestation.Id}");
        }

        //public void UpdatePerson(int id, Action<DO.Person> update)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion LineStation
        #region AdjacentStations
        public DO.AdjacentStations GetAdjacentStations(int Id)
        {
            DO.AdjacentStations AdjStation= DataSource.ListAdjacentStations.Find(p => p.id == Id);

            if (AdjStation!= null)
                return AdjStation.Clone();
            else
                throw new DO.BadAdjacentStationsIdException(Id, $"bad Adjacent Station id: {Id}");
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from AdjStation in DataSource.ListAdjacentStations
                   select AdjStation.Clone();
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            return from sic in DataSource.ListAdjacentStations
                   where predicate(sic)
                   select sic.Clone();
        }
        public void AddAdjacentStations(DO.AdjacentStations AdjStation)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(p => p.id == AdjStation.id) != null)
                throw new DO.BadAdjacentStationsIdException(AdjStation.id, "Duplicate Adjacent Station ID");
            DataSource.ListAdjacentStations.Add(AdjStation.Clone());
        }

        public void DeleteAdjacentStations(int Id)
        {
            DO.AdjacentStations AdjStation = DataSource.ListAdjacentStations.Find(p => p.id == Id);

            if (AdjStation!= null)
            {
                DataSource.ListAdjacentStations.Remove(AdjStation);
            }
            else
                throw new DO.BadAdjacentStationsIdException(Id, $"bad Adjacent Station id: {Id}");
        }

        public void UpdateAdjacentStations(DO.AdjacentStations AdjStation)
        {
            DO.AdjacentStations AdjStations = DataSource.ListAdjacentStations.Find(p => p.id == AdjStation.id);

            if (AdjStations != null)
            {
                DataSource.ListAdjacentStations.Remove(AdjStations);
                DataSource.ListAdjacentStations.Add(AdjStation.Clone());
            }
            else
                throw new DO.BadAdjacentStationsIdException(AdjStation.id, $"bad Adjacent Station id: {AdjStation.id}");
        }

        //public void UpdatePerson(int id, Action<DO.AdjacentStations> update)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion AdjacentStations

        //#region Student

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
