using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadStationCodeException : Exception
    {
        public int CODE;
        public BadStationCodeException(int code) : base() => CODE =code;
        public BadStationCodeException(int code, string message) :
            base(message) => CODE = code;
        public BadStationCodeException(int code, string message, Exception innerException) :
            base(message, innerException) => CODE = code;

        public override string ToString() => base.ToString() + $", bad station code: {CODE}";
    }
    public class BadLineIdException : Exception
    {
        public int ID;
        public BadLineIdException(int id) : base() => ID = id;
        public BadLineIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad line id: {ID}";
    }
    public class BadLineTripIdException : Exception
    {
        public int ID;
        public BadLineTripIdException(int id) : base() => ID = id;
        public BadLineTripIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineTripIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad linetrip id: {ID}";
    }
    public class BadLineStationIdException : Exception
    {
        public int ID;
        public BadLineStationIdException(int id) : base() => ID = id;
        public BadLineStationIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineStationIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad linestation id: {ID}";
    }

    public class BadAdjacentStationsIdException : Exception
    {
        public int ID;
        public BadAdjacentStationsIdException(int id) : base() => ID = id;
        public BadAdjacentStationsIdException(int id, string message) :
            base(message) => ID = id;
        public BadAdjacentStationsIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad Adjacent Station id: {ID}";
    }
    //public class BadPersonIdCourseIDException : Exception
    //{
    //    public int personID;
    //    public int courseID;
    //    public BadPersonIdCourseIDException(int perID, int crsID) : base() { personID = perID; courseID = crsID; }
    //    public BadPersonIdCourseIDException(int perID, int crsID, string message) :
    //        base(message)
    //    { personID = perID; courseID = crsID; }
    //    public BadPersonIdCourseIDException(int perID, int crsID, string message, Exception innerException) :
    //        base(message, innerException)
    //    { personID = perID; courseID = crsID; }

    //    public override string ToString() => base.ToString() + $", bad person id: {personID} and course id: {courseID}";
    //}

    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
}
