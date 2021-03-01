﻿using BLAPI;
using BO;
using DLAPI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL
{
    class BLImp : IBL 
    {
        IDL dl = DLFactory.GetDL();
        public List<BO.Areas> GetAreas() // return a list of all the areas in the window 8 and window 12
        {
            IEnumerable<DO.Areas> ListAreas = dl.GetAllAreas();
            BO.Areas AreaBO = new BO.Areas();
            List<BO.Areas> ListAreasBO = new List<BO.Areas>();
            foreach (var item in ListAreas)
            {
                AreaBO = (BO.Areas)item;
                ListAreasBO.Add(AreaBO);
            }
            return ListAreasBO;

        }
        #region Line 
        public BO.ShowStationsLine ShowStations(BO.Line line)// this function receives a line and return a object ShowStationLine with all the stations in this line,
                                                             // distances and times with the next stations
        {
            BO.ShowStationsLine stationslineBO = new BO.ShowStationsLine();
            BO.Station stationBO = new BO.Station();

            IEnumerable<DO.Line> ListofLines = dl.GetAllLines();
            IEnumerable<DO.Line> linesDO = (IEnumerable<DO.Line>)ListofLines.Where(x => x.Id == line.Id);
            int currentstation;
            int id = linesDO.FirstOrDefault().Id;
            currentstation = linesDO.FirstOrDefault().FirstStation;
            IEnumerable<DO.LineStation> linestationsDO = dl.GetAllLineStations();
            IEnumerable<DO.LineStation> linestationDO;
            while (currentstation != linesDO.FirstOrDefault().LastStation) // we pass on the list of line stations to get all the stations in the line sending ,
                                                                           // we change all the stations from DO.Station to BO.Station
                                                                           // and with adjacent stations we know distances and times with the next station
            {
                linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == currentstation));
                DO.Station stationDO = dl.GetStation(linestationDO.FirstOrDefault().Station);
                stationBO.Address = stationDO.Address;
                stationBO.Code = stationDO.Code;
                stationBO.Name = stationDO.Name;
                stationBO.Latitude = stationDO.Latitude;
                stationBO.Longitude = stationDO.Longitude;
                stationBO.Area = (BO.Areas)stationDO.Area;
                stationslineBO.ListStat.Add(stationBO);
                int nextstation = linestationDO.FirstOrDefault().NextStation;
                stationBO = new BO.Station();
                IEnumerable<DO.AdjacentStations> adjstationsDO = dl.GetAllAdjacentStations();
                IEnumerable<DO.AdjacentStations> adjstationDO = adjstationsDO.Where(x => (x.Station1 == currentstation) && (x.Station2 == nextstation));
                stationslineBO.ListDistances.Add(adjstationDO.FirstOrDefault().Distance);
                stationslineBO.ListTimes.Add(adjstationDO.FirstOrDefault().Time);
                currentstation = nextstation;
                linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == nextstation));
            }
            linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == linesDO.FirstOrDefault().LastStation));
            DO.Station stationDO2 = dl.GetStation(linestationDO.FirstOrDefault().Station);
            stationBO.Address = stationDO2.Address;
            stationBO.Code = stationDO2.Code;
            stationBO.Name = stationDO2.Name;
            stationBO.Latitude = stationDO2.Latitude;
            stationBO.Longitude = stationDO2.Longitude;
            stationBO.Area = (BO.Areas)stationDO2.Area;
            stationslineBO.ListStat.Add(stationBO);
            return stationslineBO;

        }
        
        public List<BO.Line> GetAllLineInStation(int index) // we return a list of all line numbers passing in a station according to the index received 
        {

            BO.ShowStations SS = ShowBusStations();
            return SS.linesNumbers[index];

        }
        public List<BO.Station> GetAllStationInATravel(int index, BO.Station StationStart, BO.Station StationFinish) // we return a list of all stations between StationStart
                                                                                                                     // and StationFinish
        {

            BO.TravelBetween2Stations TB=ShowTravelsBetween2Stations(StationStart,StationFinish);
            return TB.ListStations[index];
        }
        public List<string> GetAllLastStationInLine(int index) // we return a list of all last stations names of the line passing in a station according to the index received 

        {

            BO.ShowStations SS = ShowBusStations();
            return SS.lastStationNames[index];

        }

        public BO.ShowStations ShowBusStations()// This function return an object ShowStations with 4 lists
        {
            BO.ShowStations ss = new BO.ShowStations();
            List<BO.Station> listStationBO = new List<BO.Station>();
            IEnumerable<DO.Station> listStationDO = dl.GetAllStations();

            foreach (var item in listStationDO) // we convert all stations from DO.Station to BO.Station
            {
                BO.Station StationBO = new BO.Station();
                StationBO.Code = item.Code;
                StationBO.Name = item.Name;
                StationBO.Address = item.Address;
                StationBO.Longitude = item.Longitude;
                StationBO.Latitude = item.Latitude;
                StationBO.Area = (BO.Areas)item.Area;
                listStationBO.Add(StationBO);
                StationBO = new BO.Station();

            }
            listStationBO = listStationBO.OrderBy(x => x.Area.ToString()).ToList(); 
            ss.stations = listStationBO;

            List<BO.AdjacentStations> listAdjStationBO = new List<BO.AdjacentStations>();
            IEnumerable<DO.AdjacentStations> listAdjStationDO = dl.GetAllAdjacentStations();

            foreach (var item in listAdjStationDO)   // we convert all adjacent stations from DO.AdjacentStations to BO.AdjacentStations
            {
                BO.AdjacentStations AdjStationBO = new BO.AdjacentStations();
                AdjStationBO.Distance = item.Distance;
                AdjStationBO.id = item.id;
                AdjStationBO.Station1 = item.Station1;
                AdjStationBO.Station2 = item.Station2;
                AdjStationBO.Time = item.Time;
                listAdjStationBO.Add(AdjStationBO);
                AdjStationBO = new BO.AdjacentStations();

            }
            ss.adjStations = listAdjStationBO;

            int codeLine, lineId, codeLastStation; 
            List<BO.Line> ListCodeLines = new List<BO.Line>();

            List<string> ListNameOfLastStations = new List<string>();

            IEnumerable<DO.LineStation> listLineStationDO = dl.GetAllLineStations();
            foreach (var item in listStationBO)  // we pass from list of stations to list of line stations to get all lines and their last stations passing throught station
            {
                codeLine = item.Code;  
                 ListCodeLines = new List<BO.Line>();
                ListNameOfLastStations = new List<string>();
                foreach (var item1 in listLineStationDO)
                {

                    if (item1.Station == codeLine)
                    {
                        lineId = item1.LineId;


                        foreach (var item2 in dl.GetAllLines())  // we convert all lines from DO.Line to BO.Line
                        {
                            if (item2.Id == lineId)
                            {
                                BO.Line LineBO = new BO.Line();
                                LineBO.Id = item2.Id;
                                LineBO.Code = item2.Code;
                                LineBO.Area = (BO.Areas)item2.Area;
                                LineBO.FirstStation = item2.FirstStation;
                                LineBO.LastStation = item2.LastStation;
                                LineBO.CountStation = item2.CountStation;
                                ListCodeLines.Add(LineBO);
                                codeLastStation = item2.LastStation;
                                foreach (var item3 in dl.GetAllStations())
                                {
                                    if (item3.Code == codeLastStation)
                                    {
                                        ListNameOfLastStations.Add(item3.Name);
                                    }
                                }
                            }


                        }


                    }

                }
                ss.linesNumbers.Add(ListCodeLines);
                ss.lastStationNames.Add(ListNameOfLastStations);

            }
            return ss;


        }
        public void UpdateLineCode(BO.Line Line, int code) // we use this function in window 6 to update line code
        {
            DO.Line LineDO = dl.GetLine(Line.Id);
            DO.Line NewLine = new DO.Line();
            NewLine.Id = LineDO.Id;
            NewLine.Code = code;
            NewLine.Area = LineDO.Area;
            NewLine.FirstStation = LineDO.FirstStation;
            NewLine.LastStation = LineDO.LastStation;
            NewLine.CountStation = LineDO.CountStation;
            IEnumerable<DO.Line> listLines = dl.GetAllLines().Where(x => (x.Code == code) && (x.Area == (DO.Areas)Line.Area));


            if ((Line.Code != NewLine.Code) && (listLines.FirstOrDefault() == null) || (Line.Code == NewLine.Code)) 
                dl.UpdateLine(NewLine);
            else
                throw new BO.BadLineIdException(NewLine.Code, "Bad Line code"); // we can't update line code with same code and same area
            
        }
          
        public IEnumerable<BO.Line> GetAllLines() //return all the lines
        {
            BO.Line LineBO = new BO.Line();
            return from LineDO in dl.GetAllLines()
                   select new BO.Line
                   { Id = LineDO.Id, Code = LineDO.Code, Area = (BO.Areas)LineDO.Area, FirstStation = LineDO.FirstStation, LastStation = LineDO.LastStation, CountStation = LineDO.CountStation };
           

        }

        public IEnumerable<BO.Station> AddLineFirst(int code, BO.Areas area, BO.Station firstStation)// we use this function in window 8 to add new line
                                                                                                     // As soon as we choose a first station this function creates a line 
                                                                                                     // and return a list of all stations the area received without the first station
        {
            BO.Line Line = new BO.Line();
            Line.Id = dl.Countplus();
            Line.Code = code;
            Line.Area = area;
            Line.FirstStation = firstStation.Code;
            Line.LastStation = -1;
            Line.CountStation = 1;
            DO.LineStation FirstLineStation = new DO.LineStation();
            FirstLineStation.Id = dl.CountplusLineStation();
            FirstLineStation.LineId = Line.Id;
            FirstLineStation.LineStationIndex = 1;
            FirstLineStation.NextStation = -1;
            FirstLineStation.PrevStation = -1;
            FirstLineStation.Station = firstStation.Code;
            dl.AddLineStation(FirstLineStation);
            
            DO.Line LineDO = new DO.Line();
            LineDO.Id = Line.Id;
            LineDO.Code = code;
            LineDO.Area = (DO.Areas)area;
            LineDO.FirstStation = firstStation.Code;
            LineDO.LastStation = -1;
            LineDO.CountStation = 1;

           
            try
            {
                dl.AddLine(LineDO);
            }
            catch (DO.BadLineIdException ex) // if we can't add the line as we delete the line station that we create at the line 235
            {
                dl.DeleteLineStation(FirstLineStation.Id);
                throw new BO.BadLineIdException(LineDO.Id, "Bad line Id", ex);
            }
            IEnumerable<BO.Station> listStation = ShowStationArea(Line); 
            return listStation;
        }
        public void AddLine(int code, BO.Station FirstStation, BO.Station LastStation)// this function add a line
        {

            DO.Line Line = new DO.Line();
            Line = dl.GetAllLines().Where(x => (x.Code == code) && (x.FirstStation == FirstStation.Code) && (x.LastStation == -1)).FirstOrDefault();// we search the line that we created in the function AddLine First
            DO.Line line = new DO.Line();
            line.Area = Line.Area;
            line.Code = Line.Code;
            line.FirstStation = Line.FirstStation;
            line.LastStation = LastStation.Code;
            line.Id = Line.Id;
            line.CountStation = 2;
            try
            {
                dl.UpdateLine(line); // we add all missing informations
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(line.Code, "Bad line Code", ex);
            }
           
            IEnumerable<DO.LineStation> lineStationDO = dl.GetAllLineStations().Where(x => x.LineId == Line.Id);
            DO.LineStation lineStation1 = new DO.LineStation();
            lineStation1.Id = lineStationDO.FirstOrDefault().Id;
            lineStation1.LineId = lineStationDO.FirstOrDefault().LineId;
            lineStation1.Station = lineStationDO.FirstOrDefault().Station;
            lineStation1.LineStationIndex = 1;
            lineStation1.PrevStation = -1;
            lineStation1.NextStation = LastStation.Code;

            DO.LineStation lineStation2 = new DO.LineStation();
            lineStation2.Id = dl.CountplusLineStation();
            lineStation2.LineId = line.Id;
            lineStation2.Station = LastStation.Code;
            lineStation2.LineStationIndex = 2;
            lineStation2.PrevStation = line.FirstStation;
            lineStation2.NextStation = -1;
            dl.UpdateLineStation(lineStation1);
            dl.AddLineStation(lineStation2);
  
            DO.AdjacentStations adjacentStation = new DO.AdjacentStations();

            adjacentStation.Distance = (dl.CalculateDist(dl.GetStation(line.FirstStation), dl.GetStation(LastStation.Code)));
            //speed=20km/h=>333.33m/min

            double time = adjacentStation.Distance / 333.33;
            double timeinseconds = time * 60;
            int m = (int)time;
            int s = (int)timeinseconds - m * 60;
            
            adjacentStation.Time = new TimeSpan(0, m, s);
          
            adjacentStation.id = dl.CountplusAdjacentStation();
            adjacentStation.Station1 = line.FirstStation;
            adjacentStation.Station2 = LastStation.Code;
            dl.AddAdjacentStations(adjacentStation);
        }

        public void UpdateLine(BO.Line Line, BO.LineStation DeletedStation, BO.Station NewStation)// we change the DeletedStation by the NewStation
        {

            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations();
           
            DO.LineStation NewLineStation = new DO.LineStation();

            NewLineStation.Id = DeletedStation.Id;
            NewLineStation.LineId = DeletedStation.LineId;
            NewLineStation.LineStationIndex = DeletedStation.LineStationIndex;
            NewLineStation.NextStation = DeletedStation.NextStation;
            NewLineStation.PrevStation = DeletedStation.PrevStation;
            NewLineStation.Station = NewStation.Code;
            dl.UpdateLineStation(NewLineStation);
            DO.Line NewLine = new DO.Line();
            NewLine.Code = Line.Code;
            NewLine.Area = (DO.Areas)Line.Area;
            NewLine.Id = Line.Id;
            NewLine.CountStation = Line.CountStation;
            BO.AdjacentStations adjacentStationsBO1 = new BO.AdjacentStations();
           
            BO.AdjacentStations adjacentStationsBO2 = new BO.AdjacentStations();
            
            if ((NewLineStation.PrevStation == -1) || (NewLineStation.NextStation == -1))
            {
                if (NewLineStation.PrevStation == -1)  // if the station that we updated is a first station
                {
                    NewLine.FirstStation = NewStation.Code; // we change the first station by the NewStation
                    NewLine.LastStation = Line.LastStation;
                    adjacentStationsBO1.Station1 = NewLineStation.Station;
                    adjacentStationsBO1.Station2 = NewLineStation.NextStation;
                   
                    AddAdjacentStations(adjacentStationsBO1); 
                    DO.LineStation nextLineStation = new DO.LineStation();
                    nextLineStation = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == DeletedStation.NextStation).FirstOrDefault();
                    DO.LineStation stationDO = new DO.LineStation();
                    stationDO.Id = nextLineStation.Id;
                    stationDO.LineId = nextLineStation.LineId;
                    stationDO.LineStationIndex = nextLineStation.LineStationIndex;
                    stationDO.PrevStation = NewLineStation.Station;
                    stationDO.Station = nextLineStation.Station;
                    stationDO.NextStation = nextLineStation.NextStation;
                    dl.UpdateLineStation(stationDO);


                }

                else // if the station that we updated is a last station
                {
                    NewLine.LastStation = NewStation.Code; // we change the last station by the NewStation
                    NewLine.FirstStation = Line.FirstStation;
                    adjacentStationsBO1.Station1 = NewLineStation.PrevStation;
                    adjacentStationsBO1.Station2 = NewLineStation.Station;
                   
                    AddAdjacentStations(adjacentStationsBO1);
                    DO.LineStation prevLineStation = new DO.LineStation();
                    prevLineStation = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == DeletedStation.PrevStation).FirstOrDefault();
                    DO.LineStation stationDO = new DO.LineStation();
                    stationDO.Id = prevLineStation.Id;
                    stationDO.LineId = prevLineStation.LineId;
                    stationDO.LineStationIndex = prevLineStation.LineStationIndex;
                    stationDO.PrevStation = prevLineStation.PrevStation;
                    stationDO.Station = prevLineStation.Station;
                    stationDO.NextStation = NewLineStation.Station;
                    dl.UpdateLineStation(stationDO);

                }
            }
            else // if the station that we update is between 2 stations
            {
                NewLine.FirstStation = Line.FirstStation;
                NewLine.LastStation = Line.LastStation;
                adjacentStationsBO1.Station1 = NewLineStation.PrevStation;
                adjacentStationsBO1.Station2 = NewLineStation.Station;
                
                AddAdjacentStations(adjacentStationsBO1);
                DO.LineStation nextLineStation = new DO.LineStation();
                nextLineStation = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == DeletedStation.NextStation).FirstOrDefault();
                DO.LineStation stationDO = new DO.LineStation();
                stationDO.Id = nextLineStation.Id;
                stationDO.LineId = nextLineStation.LineId;
                stationDO.LineStationIndex = nextLineStation.LineStationIndex;
                stationDO.PrevStation = NewLineStation.Station;
                stationDO.Station = nextLineStation.Station;
                stationDO.NextStation = nextLineStation.NextStation;
                dl.UpdateLineStation(stationDO);

                adjacentStationsBO2.Station1 = NewLineStation.Station;
                adjacentStationsBO2.Station2 = NewLineStation.NextStation;
                
                AddAdjacentStations(adjacentStationsBO2);

                DO.LineStation prevLineStation = new DO.LineStation();
                prevLineStation = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == DeletedStation.PrevStation).FirstOrDefault();
                DO.LineStation stationDO2 = new DO.LineStation();
                stationDO2.Id = prevLineStation.Id;
                stationDO2.LineId = prevLineStation.LineId;
                stationDO2.LineStationIndex = prevLineStation.LineStationIndex;
                stationDO2.PrevStation = prevLineStation.PrevStation;
                stationDO2.Station = prevLineStation.Station;
                stationDO2.NextStation = NewLineStation.Station;
                dl.UpdateLineStation(stationDO2);

            }
            
            try
            {
                dl.UpdateLine(NewLine);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(NewLine.Code, "Bad line Code", ex);
            }

           
        }
        public void DeleteLine(int id) // this function delete a line
        {

            dl.DeleteLineStation(id);
            dl.DeleteLine(id);
            dl.DeleteLineTrip(id);

        }

        public BO.Line GetLine(int id) // this function return a line according to an id 
        {
            DO.Line LineDO = dl.GetLine(id);
            BO.Line LineBO = new BO.Line();
            LineBO.Area = (BO.Areas)LineDO.Area;
            LineBO.Code = LineDO.Code;
            LineBO.CountStation = LineDO.CountStation;
            LineBO.FirstStation = LineDO.FirstStation;
            LineBO.LastStation = LineDO.LastStation;
            LineBO.Id = LineDO.Id;
            return LineBO;
        }

        public Simulator LinesFromStation(BO.Station Station)
        {
            Simulator sim = new Simulator();
            IEnumerable<BO.Line> listLine= from lineStation in dl.GetAllLineStations()
                   where lineStation.Station == Station.Code
                   let line = dl.GetLine(lineStation.LineId)
                   select new BO.Line
                   {
                       Id = line.Id,
                       Code = line.Code,
                       Area = (BO.Areas)line.Area,
                       FirstStation = line.FirstStation,
                       LastStation = line.LastStation,
                       CountStation = line.CountStation
                   };
            sim.ListLines = listLine;
            double dist=0;
            DO.Station station1 = new DO.Station();
            DO.Station station2 = new DO.Station();
            List<double> listDistance = new List<double>();
            List<TimeSpan> listTimes = new List<TimeSpan>();
            foreach (var item in listLine)
            {
                foreach (var item1 in dl.GetAllLineStations().Where(x => x.LineId == item.Id))
                {
                    if (item1.Station!=Station.Code)
                    {
                        station1 = new DO.Station();
                        station1 = dl.GetStation(item1.Station);
                        station2 = new DO.Station();
                        station2 = dl.GetStation(item1.NextStation);
                        dist += dl.CalculateDist(station1, station2);
                       
                    }
                    else
                        break;
                }
                //speed=20km/h=>333.33m/min

                double time = dist / 333.33;
                int timeinseconds =(int) time * 60;
                TimeSpan t = new TimeSpan();
                t = new TimeSpan(0, 0, timeinseconds);
                sim.ListArrivalTimes.Add(t);
                dist = 0;
            }

            return sim;
            
        }
        public List<List<TimeSpan>> StartSimulator(TimeSpan ActualTime, Simulator sim)
        {
            List<TimeSpan> listTimesInside = new List<TimeSpan>();
            List<List<TimeSpan>> lineTripTimes = new List<List<TimeSpan>>();
            List<List<TimeSpan>> listTimes = new List<List<TimeSpan>>();
            List<TimeSpan> listTripInside = new List<TimeSpan>();
            int i = 0;
           foreach(var item in sim.ListLines)
            {
                listTripInside = new List<TimeSpan>();
                foreach (var item1 in dl.GetAllLineTrips())
                {
                    if (item1.LineId == item.Id)
                    {
                        
                        listTripInside.Add(item1.StartAt + sim.ListArrivalTimes[i]);
                    }
                }
                i ++;
                
                lineTripTimes.Add(listTripInside);
            }
            for (int j = 0; j < lineTripTimes.Count; j++)
            {
                listTimesInside = new List<TimeSpan>();
                foreach (var item in lineTripTimes[j])
                {
                   
                    if (ActualTime < item)
                    {
                        TimeSpan t = new TimeSpan();
                        t = (item - ActualTime);

                        listTimesInside.Add(t);
                    }
                    else
                    {

                        TimeSpan t = new TimeSpan();
                        TimeSpan day = new TimeSpan(1, 0, 0, 0);

                        t = (day- ActualTime)+item;

                        listTimesInside.Add(t);
                    }

                }
                listTimes.Add(listTimesInside);
            }
            return listTimes;
        }
        public List<List<TimeSpan>> ListTimer(List<List<TimeSpan>> listTimeSpan, TimeSpan t)
        {
            TimeSpan template = new TimeSpan();
            List<TimeSpan> ListInside = new List<TimeSpan>();
            List < List < TimeSpan >> ListFinal = new List<List<TimeSpan>>();
            for (int i = 0; i < listTimeSpan.Count(); i++)
            {
                ListInside = new List<TimeSpan>();
                foreach (var item in listTimeSpan[i])
                {
                    template = item;
                    template -= t;
                    ListInside.Add(template);
                }
                ListFinal.Add(ListInside);
            }
            return ListFinal;
        }
        public ListTimer ListTimerMinimun(List<List<TimeSpan>> ListFinal,IEnumerable<BO.Line> ListLines)
        {
            ListTimer ListTimerObject = new ListTimer();
            List<TimeSpan> ListMin = new List<TimeSpan>();
            
            List<BO.Line> ListLinesNumber = new List<BO.Line>();
            ListLinesNumber = ListLines.ToList();
            TimeSpan timeZero = new TimeSpan(0, 0, 0);
            for (int i = 0; i < ListFinal.Count(); i++)
            {
               
                if ((ListFinal[i].Count != 0)&&(ListFinal[i].Min() > timeZero))
                      ListMin.Add(ListFinal[i].Min());
                
                if ((ListFinal[i].Count != 0)&& (ListFinal[i].Min() < timeZero))
                {

                    ListFinal[i].Remove(ListFinal[i].Min());
                    if (ListFinal[i].Count != 0)
                        ListMin.Add(ListFinal[i].Min());
                    else
                        ListLinesNumber.RemoveAt(i);

                }
               
                
            }
            ListTimerObject.listTimer= ListMin;
            ListTimerObject.ListLines = ListLinesNumber;
            return ListTimerObject;

        }

       
                
        #endregion Line
        #region AdjacentStation
       public void AddAdjacentStations(BO.AdjacentStations AdjacentStation) // this function add an adjacent station 
        {
            DO.Station station1 = new DO.Station();
            DO.Station station2 = new DO.Station();
            foreach (var item in dl.GetAllStations())
            {
                if (item.Code == AdjacentStation.Station1)
                    station1 = item;
                if (item.Code == AdjacentStation.Station2)
                    station2 = item;
            }
            double distance = dl.CalculateDist(station1, station2);
            AdjacentStation.Distance = distance;
          
            double time = AdjacentStation.Distance / 333.33;
            double timeinseconds = time * 60;
            int m = (int)time;
            int s = (int)timeinseconds - m * 60;
          
            AdjacentStation.Time = new TimeSpan(0, m, s);
          
            DO.AdjacentStations adjacentStationsDO = new DO.AdjacentStations();
            adjacentStationsDO.Distance = AdjacentStation.Distance;
            adjacentStationsDO.id =dl.CountplusAdjacentStation();
            adjacentStationsDO.Station1 = AdjacentStation.Station1;
            adjacentStationsDO.Station2 = AdjacentStation.Station2;
            adjacentStationsDO.Time = AdjacentStation.Time;
            dl.AddAdjacentStations(adjacentStationsDO);

        }
        public void UpdateTimeAndDistanceAdjStations(BO.AdjacentStations AdjacentStation)// this function calculate the real time and distance from the longitude and latitude
        {
            DO.Station station1 = new DO.Station();
            DO.Station station2 = new DO.Station();
            foreach (var item in dl.GetAllStations())
            {
                if (item.Code == AdjacentStation.Station1)
                    station1 = item;
                if (item.Code == AdjacentStation.Station2)
                    station2 = item;
            }
            double distance = dl.CalculateDist(station1, station2);
            AdjacentStation.Distance = distance;
            //speed =20km/h=>333.33m/min
            double time = AdjacentStation.Distance / 333.33;
            double timeinseconds = time * 60;
            int m = (int)time;
            int s = (int)timeinseconds - m * 60;
          
            AdjacentStation.Time = new TimeSpan(0, m, s);
            DO.AdjacentStations adjacentStationsDO = new DO.AdjacentStations();
            adjacentStationsDO.Distance = AdjacentStation.Distance;
            adjacentStationsDO.id = AdjacentStation.id;
            adjacentStationsDO.Station1 = AdjacentStation.Station1;
            adjacentStationsDO.Station2 = AdjacentStation.Station2;
            adjacentStationsDO.Time = AdjacentStation.Time;
            dl.UpdateAdjacentStations(adjacentStationsDO);

        }
        #endregion AdjacentStation
        #region Station
        public IEnumerable<BO.Line> ShowLineInArea (BO.Areas Area)//this function return all the lines according to the area sent
        {
            IEnumerable<IGrouping<DO.Areas, DO.Line>> liststations = (IEnumerable < IGrouping < DO.Areas, DO.Line>>)dl.GetAllLines().ToList().GroupBy(x => x.Area);
            List<DO.Line> listlines = new List<DO.Line>();
            foreach (var item in liststations)
            {
                if (item.Key == (DO.Areas)Area)
                    listlines = item.ToList();
            }
            IEnumerable<BO.Line> listLines = from LineDO in listlines
                                                   select new BO.Line

                                                   { Code = LineDO.Code, Id = LineDO.Id, Area = (BO.Areas)LineDO.Area, FirstStation = LineDO.FirstStation, LastStation =LineDO.LastStation, CountStation=LineDO.CountStation};
            return listLines;

        }
        public void AddStation(int code, string name, double longitude, double latitude, string address, BO.Areas area)// this function add a new station
        {
            DO.Station station = new DO.Station();
            station.Code = code;
            station.Name = name;
            station.Longitude = longitude;
            station.Latitude = latitude;
            station.Address = address;
            station.Area = (DO.Areas)area;
            try
            {
                dl.AddStation(station);
            }
            catch (DO.BadStationCodeException ex)// we can't add a duplicate code station
            {
                throw new BO.BadStationCodeException(station.Code, "Bad Station code", ex);
            }
         
        }
        public void DeleteStation(int code) // this function deletes a station
        {
            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations();
            List<DO.LineStation> ListLineStationsDeleted = dl.GetAllLineStations().Where(x => x.Station == code).ToList();
            DO.LineStation LineStationDO2 = new DO.LineStation();
            DO.LineStation LineStationDO1 = new DO.LineStation();
            BO.AdjacentStations adjStation = new BO.AdjacentStations();
            foreach (var item in ListLineStationsDeleted)
            {
                if (item.PrevStation == -1)// if the station deleted is the first station in a line
                {
                    IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex == 2);


                    LineStationDO2.Id = lineStation2.FirstOrDefault().Id;
                    LineStationDO2.LineId = lineStation2.FirstOrDefault().LineId;
                    LineStationDO2.LineStationIndex = 1;
                    LineStationDO2.PrevStation = item.PrevStation; // we update the previous station of the next station of the station deleted
                    LineStationDO2.NextStation = lineStation2.FirstOrDefault().NextStation;
                    LineStationDO2.Station = lineStation2.FirstOrDefault().Station;

                    DO.Line line = dl.GetAllLines().Where(x => x.Id == item.LineId).FirstOrDefault();
                    DO.Line Line = new DO.Line();
                    Line.Area = line.Area;
                    Line.Code = line.Code;
                    Line.CountStation = --line.CountStation;
                    Line.FirstStation = lineStation2.FirstOrDefault().Station;
                    Line.LastStation = line.LastStation;
                    Line.Id = line.Id;
                    dl.UpdateLine(Line);
                    dl.UpdateLineStation(LineStationDO2);

                }
                if (item.NextStation == -1) // if the station deleted is the last station in a line
                {
                    IEnumerable<DO.LineStation> lineStation1 = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex == item.LineStationIndex - 1);
                   
                    LineStationDO1.Id = lineStation1.FirstOrDefault().Id;
                    LineStationDO1.LineId = lineStation1.FirstOrDefault().LineId;
                    LineStationDO1.LineStationIndex = lineStation1.FirstOrDefault().LineStationIndex;
                    LineStationDO1.PrevStation = lineStation1.FirstOrDefault().PrevStation;
                    LineStationDO1.NextStation = item.NextStation; // we update the next station of the previous station of the station deleted
                    LineStationDO1.Station = lineStation1.FirstOrDefault().Station;

                    DO.Line line = dl.GetAllLines().Where(x => x.Id == item.LineId).FirstOrDefault();
                    DO.Line Line = new DO.Line();
                    Line.Area = line.Area;
                    Line.Code = line.Code;
                    Line.CountStation = --line.CountStation;
                    Line.FirstStation = line.FirstStation;
                    Line.LastStation = lineStation1.FirstOrDefault().Station;
                    Line.Id = line.Id;
                    dl.UpdateLine(Line);
                    dl.UpdateLineStation(LineStationDO1);

                }
                else
                {
                    if ((item.PrevStation != -1) && (item.NextStation != -1)) // if the station deleted is between 2 stations
                    {
                        IEnumerable<DO.LineStation> lineStation1 = ListLineStations.Where(x => x.LineId == (item.LineId) && (x.LineStationIndex == item.LineStationIndex - 1));
                        IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => (x.LineId == item.LineId) && (x.LineStationIndex == item.LineStationIndex + 1));
                        DO.LineStation PreLineStation = new DO.LineStation();
                        PreLineStation.Id = lineStation1.FirstOrDefault().Id;
                        PreLineStation.LineId = lineStation1.FirstOrDefault().LineId;
                        PreLineStation.LineStationIndex = lineStation1.FirstOrDefault().LineStationIndex;
                        PreLineStation.PrevStation = lineStation1.FirstOrDefault().PrevStation;
                        PreLineStation.NextStation = item.NextStation;
                        PreLineStation.Station = lineStation1.FirstOrDefault().Station;


                        DO.LineStation LineStationDO3 = new DO.LineStation();
                        LineStationDO3.Id = lineStation2.FirstOrDefault().Id;
                        LineStationDO3.LineId = lineStation2.FirstOrDefault().LineId;
                        LineStationDO3.LineStationIndex = lineStation2.FirstOrDefault().LineStationIndex;
                        LineStationDO3.PrevStation = item.PrevStation;
                        LineStationDO3.NextStation = lineStation2.FirstOrDefault().NextStation;
                        LineStationDO3.Station = lineStation2.FirstOrDefault().Station;


                        dl.UpdateLineStation(PreLineStation);

                        dl.UpdateLineStation(LineStationDO3);
                        adjStation.Station1 = PreLineStation.Station;
                        adjStation.Station2 = LineStationDO3.Station;
                        AddAdjacentStations(adjStation);

                    }
                }
                List<DO.LineStation> lineStationsUpdateIndex = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex > item.LineStationIndex).ToList();
                foreach (var item1 in lineStationsUpdateIndex) // we update all the index of line stations after the station deleted
                {

                    DO.LineStation LineStationDO = new DO.LineStation();
                    LineStationDO.Id = item1.Id;
                    LineStationDO.LineId = item1.LineId;
                    LineStationDO.LineStationIndex = --item1.LineStationIndex;
                    LineStationDO.NextStation = item1.NextStation;
                    LineStationDO.PrevStation = item1.PrevStation;
                    LineStationDO.Station = item1.Station;
                    dl.UpdateLineStation(LineStationDO);

                }

            }

            dl.DeleteListLineStations(code);
            IEnumerable<DO.LineStation> L = dl.GetAllLineStations().Where(x => x.Station == code);
            dl.DeleteStation(code);



        }

            public IEnumerable<BO.Station> ShowStationArea(BO.Line line) // this function return a list of all station with similar area of the line sent 
                                                                         // without stations that are in this line
        {
           IEnumerable<IGrouping<DO.Areas, DO.Station>> liststations = (IEnumerable <IGrouping< DO.Areas, DO.Station >>) dl.GetAllStations().ToList().GroupBy(x => x.Area);
            List<DO.Station> liststat = new List<DO.Station>();
            foreach(var item in liststations)
            {
                if (item.Key == (DO.Areas)line.Area)
                    liststat = item.ToList();
            }
                
           

            IEnumerable<BO.Station> listStations = from StationDO in liststat
                                                   select new BO.Station

                                                   { Code = StationDO.Code, Name = StationDO.Name, Longitude = StationDO.Longitude, Latitude = StationDO.Latitude, Address = StationDO.Address, Area = (BO.Areas)StationDO.Area };

           
            IEnumerable<DO.LineStation> ListLineStation = dl.GetAllLineStations().Where(x => x.LineId == line.Id);

            IEnumerable<BO.Station> ListStationLine = from StationLineDO in dl.GetAllStations() // we convert the list of line stations to list of stations
                                                      from LineStationDO in ListLineStation
                                                      where StationLineDO.Code == LineStationDO.Station
                                                      select new BO.Station
                                                      { Code = StationLineDO.Code, Name = StationLineDO.Name, Longitude = StationLineDO.Longitude, Latitude = StationLineDO.Latitude, Address = StationLineDO.Address, Area = (BO.Areas)StationLineDO.Area };

            IEnumerable<BO.Station> listStation = listStations.Where(p => ListStationLine.All(p2 => p2.Code != p.Code));

            return listStation;


        }

        public void UpdateStation(BO.Station StationToUpdate,  string name, string address) // this function updates the name and the address of a station
        {
            DO.Station StationDO = new DO.Station();
            StationDO.Area = (DO.Areas)StationToUpdate.Area;
            StationDO.Longitude = StationToUpdate.Longitude;
            StationDO.Latitude = StationToUpdate.Latitude;
            StationDO.Code = StationToUpdate.Code;
            StationDO.Name = name;
            StationDO.Address = address;
            try
            {
                dl.UpdateStation(StationDO);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(StationDO.Code, "Bad Station code", ex);
            }
           

        }

        public IEnumerable<BO.Station> GetStationWithArea(BO.Areas area) // this function return a list of all stations according to the area sent
        {

            IEnumerable<DO.Station> listStationDO = dl.GetAllStations().Where(x => (BO.Areas)x.Area == area);
            IEnumerable<BO.Station> ListStationBO = from StationDO in listStationDO
                                                    select new BO.Station
                                                    { Code = StationDO.Code, Name = StationDO.Name, Longitude = StationDO.Longitude, Latitude = StationDO.Latitude, Address = StationDO.Address, Area = (BO.Areas)StationDO.Area };

            return ListStationBO;
        }
        public List<BO.Station> GetAllStationWithoutStartStation(BO.Station StationStart) // this function the stations without the StationStart
        {
            IEnumerable<BO.Station> listStations = from StationDO in dl.GetAllStations()
                                                   let Code=StationStart.Code
                                                   where StationDO.Code != Code
                                                   select new BO.Station
                                                   { Code = StationDO.Code, Name = StationDO.Name, Longitude = StationDO.Longitude, Latitude = StationDO.Latitude, Address = StationDO.Address, Area = (BO.Areas)StationDO.Area };

            return listStations.ToList();
        }
        public TravelBetween2Stations ShowTravelsBetween2Stations(BO.Station StationStart, BO.Station StationFinish) // this function returns an object TravelBetweem2Stations
                                                                                                                     // with all possible routes between 2 stations 
        {
            TravelBetween2Stations Travel = new TravelBetween2Stations();
            BO.ShowStations ShowStations = new ShowStations();
            ShowStations = ShowBusStations();
            int index1 = 0, index2 = 0;
            foreach (var item in ShowStations.stations)
            {
                if (item.Code == StationStart.Code)
                    break;
                index1++;
            }
            foreach (var item in ShowStations.stations)
            {
                if (item.Code == StationFinish.Code)
                    break;
                index2++;
            }
          
            List<BO.Line> LineStations1 = ShowStations.linesNumbers[index1];
            List<BO.Line> LineStations2 = ShowStations.linesNumbers[index2];
            List<List<BO.Station>> ListStations = new List<List<BO.Station>>();
            List<BO.Station> LB = new List<BO.Station>();
            List<BO.Line> listLines = new List<BO.Line>();
            foreach (var item11 in LineStations1)
            {
                foreach (var item22 in LineStations2)
                {
                    if (item11.Id == item22.Id)
                    {
                        listLines.Add(item11);

                    }
                }
            }
            List<BO.Line> listLinesFinal = new List<BO.Line>();
          
            try
            {
                if (listLines != null)
                {

                    foreach (var item1 in listLines)
                    {

                        LB = new List<BO.Station>();

                        DO.LineStation LineStation1 = new DO.LineStation();
                        DO.LineStation LineStation2 = new DO.LineStation();
                        LineStation1 = dl.GetAllLineStations().Where(x => (x.LineId == item1.Id) && (x.Station == StationStart.Code)).FirstOrDefault();
                        LineStation2 = dl.GetAllLineStations().Where(x => (x.LineId == item1.Id) && (x.Station == StationFinish.Code)).FirstOrDefault();
                        try
                        {
                            if ((LineStation1 != null) && (LineStation2 != null))
                            {

                                if (LineStation1.LineStationIndex < LineStation2.LineStationIndex)
                                {
                                   
                                    LB.Add(StationStart);
                                    listLinesFinal.Add(item1);

                                    for (int j = LineStation1.LineStationIndex; j < LineStation2.LineStationIndex; j++)
                                    {
                                        DO.LineStation LineStationNext = new DO.LineStation();
                                        LineStationNext = dl.GetAllLineStations().Where(x => (x.LineId == item1.Id) && (x.LineStationIndex == j + 1)).FirstOrDefault();
                                        DO.Station StationNext = new DO.Station();
                                        StationNext = dl.GetStation(LineStationNext.Station);
                                        BO.Station StationNextBO = new BO.Station();
                                        StationNextBO.Address = StationNext.Address;
                                        StationNextBO.Area = (BO.Areas)StationNext.Area;
                                        StationNextBO.Code = StationNext.Code;
                                        StationNextBO.Latitude = StationNext.Latitude;
                                        StationNextBO.Longitude = StationNext.Longitude;
                                        StationNextBO.Name = StationNext.Name;            
                                        LB.Add(StationNextBO);
                                       
                                    }

                                }
                                
                                
                            }
                            
                        
                        }
                        catch (DO.BadStationCodeException ex)
                        {
                            throw new BO.BadStationCodeException(StationStart.Code, "Bad Station code");
                        }
                        if (LB.Count() != 0)
                        {
                            ListStations.Add(LB);

                            Travel.ListNumberStationBetween2Stations.Add(LB.Count());
                            string last = dl.GetStation(item1.LastStation).Name;
                            Travel.ListLastStation.Add(last);
                        }

                    }
                }
                if ((ListStations.Count == 0) || (Travel.ListNumberStationBetween2Stations.Count ==0) || (listLinesFinal.Count ==0) || (Travel.ListLastStation.Count ==0))
                    throw new BO.BadStationCodeException(StationStart.Code, "Bad Station code");
                else
                {
                    Travel.ListLines = listLinesFinal;
                    Travel.ListStations = ListStations;
                    return Travel;
                }
            }

            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(StationStart.Code, "Bad Station code", ex);
            }
      
        }
        

        
        #endregion Station
        #region LineStation
        public IEnumerable<BO.LineStation> GetLineStation(int LineId) // this function returns a line station in line according to the line id
        {
           
            return from LineStationDO in dl.GetAllLineStations()
                   where LineStationDO.LineId == LineId
                   orderby LineStationDO.LineStationIndex
                   select new BO.LineStation
                   { LineId = LineStationDO.LineId, Station = LineStationDO.Station, LineStationIndex = LineStationDO.LineStationIndex, PrevStation = LineStationDO.PrevStation, NextStation = LineStationDO.NextStation, Id = LineStationDO.Id };
        }

        public BO.Line AddLineStation(BO.Line Line, BO.Station Station)//this function adds a line station at the end of list of line station in line
        {
            DO.LineStation lineStation = new DO.LineStation();
            lineStation.Id = dl.CountplusLineStation();
            lineStation.LineId = Line.Id;
            lineStation.Station = Station.Code;
            lineStation.LineStationIndex = ++Line.CountStation;
            lineStation.PrevStation = Line.LastStation;
            lineStation.NextStation = -1;
            DO.LineStation lastStation = dl.GetAllLineStations().Where(x => (x.LineId == Line.Id) && (x.Station == Line.LastStation)).FirstOrDefault();

            DO.LineStation LastStation = new DO.LineStation();
            LastStation.Id = lastStation.Id;
            LastStation.LineId = lastStation.LineId;
            LastStation.LineStationIndex = lastStation.LineStationIndex;
            LastStation.PrevStation = lastStation.PrevStation;
            LastStation.NextStation = Station.Code;
            LastStation.Station = lastStation.Station;
            
          
            dl.UpdateLineStation(LastStation);
            DO.Line LineDO = new DO.Line();
            LineDO.Area = (DO.Areas)Line.Area;
            LineDO.Code = Line.Code;
            LineDO.CountStation = Line.CountStation;
            LineDO.FirstStation = Line.FirstStation;
            LineDO.LastStation = Station.Code;
            LineDO.Id = Line.Id;
            try
            {
                dl.UpdateLine(LineDO);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(LineDO.Code, "Bad Line code", ex);
            }
           
            BO.AdjacentStations adjStation = new BO.AdjacentStations();
            adjStation.Station1 = lastStation.Station;
            adjStation.Station2 = Station.Code;
          
            AddAdjacentStations(adjStation);
            dl.AddLineStation(lineStation);

            BO.Line LineBO = new BO.Line();
            LineBO.Area = (BO.Areas)LineDO.Area;
            LineBO.Code = LineDO.Code;
            LineBO.CountStation = LineDO.CountStation;
            LineBO.FirstStation = LineDO.FirstStation;
            LineBO.LastStation = LineDO.LastStation; 
            LineBO.Id = LineDO.Id;
            return LineBO;

        }
        public void RemoveLineStation(BO.Line Line, int code) // this function remove a line station
        {
            
                IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations().Where(x => x.LineId == Line.Id);
                IEnumerable<DO.LineStation> LineStationsDeleted = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == code);
                int deletedIndex = LineStationsDeleted.FirstOrDefault().LineStationIndex + 1;
                DO.LineStation lineStationDO2 = new DO.LineStation();
            BO.AdjacentStations adjstation = new BO.AdjacentStations();

            if (Line.CountStation >= 2)
            {
                foreach (var item in ListLineStations)
                {
                    if ((LineStationsDeleted.FirstOrDefault().PrevStation == -1))
                    {
                        if (item.PrevStation == -1) // if we remove a first station 
                        {
                            IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineStationIndex == 2);
                            lineStationDO2.Id = lineStation2.FirstOrDefault().Id;
                            lineStationDO2.LineId = lineStation2.FirstOrDefault().LineId;
                            lineStationDO2.LineStationIndex = 1;
                            lineStationDO2.NextStation = lineStation2.FirstOrDefault().NextStation; 
                            lineStationDO2.PrevStation = -1; //we update the previous station of the next station of the line station deleted
                            lineStationDO2.Station = lineStation2.FirstOrDefault().Station;
                            Line.FirstStation = lineStation2.FirstOrDefault().Station;
                            DO.Line lineDO = new DO.Line();
                            lineDO.Area = (DO.Areas)Line.Area;
                            lineDO.Id = Line.Id;
                            lineDO.Code = Line.Code;
                            lineDO.FirstStation = Line.FirstStation;
                            lineDO.LastStation = Line.LastStation;
                            lineDO.CountStation = --Line.CountStation;
                            dl.UpdateLine(lineDO);
                            dl.UpdateLineStation(lineStationDO2);



                        }
                    }
                    if ((LineStationsDeleted.FirstOrDefault().NextStation == -1))
                    {
                        if (item.NextStation == -1) // if we remove a last station 
                        {
                            IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineStationIndex == item.LineStationIndex - 1);
                            lineStationDO2.Id = lineStation2.FirstOrDefault().Id;
                            lineStationDO2.LineId = lineStation2.FirstOrDefault().LineId;
                            lineStationDO2.LineStationIndex = lineStation2.FirstOrDefault().LineStationIndex;
                            lineStationDO2.NextStation = -1;  //we update the next station of the previous station of the line station deleted
                            lineStationDO2.PrevStation = lineStation2.FirstOrDefault().PrevStation;
                            lineStationDO2.Station = lineStation2.FirstOrDefault().Station;

                            Line.LastStation = lineStation2.FirstOrDefault().Station;
                            DO.Line lineDO = new DO.Line();
                            lineDO.Area = (DO.Areas)Line.Area;
                            lineDO.Id = Line.Id;
                            lineDO.Code = Line.Code;
                            lineDO.FirstStation = Line.FirstStation;
                            lineDO.LastStation = Line.LastStation;
                            lineDO.CountStation = --Line.CountStation;
                            dl.UpdateLine(lineDO);
                            dl.UpdateLineStation(lineStationDO2);


                        }
                    }
                    else // if the station deleted is between 2 stations, we update the previous and the next station
                    {
                        if ((item.Station == LineStationsDeleted.FirstOrDefault().Station) && (LineStationsDeleted.FirstOrDefault().PrevStation != -1) && (LineStationsDeleted.FirstOrDefault().NextStation != -1))
                        {
                            IEnumerable<DO.LineStation> lineStation1 = ListLineStations.Where(x => x.LineStationIndex == item.LineStationIndex - 1);
                            IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineStationIndex == item.LineStationIndex + 1);

                            DO.LineStation lineStationPrev = new DO.LineStation();
                            DO.LineStation lineStationAfter = new DO.LineStation();


                            lineStationPrev.Id = lineStation1.FirstOrDefault().Id;
                            lineStationPrev.LineId = lineStation1.FirstOrDefault().LineId;
                            lineStationPrev.LineStationIndex = lineStation1.FirstOrDefault().LineStationIndex;
                            lineStationPrev.NextStation = item.NextStation;
                            lineStationPrev.PrevStation = lineStation1.FirstOrDefault().PrevStation;
                            lineStationPrev.Station = lineStation1.FirstOrDefault().Station;

                            lineStationAfter.Id = lineStation2.FirstOrDefault().Id;
                            lineStationAfter.LineId = lineStation2.FirstOrDefault().LineId;
                            lineStationAfter.LineStationIndex = deletedIndex - 1;
                            lineStationAfter.NextStation = lineStation2.FirstOrDefault().NextStation;
                            lineStationAfter.PrevStation = item.PrevStation;
                            lineStationAfter.Station = lineStation2.FirstOrDefault().Station;
                            Line.CountStation = --Line.CountStation;
                            dl.UpdateLineStation(lineStationPrev);
                            dl.UpdateLineStation(lineStationAfter);
                            adjstation.Station1 = lineStationPrev.Station;
                            adjstation.Station2 = lineStationAfter.Station;
                            
                            AddAdjacentStations(adjstation);

                        }

                    }
                    if ((item.Station == LineStationsDeleted.FirstOrDefault().Station))
                    {
                        List<DO.LineStation> lineStationsUpdateIndex = new List<DO.LineStation>();
                        lineStationsUpdateIndex = ListLineStations.Where(x => x.LineStationIndex > deletedIndex).ToList();
                        DO.LineStation LineStationDO = new DO.LineStation();

                        foreach (var item1 in lineStationsUpdateIndex)
                        {
                            LineStationDO.Id = item1.Id;
                            LineStationDO.LineId = item1.LineId;
                            LineStationDO.LineStationIndex = --item1.LineStationIndex;
                            LineStationDO.NextStation = item1.NextStation;
                            LineStationDO.PrevStation = item1.PrevStation;
                            LineStationDO.Station = item1.Station;

                            dl.UpdateLineStation(LineStationDO);

                        }

                        dl.DeleteLineStation(LineStationsDeleted.FirstOrDefault().Id);


                        break;
                    }

                }


               
            }
            else if(Line.CountStation == 1)
            {
               
                dl.DeleteLineStation(LineStationsDeleted.FirstOrDefault().Id);
                dl.DeleteLine(Line.Id);
            }
            


        }
        public IEnumerable<BO.LineStation> GetAllLinesStation(int lineid)// this function returns a line station in line according to the line id
        {
            BO.Line LineStationBO = new BO.Line();
            return from LineStationDO in dl.GetAllLineStations()
                   where LineStationDO.LineId == lineid
                   orderby LineStationDO.LineStationIndex
                   select new BO.LineStation
                   { Id = LineStationDO.Id, LineId = LineStationDO.LineId, Station = LineStationDO.Station, PrevStation = LineStationDO.PrevStation, NextStation = LineStationDO.NextStation, LineStationIndex = LineStationDO.LineStationIndex };
        }
        #endregion LineStation
        #region User

        public void AddUser(string username, string password, bool admin) // this function adds a new user
        {
            DO.User user = new DO.User();
            user.UserName = username;
            user.Password = password;
            user.Admin = admin;
            try
            {
                dl.AddUser(user); 
            }
            catch (DO.BadUserNameException ex)// we can't add an user with a duplicate user name 
            {
                throw new BO.BadUserNameException(user.UserName, "Bad user name", ex);
            }
           
        }

        public void CheckUserWorker(string UserName, string password) // this function checks if the user name and the password match and if the user is a director/worker
        {
            
            try
            {
                DO.User user = dl.GetUser(UserName);
                if ((user.Password == password) && (user.Admin))
                    return;
                else
                    throw new BO.BadPasswordUserException(user.Password, "Bad password");
            }
            catch(DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(UserName, "Bad User name", ex);
            }

           
        }

        public void  CheckPassword(string password, string password1) // this function checks if the password and the confirmation password match 
        {
             if (password == password1)
                    return;
            else
            {
                throw new BO.BadPasswordUserException(password, "Bad password");
            }

        }

        public void CheckUserPassenger(string UserName,string password) //this function checks if the user name and password match and if the user is a passenger 
        {
          
            try
            {
                DO.User user = dl.GetUser(UserName);
                if ((user.Password == password) && (!user.Admin))
                    return;
                else
                    throw new BO.BadPasswordUserException(user.Password, "Bad password");
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(UserName, "Bad User name", ex);
            }
        }
        #endregion User
        #region LineTrip
        public IEnumerable<BO.LineTrip> GetAllLineTrips(BO.Line Line) //this function return all the line trips according to the line sent
        {
            
                 BO.LineTrip LineTripBO = new BO.LineTrip();
            return from LineTripDO in dl.GetAllLineTrips()
                   where LineTripDO.LineId==Line.Id
                   select new BO.LineTrip
                   { Id = LineTripDO.Id, LineId = LineTripDO.LineId, StartAt= LineTripDO.StartAt, Frequency = LineTripDO.Frequency, FinishAt = LineTripDO.FinishAt };

        }
        public void RemoveLineTrip (BO.LineTrip LineTrip) // this function removes a line trip
        {
            DO.LineTrip LineTripDO = dl.GetLineTrip(LineTrip.Id);
            dl.DeleteLineTrip(LineTripDO.Id);
        }
        public List<BO.LineTrip> ShowLineTrips(BO.Line Line) //this function return a list all the line trips according to the line sent
        {
            List<DO.LineTrip> ListLineTripsDO = dl.GetAllLineTrips().Where(x => x.LineId == Line.Id).ToList();
            List<BO.LineTrip> ListLineTripsBO = new List<BO.LineTrip>();
            foreach(var item in ListLineTripsDO)
            {
                BO.LineTrip LineTripBO = new BO.LineTrip();
                LineTripBO.Id = item.Id;
                LineTripBO.LineId = item.LineId;
                LineTripBO.StartAt = item.StartAt;
                LineTripBO.Frequency = item.Frequency;
                LineTripBO.FinishAt=UpdateFinishAt(LineTripBO);
                ListLineTripsBO.Add(LineTripBO);
            }
            return ListLineTripsBO;
        }
        public TimeSpan UpdateFinishAt(BO.LineTrip LineTrip) // this function calculates the finish time according the travel of a line
        {
            DO.LineTrip LineTripDO = new DO.LineTrip();
            LineTripDO.Id = LineTrip.Id;
            LineTripDO.LineId = LineTrip.LineId;
            LineTripDO.StartAt = LineTrip.StartAt;
            LineTripDO.Frequency = LineTrip.Frequency;
            List<DO.LineStation> ListLineStations = dl.GetAllLineStations().Where(x => x.LineId == LineTrip.LineId).ToList();
            TimeSpan TimeFinal = LineTrip.StartAt;
            foreach(var item in ListLineStations)
            {
                if (item.NextStation != -1)
                {
                    DO.AdjacentStations AdjStation = new DO.AdjacentStations();
                    AdjStation = dl.GetAdjacentStations(item.Station, item.NextStation);
                    TimeFinal += AdjStation.Time;
                }
            }

            LineTripDO.FinishAt = TimeFinal;
            dl.UpdateLineTrip(LineTripDO);
            return TimeFinal;
        }
        public List<string> Listhours() // this function returns a list of all possible hours 
        {
            List<string> ListHours = new List<string>();
            for(int i=0;i<24;i++)
            {
                if (i <= 9)
                {
                    string str = ("0" + i);
                    ListHours.Add(str);
                }
                else
                {
                    ListHours.Add(""+i);
                }
            }
            return ListHours;
        }
       public  List<string > ListMinOrSec()  // this function returns a list of all possible minutes or seconds 
        {
            List<string> ListMinOrSec = new List<string>();
            for(int i=0;i<60;i++)
            {
                if(i<=9)
                {
                    string str = ("0" + i);
                    ListMinOrSec.Add(str);
                } 
                else
                ListMinOrSec.Add(""+i);
            }
            return ListMinOrSec;
        }
        public void AddLineTrip(BO.Line Line, TimeSpan StartAt) // this function add a new line trip
        {
          

            DO.LineTrip LineTripDO = new DO.LineTrip();
            LineTripDO.Id = dl.CountplusIdLineTrip();
            LineTripDO.LineId = Line.Id;
            LineTripDO.StartAt = StartAt;
            LineTripDO.Frequency = new TimeSpan(0, 0, 0);
            LineTripDO.FinishAt = new TimeSpan(0, 0, 0);
            dl.AddLineTrip(LineTripDO);

            BO.LineTrip LineTripBO = new BO.LineTrip();
            LineTripBO.Id = LineTripDO.Id;
            LineTripBO.LineId = LineTripDO.LineId;
            LineTripBO.StartAt = LineTripDO.StartAt;
            LineTripBO.Frequency = LineTripDO.Frequency;
            LineTripBO.FinishAt = UpdateFinishAt(LineTripBO);


        }
        #endregion LineTrip

    }
}
