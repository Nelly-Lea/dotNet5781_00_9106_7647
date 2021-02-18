using BLAPI;
using DLAPI;
using System;
using System.Collections.Generic;
using System.Linq;

using BO;
//using BO;

namespace BL
{
   class BLImp : IBL //internal pas publiccccc
    {
        IDL dl = DLFactory.GetDL();
        #region Line 
       public  BO.ShowStationsLine ShowStations(BO.Line line)// pas public!!!
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
            while (currentstation!= linesDO.FirstOrDefault().LastStation)
            {
                //DO.LineStation linestationDO = dl.GetLineStation(id);

                 linestationDO = linestationsDO.Where(x => (x.LineId == id) && (x.Station == currentstation));
                DO.Station stationDO = dl.GetStation(linestationDO.FirstOrDefault().Station);
                stationBO.Address = stationDO.Address;
                stationBO.Code = stationDO.Code;
                stationBO.Name= stationDO.Name;
                stationBO.Latitude= stationDO.Latitude;
                stationBO.Longitude = stationDO.Longitude;
                stationBO.Area = (BO.Areas)stationDO.Area;
                stationslineBO.ListStat.Add(stationBO);
                int nextstation = linestationDO.FirstOrDefault().NextStation;
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

        public BO.ShowStations ShowBusStations()
        {
            BO.ShowStations ss = new BO.ShowStations();
            List<BO.Station> listStationBO=new List<BO.Station>();
            IEnumerable<DO.Station> listStationDO = dl.GetAllStations();
            
            foreach(var item in listStationDO)
            {
                BO.Station StationBO = new BO.Station();
                StationBO.Code = item.Code;
                StationBO.Name = item.Name;
                StationBO.Address = item.Address;
                StationBO.Longitude = item.Longitude;
                StationBO.Latitude = item.Latitude;
                StationBO.Area = (BO.Areas)item.Area;
              listStationBO.Add(StationBO);

            }

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

            }
            ss.adjStations = listAdjStationBO;
            int codeLine,lineId,codeLastStation;
            List<int> listinterieurcode = new List<int>();
            List<string> listinterieurname = new List<string>();

            IEnumerable<DO.LineStation> listLineStationDO=dl.GetAllLineStations();
            //int i = 0;
            foreach(var item in listStationBO)
            {
                codeLine = item.Code;
                listinterieurcode = new List<int>();
                listinterieurname = new List<string>();
                foreach (var item1 in listLineStationDO)
                {

                    if(item1.Station==codeLine)
                    {
                        lineId = item1.LineId;
                        
                       
                        foreach (var item2 in dl.GetAllLines())
                        {
                            if (item2.Id == lineId)
                            {

                                //ss.linesNumbers[i] = new List<int>();
                                //ss.linesNumbers[i].Add(item2.Code);
                               
                                listinterieurcode.Add(item2.Code);
                                codeLastStation = item2.LastStation;
                                foreach(var item3 in dl.GetAllStations())
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
                ss.linesNumbers.Add(listinterieurcode);
                ss.lastStationNames.Add(listinterieurname);
               
              
            }
            return ss;


        }
        public void UpdateLineCode(BO.Line Line,int code)
        {
            DO.Line LineDO = dl.GetLine(Line.Id);
            DO.Line NewLine = new DO.Line();
            NewLine.Id = LineDO.Id;
            NewLine.Code = code;
            NewLine.Area = LineDO.Area;
            NewLine.FirstStation = LineDO.FirstStation;
            NewLine.LastStation = LineDO.LastStation;
            NewLine.CountStation = LineDO.CountStation;
           
            dl.UpdateLine(NewLine);

        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            BO.Line LineBO = new BO.Line();
            return from LineDO in dl.GetAllLines()
                   select new BO.Line
                   { Id = LineDO.Id, Code = LineDO.Code, Area = (BO.Areas)LineDO.Area, FirstStation = LineDO.FirstStation, LastStation = LineDO.LastStation, CountStation = LineDO.CountStation  };
            //       {
            //    LineBO.Area = (BO.Areas)LineDO.Area,
            //    LineBO.Code = LineDO.Code,
            //    LineBO.CountStation = LineDO.CountStation;
            //    LineBO.Id = LineDO.Id;
            //    LineBO.FirstStation = LineDO.FirstStation;
            //    LineBO.LastStation = LineDO.LastStation;
            //}
            //IEnumerable<BO.Line> ListOfLines=new List<BO.Line>();


        }
        public void AddLine(int code,BO.Areas area, int firstation, int laststation)
        {
            DO.Line line = new DO.Line();
            line.Area =(DO.Areas) area;
            line.Code = code;
            line.FirstStation = firstation;
            line.LastStation = laststation;
            line.Id = dl.Countplus();
            line.CountStation = 2;
            dl.AddLine(line);

            DO.LineStation lineStation1 = new DO.LineStation();
            lineStation1.LineId = line.Id;
            lineStation1.Station = line.FirstStation;
            lineStation1.LineStationIndex = 1;
            lineStation1.PrevStation = -1;
            lineStation1.NextStation = laststation;

            DO.LineStation lineStation2 = new DO.LineStation();
            lineStation2.LineId = line.Id;
            lineStation2.Station = line.LastStation;
            lineStation1.LineStationIndex = 2;
            lineStation1.PrevStation = line.FirstStation;
            lineStation1.NextStation = -1;

            dl.AddLineStation(lineStation1);
            dl.AddLineStation(lineStation2);

            DO.AdjacentStations adjacentStation = new DO.AdjacentStations();

            adjacentStation.Distance = (dl.CalculateDist(dl.GetStation(firstation), dl.GetStation(laststation)));
            //vitesse=30km/h=>500m/min
            double time = adjacentStation.Distance / 500;
            int h = (int)time / 60;
            int m = (int)time % 60;
            adjacentStation.Time = new TimeSpan(h, m, 0);
            adjacentStation.id = dl.CountplusAdjacentStation();
            adjacentStation.Station1 = firstation;
            adjacentStation.Station2 = laststation;
            dl.AddAdjacentStations(adjacentStation);
        }

        public void UpdateLine(BO.Line Line, BO.LineStation DeletedStation, BO.Station NewStation)
        {
            
            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations();
           // IEnumerable<DO.LineStation> LineStations = ListLineStations.Where(x => (x.LineId == Line.Id) && (x.Station == DeletedStation.Station));
            DO.LineStation NewLineStation = new DO.LineStation();

            NewLineStation.Id =  DeletedStation.Id;
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
                    adjacentStationsBO1.id = dl.CountplusAdjacentStation();
                    UpdateTimeAndDistance(adjacentStationsBO1);
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
                    adjacentStationsBO1.id = dl.CountplusAdjacentStation();
                    UpdateTimeAndDistance(adjacentStationsBO1);
                    DO.LineStation prevLineStation = new DO.LineStation();
                    prevLineStation = dl.GetAllLineStations().Where(x => x.LineId == Line.Id && x.Station == DeletedStation.PrevStation).FirstOrDefault();
                    DO.LineStation stationDO = new DO.LineStation();
                    stationDO.Id = prevLineStation.Id;
                    stationDO.LineId =prevLineStation.LineId;
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
                adjacentStationsBO1.id = dl.CountplusAdjacentStation();
                UpdateTimeAndDistance(adjacentStationsBO1);
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
                adjacentStationsBO2.id = dl.CountplusAdjacentStation();
                UpdateTimeAndDistance(adjacentStationsBO2);

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
          
            dl.UpdateLine(NewLine);
        }
        public void DeleteLine(int id)
        {
            
           
            dl.DeleteLineStation(id);
            dl.DeleteLine(id);
            dl.DeleteLineTrip(id);
            
        }
        #endregion Line
        #region AdjacentStation
        void UpdateTimeAndDistance(BO.AdjacentStations AdjacentStation)
        {
            DO.Station station1 = new DO.Station();
            DO.Station station2 = new DO.Station();
            foreach(var item in dl.GetAllStations())
            {
                if (item.Code == AdjacentStation.Station1)
                    station1 = item;
                if (item.Code == AdjacentStation.Station2)
                    station2 = item;
            }
            double distance = dl.CalculateDist(station1, station2);
            AdjacentStation.Distance = distance;
            //vitesse=30km/h=>500m/min
            double time = AdjacentStation.Distance / 500;
            int h = (int)time / 60;
            int m = (int)time % 60;
            AdjacentStation.Time = new TimeSpan(h, m, 0);
            DO.AdjacentStations adjacentStationsDO = new DO.AdjacentStations();
            adjacentStationsDO.Distance = AdjacentStation.Distance;
            adjacentStationsDO.id = AdjacentStation.id;
            adjacentStationsDO.Station1 = AdjacentStation.Station1;
            adjacentStationsDO.Station2 = AdjacentStation.Station2;
            adjacentStationsDO.Time = AdjacentStation.Time;
            dl.AddAdjacentStations(adjacentStationsDO);

        }
        #endregion AdjacentStation
        #region Station
        public void AddStation(int code, string name,double longitude,double latitude,string address, BO.Areas area)
        {
            DO.Station station = new DO.Station();
            station.Code = code;
            station.Name = name;
            station.Longitude = longitude;
            station.Latitude = latitude;
            station.Address = address;
            station.Area = (DO.Areas)area;
            dl.AddStation(station);
        }
        public void DeleteStation(int code)
        {
            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations();
            IEnumerable<DO.LineStation> ListLineStationsDeleted = dl.GetAllLineStations().Where(x => x.Station == code);
            foreach(var item in ListLineStationsDeleted)
            {
                if(item.PrevStation==-1)
                {
                    IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex == 2);
                    lineStation2.FirstOrDefault().PrevStation = -1;
                    lineStation2.FirstOrDefault().LineStationIndex = 1;
                    DO.Line line = dl.GetAllLines().Where(x => x.Id == item.LineId).FirstOrDefault();
                    line.FirstStation = lineStation2.FirstOrDefault().Station;
                    dl.UpdateLine(line);
                    dl.UpdateLineStation(lineStation2.FirstOrDefault());
                }
                if (item.NextStation == -1)
                {
                    IEnumerable<DO.LineStation> lineStation2 = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex == item.LineStationIndex-1);
                    lineStation2.FirstOrDefault().NextStation = -1;
                    DO.Line line = dl.GetAllLines().Where(x => x.Id == item.LineId).FirstOrDefault();
                    line.LastStation = lineStation2.FirstOrDefault().Station;
                    dl.UpdateLine(line);
                    dl.UpdateLineStation(lineStation2.FirstOrDefault());
                }
                else
                {
                    IEnumerable<DO.LineStation> lineStation1 = ListLineStations.Where(x => x.LineId ==( item.LineId )&&( x.LineStationIndex == item.LineStationIndex-1));
                    IEnumerable<DO.LineStation>lineStation2=ListLineStations.Where(x=>(x.LineId==item.LineId)&&( x.LineStationIndex == item.LineStationIndex + 1));
                    lineStation1.FirstOrDefault().NextStation = item.NextStation;
                    lineStation2.FirstOrDefault().PrevStation = item.PrevStation;
                    dl.UpdateLineStation(lineStation1.FirstOrDefault());
                    dl.UpdateLineStation(lineStation2.FirstOrDefault());
                }
                IEnumerable<DO.LineStation> lineStationsUpdateIndex = ListLineStations.Where(x => x.LineId == item.LineId && x.LineStationIndex > item.LineStationIndex);
                foreach(var item1 in lineStationsUpdateIndex)
                {
                    item1.LineStationIndex--;
                    dl.UpdateLineStation(item1);
                }
                dl.DeleteLineStation(item.Id);
            }


            dl.DeleteStation(code);
            


        }

        public IEnumerable<BO.Station> ShowStationArea(BO.Line line)
        {
           
            
            IEnumerable<BO.Station> listStations=from StationDO in dl.GetAllStations()
                   where StationDO.Area == (DO.Areas)line.Area
                   select new BO.Station
                   { Code = StationDO.Code, Name =StationDO.Name, Longitude = StationDO.Longitude, Latitude = StationDO.Latitude,Address = StationDO.Address, Area= (BO.Areas)StationDO.Area};
            
            IEnumerable<DO.LineStation> ListLineStation = dl.GetAllLineStations().Where(x => x.LineId == line.Id);
           
            IEnumerable<BO.Station> ListStationLine = from StationLineDO in dl.GetAllStations()
                                                      from LineStationDO in ListLineStation
                                                      where StationLineDO.Code == LineStationDO.Station
                                                      select new BO.Station
                                                      { Code = StationLineDO.Code, Name = StationLineDO.Name, Longitude = StationLineDO.Longitude, Latitude = StationLineDO.Latitude, Address = StationLineDO.Address, Area = (BO.Areas)StationLineDO.Area };
           
            IEnumerable<BO.Station> listStation = listStations.Where(p => ListStationLine.All(p2 => p2.Code != p.Code));
            
            return listStation;

            
        }

        public void UpdateStation(BO.Station StationToUpdate,int code, string name, string address)
        {
            DO.Station StationDO = new DO.Station();
            StationDO.Area = (DO.Areas) StationToUpdate.Area;
            StationDO.Longitude = StationToUpdate.Longitude;
            StationDO.Latitude = StationToUpdate.Latitude;
            StationDO.Code = code;
            StationDO.Name = name;
            StationDO.Address = address;
            dl.UpdateStation(StationDO);

        }

        //FONCTION UPDATE
        #endregion Station
        #region LineStation
        public IEnumerable<BO.LineStation> GetLineStation(int LineId)
        {
            //BO.LineStation lineStationBO = new BO.LineStation();
            //List<BO.LineStation> ListOfLineStations = new List<LineStation>();
            //foreach(var item in dl.GetAllLineStations())
            //{
            //    if(item.LineId==LineId)
            //    {
            //        lineStationBO.Id = item.Id;
            //        lineStationBO.LineId = item.LineId;
            //        lineStationBO.LineStationIndex = item.LineStationIndex;
            //        lineStationBO.NextStation = item.NextStation;
            //        lineStationBO.PrevStation = item.PrevStation;
            //        lineStationBO.Station = item.Station;
            //        ListOfLineStations.Add(lineStationBO);
            //    }

            //}
            //return ListOfLineStations;
            return from LineStationDO in dl.GetAllLineStations()
                   where LineStationDO.LineId == LineId
                   select new BO.LineStation
                   { LineId = LineStationDO.LineId, Station = LineStationDO.Station, LineStationIndex = LineStationDO.LineStationIndex, PrevStation = LineStationDO.PrevStation, NextStation = LineStationDO.NextStation, Id = LineStationDO.Id };
        }

        public IEnumerable<BO.LineStation> GetAllLinesStation(int lineid)
        {
            BO.Line LineStationBO = new BO.Line();
            return from LineStationDO in dl.GetAllLineStations()
                   where LineStationDO.LineId==lineid
                   select new BO.LineStation
                   { Id = LineStationDO.Id, LineId = LineStationDO.LineId, Station = LineStationDO.Station, PrevStation = LineStationDO.PrevStation, NextStation = LineStationDO.NextStation, LineStationIndex = LineStationDO.LineStationIndex };
        }
            public void AddLineStation(BO.Line Line, BO.Station Station)
        {
            DO.LineStation lineStation = new DO.LineStation();
            lineStation.LineId = Line.Id;
            lineStation.Station = Station.Code;
            lineStation.LineStationIndex = ++Line.CountStation;
            lineStation.PrevStation = Line.LastStation;
            lineStation.NextStation = -1;
            DO.LineStation lastStation = dl.GetAllLineStations().Where(x => (x.LineId == Line.Id )&& (x.Station == Line.LastStation)).FirstOrDefault();
            lastStation.NextStation = Station.Code;
            dl.UpdateLineStation(lastStation);
            DO.Line LineDO = new DO.Line();
            LineDO.Area = (DO.Areas)Line.Area;
            LineDO.Code = Line.Code;
            LineDO.CountStation = ++Line.CountStation;
            LineDO.FirstStation = Line.FirstStation;
            LineDO.LastStation = Station.Code;
            LineDO.Id = Line.Id;
            dl.UpdateLine(LineDO);
            BO.AdjacentStations adjStation = new BO.AdjacentStations();
            adjStation.Station1 = lastStation.Station;
            adjStation.Station2 = Station.Code;
            adjStation.id = dl.CountplusAdjacentStation();
            UpdateTimeAndDistance(adjStation);
            DO.AdjacentStations adjstationDO = new DO.AdjacentStations();
            adjstationDO.id = adjStation.id;
            adjstationDO.Station1 = adjStation.Station1;
            adjstationDO.Station2 = adjStation.Station2;
            adjstationDO.Time = adjStation.Time;
            adjstationDO.Distance = adjStation.Distance;
            dl.UpdateAdjacentStations(adjstationDO);



        }
        public void  RemoveLineStation(BO.Line Line, int code)
        {
            IEnumerable<DO.LineStation> ListLineStations = dl.GetAllLineStations().Where(x => x.LineId == Line.Id);
            IEnumerable<DO.LineStation> LineStationsDeleted = dl.GetAllLineStations().Where(x => x.LineId == Line.Id&&x.Station==code);
            int deletedIndex = LineStationsDeleted.FirstOrDefault().LineStationIndex+1;
            DO.LineStation lineStationDO2 = new DO.LineStation();
            foreach(var item in ListLineStations)
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
                    if ((item.Station == LineStationsDeleted.FirstOrDefault().Station)&&(LineStationsDeleted.FirstOrDefault().PrevStation!=-1)&& (LineStationsDeleted.FirstOrDefault().NextStation != -1))
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
                        lineStationAfter.LineStationIndex = deletedIndex-1;
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
                    lineStationsUpdateIndex= ListLineStations.Where(x => x.LineStationIndex > deletedIndex).ToList();
                    DO.LineStation LineStationDO = new DO.LineStation();
                    ////(x => x.LineId == item.LineId && x.LineStationIndex > item.LineStationIndex);
                    //List<DO.LineStation> lineStationsUpdateIndex = new List<DO.LineStation> ();
                    //List<DO.LineStation> L = new List<DO.LineStation>();
                    //L = ListLineStations.ToList();
                    //lineStationsUpdateIndex = L.FindAll(x => x.LineStationIndex > item.LineStationIndex);
                    foreach (var item1 in lineStationsUpdateIndex)
                    {
                        LineStationDO.Id = item1.Id;
                        LineStationDO.LineId = item1.LineId;
                        LineStationDO.LineStationIndex = --item1.LineStationIndex;
                        LineStationDO.NextStation = item1.NextStation;
                        LineStationDO.PrevStation = item1.PrevStation;
                        LineStationDO.Station = item1.Station;

                       // item1.LineStationIndex--;
                       
                        dl.UpdateLineStation(LineStationDO);

                    }

                    dl.DeleteLineStation(LineStationsDeleted.FirstOrDefault().Id);
                   //IEnumerable<DO.LineStation> l = dl.GetAllLineStations().Where(x => x.LineId == Line.Id);

                    break;
                }
               
            }


            IEnumerable<DO.LineStation> L = dl.GetAllLineStations().Where(x=>x.LineId==Line.Id);


        }
        #endregion LineStation
        #region User

