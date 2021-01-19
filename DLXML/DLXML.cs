using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
//using DO;

namespace DL
{
    sealed class DLXML : IDL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        XElement stationRoot;
        XElement LineRoot;
        XElement LineStationRoot;
        XElement LineTripRoot;
        XElement AdjacentStationRoot;
        XElement UserRoot;
        XElement BusRoot;
        XElement TripRoot;
        string stationPath = @"StationXml.xml"; //XElement
        string LinePath = @"LineXml.xml";
        string LineStationPath = @"LineStationXml.xml";
        string LineTripPath = @"LineTripXml.xml";
        string AdjacentStationPath= @"AdjacentStationXml.xml";
        string UserPath = @"UserXml.xml";
        string BusPath= @"BusXml.xml";
        string TripPath = @"TripXml.xml";
        #region Station

        public void SaveStationListLinq(List<Station> stationList)
        {
            stationRoot = new XElement("stations",
            from p in stationList
            select new XElement("station",
            new XElement("code", p.Code),
            new XElement("name",p.Name),
            new XElement("longitude", p.Longitude),
            new XElement("latitude", p.Latitude)
            )
            
            );
            stationRoot.Save(stationPath);
        }

        public DO.Station GetStation(int code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            DO.Station stat = ListStations.Find(p => p.Code ==code);
            if (stat != null)
                return stat; //no need to Clone()
            else
                throw new DO.BadStationCodeException(code, $"bad station code: {code}");
        }

