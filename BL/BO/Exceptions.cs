using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BadStationCodeException : Exception
    {
        public int CODE;
        public BadStationCodeException(int code) : base() => CODE = code;
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
    public class BadLineCodeException : Exception
    {
        public int CODE;

        public BadLineCodeException(int code) : base() => CODE = code;
       

        public override string ToString() => base.ToString() + $", impossible to add this line: {CODE}"; //if we add a line that already exist but with wrong first and last station
                                                                                                        //or if we add a line that already exits with same first and last station 
    }

    public class BadLineTripIdException : Exception
    {
        public int ID;
        public BadLineTripIdException(int id) : base() => ID = id;
        public BadLineTripIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineTripIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad line trip id: {ID}";
    }
    public class BadLineTripLineIdException : Exception
    {
        public int LINEID;
       
        public BadLineTripLineIdException(int lineid, string message) :
            base(message) => LINEID = lineid;
   

        public override string ToString() => base.ToString() + $", bad line trip line id: {LINEID}";
    }
    public class BadLineStationIdException : Exception
    {
        public int LINEID;
        public BadLineStationIdException(int lineid) : base() => LINEID = lineid;
        public BadLineStationIdException(int lineid, string message) :
            base(message) => LINEID = lineid;
        public BadLineStationIdException(int lineid, string message, Exception innerException) :
            base(message, innerException) => LINEID = lineid;

        public override string ToString() => base.ToString() + $", bad line station line id: {LINEID}";
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
    public class BadLicenseNumException : Exception
    {
        public int LicenseNum;
        public BadLicenseNumException(int licensenum) : base() => LicenseNum = licensenum;
        public BadLicenseNumException(int licensenum, string message) :
            base(message) => LicenseNum = licensenum;
        public BadLicenseNumException(int licensenum, string message, Exception innerException) :
            base(message, innerException) => LicenseNum = licensenum;

        public override string ToString() => base.ToString() + $", bad license num: {LicenseNum}";
    }

    public class BadTripIdException : Exception
    {
        public int TripId;
        public BadTripIdException(int tripId) : base() => TripId = tripId;
        public BadTripIdException(int tripId, string message) :
            base(message) => TripId = tripId;
        public BadTripIdException(int tripId, string message, Exception innerException) :
            base(message, innerException) => TripId = tripId;

        public override string ToString() => base.ToString() + $", bad Trip Id: {TripId}";
    }


    public class BadUserNameException : Exception
    {
        public string UserName;
        public BadUserNameException(string userName) : base() => UserName = userName;
        public BadUserNameException(string userName, string message) :
            base(message) => UserName = userName;
        public BadUserNameException(string userName, string message, Exception innerException) :
            base(message, innerException) => UserName = userName;

        public override string ToString() => base.ToString() + $", bad User Name:{UserName}";
    }
    public class BadPasswordUserException : Exception
    {
        public string Password;
        public BadPasswordUserException(string password) : base() => Password = password;
        public BadPasswordUserException(string password, string message) :
            base(message) => Password = password;
        public BadPasswordUserException(string password, string message, Exception innerException) :
            base(message, innerException) => Password = password;

        public override string ToString() => base.ToString() + $", bad User Password:{Password}";
    }
    public class BadInputException : Exception
    {
        string Message;
        public BadInputException(string message) :
              base(message) => Message = message;
      
        public override string ToString() => base.ToString() + $", bad input:{Message}";
    }
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