        public void AddUser(string username, string password, bool admin)
        {
            DO.User user = new DO.User();
            user.UserName = username;
            user.Password = password;
            user.Admin = admin;
        
            dl.AddUser(user);
        }

        public bool CheckUserWorker(string UserName,string password)
        {
            DO.User user = dl.GetUser(UserName);
            if ((user.Password == password) && (user.Admin))
                return true;
            else
                return false;
        }
  

        #endregion User
        //#region Student
        //BO.Student studentDoBoAdapter(DO.Student studentDO)
        //{
        //    BO.Student studentBO = new BO.Student();
        //    DO.Person personDO;
        //    int id = studentDO.ID;
        //    try
        //    {
        //        personDO = dl.GetPerson(id);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }
        //    personDO.CopyPropertiesTo(studentBO);
        //    //studentBO.ID = personDO.ID;
        //    //studentBO.BirthDate = personDO.BirthDate;
        //    //studentBO.City = personDO.City;
        //    //studentBO.Name = personDO.Name;
        //    //studentBO.HouseNumber = personDO.HouseNumber;
        //    //studentBO.Street = personDO.Street;
        //    //studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

        //    studentDO.CopyPropertiesTo(studentBO);
        //    //studentBO.StartYear = studentDO.StartYear;
        //    //studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
        //    //studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