        public void AddStation(DO.Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            if (ListStations.FirstOrDefault(s => s.Code == station.Code) != null)
                throw new DO.BadStationCodeException(station.Code, "Duplicate Station code");

            if (GetStation(station.Code) == null)
                throw new DO.BadStationCodeException(station.Code, "Missing station code");

            ListStations.Add(station); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);

        }
        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            return from station in ListStations
                   select station; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Station> ListStudents = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetStationListWithSelectedFields(Func<DO.Station, object> generate)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            return from station in ListStations
                   select generate(station);
        }
        public void UpdateStation(DO.Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            DO.Station sta = ListStations.Find(p => p.Code == station.Code);
            if (sta != null)
            {
                ListStations.Remove(sta);
                ListStations.Add(station); //no nee to Clone()
            }
            else
                throw new DO.BadStationCodeException(station.Code, $"bad Station code: {station.Code}");

            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);
        }

        public void UpdateStation(int code, Action<DO.Station> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            DO.Station sta = ListStations.Find(p => p.Code == code);

            if (sta != null)
            {
                ListStations.Remove(sta);
            }
            else
                throw new DO.BadStationCodeException(code, $"bad Station code: {code}");

            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);
        }
        #endregion Station

        #region Line

        public void SaveLineListLinq(List<Line> lineList)
        {
            LineRoot = new XElement("lines",
            from p in lineList
            select new XElement("line",
            new XElement("id", p.Id),
            new XElement("code", p.Code),
            new XElement("area", p.Area),
            new XElement("firststation", p.FirstStation),
            new XElement("laststation",p.LastStation)
            )

            );
            LineRoot.Save(LinePath);
        }

        public DO.Line GetLine(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            DO.Line l = ListLines.Find(p => p.Id ==id);
            if (l != null)
                return l; //no need to Clone()
            else
                throw new DO.BadLineIdException(id, $"bad Line id: {id}");
        }

        public void AddLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            if (ListLines.FirstOrDefault(s => s.Id == line.Id) != null)
                throw new DO.BadLineIdException(line.Id, "Duplicate line id");

            if (GetLine(line.Id) == null)
                throw new DO.BadLineIdException(line.Id, "Missing line id");

            ListLines.Add(line); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);

        }
        public IEnumerable<DO.Line> GetAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            return from line in ListLines
                   select line; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Line> ListStudents = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetLineListWithSelectedFields(Func<DO.Line, object> generate)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            return from line in ListLines
                   select generate(line);
        }
        public void UpdateLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            DO.Line l = ListLines.Find(p => p.Id == line.Id);
            if (l != null)
            {
                ListLines.Remove(l);
                ListLines.Add(line); //no nee to Clone()
            }
            else
                throw new DO.BadLineIdException(line.Id, $"bad Line id: {line.Id}");

            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
        }

        public void UpdateLine(int id, Action<DO.Line> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            DO.Line l = ListLines.Find(p => p.Id ==id);

            if (l != null)
            {
                ListLines.Remove(l);
            }
            else
                throw new DO.BadLineIdException(id, $"bad Line id: {id}");

            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
        }
        #endregion Line

        #region LineStation
        public void SaveLineStationListLinq(List<LineStation> LineStationList)
        {
            LineStationRoot = new XElement("linestations",
            from p in LineStationList
            select new XElement("linestation",
             new XElement("id", p.Id),
            new XElement("lineid", p.LineId),
            new XElement("station", p.Station),
            new XElement("linestationindex", p.LineStationIndex),
            new XElement("prevstation", p.PrevStation),
            new XElement("nextstation", p.NextStation)
            )

            );
            LineStationRoot.Save(LineStationPath);
        }

        public DO.LineStation GetLineStation(int id)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            DO.LineStation l = ListLineStations.Find(p => p.Id == id);
            if (l != null)
                return l; //no need to Clone()
            else
                throw new DO.BadLineStationIdException(id, $"bad Line Station id: {id}");
        }

        public void AddLineStation(DO.LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            if (ListLineStations.FirstOrDefault(s => s.Id == lineStation.Id) != null)
                throw new DO.BadLineStationIdException(lineStation.Id, "Duplicate Line Station id");

            if (GetLineStation(lineStation.Id) == null)
                throw new DO.BadLineStationIdException(lineStation.Id, "Missing Line Station id");

            ListLineStations.Add(lineStation); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);

        }
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            return from LineStation in ListLineStations
                   select LineStation; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<LineStation> ListStudents = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetLineStationListWithSelectedFields(Func<DO.LineStation, object> generate)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            return from LineStation in ListLineStations
                   select generate(LineStation);
        }
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            DO.LineStation l = ListLineStations.Find(p => p.Id == lineStation.Id);
            if (l != null)
            {
                ListLineStations.Remove(l);
                ListLineStations.Add(lineStation); //no nee to Clone()
            }
            else
                throw new DO.BadLineStationIdException(lineStation.Id, $"bad Line Station id: {lineStation.Id}");

            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
        }

        public void UpdateLineStation(int id, Action<DO.LineStation> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineStation(int id)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            DO.LineStation l = ListLineStations.Find(p => p.Id == id);

            if (l != null)
            {
                ListLineStations.Remove(l);
            }
            else
                throw new DO.BadLineStationIdException(id, $"bad Line Station id: {id}");

            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
        }
        #endregion LineStation

        #region LineTrip

        public void SaveLineTripListLinq(List<LineTrip> lineTripList)
        {
            LineTripRoot = new XElement("linetrips",
            from p in lineTripList
            select new XElement("linetrip",
            new XElement("id", p.Id),
            new XElement("lineid", p.LineId),
            new XElement("startat", p.StartAt),
            new XElement("frequency", p.Frequency),
            new XElement("finishat",p.FinishAt)
            )

            );
            LineTripRoot.Save(LineTripPath);
        }

        public DO.LineTrip GetLineTrip(int id)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            DO.LineTrip stat = ListLineTrips.Find(p => p.Id == id);
            if (stat != null)
                return stat; //no need to Clone()
            else
                throw new DO.BadLineTripIdException(id, $"bad Line Trip id: {id}");
        }

        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            if (ListLineTrips.FirstOrDefault(s => s.Id == lineTrip.Id) != null)
                throw new DO.BadLineTripIdException(lineTrip.Id, "Duplicate Line Trip id");

            if (GetLineTrip(lineTrip.Id) == null)
                throw new DO.BadLineTripIdException(lineTrip.Id, "Missing Line Trip id");

            ListLineTrips.Add(lineTrip); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLineTrips, LineTripPath);

        }
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            return from LineTrip in ListLineTrips
                   select LineTrip; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<LineTrip> ListStudents = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetLineTripListWithSelectedFields(Func<DO.LineTrip, object> generate)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            return from LineTrip in ListLineTrips
                   select generate(LineTrip);
        }
        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            DO.LineTrip lt = ListLineTrips.Find(p => p.Id == lineTrip.Id);
            if (lt != null)
            {
                ListLineTrips.Remove(lt);
                ListLineTrips.Add(lineTrip); //no nee to Clone()
            }
            else
                throw new DO.BadLineTripIdException(lineTrip.Id, $"bad Line Trip id: {lineTrip.Id}");

            XMLTools.SaveListToXMLSerializer(ListLineTrips, LineTripPath);
        }

        public void UpdateLineTrip(int id, Action<DO.LineTrip> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int id)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            DO.LineTrip lt = ListLineTrips.Find(p => p.Id == id);

            if (lt != null)
            {
                ListLineTrips.Remove(lt);
            }
            else
                throw new DO.BadLineTripIdException(id, $"bad Line Trip id: {id}");

            XMLTools.SaveListToXMLSerializer(ListLineTrips, LineTripPath);
        }

        #endregion LineTrip
        #region AdjacentStation
        public void SaveAdajcentStationListLinq(List<AdjacentStations> adjacentstationList)
        {
            AdjacentStationRoot = new XElement("adjacentstations",
            from p in adjacentstationList
            select new XElement("adjacentstation",
            new XElement("id", p.id),
            new XElement("station1", p.Station1),
            new XElement("station2", p.Station2),
            new XElement("distance", p.Distance),
             new XElement("time", p.Time)
            )

            );
            AdjacentStationRoot.Save(AdjacentStationPath);
        }

        public DO.AdjacentStations GetAdjacentStations(int id)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            DO.AdjacentStations adjstat = ListAdjacentStations.Find(p => p.id == id);
            if (adjstat != null)
                return adjstat; //no need to Clone()
            else
                throw new DO.BadAdjacentStationsIdException(id, $"bad AdjacentStation id: {id}");
        }

        public void AddAdjacentStations(DO.AdjacentStations AdjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            if (ListAdjacentStations.FirstOrDefault(s => s.id == AdjacentStations.id) != null)
                throw new DO.BadAdjacentStationsIdException(AdjacentStations.id, "Duplicate AdjacentStations id");

            if (GetAdjacentStations(AdjacentStations.id) == null)
                throw new DO.BadAdjacentStationsIdException(AdjacentStations.id, "Missing AdjacentStations id");

            ListAdjacentStations.Add(AdjacentStations); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationPath);

        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            return from AdjacentStations in ListAdjacentStations
                   select AdjacentStations; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<AdjacentStations> ListStudents = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationsPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetAdjacentStationsListWithSelectedFields(Func<DO.AdjacentStations, object> generate)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            return from AdjacentStations in ListAdjacentStations
                   select generate(AdjacentStations);
        }
        public void UpdateAdjacentStations(DO.AdjacentStations AdjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            DO.AdjacentStations adjsta = ListAdjacentStations.Find(p => p.id == AdjacentStations.id);
            if (adjsta != null)
            {
                ListAdjacentStations.Remove(adjsta);
                ListAdjacentStations.Add(AdjacentStations); //no nee to Clone()
            }
            else
                throw new DO.BadAdjacentStationsIdException(AdjacentStations.id, $"bad AdjacentStations id: {AdjacentStations.id}");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationPath);
        }

        public void UpdateAdjacentStations(int id, Action<DO.AdjacentStations> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdjacentStations(int id)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            DO.AdjacentStations adjsta = ListAdjacentStations.Find(p => p.id == id);

            if (adjsta != null)
            {
                ListAdjacentStations.Remove(adjsta);
            }
            else
                throw new DO.BadAdjacentStationsIdException(id, $"bad AdjacentStations id: {id}");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationPath);
        }

        #endregion AdjacentAdjacentStations

        #region user 

        public void SaveUserListLinq(List<User> UserList)
        {
            UserRoot = new XElement("users",
            from p in UserList
            select new XElement("user",
            new XElement("username", p.UserName),
            new XElement("password", p.Password),
            new XElement("admin", p.Admin)
            
            )

            );
            UserRoot.Save(UserPath);
        }

        public DO.User GetUser(string userName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            DO.User us = ListUsers.Find(p => p.UserName == userName);
            if (us != null)
                return us; //no need to Clone()
            else
                throw new DO.BadUserNameException(userName, $"bad User Name: {userName}");
        }

        public void AddUser(DO.User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            if (ListUsers.FirstOrDefault(s => s.UserName == user.UserName) != null)
                throw new DO.BadUserNameException(user.UserName, "Duplicate User Name");

            if (GetUser(user.UserName) == null)
                throw new DO.BadUserNameException(user.UserName, "Missing User Name");

            ListUsers.Add(user); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);

        }
        public IEnumerable<DO.User> GetAllUsers()
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            return from User in ListUsers
                   select User; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<User> ListStudents = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetUserListWithSelectedFields(Func<DO.User, object> generate)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            return from User in ListUsers
                   select generate(User);
        }
        public void UpdateUser(DO.User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            DO.User us = ListUsers.Find(p => p.UserName == user.UserName);
            if (us != null)
            {
                ListUsers.Remove(us);
                ListUsers.Add(user); //no nee to Clone()
            }
            else
                throw new DO.BadUserNameException(user.UserName, $"bad User Name: {user.UserName}");

            XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);
        }

        public void UpdateUser(string UserName, Action<DO.User> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string UserName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            DO.User us = ListUsers.Find(p => p.UserName == UserName);

            if (us != null)
            {
                ListUsers.Remove(us);
            }
            else
                throw new DO.BadUserNameException(UserName, $"bad User Name: {UserName}");

            XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);
        }

        #endregion user
        #region Bus
        public void SaveBusListLinq(List<Bus> BusList)
        {
            BusRoot = new XElement("buses",
            from p in BusList
            select new XElement("bus",
            new XElement("licensenum", p.LicenceNum),
            new XElement("fromdate", p.FromDate),
            new XElement("totaltrip", p.TotalTrip),
            new XElement("fuelremain", p.FuelRemain),
             new XElement("status", p.Status)
            )

            );
            BusRoot.Save(BusPath);
        }

        public DO.Bus GetBus(int licensenum)
        {
            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            DO.Bus bus = ListBus.Find(p => p.LicenceNum == licensenum);
            if (bus != null)
                return bus; //no need to Clone()
            else
                throw new DO.BadLicenseNumException(licensenum, $"bad Bus license num: {licensenum}");
        }

        public void AddBus(DO.Bus Bus)
        {
            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            if (ListBus.FirstOrDefault(s => s.LicenceNum == Bus.LicenceNum) != null)
                throw new DO.BadLicenseNumException(Bus.LicenceNum, "Duplicate Bus licensenum");

            if (GetBus(Bus.LicenceNum) == null)
                throw new DO.BadLicenseNumException(Bus.LicenceNum, "Missing Bus licensenum");

            ListBus.Add(Bus); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBus, BusPath);

        }
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            return from Bus in ListBus
                   select Bus; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Bus> ListStudents = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetBusListWithSelectedFields(Func<DO.Bus, object> generate)
        {
            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            return from Bus in ListBus
                   select generate(Bus);
        }
        public void UpdateBus(DO.Bus Bus)
        {
            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            DO.Bus bus = ListBus.Find(p => p.LicenceNum == Bus.LicenceNum);
            if (bus != null)
            {
                ListBus.Remove(bus);
                ListBus.Add(Bus); //no nee to Clone()
            }
            else
                throw new DO.BadLicenseNumException(Bus.LicenceNum, $"bad Bus licensenum: {Bus.LicenceNum}");

            XMLTools.SaveListToXMLSerializer(ListBus, BusPath);
        }

        public void UpdateBus(int licensenum, Action<DO.Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licensenum)
        {
            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

            DO.Bus bus = ListBus.Find(p => p.LicenceNum == licensenum);

            if (bus != null)
            {
                ListBus.Remove(bus);
            }
            else
                throw new DO.BadLicenseNumException(licensenum, $"bad Bus license num: {licensenum}");

            XMLTools.SaveListToXMLSerializer(ListBus, BusPath);
        }

        #endregion Bus

        #region Trip
        public void SaveTripListLinq(List<Trip> TripList)
        {
            TripRoot = new XElement("trips",
            from p in TripList
            select new XElement("trip",
            new XElement("id", p.Id),
            new XElement("username", p.UserName),
            new XElement("lineid", p.LineId),
            new XElement("instation", p.InStation),
            new XElement("inat", p.InAt),
            new XElement("outstation", p.OutStation),
             new XElement("outat", p.OutAt)
            )

            );
            TripRoot.Save(TripPath);
        }

        public DO.Trip GetTrip(int id)
        {
            List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

            DO.Trip trip = ListTrips.Find(p => p.Id == id);
            if (trip != null)
                return trip; //no need to Clone()
            else
                throw new DO.BadTripIdException(id, $"bad Trip id: {id}");
        }

        public void AddTrip(DO.Trip Trip)
        {
            List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

            if (ListTrips.FirstOrDefault(s => s.Id == Trip.Id) != null)
                throw new DO.BadTripIdException(Trip.Id, "Duplicate Trip id");

            if (GetTrip(Trip.Id) == null)
                throw new DO.BadTripIdException(Trip.Id, "Missing Trip id");

            ListTrips.Add(Trip); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListTrips, TripPath);

        }
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

            return from Trip in ListTrips
                   select Trip; //no need to Clone()
        }
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Trip> ListStudents = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        public IEnumerable<object> GetTripListWithSelectedFields(Func<DO.Trip, object> generate)
        {
            List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

            return from Trip in ListTrips
                   select generate(Trip);
        }
        public void UpdateTrip(DO.Trip Trip)
        {
            List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

            DO.Trip trip = ListTrips.Find(p => p.Id == Trip.Id);
            if (trip != null)
            {
                ListTrips.Remove(trip);
                ListTrips.Add(Trip); //no nee to Clone()
            }
            else
                throw new DO.BadTripIdException(Trip.Id, $"bad Trip id: {Trip.Id}");

            XMLTools.SaveListToXMLSerializer(ListTrips, TripPath);
        }

        public void UpdateTrip(int id, Action<DO.Trip> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrip(int id)
        {
            List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

            DO.Trip trip = ListTrips.Find(p => p.Id == id);

            if (trip != null)
            {
                ListTrips.Remove(trip);
            }
            else
                throw new DO.BadTripIdException(id, $"bad Trip id: {id}");

            XMLTools.SaveListToXMLSerializer(ListTrips, TripPath);
        }

        #endregion Trip

        //string studentsPath = @"StudentsXml.xml"; //XMLSerializer
        //string coursesPath = @"CoursesXml.xml"; //XMLSerializer
        //string lecturersPath = @"LecturersXml.xml"; //XMLSerializer
        //string lectInCoursesPath = @"LecturerInCourseXml.xml"; //XMLSerializer
        //string studInCoursesPath = @"StudentInCoureseXml.xml"; //XMLSerializer


        //#endregion

        //#region Person
        //public DO.Person GetPerson(int id)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    Person p = (from per in personsRootElem.Elements()
        //                where int.Parse(per.Element("ID").Value) == id
        //                select new Person()
        //                {
        //                    ID = Int32.Parse(per.Element("ID").Value),
        //                    Name = per.Element("Name").Value,
        //                    Street = per.Element("Street").Value,
        //                    HouseNumber = Int32.Parse(per.Element("HouseNumber").Value),
        //                    City = per.Element("City").Value,
        //                    BirthDate = DateTime.Parse(per.Element("BirthDate").Value),
        //                    PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), per.Element("PersonalStatus").Value)
        //                }
        //                ).FirstOrDefault();

        //    if (p == null)
        //        throw new DO.BadPersonIdException(id, $"bad person id: {id}");

        //    return p;
        //}
        //public IEnumerable<DO.Person> GetAllPersons()
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    return (from p in personsRootElem.Elements()
        //            select new Person()
        //            {
        //                ID = Int32.Parse(p.Element("ID").Value),
        //                Name = p.Element("Name").Value,
        //                Street = p.Element("Street").Value,
        //                HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
        //                City = p.Element("City").Value,
        //                BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
        //                PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value)
        //            }
        //           );
        //}
        //public IEnumerable<DO.Person> GetAllPersonsBy(Predicate<DO.Person> predicate)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    return from p in personsRootElem.Elements()
        //           let p1 = new Person()
        //           {
        //               ID = Int32.Parse(p.Element("ID").Value),
        //               Name = p.Element("Name").Value,
        //               Street = p.Element("Street").Value,
        //               HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
        //               City = p.Element("City").Value,
        //               BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
        //               PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value)
        //           }
        //           where predicate(p1)
        //           select p1;
        //}
        //public void AddPerson(DO.Person person)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    XElement per1 = (from p in personsRootElem.Elements()
        //                     where int.Parse(p.Element("ID").Value) == person.ID
        //                     select p).FirstOrDefault();

        //    if (per1 != null)
        //        throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");

        //    XElement personElem = new XElement("Person",
        //                           new XElement("ID", person.ID),
        //                           new XElement("Name", person.Name),
        //                           new XElement("Street", person.Street),
        //                           new XElement("HouseNumber", person.HouseNumber.ToString()),
        //                           new XElement("City", person.City),
        //                           new XElement("BirthDate", person.BirthDate),
        //                           new XElement("PersonalStatus", person.PersonalStatus.ToString()));

        //    personsRootElem.Add(personElem);

        //    XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //}

        //public void DeletePerson(int id)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    XElement per = (from p in personsRootElem.Elements()
        //                    where int.Parse(p.Element("ID").Value) == id
        //                    select p).FirstOrDefault();

        //    if (per != null)
        //    {
        //        per.Remove();
        //        XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        //}

        //public void UpdatePerson(DO.Person person)
        //{
        //    XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //    XElement per = (from p in personsRootElem.Elements()
        //                    where int.Parse(p.Element("ID").Value) == person.ID
        //                    select p).FirstOrDefault();

        //    if (per != null)
        //    {
        //        per.Element("ID").Value = person.ID.ToString();
        //        per.Element("Name").Value = person.Name;
        //        per.Element("Street").Value = person.Street;
        //        per.Element("HouseNumber").Value = person.HouseNumber.ToString();
        //        per.Element("City").Value = person.City;
        //        per.Element("BirthDate").Value = person.BirthDate.ToString();
        //        per.Element("PersonalStatus").Value = person.PersonalStatus.ToString();

        //        XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(person.ID, $"bad person id: {person.ID}");
        //}

        //public void UpdatePerson(int id, Action<DO.Person> update)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion Person

        //#region Student
        //public DO.Student GetStudent(int id)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    DO.Student stu = ListStudents.Find(p => p.ID == id);
        //    if (stu != null)
        //        return stu; //no need to Clone()
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        //}
        //public void AddStudent(DO.Student student)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    if (ListStudents.FirstOrDefault(s => s.ID == student.ID) != null)
        //        throw new DO.BadPersonIdException(student.ID, "Duplicate student ID");

        //    if (GetPerson(student.ID) == null)
        //        throw new DO.BadPersonIdException(student.ID, "Missing person ID");

        //    ListStudents.Add(student); //no need to Clone()

        //    XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);

        //}
        //public IEnumerable<DO.Student> GetAllStudents()
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    return from student in ListStudents
        //           select student; //no need to Clone()
        //}
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        //public IEnumerable<object> GetStudentListWithSelectedFields(Func<DO.Student, object> generate)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    return from student in ListStudents
        //           select generate(student);
        //}
        //public void UpdateStudent(DO.Student student)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    DO.Student stu = ListStudents.Find(p => p.ID == student.ID);
        //    if (stu != null)
        //    {
        //        ListStudents.Remove(stu);
        //        ListStudents.Add(student); //no nee to Clone()
        //    }
        //    else
        //        throw new DO.BadPersonIdException(student.ID, $"bad student id: {student.ID}");

        //    XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);
        //}

        //public void UpdateStudent(int id, Action<DO.Student> update)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteStudent(int id)
        //{
        //    List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

        //    DO.Student stu = ListStudents.Find(p => p.ID == id);

        //    if (stu != null)
        //    {
        //        ListStudents.Remove(stu);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(id, $"bad student id: {id}");

        //    XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);
        //}
        //#endregion Student

        //#region StudentInCourse
        //public IEnumerable<DO.StudentInCourse> GetStudentsInCourseList(Predicate<DO.StudentInCourse> predicate)
        //{
        //    List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

        //    return from sic in ListStudInCourses
        //           where predicate(sic)
        //           select sic; //no need to Clone()
        //}
        //public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        //{
        //    List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

        //    if (ListStudInCourses.FirstOrDefault(cis => (cis.PersonId == perID && cis.CourseId == courseID)) != null)
        //        throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is already registered to course ID");

        //    DO.StudentInCourse sic = new DO.StudentInCourse() { PersonId = perID, CourseId = courseID, Grade = grade };

        //    ListStudInCourses.Add(sic);

        //    XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);
        //}

        //public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        //{
        //    List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

        //    DO.StudentInCourse sic = ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

        //    if (sic != null)
        //    {
        //        sic.Grade = grade;
        //    }
        //    else
        //        throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");

        //    XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);
        //}

        //public void DeleteStudentInCourse(int perID, int courseID)
        //{
        //    List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

        //    DO.StudentInCourse sic = ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

        //    if (sic != null)
        //    {
        //        ListStudInCourses.Remove(sic);
        //    }
        //    else
        //        throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");

        //    XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);

        //}
        //public void DeleteStudentFromAllCourses(int perID)
        //{
        //    List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

        //    ListStudInCourses.RemoveAll(p => p.PersonId == perID);

        //    XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);

        //}

        //#endregion StudentInCourse

        //#region Course
        //public DO.Course GetCourse(int id)
        //{
        //    List<Course> ListCourses = XMLTools.LoadListFromXMLSerializer<Course>(coursesPath);

        //    return ListCourses.Find(c => c.ID == id); //no need to Clone()

        //    //if not exist throw exception etc.
        //}

        //public IEnumerable<DO.Course> GetAllCourses()
        //{
        //    List<Course> ListCourses = XMLTools.LoadListFromXMLSerializer<Course>(coursesPath);

        //    return from course in ListCourses
        //           select course; //no need to Clone()
        //}

        //#endregion Course

        //#region Lecturer
        //public IEnumerable<DO.LecturerInCourse> GetLecturersInCourseList(Predicate<DO.LecturerInCourse> predicate)
        //{
        //    List<LecturerInCourse> ListLectInCourses = XMLTools.LoadListFromXMLSerializer<LecturerInCourse>(lectInCoursesPath);

        //    return from sic in ListLectInCourses
        //           where predicate(sic)
        //           select sic; //no need to Clone()
        //}
        #endregion


    }
}