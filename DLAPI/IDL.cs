using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DLAPI
{
    public interface IDL
    {
        List<DO.Areas> GetAllAreas();

        #region Station
       DO.Station GetStation(int code);
        IEnumerable<DO.Station> GetAllStations();
       
        void AddStation(DO.Station Station);
        void UpdateStation(DO.Station Station);
      
        void DeleteStation(int code); // removes only Station, does not remove the appropriate Person...
        #endregion
        #region Line
        
        DO.Line GetLine(int id);
        int Countplus();
        IEnumerable<DO.Line> GetAllLines();
       
        void AddLine(DO.Line Line);
        void UpdateLine(DO.Line Line);
       
        void DeleteLine(int id); // removes only Line, does not remove the appropriate Person...

        #endregion Line

        #region LineStation
        int CountplusLineStation();
        DO.LineStation GetLineStation(int id);
        IEnumerable<DO.LineStation> GetAllLineStations();

       
        void AddLineStation(DO.LineStation LineStation);
        void UpdateLineStation(DO.LineStation LineStation);
  
        void DeleteLineStation(int id); // removes only LineStation, does not remove the appropriate Person...
        void DeleteListLineStations(int station);
        #endregion LineStationStation
        #region AdjacentStation
        int CountplusAdjacentStation();
         double CalculateDist(DO.Station stat1, DO.Station stat2);
        DO.AdjacentStations GetAdjacentStations(int Station1, int Station2);
        IEnumerable<DO.AdjacentStations> GetAllAdjacentStations();

        void AddAdjacentStations(DO.AdjacentStations AdjStation);
        void UpdateAdjacentStations(DO.AdjacentStations AdjacentStations);
        
        void DeleteAdjacentStations(int id); // removes only AdjacentStations, does not remove the appropriate Person...

        #endregion AdjacentStation

        #region LineTrip
        DO.LineTrip GetLineTrip(int id);
        IEnumerable<DO.LineTrip> GetAllLineTrips();

        void AddLineTrip(DO.LineTrip LineTrip);
        void UpdateLineTrip(DO.LineTrip LineTrip);
        
        void DeleteLineTrip(int id); // removes only LineTrip, does not remove the appropriate Person...


        #endregion LineTrip
        #region Trip
        DO.Trip GetTrip(int id);
        IEnumerable<DO.Trip> GetAllTrips();

        void AddTrip(DO.Trip Trip);
        void UpdateTrip(DO.Trip Trip);
        
        void DeleteTrip(int id); // removes only Trip, does not remove the appropriate Person...

        #endregion Trip
        #region Bus
       
        DO.Bus GetBus(int licensenum);
        IEnumerable<DO.Bus> GetAllBuses();

        void AddBus(DO.Bus Bus);
        void UpdateBus(DO.Bus Bus);
      
        void DeleteBus(int licensenum); // removes only Bus, does not remove the appropriate Person...
        #endregion Bus 
        #region User
        DO.User GetUser(string username);
        IEnumerable<DO.User> GetAllUsers();

        void AddUser(DO.User User);
        void UpdateUser(DO.User User);
     
        void DeleteUser(string username); // removes only User, does not remove the appropriate Person...

        #endregion User

    }
}