        //    studentBO.ListOfCourses = from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
        //                              let course = dl.GetCourse(sic.CourseId)
        //                              select course.CopyToStudentCourse(sic);
        //    //new BO.StudentCourse()
        //    //{
        //    //    ID = course.ID,
        //    //    Number = course.Number,
        //    //    Name = course.Name,
        //    //    Year = course.Year,
        //    //    Semester = (BO.Semester)(int)course.Semester,
        //    //    Grade = sic.Grade
        //    //};

        //    return studentBO;
        //}

        //public BO.Student GetStudent(int id)
        //{
        //    DO.Student studentDO;
        //    try
        //    {
        //        studentDO = dl.GetStudent(id);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
        //    }
        //    return studentDoBoAdapter(studentDO);
        //}

        //public IEnumerable<BO.Student> GetAllStudents()
        //{
        //    //return from item in dl.GetStudentListWithSelectedFields( (stud) => { return GetStudent(stud.ID); } )
        //    //       let student = item as BO.Student
        //    //       orderby student.ID
        //    //       select student;
        //    return from studentDO in dl.GetAllStudents()
        //           orderby studentDO.ID
        //           select studentDoBoAdapter(studentDO);
        //}
        //public IEnumerable<BO.Student> GetStudentsBy(Predicate<BO.Student> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<BO.ListedPerson> GetStudentIDNameList()
        //{
        //    return from item in dl.GetStudentListWithSelectedFields((studentDO) =>
        //    {
        //        try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
        //        return new BO.ListedPerson() { ID = studentDO.ID, Name = dl.GetPerson(studentDO.ID).Name };
        //    })
        //           let studentBO = item as BO.ListedPerson
        //           //orderby student.ID
        //           select studentBO;
        //}

