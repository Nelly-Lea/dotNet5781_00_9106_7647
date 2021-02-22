using BLAPI;
using BO;
using DLAPI;
using System;
using System.Collections.Generic;
using System.Linq;
//using BO;

namespace BL
{
    class BLImp : IBL //internal pas publiccccc
    {
        IDL dl = DLFactory.GetDL();
        public List<BO.Areas> GetAreas()
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
        public BO.ShowStationsLine ShowStations(BO.Line line)// pas public!!!
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
            while (currentstation != linesDO.FirstOrDefault().LastStation)
            {
                //DO.LineStation linestationDO = dl.GetLineStation(id);

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
        //public List<int> GetAllLineInStation(int index)

        //{

        //    BO.ShowStations SS = ShowBusStations();
        //    return SS.linesNumbers[index];

        //}
        public List<BO.Line> GetAllLineInStation(int index)

        {

            BO.ShowStations SS = ShowBusStations();
            return SS.linesNumbers[index];

        }
        public List<string> GetAllLastStationInLine(int index)

        {

            BO.ShowStations SS = ShowBusStations();
            return SS.lastStationNames[index];

        }

        public BO.ShowStations ShowBusStations()
        {
            BO.ShowStations ss = new BO.ShowStations();
            List<BO.Station> listStationBO = new List<BO.Station>();
            IEnumerable<DO.Station> listStationDO = dl.GetAllStations();

            foreach (var item in listStationDO)
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

            foreach (var item in listAdjStationDO)
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
           // List<int> listinterieurcode = new List<int>();

            List<BO.Line> ListInterieurCode = new List<BO.Line>();//ajout

            List<string> listinterieurname = new List<string>();

            IEnumerable<DO.LineStation> listLineStationDO = dl.GetAllLineStations();
            //int i = 0;
            foreach (var item in listStationBO)
            {
                codeLine = item.Code;
                //listinterieurcode = new List<int>();
                 ListInterieurCode = new List<BO.Line>();//ajout
                listinterieurname = new List<string>();
                foreach (var item1 in listLineStationDO)
                {

                    if (item1.Station == codeLine)
                    {
                        lineId = item1.LineId;


                        foreach (var item2 in dl.GetAllLines())
                        {
                            if (item2.Id == lineId)
                            {

                                //ss.linesNumbers[i] = new List<int>();
                                //ss.linesNumbers[i].Add(item2.Code);
                                BO.Line LineBO = new BO.Line();//ajout
                                LineBO.Id = item2.Id;//ajout
                                LineBO.Code = item2.Code;//ajout
                                LineBO.Area = (BO.Areas)item2.Area;//ajout
                                LineBO.FirstStation = item2.FirstStation;//ajout
                                LineBO.LastStation = item2.LastStation;//ajout
                                LineBO.CountStation = item2.CountStation;//ajout
                                ListInterieurCode.Add(LineBO);//ajout

                              //  listinterieurcode.Add(item2.Code);
                                codeLastStation = item2.LastStation;
                                foreach (var item3 in dl.GetAllStations())
                                {
                                    if (item3.Code == codeLastStation)
                                    {
                                        //ss.lastStationNames[i].Add(item3.Name);

                                        listinterieurname.Add(item3.Name);
                                    }
                                }
                            }


                        }


                    }

                }
                // i++;
                ss.linesNumbers.Add(ListInterieurCode);//ajout
               // ss.linesNumbers.Add(listinterieurcode);
                ss.lastStationNames.Add(listinterieurname);


            }
            return ss;


        }
        public void UpdateLineCode(BO.Line Line, int code)
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
                throw new BO.BadLineIdException(NewLine.Code, "Bad Line code");
            
        }
          
        public IEnumerable<BO.Line> GetAllLines()
        {
            BO.Line LineBO = new BO.Line();
            return from LineDO in dl.GetAllLines()
                   select new BO.Line
                   { Id = LineDO.Id, Code = LineDO.Code, Area = (BO.Areas)LineDO.Area, FirstStation = LineDO.FirstStation, LastStation = LineDO.LastStation, CountStation = LineDO.CountStation };
           

        }

