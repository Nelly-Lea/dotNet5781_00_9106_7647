using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DLAPI;
using System.Device.Location;

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
        public List<DO.Areas> GetAllAreas()
        {
             List<DO.Areas> ListAreas = Enum.GetValues(typeof(DO.Areas)).Cast<DO.Areas>().ToList();
            return ListAreas;

        }
       
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
        public int Countplus()
        {
        
            return ++DataSource.id;
        }
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
            DO.Line l = new DO.Line();
            if (DataSource.ListLines.FirstOrDefault(s => s.Id == line.Id) != null)
                throw new DO.BadLineIdException(line.Id, "Duplicate line ID");
            l = DataSource.ListLines.Find(x => (x.Code == line.Code)&&(l.Area==line.Area));
            if(l!=null)
            {
                if ((l.FirstStation != line.LastStation) && (l.LastStation != line.FirstStation) || ((l.FirstStation == line.FirstStation) && (l.LastStation != line.LastStation)))
                    throw new DO.BadLineCodeException(l.Code);      
            }
           
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
            DO.Line lines = DataSource.ListLines.Find(p => p.Id == line.Id);

            if (lines != null)
            {
                DataSource.ListLines.Remove(lines);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new DO.BadStationCodeException(line.Id, $"bad line id: {line.Id}");
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

        public void DeleteLineTrip(int lineid)
        {
            //DO.LineTrip linetrip = DataSource.ListLineTrip.Find(p => p.LineId == lineid);

            //if (linetrip != null)
            //{
            //    DataSource.ListLineTrip.Remove(linetrip);
            //}
            //else
            //    throw new DO.BadLineTripLineIdException(lineid, $"bad linetrip line id: {lineid}");
            DataSource.ListLineTrip.RemoveAll(x => x.LineId == lineid);
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
        public int CountplusLineStation()
        {
            return ++DataSource.idLineStation;
        }
        public DO.LineStation GetLineStation(int id)
        {
            DO.LineStation linestation = DataSource.ListLineStations.Find(p => (p.LineId == id));

            if (linestation != null)
                return linestation.Clone();
            else
                throw new DO.BadLineStationIdException(id, $"bad stationline id: {id}");
        }
        public IEnumerable<DO.LineStation> GetAllLineStations()
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
            //  IEnumerable<DO.LineStation> linestations = DataSource.ListLineStations.Find(p => p.LineId == lineid);
          

          //foreach(var item in DataSource.ListLineStations)
          //  {
          //      if(item.LineId==lineid)
          //          DataSource.ListLineStations.Remove(item);
          //     }
            DataSource.ListLineStations.RemoveAll(x => x.Id == id);
            
            //if (linestation != null)
            //{
            //    DataSource.ListLineStations.Remove(linestation);
            //}
            //else
            //    throw new DO.BadLineStationIdException(lineid, $"bad line station LineId: {lineid}");
        }
        public void DeleteListLineStations(int station)//Supprimer un list de line station en fonction du numero de station
        {
            
            DataSource.ListLineStations.RemoveAll(x => x.Station == station);

            //if (linestation != null)
            //{
            //    DataSource.ListLineStations.Remove(linestation);
            //}
            //else
            //    throw new DO.BadLineStationIdException(lineid, $"bad line station LineId: {lineid}");
        }

        public void UpdateLineStation(DO.LineStation linestation)
        {
            DO.LineStation linestations = DataSource.ListLineStations.Find(p => p.Id == linestation.Id);

            if (linestations != null)
            {
                DataSource.ListLineStations.Remove(linestations);
                DataSource.ListLineStations.Add(linestation.Clone());
               
                DataSource.ListLineStations = DataSource.ListLineStations.OrderBy(x => x.LineStationIndex).ToList();
               
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
        public int CountplusAdjacentStation()
        {
            return ++DataSource.idAdjStation;
        }
        public double CalculateDist(DO.Station stat1, DO.Station stat2)
        {
            GeoCoordinate p1 = new GeoCoordinate(stat1.Latitude, stat1.Longitude);
            GeoCoordinate p2 = new GeoCoordinate(stat2.Latitude, stat2.Longitude);
            double distance = p1.GetDistanceTo(p2);
            return distance;
        }
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
        #region Bus
        public DO.Bus GetBus(int licensenum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(p => p.LicenceNum == licensenum);

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BadLicenseNumException(licensenum, $"bad license num: {licensenum}");
        }
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();

        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenceNum == bus.LicenceNum) != null)
                throw new DO.BadLicenseNumException(bus.LicenceNum, "Duplicate license num");
            DataSource.ListBuses.Add(bus.Clone());
        }

        public void DeleteBus(int licensenum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(p => p.LicenceNum == licensenum);

            if (bus != null)
            {
                DataSource.ListBuses.Remove(bus);
            }
            else
                throw new DO.BadStationCodeException(licensenum, $"bad license num: {licensenum}");
        }

        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus buses= DataSource.ListBuses.Find(p => p.LicenceNum == bus.LicenceNum);

            if (buses != null)
            {
                DataSource.ListBuses.Remove(buses);
                DataSource.ListBuses.Add(bus.Clone());
            }
            else
                throw new DO.BadLicenseNumException(bus.LicenceNum, $"bad licence num: {bus.LicenceNum}");
        }

        //public void UpdateStation(int id, Action<DO.Station> update)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion Bus

        #region Trip
        public DO.Trip GetTrip(int id)
        {
            DO.Trip Trip = DataSource.ListTrip.Find(p => p.Id == id);

            if (Trip != null)
                return Trip.Clone();
            else
                throw new DO.BadTripIdException(id, $"bad Trip id: {id}");
        }
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            return from trip in DataSource.ListTrip
                   select trip.Clone();
        }
        public IEnumerable<DO.Trip> GetAllTripBy(Predicate<DO.Trip> predicate)
        {
            return from trip in DataSource.ListTrip
                   where predicate(trip)
                   select trip.Clone();

        }
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrip.FirstOrDefault(p => p.Id == trip.Id) != null)
                throw new DO.BadTripIdException(trip.Id, "Duplicate trip id");
            DataSource.ListTrip.Add(trip.Clone());
        }

        public void DeleteTrip(int id)
        {
            DO.Trip trip = DataSource.ListTrip.Find(p => p.Id == id);

            if (trip != null)
            {
                DataSource.ListTrip.Remove(trip);
            }
            else
                throw new DO.BadTripIdException(id, $"bad trip id: {id}");
        }

        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip t = DataSource.ListTrip.Find(p => p.Id == trip.Id);

            if (t != null)
            {
                DataSource.ListTrip.Remove(t);
                DataSource.ListTrip.Add(trip.Clone());
            }
            else
                throw new DO.BadTripIdException(trip.Id, $"bad trip id: {trip.Id}");
        }



        //public void UpdateStation(int id, Action<DO.Station> update)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion Trip

        #region user

        public DO.User GetUser(string userName)
        {
            DO.User us = DataSource.ListUsers.Find(p => p.UserName == userName);

            if (us != null)
                return us.Clone();
            else
                throw new DO.BadUserNameException(userName, $"bad user name: {userName}");
        }
        public IEnumerable<DO.User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            return from user in DataSource.ListUsers
                   where predicate(user)
                   select user.Clone();

        }
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName) != null)
                throw new DO.BadUserNameException(user.UserName, "Duplicate user name");
            DataSource.ListUsers.Add(user.Clone());
        }

        public void DeleteUser(string userName)
        {
            DO.User us = DataSource.ListUsers.Find(p => p.UserName==userName);

            if (us != null)
            {
                DataSource.ListUsers.Remove(us);
            }
            else
                throw new DO.BadUserNameException(userName, $"bad user name: {userName}");
        }

        public void UpdateUser(DO.User user)
        {
            DO.User us = DataSource.ListUsers.Find(p => p.UserName == user.UserName);

            if (us!= null)
            {
                DataSource.ListUsers.Remove(us);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new DO.BadUserNameException(user.UserName, $"bad user name: {user.UserName}");
        }

        //public void UpdateUser(string UserName, Action<DO.Station> update)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion user
       
    }
}
