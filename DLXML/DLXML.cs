using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Device.Location;
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
       
        public List<DO.Areas> GetAllAreas()
        {
            List<DO.Areas> ListAreas = Enum.GetValues(typeof(DO.Areas)).Cast<DO.Areas>().ToList();
                return ListAreas;
          
        }
        #region DS XML Files
        XElement StationRoot;
       XElement LineRoot;
        XElement LineStationRoot;
        XElement LineTripRoot;
        XElement AdjacentStationRoot;
        XElement UserRoot;
        //XElement BusRoot;
        //XElement TripRoot;
        string stationPath = @"StationXml1.xml"; //XElement
        string LinePath = @"LineXml1.xml";
        string LineStationPath = @"LineStationXml1.xml";
        string LineTripPath = @"LineTripXml1.xml";
        string AdjacentStationPath= @"AdjacentStationXml1.xml";
        string UserPath = @"UserXml1.xml";
        //string BusPath= @"BusXml1.xml";
        //string TripPath = @"TripXml1.xml";

        XDocument stationXML = XDocument.Load("StationXml1.xml");
        #region Station

        public void SaveStationListLinq(List<Station> StationList)
        {
           StationRoot = new XElement("stations",
            from p in StationList
            select new XElement("station",
             new XElement("code", p.Code),
            new XElement("name", p.Name),
            new XElement("longitude", p.Longitude),
            new XElement("latitude", p.Latitude),
            new XElement("address", p.Address),
            new XElement("area", p.Area)
            )

            );
            StationRoot.Save(stationPath);
        }
        public DO.Station GetStation(int code)
        {
            
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            DO.Station sta = ListStations.Find(p => p.Code == code);
            if (sta != null)
                return sta; //no need to Clone()
            else
                throw new DO.BadStationCodeException(code, $"bad station code: {code}");

        }
        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            return from station in ListStations
                   select station; //no need to Clone()

        }

        public void AddStation(DO.Station station)
        {
            
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            if (ListStations.FirstOrDefault(s => s.Code == station.Code) != null)
                throw new DO.BadStationCodeException(station.Code, "Duplicate station code");

            ListStations.Add(station); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);


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
                throw new DO.BadStationCodeException(code, $"bad station code: {code}");

            XMLTools.SaveListToXMLSerializer(ListStations,stationPath);

        }
        public void UpdateStation(DO.Station station)
        {
           

            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            DO.Station sta = ListStations.Find(p => p.Code == station.Code);
            if (sta != null)
            {
                ListStations.Remove(sta);
                ListStations.Add(station); //no need to Clone()
            }
            else
                throw new DO.BadStationCodeException(station.Code, $"bad station code: {station.Code}");

            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);

        }


        #endregion Station

        #region Line

        public void SaveLineListLinq(List<Line> LineList)
        {
            LineRoot = new XElement("lines",
            from p in LineList
            select new XElement("line",
             new XElement("id", p.Id),
            new XElement("code", p.Code),
            new XElement("area", p.Area),
            new XElement("firststation", p.FirstStation),
            new XElement("laststation", p.LastStation),
            new XElement("countstation", p.CountStation)
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
        public int Countplus() //this function return the id for a new line
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);
            if (ListLines.Count()==0)
            {
                return 1;
            }
            int max = ListLines.Max(p => p.Id);
            return ++max;
           
            
        }
        public void AddLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);


            Line l = new Line();
            l = ListLines.Find(x => (x.Code == line.Code) && (x.Area == line.Area));
            if (l != null)
                throw new DO.BadLineIdException(line.Code, $"bad line code: {line.Code}");

            ListLines.Add(line); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);

        }
        public IEnumerable<DO.Line> GetAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            return from line in ListLines
                   select line; //no need to Clone()
        }
      
      
        public void UpdateLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(LinePath);

            DO.Line l = ListLines.Find(p => p.Id == line.Id);
            if (l != null)
            {
                ListLines.Remove(l);
                ListLines.Add(line); //no need to Clone()
            }
            else
                throw new DO.BadLineIdException(line.Id, $"bad Line id: {line.Id}");

            XMLTools.SaveListToXMLSerializer(ListLines, LinePath);
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
        public int CountplusLineStation()// this function returns the id for s new ine station
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            if(ListLineStations.Count()==0)
            {
                return 1;
            }
            int max = ListLineStations.Max(p => p.Id);
            return ++max;
           
        }


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
        public void DeleteListLineStations(int station)
        {
             List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);
            ListLineStations.RemoveAll(x => x.Station == station);

            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);
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

            ListLineStations.Add(lineStation); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLineStations, LineStationPath);

        }
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationPath);

            return from LineStation in ListLineStations
                   select LineStation; //no need to Clone()
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
            new XElement("finishat", p.FinishAt)
            )

            );
            LineTripRoot.Save(LineTripPath);
        }
    public int CountplusIdLineTrip() // this function returns the id for a new line trip
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);
            if(ListLineTrips.Count()==0)
            {
                return 1;
            }
            int max=ListLineTrips.Max(p => p.Id);
            return ++max;
            

        }
        public DO.LineTrip GetLineTrip(int id)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            DO.LineTrip LineTrip = ListLineTrips.Find(p => p.Id == id);
            if (LineTrip != null)
                return LineTrip; //no need to Clone()
            else
                throw new DO.BadLineTripIdException(id, $"bad Line Trip id: {id}");
        }

        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            if (ListLineTrips.FirstOrDefault(s => s.Id == lineTrip.Id) != null)
                throw new DO.BadLineTripIdException(lineTrip.Id, "Duplicate Line Trip id");

            
            ListLineTrips.Add(lineTrip); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLineTrips, LineTripPath);

        }
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            return from LineTrip in ListLineTrips
                   select LineTrip; //no need to Clone()
        }
        


        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(LineTripPath);

            DO.LineTrip lt = ListLineTrips.Find(p => p.Id == lineTrip.Id);
            if (lt != null)
            {
                ListLineTrips.Remove(lt);
                ListLineTrips.Add(lineTrip); //no need to Clone()
            }
            else
                throw new DO.BadLineTripIdException(lineTrip.Id, $"bad Line Trip id: {lineTrip.Id}");

            XMLTools.SaveListToXMLSerializer(ListLineTrips, LineTripPath);
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
        public DO.AdjacentStations GetAdjacentStations(int Station1, int Station2)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            DO.AdjacentStations AdjStation = ListAdjacentStations.Find(p => (p.Station1 == Station1) && (p.Station2 == Station2));

            if (AdjStation != null)
                return AdjStation;
            else
                throw new DO.BadAdjacentStationsException("bad Adjacent Station");
        }
        public int CountplusAdjacentStation()//this function returns the id for a new adjacent stations
        {
            List<AdjacentStations> ListAdjStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);
            int max = ListAdjStations.Max(p => p.id);
            return ++max;
      
        }
        public double CalculateDist(DO.Station stat1, DO.Station stat2) // this function calculates time and distance between stat1 and stat2 
        {
            GeoCoordinate p1 = new GeoCoordinate(stat1.Latitude, stat1.Longitude);
            GeoCoordinate p2 = new GeoCoordinate(stat2.Latitude, stat2.Longitude);
            double distance = p1.GetDistanceTo(p2);
            return distance;

        }
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
                throw new DO.BadAdjacentStationsException( "bad AdjacentStation id");
        }

        public void AddAdjacentStations(DO.AdjacentStations AdjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            if (ListAdjacentStations.FirstOrDefault(s => s.id == AdjacentStations.id) != null)
                throw new DO.BadAdjacentStationsException( "Duplicate AdjacentStations id");

           
            ListAdjacentStations.Add(AdjacentStations); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationPath);

        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            return from AdjacentStations in ListAdjacentStations
                   select AdjacentStations; //no need to Clone()
        }
       

        public void UpdateAdjacentStations(DO.AdjacentStations AdjacentStations)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(AdjacentStationPath);

            DO.AdjacentStations adjsta = ListAdjacentStations.Find(p => p.id == AdjacentStations.id);
            if (adjsta != null)
            {
                ListAdjacentStations.Remove(adjsta);
                ListAdjacentStations.Add(AdjacentStations); //no need to Clone()
            }
            else
                throw new DO.BadAdjacentStationsException("bad AdjacentStations id");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, AdjacentStationPath);
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
                throw new DO.BadAdjacentStationsException("bad AdjacentStations id");

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

           
            ListUsers.Add(user); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);

        }
        public IEnumerable<DO.User> GetAllUsers()
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            return from User in ListUsers
                   select User; //no need to Clone()
        }
        

       
        public void UpdateUser(DO.User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UserPath);

            DO.User us = ListUsers.Find(p => p.UserName == user.UserName);
            if (us != null)
            {
                ListUsers.Remove(us);
                ListUsers.Add(user); //no need to Clone()
            }
            else
                throw new DO.BadUserNameException(user.UserName, $"bad User Name: {user.UserName}");

            XMLTools.SaveListToXMLSerializer(ListUsers, UserPath);
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
        //public void SaveBusListLinq(List<Bus> BusList)
        //{
        //    BusRoot = new XElement("buses",
        //    from p in BusList
        //    select new XElement("bus",
        //    new XElement("licensenum", p.LicenceNum),
        //    new XElement("fromdate", p.FromDate),
        //    new XElement("totaltrip", p.TotalTrip),
        //    new XElement("fuelremain", p.FuelRemain),
        //     new XElement("status", p.Status)
        //    )

        //    );
        //    BusRoot.Save(BusPath);
        //}

        //public DO.Bus GetBus(int licensenum)
        //{
        //    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    DO.Bus bus = ListBus.Find(p => p.LicenceNum == licensenum);
        //    if (bus != null)
        //        return bus; //no need to Clone()
        //    else
        //        throw new DO.BadLicenseNumException(licensenum, $"bad Bus license num: {licensenum}");
        //}

        //public void AddBus(DO.Bus Bus)
        //{
        //    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    if (ListBus.FirstOrDefault(s => s.LicenceNum == Bus.LicenceNum) != null)
        //        throw new DO.BadLicenseNumException(Bus.LicenceNum, "Duplicate Bus licensenum");

        //    if (GetBus(Bus.LicenceNum) == null)
        //        throw new DO.BadLicenseNumException(Bus.LicenceNum, "Missing Bus licensenum");

        //    ListBus.Add(Bus); //no need to Clone()

        //    XMLTools.SaveListToXMLSerializer(ListBus, BusPath);

        //}
        //public IEnumerable<DO.Bus> GetAllBuses()
        //{
        //    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    return from Bus in ListBus
        //           select Bus; //no need to Clone()
        //}
        //public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        //{
        //    List<Bus> ListStudents = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    return from student in ListStudents
        //           select generate(student.ID, GetPerson(student.ID).Name);
        //}

        //public IEnumerable<object> GetBusListWithSelectedFields(Func<DO.Bus, object> generate)
        //{
        //    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    return from Bus in ListBus
        //           select generate(Bus);
        //}
        //public void UpdateBus(DO.Bus Bus)
        //{
        //    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    DO.Bus bus = ListBus.Find(p => p.LicenceNum == Bus.LicenceNum);
        //    if (bus != null)
        //    {
        //        ListBus.Remove(bus);
        //        ListBus.Add(Bus); //no nee to Clone()
        //    }
        //    else
        //        throw new DO.BadLicenseNumException(Bus.LicenceNum, $"bad Bus licensenum: {Bus.LicenceNum}");

        //    XMLTools.SaveListToXMLSerializer(ListBus, BusPath);
        //}

        //public void UpdateBus(int licensenum, Action<DO.Bus> update)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteBus(int licensenum)
        //{
        //    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(BusPath);

        //    DO.Bus bus = ListBus.Find(p => p.LicenceNum == licensenum);

        //    if (bus != null)
        //    {
        //        ListBus.Remove(bus);
        //    }
        //    else
        //        throw new DO.BadLicenseNumException(licensenum, $"bad Bus license num: {licensenum}");

        //    XMLTools.SaveListToXMLSerializer(ListBus, BusPath);
        //}

        #endregion Bus

        #region Trip
        //public void SaveTripListLinq(List<Trip> TripList)
        //{
        //    TripRoot = new XElement("trips",
        //    from p in TripList
        //    select new XElement("trip",
        //    new XElement("id", p.Id),
        //    new XElement("username", p.UserName),
        //    new XElement("lineid", p.LineId),
        //    new XElement("instation", p.InStation),
        //    new XElement("inat", p.InAt),
        //    new XElement("outstation", p.OutStation),
        //     new XElement("outat", p.OutAt)
        //    )

        //    );
        //    TripRoot.Save(TripPath);
        //}

        //public DO.Trip GetTrip(int id)
        //{
        //    List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    DO.Trip trip = ListTrips.Find(p => p.Id == id);
        //    if (trip != null)
        //        return trip; //no need to Clone()
        //    else
        //        throw new DO.BadTripIdException(id, $"bad Trip id: {id}");
        //}

        //public void AddTrip(DO.Trip Trip)
        //{
        //    List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    if (ListTrips.FirstOrDefault(s => s.Id == Trip.Id) != null)
        //        throw new DO.BadTripIdException(Trip.Id, "Duplicate Trip id");

        //    if (GetTrip(Trip.Id) == null)
        //        throw new DO.BadTripIdException(Trip.Id, "Missing Trip id");

        //    ListTrips.Add(Trip); //no need to Clone()

        //    XMLTools.SaveListToXMLSerializer(ListTrips, TripPath);

        //}
        //public IEnumerable<DO.Trip> GetAllTrips()
        //{
        //    List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    return from Trip in ListTrips
        //           select Trip; //no need to Clone()
        //}
        ////public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        ////{
        ////    List<Trip> ListStudents = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        ////    return from student in ListStudents
        ////           select generate(student.ID, GetPerson(student.ID).Name);
        ////}

        //public IEnumerable<object> GetTripListWithSelectedFields(Func<DO.Trip, object> generate)
        //{
        //    List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    return from Trip in ListTrips
        //           select generate(Trip);
        //}
        //public void UpdateTrip(DO.Trip Trip)
        //{
        //    List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    DO.Trip trip = ListTrips.Find(p => p.Id == Trip.Id);
        //    if (trip != null)
        //    {
        //        ListTrips.Remove(trip);
        //        ListTrips.Add(Trip); //no nee to Clone()
        //    }
        //    else
        //        throw new DO.BadTripIdException(Trip.Id, $"bad Trip id: {Trip.Id}");

        //    XMLTools.SaveListToXMLSerializer(ListTrips, TripPath);
        //}

        //public void UpdateTrip(int id, Action<DO.Trip> update)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteTrip(int id)
        //{
        //    List<Trip> ListTrips = XMLTools.LoadListFromXMLSerializer<Trip>(TripPath);

        //    DO.Trip trip = ListTrips.Find(p => p.Id == id);

        //    if (trip != null)
        //    {
        //        ListTrips.Remove(trip);
        //    }
        //    else
        //        throw new DO.BadTripIdException(id, $"bad Trip id: {id}");

        //    XMLTools.SaveListToXMLSerializer(ListTrips, TripPath);
        //}

        #endregion Trip
        #endregion

    }
}