        //public void UpdateStudentPersonalDetails(BO.Student student)
        //{
        //    //Update DO.Person            
        //    DO.Person personDO = new DO.Person();
        //    student.CopyPropertiesTo(personDO);
        //    try
        //    {
        //        dl.UpdatePerson(personDO);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }

        //    //Update DO.Student            
        //    DO.Student studentDO = new DO.Student();
        //    student.CopyPropertiesTo(studentDO);
        //    try
        //    {
        //        dl.UpdateStudent(studentDO);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }

        //}

        //public void DeleteStudent(int id)
        //{
        //    try
        //    {
        //        dl.DeletePerson(id);
        //        dl.DeleteStudent(id);
        //        dl.DeleteStudentFromAllCourses(id);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
        //    }
        //}

        //#endregion

        //#region StudentIn Course
        //public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        //{
        //    try
        //    {
        //        dl.AddStudentInCourse(perID, courseID, grade);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //}

        //public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        //{
        //    try
        //    {
        //        dl.UpdateStudentGradeInCourse(perID, courseID, grade);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //}

        //public void DeleteStudentInCourse(int perID, int courseID)
        //{
        //    try
        //    {
        //        dl.DeleteStudentInCourse(perID, courseID);
        //    }
        //    catch (DO.BadPersonIdCourseIDException ex)
        //    {
        //        throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
        //    }
        //}
        //#endregion

        //#region Course

        //BO.Course courseDoBoAdapter(DO.Course courseDO)
        //{
        //    BO.Course courseBO = new BO.Course();
        //    int id = courseDO.ID;
        //    courseDO.CopyPropertiesTo(courseBO);

        //    courseBO.Lecturers = from lic in dl.GetLecturersInCourseList(lic => lic.CourseId == id)
        //                         let course = dl.GetCourse(lic.CourseId)
        //                         select (BO.CourseLecturer)course.CopyPropertiesToNew(typeof(BO.CourseLecturer));
        //    return courseBO;
        //}
        //public IEnumerable<BO.Course> GetAllCourses()
        //{
        //    return from crsDO in dl.GetAllCourses()
        //           select courseDoBoAdapter(crsDO);
        //}

        //public IEnumerable<BO.StudentCourse> GetAllCoursesPerStudent(int id)
        //{
        //    return from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
        //           let course = dl.GetCourse(sic.CourseId)
        //           select course.CopyToStudentCourse(sic);
        //}

        //#endregion


    }
}