        public IEnumerable<BO.Station> AddLineFirst(int code, BO.Areas area, BO.Station firstStation)
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
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(LineDO.Id, "Bad line Id", ex);
            }
            //dl.AddLine(LineDO);
            IEnumerable<BO.Station> listStation = ShowStationArea(Line);
            return listStation;
        }
        public void AddLine(int code, BO.Station FirstStation, BO.Station LastStation)
        {

            DO.Line Line = new DO.Line();
            Line = dl.GetAllLines().Where(x => (x.Code == code) && (x.FirstStation == FirstStation.Code) && (x.LastStation == -1)).FirstOrDefault();
            DO.Line line = new DO.Line();
            line.Area = Line.Area;
            line.Code = Line.Code;
            line.FirstStation = Line.FirstStation;
            line.LastStation = LastStation.Code;
            line.Id = Line.Id;
            line.CountStation = 2;
            try
            {
                dl.UpdateLine(line);
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
            double time = adjacentStation.Distance / 333.33;
            double timeinseconds = time * 60;
            int m = (int)time;
            int s = (int)timeinseconds - m * 60;
            
            adjacentStation.Time = new TimeSpan(0, m, s);
            ////vitesse=20km/h=>333.33m/min
           
            adjacentStation.id = dl.CountplusAdjacentStation();
            adjacentStation.Station1 = line.FirstStation;
            adjacentStation.Station2 = LastStation.Code;
            dl.AddAdjacentStations(adjacentStation);
        }

        public void UpdateLine(BO.Line Line, BO.LineStation DeletedStation, BO.Station NewStation)
        {

            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations();
            // IEnumerable<DO.LineStation> LineStations = ListLineStations.Where(x => (x.LineId == Line.Id) && (x.Station == DeletedStation.Station));
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
            //  DO.AdjacentStations adjacentStationsDO1 = new DO.AdjacentStations();

            BO.AdjacentStations adjacentStationsBO2 = new BO.AdjacentStations();
            //  DO.AdjacentStations adjacentStationsDO2 = new DO.AdjacentStations();



            if ((NewLineStation.PrevStation == -1) || (NewLineStation.NextStation == -1))
            {
                if (NewLineStation.PrevStation == -1)
                {
                    NewLine.FirstStation = NewStation.Code;
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

                else
                {
                    NewLine.LastStation = NewStation.Code;
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
            else
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
        public void DeleteLine(int id)
        {


            dl.DeleteLineStation(id);

            dl.DeleteLine(id);
            dl.DeleteLineTrip(id);

        }
        #endregion Line
        #region AdjacentStation
       public void AddAdjacentStations(BO.AdjacentStations AdjacentStation)
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
        public void UpdateTimeAndDistanceAdjStations(BO.AdjacentStations AdjacentStation)
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
            //vitesse=20km/h=>333.33m/min
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
        public void AddStation(int code, string name, double longitude, double latitude, string address, BO.Areas area)
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
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(station.Code, "Bad Station code", ex);
            }
           // dl.AddStation(station);
        }
        public void DeleteStation(int code)
        {
            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations();
            List<DO.LineStation> ListLineStationsDeleted = dl.GetAllLineStations().Where(x => x.Station == code).ToList();
            DO.LineStation LineStationDO2 = new DO.LineStation();
            DO.LineStation LineStationDO1 = new DO.LineStation();
            foreach (var item in ListLineStationsDeleted)
            {
                if (item.PrevStation == -1)
                {
                    IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex == 2);


                    LineStationDO2.Id = lineStation2.FirstOrDefault().Id;
                    LineStationDO2.LineId = lineStation2.FirstOrDefault().LineId;
                    LineStationDO2.LineStationIndex = 1;
                    LineStationDO2.PrevStation = item.PrevStation;
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
                if (item.NextStation == -1)
                {
                    IEnumerable<DO.LineStation> lineStation1 = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex == item.LineStationIndex - 1);
                    //lineStation2.FirstOrDefault().NextStation = -1;

                    LineStationDO1.Id = lineStation1.FirstOrDefault().Id;
                    LineStationDO1.LineId = lineStation1.FirstOrDefault().LineId;
                    LineStationDO1.LineStationIndex = lineStation1.FirstOrDefault().LineStationIndex;
                    LineStationDO1.PrevStation = lineStation1.FirstOrDefault().PrevStation;
                    LineStationDO1.NextStation = item.NextStation;
                    LineStationDO1.Station = lineStation1.FirstOrDefault().Station;

                    DO.Line line = dl.GetAllLines().Where(x => x.Id == item.LineId).FirstOrDefault();
                    // line.LastStation = lineStation2.FirstOrDefault().Station;
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
                   
                }
                List<DO.LineStation> lineStationsUpdateIndex = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex > item.LineStationIndex).ToList();
            foreach (var item1 in lineStationsUpdateIndex)
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

        dl.DeleteListLineStations(code);  // j'hesite a nettre cette ligne
            dl.DeleteStation(code);



        }

            public IEnumerable<BO.Station> ShowStationArea(BO.Line line)
        {


            IEnumerable<BO.Station> listStations = from StationDO in dl.GetAllStations()
                                                   where StationDO.Area == (DO.Areas)line.Area
                                                   select new BO.Station
                                                   { Code = StationDO.Code, Name = StationDO.Name, Longitude = StationDO.Longitude, Latitude = StationDO.Latitude, Address = StationDO.Address, Area = (BO.Areas)StationDO.Area };

            IEnumerable<DO.LineStation> ListLineStation = dl.GetAllLineStations().Where(x => x.LineId == line.Id);

            IEnumerable<BO.Station> ListStationLine = from StationLineDO in dl.GetAllStations()
                                                      from LineStationDO in ListLineStation
                                                      where StationLineDO.Code == LineStationDO.Station
                                                      select new BO.Station
                                                      { Code = StationLineDO.Code, Name = StationLineDO.Name, Longitude = StationLineDO.Longitude, Latitude = StationLineDO.Latitude, Address = StationLineDO.Address, Area = (BO.Areas)StationLineDO.Area };

            IEnumerable<BO.Station> listStation = listStations.Where(p => ListStationLine.All(p2 => p2.Code != p.Code));

            return listStation;


        }

        public void UpdateStation(BO.Station StationToUpdate,  string name, string address)
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

        public IEnumerable<BO.Station> GetStationWithArea(BO.Areas area)
        {

            IEnumerable<DO.Station> listStationDO = dl.GetAllStations().Where(x => (BO.Areas)x.Area == area);
            IEnumerable<BO.Station> ListStationBO = from StationDO in listStationDO
                                                    select new BO.Station
                                                    { Code = StationDO.Code, Name = StationDO.Name, Longitude = StationDO.Longitude, Latitude = StationDO.Latitude, Address = StationDO.Address, Area = (BO.Areas)StationDO.Area };

            return ListStationBO;
        }

        //public List<BO.Station> ShowTravelsBetween2Stations(BO.Station StationStart, BO.Station StationFinish)
        //{
        //    BO.ShowStations ShowStations = new ShowStations();
        //    ShowStations = ShowBusStations();
        //    int index1 = 0, index2 = 0;
        //    foreach(var item in ShowStations.stations)
        //    {
        //        if (item.Code == StationStart.Code)
        //            break;
        //        index1++;
        //    }
        //    foreach (var item in ShowStations.stations)
        //    {
        //        if (item.Code == StationFinish.Code)
        //            break;
        //        index2++;
        //    }
        //    List<BO.Line> LineStations1 = ShowStations.linesNumbers[index1];
        //    List<BO.Line> LineStations2 = ShowStations.linesNumbers[index2];

        //    List<BO.Line> listLines = LineStations1.Where(p => LineStations2.All(p2 => p2.Code == p.Code)).ToList();
        //    int indexlinestation1=0, indexlinestation2=0;
        //    if(listLines!=null)
        //    {
        //        IEnumerable<DO.LineStation> listLineStation =dl.GetAllLineStations().Where(x=>x.LineId==)
        //    }
        //    List < List<BO.LineStation> >ListlineStations= new List<List<BO.LineStation>>();
        //    int i = 0;
        //    foreach(var item1 in listLines)
        //    {
        //        foreach(var item2 in dl.GetAllLineStations().Where(x=>x.LineId==item1.Id))
        //        {
        //            if (item2.Station == StationStart.Code)
        //                break;
        //            indexlinestation1++;
        //        }
        //        foreach (var item3 in dl.GetAllLineStations().Where(x => x.LineId == item1.Id))
        //        {
        //            if (item3.Station == StationFinish.Code)
        //                break;
        //            indexlinestation2++;
        //        }
        //        if(indexlinestation1<indexlinestation2)
        //        {
        //            ListlineStations[i].Add(dl.GetLineStation())
        //        }
        //        i++;
        //    }
        }

        //FONCTION UPDATE
        #endregion Station
        #region LineStation
        public IEnumerable<BO.LineStation> GetLineStation(int LineId)
        {
           
            return from LineStationDO in dl.GetAllLineStations()
                   where LineStationDO.LineId == LineId
                   select new BO.LineStation
                   { LineId = LineStationDO.LineId, Station = LineStationDO.Station, LineStationIndex = LineStationDO.LineStationIndex, PrevStation = LineStationDO.PrevStation, NextStation = LineStationDO.NextStation, Id = LineStationDO.Id };
        }

        public IEnumerable<BO.LineStation> GetAllLinesStation(int lineid)
        {
            BO.Line LineStationBO = new BO.Line();
            return from LineStationDO in dl.GetAllLineStations()
                   where LineStationDO.LineId == lineid
                   select new BO.LineStation
                   { Id = LineStationDO.Id, LineId = LineStationDO.LineId, Station = LineStationDO.Station, PrevStation = LineStationDO.PrevStation, NextStation = LineStationDO.NextStation, LineStationIndex = LineStationDO.LineStationIndex };
        }
        public void AddLineStation(BO.Line Line, BO.Station Station)
        {
            DO.LineStation lineStation = new DO.LineStation();
            lineStation.Id = dl.CountplusLineStation();
            lineStation.LineId = Line.Id;
            lineStation.Station = Station.Code;
            lineStation.LineStationIndex = ++Line.CountStation;
            lineStation.PrevStation = Line.LastStation;
            lineStation.NextStation = -1;
            DO.LineStation lastStation = dl.GetAllLineStations().Where(x => (x.LineId == Line.Id) && (x.Station == Line.LastStation)).FirstOrDefault();
            lastStation.NextStation = Station.Code;
            dl.UpdateLineStation(lastStation);
            DO.Line LineDO = new DO.Line();
            LineDO.Area = (DO.Areas)Line.Area;
            LineDO.Code = Line.Code;
            LineDO.CountStation = ++Line.CountStation;
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


        }
        public void RemoveLineStation(BO.Line Line, int code)
        {
            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations().Where(x => x.LineId == Line.Id);
            IEnumerable<DO.LineStation> LineStationsDeleted = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == code);
            int deletedIndex = LineStationsDeleted.FirstOrDefault().LineStationIndex + 1;
            DO.LineStation lineStationDO2 = new DO.LineStation();
            foreach (var item in ListLineStations)
            {
                if (LineStationsDeleted.FirstOrDefault().PrevStation == -1)
                {
                    if (item.PrevStation == -1)
                    {
                        IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineStationIndex == 2);
                        lineStationDO2.Id = lineStation2.FirstOrDefault().Id;
                        lineStationDO2.LineId = lineStation2.FirstOrDefault().LineId;
                        lineStationDO2.LineStationIndex = 1;
                        lineStationDO2.NextStation = lineStation2.FirstOrDefault().NextStation;
                        lineStationDO2.PrevStation = -1;
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
                if (LineStationsDeleted.FirstOrDefault().NextStation == -1)
                {
                    if (item.NextStation == -1)
                    {
                        IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineStationIndex == item.LineStationIndex - 1);
                        lineStationDO2.Id = lineStation2.FirstOrDefault().Id;
                        lineStationDO2.LineId = lineStation2.FirstOrDefault().LineId;
                        lineStationDO2.LineStationIndex = lineStation2.FirstOrDefault().LineStationIndex;
                        lineStationDO2.NextStation = -1;
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
                else
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

                    }

                }
                if (item.Station == LineStationsDeleted.FirstOrDefault().Station)
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


            IEnumerable<DO.LineStation> L = dl.GetAllLineStations().Where(x => x.LineId == Line.Id);


        }
        #endregion LineStation
        #region User

        public void AddUser(string username, string password, bool admin)
        {
            DO.User user = new DO.User();
            user.UserName = username;
            user.Password = password;
            user.Admin = admin;
            try
            {
                dl.AddUser(user);
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(user.UserName, "Bad user name", ex);
            }
           
        }

        public void CheckUserWorker(string UserName, string password)
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

        public void  CheckPassword(string password, string password1)
        {
             if (password == password1)
                    return;
            else
            {
                throw new BO.BadPasswordUserException(password, "Bad password");
            }

        }

        public void CheckUserPassenger(string UserName,string password)
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
       

    }
}
