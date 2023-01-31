using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;
using GTA.Native;
using HighBeam.NewHighwayTraffic;
using static HighBeam.NewHighwayTraffic.Index;
using static HighBeam.NewHighwayTraffic.CarList;
using static HighBeam.NewHighwayTraffic.Helpers;
using static HighBeam.NewHighwayTraffic.Zone;
using static HighBeam.Main;
using static HighBeam.NewHighwayTraffic.FakeTraffic;
using static HighBeam.NewHighwayTraffic.MoveCar;
using static HighBeam.ZoneCreatorOld;
using System.Diagnostics;
using static HighBeam.Paths;

namespace HighBeam.NewHighwayTraffic
{
    public static class SingleRoadRadar
    {
        private static int test = 0;
        public static int maxNumOfSpawnedCars = 30;
        private static float rightLaneSpeed = (float)(100 / 3.6f);
        public static int maxFake = 0;
        public static int minFake = 0;
        public static float minLeftLaneSpeed = (float)(110 / 3.6f);
        public static Stopwatch spawnTrafficStopwatch = new Stopwatch();
        public static Stopwatch delayBeetwenSpawnsStopwatch = new Stopwatch();
        public static int delayAt = 0;
        public static int delayLength = 0;
        public static bool isTruckMode = false;
        public static int carsLeftLaneCount = 0;
        public static int carsRightLaneCount = 0;
        public static Stopwatch spawnFasterCarsStopwatch = new Stopwatch();
        public static int spawnFasterCarDelay = 0;
        public static float speedForZone = -10f;

        private static bool isNight = false;

        public static List<Vehicle> carList = new List<Vehicle>();
        public static List<Vehicle> carListFake = new List<Vehicle>();
        private static Stopwatch steeringStopwatch = new Stopwatch();
        private static Stopwatch steeringStopwatchFake = new Stopwatch();
        private static int currentVehIndex = 0;
        private static Vehicle veh;
        private static Stopwatch carSpawnStopwatch = new Stopwatch();
        private static Stopwatch carSpawnFakeStopwatch = new Stopwatch();

        public static LaneModel currentPath = null;
        private static LaneModel currentFakePath = null;
        public static string currentPathName = null;

        private static Vector3 startPoint;
        private static Vector3 endPoint;

        private static int maxCars = 4;
        private static int maxFakeCars = 4;
        private static int maxTotalCars = 12;
        private static Random spawnFakeCarRnd = new Random();
        private static int timeToSpawnFakeCar = 0;

        private static Random spawnCarRnd = new Random();
        private static int timeToSpawnCar = 0;

        private static int minTime = 0;
        private static int maxTime = 0;

        private static int minTimeFake = 0;
        private static int maxTimeFake = 0;
        public static bool isNoTraffic = false;

        private static Stopwatch carToDelStopwatch = new Stopwatch();

        private static Random spawnColumnRnd = new Random();
        private static Random spawnFakeColumnRnd = new Random();
        private static bool isTwoLaneRoad = false;

        private static int testSpawn = 1;
        private static int id = 0;
        private static bool isLowSpeed = false;

        private static bool isCity = false;

        private static bool isInWinterZone = false;

        private static Stopwatch findRouteStopwatch = new Stopwatch();
        private static int h = 0;

        public static void RunSingleRoad()
        {
            veh = Game.Player.Character.LastVehicle;
            // RunZoneCreator();
            //  Function.Call((Hash)0xB3B3359379FE77D3, 0.1f);
            //   Function.Call((Hash)0x245A6883D966D537, 0.1f);

            if (isOnHighway)
            { 
                try 
                {
                    CheckBounds();

                    if (currentPath != null)
                    {
                        GetTraffic();
                        //   var pos = currentPath.PathList[currentVehIndex].Position;
                        //   Function.Call((Hash)0x6B7256074AE34680, pos.X, pos.Y, pos.Z, pos.X, pos.Y, (pos.Z + 10f), 255, 5, 5, 255);

                        AutobahnPropStreamer.Beacons();
                        // var h = Function.Call<int>((Hash)0x25223CA6B4D20B7F);
                        //  var m = Function.Call<int>((Hash)0x13D2B8ADD79640F2);
                        //  isNight = (h > 18 || h < 7);

                        //  var light1 = currentPath.PathList[currentVehIndex].Position;
                        //    Function.Call((Hash)0x6B7256074AE34680, light1.X, light1.Y, light1.Z, light1.X, light1.Y, (light1.Z + 5f), 255, 5, 5, 255);
                        isLowSpeed = (veh.Speed * 3.6) < 130 && isTwoLaneRoad;

                        if ((currentVehIndex + 100) < currentPath.PathList.Count - 150)
                        {
                            if (currentPath.PathList[currentVehIndex + 100].Position.DistanceTo(veh.Position) < 25)
                            {
                                currentVehIndex += 100;
                            }
                        }
                        if (isTwoLaneRoad)
                        {
                            ManageCar2();
                            ManageFakeCar2();

                        }
                        else
                        {
                            ManageCar();
                            ManageFakeCar();
                        }

                        ManageCarDelete();

                        if (isInWinterZone)
                        {
                            if (Game.IsControlJustPressed(0, Control.VehicleHorn))
                            {
                                findRouteStopwatch.Start();
                            }
                            if (Game.IsControlJustReleased(0, Control.VehicleHorn))
                            {
                                findRouteStopwatch = new Stopwatch();
                            }
                            if (findRouteStopwatch.IsRunning && findRouteStopwatch.ElapsedMilliseconds > 5000)
                            {
                                findRouteStopwatch = new Stopwatch();
                                FindClosestIndex();
                                UI.ShowSubtitle("Finding path");
                            }
                        }


                        if (Game.IsControlJustPressed(0, Control.VehicleSelectNextWeapon) && false)
                        {
                            if (testSpawn == 0)
                                SpawnCar(1, false, true);
                            if (testSpawn == 1)
                            {
                                SpawnCar(1, true, true);

                            }
                            //  testSpawn++;
                            // if (testSpawn > 1)
                            // {
                            //     testSpawn = 0;
                            // }
                        }

                        if (Game.IsControlJustPressed(0, Control.VehicleExit))
                        {
                            currentVehIndex += 250;
                        }

                        if (carSpawnFakeStopwatch.ElapsedMilliseconds > timeToSpawnFakeCar)
                        {
                            var rndd = spawnColumnRnd.Next(2, isTwoLaneRoad ? 9 : 18);
                            if (rndd < 5)
                            {
                                var dist = 0;
                                for (var i = 0; i < rndd; i++)
                                {
                                    dist = SpawnCarFake(dist);
                                }
                            }
                            else
                            {
                                SpawnCarFake();
                            }

                            timeToSpawnFakeCar = spawnFakeCarRnd.Next(minTimeFake, maxTimeFake);
                            carSpawnFakeStopwatch = new Stopwatch();
                        }
                        //  if (currentPath != null)
                        //   { 
                        //     UI.ShowSubtitle(currentVehIndex.ToString());
                        //  }
                        if (carSpawnStopwatch.ElapsedMilliseconds > timeToSpawnCar)
                        {

                            var rndd = spawnFakeColumnRnd.Next(2, 12);
                            if (rndd < 6 || (isTwoLaneRoad && rndd < 10))
                            {
                                var dist = 0;
                                for (var i = 0; i < rndd; i++)
                                {
                                    dist = SpawnCar(dist, isLeftLane: isLowSpeed);
                                }
                            }
                            else
                            {
                                SpawnCar(isLeftLane: isLowSpeed);
                            }

                            timeToSpawnCar = spawnCarRnd.Next(minTime, maxTime);
                            carSpawnStopwatch = new Stopwatch();
                        }
                        if (!carSpawnStopwatch.IsRunning)
                        {
                            carSpawnStopwatch.Start();
                        }
                        if (!carSpawnFakeStopwatch.IsRunning)
                        {
                            carSpawnFakeStopwatch.Start();
                        }
                    }
                    else
                    {
                        isCity = false;
                    }
                }
                catch (Exception e)
                {
                    UI.Notify(e.Message + " ");
                }

            }
            else if (currentPath != null || currentFakePath != null)
            {
                currentPath = null;
                currentFakePath = null;
                isNoTraffic = false;
                currentPathName = null;
                RemoveAllCars();
            }
            NorthYankton();
        }

        private static void FindClosestIndex()
        {
            var currIndx = 0;
            var lastPos = 9999999f;
            currentPath = ReadPath("dk7_s");
            currentFakePath = ReadPath("dk7_n");
            dontCheckBoundsStopwatch.Start();
            CurrentZone = new HighwayZoneModel();

            for (var i = 0; i < currentPath.PathList.Count; i++)
            {
                var path = currentPath.PathList[i];
                if (currentPath.PathList[i].Position.DistanceTo(veh.Position) < lastPos)
                {
                    currIndx = i;
                    lastPos = currentPath.PathList[i].Position.DistanceTo(veh.Position);
                }
            }
            currentVehIndex = currIndx;
        }

        private static void ManageCarDelete()
        {
            if (carToDelStopwatch.ElapsedMilliseconds > 700)
            {
                var h = Function.Call<int>((Hash)0x25223CA6B4D20B7F);
                var m = Function.Call<int>((Hash)0x13D2B8ADD79640F2);
                bool isBadWeather = Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Rain")
                       || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Thunder")
                       || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Clearing")
                        || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Foggy");
                for (var i = 0; i < carList.Count; i++)
                {
                    var testCar = carList[i];
                    if (isBadWeather || (h > 18 || h < 7))
                    {
                        testCar.LightsOn = true;
                        testCar.HighBeamsOn = true;
                    }
                    else
                    {
                        testCar.LightsOn = false;
                        testCar.HighBeamsOn = false;
                    }
                    CheckIfCarIsToDel(testCar, i);

                }
                for (var i = 0; i < carListFake.Count; i++)
                {
                    var testCar = carListFake[i];
                    if (isBadWeather || (h > 18 || h < 7))
                    {
                        testCar.LightsOn = true;
                        testCar.HighBeamsOn = true;
                    }
                    else
                    {
                        testCar.LightsOn = false;
                        testCar.HighBeamsOn = false;
                    }
                    CheckIfCarIsToDel(testCar, i, true);
                }
                carToDelStopwatch = new Stopwatch();

            }
            if (!carToDelStopwatch.IsRunning)
                carToDelStopwatch.Start();

        }

        private static LaneModel ReadPath(string filename)
        {
            var text = System.IO.File.ReadAllText($@"D:\Steam\steamapps\common\Grand Theft Auto V\paths\{filename}.txt");

            var obj = text.Split(';');

            LaneModel lane = new LaneModel() { Name = "x", PathList = new List<PathModel>() };
            for (var i = 0; i < obj.Length; i++)
            {

                if (obj[i].Length > 5)
                {
                    var splitted = obj[i].Split('_');
                    var vect = splitted[0].Split(',');
                    var model = new PathModel()
                    {
                        Position = new Vector3(float.Parse(vect[0]), float.Parse(vect[1]), float.Parse(vect[2])),
                        Direction = float.Parse(splitted[1])
                    };
                    lane.PathList.Add(model);
                }

            }
            return lane;
        }

        private static Stopwatch dontCheckBoundsStopwatch = new Stopwatch();
        private static void CheckBounds()
        {
            //   UI.ShowSubtitle(Game.Player.Character.Position.X + "  " + Game.Player.Character.Position.Y + " " + Game.Player.Character.Position.Z + " " + Game.Player.Character.Heading + "   " + currentVehIndex);
            if (dontCheckBoundsStopwatch.ElapsedMilliseconds > 22000)
            {
                dontCheckBoundsStopwatch = new Stopwatch();
            }
            if (!dontCheckBoundsStopwatch.IsRunning)
            {

                // s86 dg - sosnow
                // end  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-2210f, 5631.6f, 10.1f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 20)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("s86_s");
                            currentFakePath = ReadPath("s86_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 3500;
                            isTwoLaneRoad = true;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            World.GetAllVehicles().ToList().ForEach(c =>
                            {
                                if (c.DisplayName != veh.DisplayName)
                                {
                                    c.Delete();
                                }
                            });
                            isCity = true;
                            currentPathName = "s86";
                        }
                        else
                        {
                            currentPath = null;
                            currentPathName = null;
                            currentFakePath = null;
                            isInWinterZone = false;
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }


                // a4 -1 new bahn
                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(3008.5f, -475f, 6.6f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 50)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("a4_-1_n");
                            currentFakePath = ReadPath("a4_-1_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 600;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            isTwoLaneRoad = true;
                            currentPathName = "a4_-1";
                        }
                        else
                        {
                            currentPath = null;
                            currentPathName = null;
                            currentFakePath = null;
                            isInWinterZone = false;
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }


                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(11807.9f, 15339.2f, 6.2f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 20)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPathName == "a4_-1")
                        {
                            currentPath = ReadPath("a4_-3_n");
                            currentFakePath = ReadPath("a4_-3_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            isTwoLaneRoad = false;
                            currentPathName = null;
                        }
                        else
                        {
                            currentPath = null;
                            currentPathName = null;
                            currentFakePath = null;
                            RemoveAllCars();
                            currentPath = ReadPath("a4_-1_s");
                            currentFakePath = ReadPath("a4_-1_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 1300;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            isTwoLaneRoad = true;
                        }
                    }
                }
                // end 
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(21153f, 13576.1f, 4.0f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 20)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("a4_-3_s");
                            currentFakePath = ReadPath("a4_-3_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 2200;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            isTwoLaneRoad = false;
                            currentPathName = null;
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            currentPathName = null;
                            dontCheckBoundsStopwatch.Start();
                            RemoveAllCars();
                        }
                    }
                }

                // a4_dk7 zakpn

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(5165.4f, -986.2f, 6f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 40)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("a4_dk7_n");
                            currentFakePath = ReadPath("a4_dk7_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            isTwoLaneRoad = true;
                            isInWinterZone = true;
                        }
                        else
                        {
                            currentPath = null;
                            currentPathName = null;
                            currentFakePath = null;
                            isInWinterZone = false;
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }
                // end 
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(6204.7f, -6275.5f, 84.5f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 11)
                    {
                        currentPath = ReadPath("a4_dk7_s");
                        currentFakePath = ReadPath("a4_dk7_n");
                        dontCheckBoundsStopwatch.Start();
                        currentVehIndex = 750;
                        CurrentZone = new HighwayZoneModel();
                        RemoveAllCarsFromAutobahn(true);
                        isTwoLaneRoad = true;
                        currentPathName = null;
                    }
                }
                // Local dk47 zakpn

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(6495.2f, -6377.2f, 93.6f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 13)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null || isTwoLaneRoad)
                        {
                            currentPath = ReadPath("dk7_n");
                            currentFakePath = ReadPath("dk7_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCars();
                            isTwoLaneRoad = false;
                        }
                        else
                        {
                            currentPath = null;
                            currentPathName = null;
                            currentFakePath = null;
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }
                // end 
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(6204.7f, -6275.5f, 84.5f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 11)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("dk7_s");
                            currentFakePath = ReadPath("dk7_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 1000;
                            CurrentZone = new HighwayZoneModel();
                            currentPathName = "dk7_n";
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            currentPathName = null;
                            dontCheckBoundsStopwatch.Start();
                            RemoveAllCars();
                        }
                    }
                }

                // dw794

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(2552.4f, 1641.5f, 29.1f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 20)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("dw794_n");
                            currentFakePath = ReadPath("dw794_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }
                // end 
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(3021.518f, 4216.963f, 59.26497f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 20)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("dw794_s");
                            currentFakePath = ReadPath("dw794_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            dontCheckBoundsStopwatch.Start();
                            RemoveAllCars();
                        }
                    }
                }

                // dk94 exit on a4

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-11938.7f, -11036.7f, 12.7f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 19)
                    {
                        var heading = Game.Player.Character.Heading;

                        currentPath = ReadPath("a4_sf_n");
                        currentFakePath = ReadPath("a4_sf_s");
                        dontCheckBoundsStopwatch.Start();
                        currentVehIndex = 2700;
                        CurrentZone = new HighwayZoneModel();
                        AutobahnPropStreamer.currentHwySection.EndConnectPoint = new Vector3(-10515.8f, -5665.2f, 0.7f);
                        RemoveAllCars();
                        isTwoLaneRoad = true;
                        Main.isInSFLVZone = true;
                        currentPathName = "a4_sf";
                        isCity = false;

                    }
                }

                // a4 exit on dk94

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-11659.6f, -11349.8f, 18.1f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 14)
                    {

                        currentPath = ReadPath("dk94_s");
                        currentFakePath = ReadPath("dk94_n");
                        dontCheckBoundsStopwatch.Start();
                        currentVehIndex = 2500;
                        CurrentZone = new HighwayZoneModel();
                        AutobahnPropStreamer.currentHwySection.EndConnectPoint = new Vector3(-12728.4f, -13143.4f, 6.3f);
                        RemoveAllCars();
                        isTwoLaneRoad = true;
                        Main.isInSFLVZone = false;

                    }
                }

                // sf a4 start
                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-7303.5f, 4387.4f, 6.6f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);

                    if (dis < 40)
                    {
                        if (currentPathName == "s86")
                        {
                            currentPath = ReadPath("a4_sf_s");
                            currentFakePath = ReadPath("a4_sf_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 1200;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCars();
                            isTwoLaneRoad = true;
                            isCity = false;
                        }
                        if (currentPathName == "a4_sf")
                        {
                            currentPath = ReadPath("s86_n");
                            currentFakePath = ReadPath("s86_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 3500;
                            isTwoLaneRoad = true;
                            isCity = true;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            currentPathName = "s86";
                        }
                    }
                }

                // dk94

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-2491.4f, -209.4f, 17.7f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 50)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("dk94_n");
                            currentFakePath = ReadPath("dk94_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            AutobahnPropStreamer.currentHwySection.EndConnectPoint = new Vector3(-6676.3f, -1578f, 19.4f);
                            RemoveAllCarsFromAutobahn(true);
                            isTwoLaneRoad = true;
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            isTwoLaneRoad = false;
                            AutobahnPropStreamer.currentHwySection.EndConnectPoint = veh.GetOffsetInWorldCoords(new Vector3(0, -600f, 0));
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }
                // end 
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-11709.9f, -12503f, 6.1f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 50)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("dk94_s");
                            currentFakePath = ReadPath("dk94_n");
                            dontCheckBoundsStopwatch.Start();
                            isTwoLaneRoad = true;
                            currentVehIndex = 800;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                        }
                        else
                        {
                            isTwoLaneRoad = false;
                            currentPath = null;
                            currentFakePath = null;
                            dontCheckBoundsStopwatch.Start();
                            RemoveAllCars();
                        }
                    }
                }

                // s1

                // start  
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-656.4f, 6191.5f, 15.1f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 20)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("s1_n");
                            currentFakePath = ReadPath("s1_s");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            Main.isInSFLVZone = true;
                            isTwoLaneRoad = true;
                            AutobahnPropStreamer.forceDir = true;
                            isNoTraffic = true;
                            AutobahnPropStreamer.currentHwySection = AutobahnPropStreamer.hwySections[12];
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            currentPathName = null;
                            isTwoLaneRoad = false;
                            isNoTraffic = false;
                            Main.isInSFLVZone = false;
                            RemoveAllCars();
                            dontCheckBoundsStopwatch.Start();
                        }
                    }
                }
                // middle for loading fake zone
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-1949.2f, 5082.9f, 14.7f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    var head = veh.Heading;
                    if (dis < 20 && head < 320f && head > 274f)
                    {
                        dontCheckBoundsStopwatch.Start();
                        isInSFLVZone = false;
                        currentPathName = null;
                        AutobahnPropStreamer.forceDir = true;
                        AutobahnPropStreamer.currentHwySection = AutobahnPropStreamer.hwySections[8];
                    }
                }
                // end 
                if (!dontCheckBoundsStopwatch.IsRunning)
                {
                    Vector3 loadPoint = new Vector3(-6411.07f, 3934.417f, 43.32069f);
                    var dis = Game.Player.Character.Position.DistanceTo(loadPoint);
                    if (dis < 70)
                    {
                        var heading = Game.Player.Character.Heading;
                        if (currentPath == null)
                        {
                            currentPath = ReadPath("s1_s");
                            currentPathName = "s1_s";
                            currentFakePath = ReadPath("s1_n");
                            dontCheckBoundsStopwatch.Start();
                            currentVehIndex = 0;
                            isTwoLaneRoad = true;
                            CurrentZone = new HighwayZoneModel();
                            RemoveAllCarsFromAutobahn(true);
                            isNoTraffic = true;
                        }
                        else
                        {
                            currentPath = null;
                            currentFakePath = null;
                            isTwoLaneRoad = false;
                            dontCheckBoundsStopwatch.Start();
                            isNoTraffic = false;
                            currentPathName = null;
                            RemoveAllCars();
                        }
                    }
                }
            }

        }

        public static void RemoveAllCarsFromAutobahn(bool deleteBro = false)
        {
            foreach (var c in fakeLightList)
            {
                c.Vehicle.Delete();
            }
            fakeLightList = new List<CarSpawner.GeneralCar>();

            foreach (var c in CarSpawner.carList)
            {
                c.Vehicle.Delete();
            }
            CarSpawner.carList = new List<CarSpawner.GeneralCar>();
            if (deleteBro)
                Brother.bro = null;
        }

        private static void RemoveAllCars()
        {
            foreach (var c in carList)
            {
                c.Delete();
            }
            carList = new List<Vehicle>();
            indxArr = new int[30000];
            overtakeArr = new float[30000];
            id = 0;

            foreach (var c in carListFake)
            {
                c.Delete();
            }
            carListFake = new List<Vehicle>();
            indxArrFake = new int[30000];

        }

        private static void CheckIfCarIsToDel(Vehicle car, int carIndex, bool isFake = false)
        {
            var indx = currentVehIndex + 250;
            indx = indx > (currentPath.PathList.Count - 1) ? currentVehIndex : indx;
            var posInFront = currentPath.PathList[indx].Position;
            bool isToDel = false;
            var currentIndex = isFake ? indxArrFake[carIndex] : indxArr[carIndex];

            if (isFake)
            {
                if (currentIndex > (currentFakePath.PathList.Count - 150))
                {
                    isToDel = true;
                }
                else
                {
                    var fInd = currentVehIndex + 20;
                    var bInd = currentVehIndex - 20;
                    if (fInd < currentPath.PathList.Count && bInd > 0)
                    {
                        var playerFrontPos = currentPath.PathList[fInd].Position;
                        var playerBackPos = currentPath.PathList[bInd].Position;
                        isToDel = car.Position.DistanceTo(playerBackPos) < car.Position.DistanceTo(playerFrontPos);
                        // isToDel = (car.Position.DistanceTo(veh.Position) < 50) && (car.Position.DistanceTo(posInFront) - 23) > veh.Position.DistanceTo(posInFront);
                        if (currentIndex == (currentFakePath?.PathList.Count - 1))
                        {
                            isToDel = true;
                        }
                    }
                }
            }
            else
            {
                if (isLowSpeed && isTwoLaneRoad)
                {
                    if (car.DashboardColor == VehicleColor.MetallicMidnightSilver)
                    {
                        isToDel = currentIndex > (currentVehIndex + 150);
                    }
                    else
                    {
                        isToDel = currentIndex > (currentVehIndex + 500) || (currentVehIndex - 100) > currentIndex;
                    }
                }
                else
                {
                    var highSpeed = (veh.Speed * 3.6) > 130;
                    isToDel = (veh.Position.DistanceTo(car.Position) > (highSpeed || !isTwoLaneRoad ? 750 : 290))
                        || (((currentVehIndex - 100) > currentIndex) && (highSpeed || !isTwoLaneRoad));
                    if (currentIndex == (currentPath?.PathList.Count - 1))
                    {
                        isToDel = true;
                    }
                    if ((currentIndex) > (currentPath.PathList.Count - 150))
                        isToDel = true;
                }
            }



            if (isToDel)
            {
                car.Delete();
                if (isFake)
                {
                    carListFake.Remove(car);
                    indxArrFake = indxArrFake.Where((val, inn) => inn != carIndex).ToArray();
                }
                else
                {
                    carList.Remove(car);
                    indxArr = indxArr.Where((val, inn) => inn != carIndex).ToArray();
                }
            }
        }

        private static int[] indxArr = new int[30000];
        private static int[] indxArrFake = new int[30000];

        private static float[] overtakeArr = new float[30000];

        private static void ManageCar()
        {
            for (var i = 0; i < carList.Count; i++)
            {
                var testCar = carList[i];
                var currentIndex = indxArr[i];

                currentIndex += 1;

                indxArr[i] = currentIndex;

                if ((currentIndex) > (currentPath.PathList.Count - 150))
                {
                    CheckIfCarIsToDel(testCar, i);
                }
                else if (currentIndex % 20 == 0 || (testCar.Position.DistanceTo(veh.Position) < 22 && currentIndex % 1 == 0) || (testCar.Position.DistanceTo(veh.Position) < 100 && currentIndex % 4 == 0))
                {
                    var itm = currentPath.PathList[currentIndex];
                    testCar.Position = itm.Position;
                    testCar.Heading = itm.Direction;
                }
            }
        }



        private static void ManageFakeCar()
        {
            for (var i = 0; i < carListFake.Count; i++)
            {
                var testCar = carListFake[i];
                var currentIndex = indxArrFake[i];

                currentIndex += 1;
                indxArrFake[i] = currentIndex;

                if ((currentIndex) > (currentFakePath.PathList.Count - 150))
                {
                    CheckIfCarIsToDel(testCar, i, true);
                }
                else
                {
                    if (currentIndex % 60 == 0 || (testCar.Position.DistanceTo(veh.Position) < 150 && currentIndex % 2 == 0) || (testCar.Position.DistanceTo(veh.Position) < 220 && currentIndex % 6 == 0))
                    {
                        var itm = currentFakePath.PathList[currentIndex];
                        testCar.Position = itm.Position;
                        testCar.Heading = itm.Direction;
                    }
                }
            }
        }

        // TWO LANE ROAD

        private static void ManageCar2()
        {
            for (var i = 0; i < carList.Count; i++)
            {
                var testCar = carList[i];
                var currentIndex = indxArr[i];
                var isLeftLane = testCar.DashboardColor == VehicleColor.MetallicMidnightSilver && isTwoLaneRoad;

                if (!isCity)
                {
                    if (isLeftLane)
                        currentIndex += currentIndex % 4 == 0 ? 2 : 1;
                    else
                        currentIndex += 1;
                }

                if (isCity)
                {
                    if (isLeftLane)
                        currentIndex += currentIndex % 5 == 0 ? 2 : 1;
                    else
                        currentIndex += 1;
                }

                indxArr[i] = currentIndex;
                var nIndex = int.Parse(testCar.NumberPlate);
                if ((currentIndex) > (currentPath.PathList.Count - 150))
                {
                    CheckIfCarIsToDel(testCar, i);
                }
                else if (
                    currentIndex % 3 == 0
                    || (testCar.Position.DistanceTo(veh.Position) < 22 && currentIndex % 1 == 0)
                    || (testCar.Position.DistanceTo(veh.Position) < 100 && currentIndex % 1 == 0)
                    || (testCar.TrimColor == VehicleColor.BrushedGold || testCar.TrimColor == VehicleColor.HotPink)
                    )
                {
                    var itm = currentPath.PathList[currentIndex];
                    testCar.Position = itm.Position;
                    if (isLeftLane && isTwoLaneRoad)
                    {
                        var overtakeOffset = overtakeArr[nIndex];
                        if (testCar.TrimColor == VehicleColor.BrushedGold)
                        {
                            if (overtakeOffset > 0f)
                                overtakeArr[nIndex] -= 0.04f;
                            if (overtakeOffset <= 0f)
                            {
                                overtakeArr[nIndex] = 0f;
                                testCar.RightIndicatorLightOn = false;
                                testCar.TrimColor = VehicleColor.MatteBlue;
                            }
                        }
                        if (testCar.TrimColor == VehicleColor.HotPink)
                        {
                            if (overtakeOffset < 6.5f)
                                overtakeArr[nIndex] += 0.04f;
                            if (overtakeOffset >= 6.5f)
                            {
                                overtakeArr[nIndex] = 6.5f;
                                testCar.LeftIndicatorLightOn = false;
                                testCar.TrimColor = VehicleColor.MatteBlue;
                            }
                        }
                        testCar.Position = testCar.GetOffsetInWorldCoords(new Vector3(-(overtakeArr[nIndex]), 0, 0));
                    }
                    testCar.Heading = itm.Direction;
                }
                if (currentIndex % 100 == 0 && isLeftLane && testCar.Position.DistanceTo(veh.Position) < (!isNight ? 40f : 65f) && isTwoLaneRoad)
                {
                    LaneRadar(testCar, nIndex, currentIndex);
                }
            }
        }

        private static void LaneRadar(Vehicle car, int index, int currentIndex)
        {
            bool isCarInFront = false;

            if (overtakeArr[index] == 0f)
            {
                float sensor = 10f;
                for (var i = 0; i < 5; i++)
                {
                    var vehInFront = World.GetClosestVehicle(car.GetOffsetInWorldCoords(new Vector3(0, sensor, 0)), 2f);
                    if (vehInFront?.DisplayName != null)
                    {
                        isCarInFront = true;
                        car.TrimColor = VehicleColor.HotPink;
                        overtakeArr[index] = 0f;
                        car.LeftIndicatorLightOn = true;
                        break;
                    }
                    sensor += 2f;
                }
            }
            if (overtakeArr[index] >= 6.5f && (currentIndex > (currentVehIndex + 20) || !isLowSpeed))
            {
                float sensor = 0f;
                for (var i = 0; i < 25; i++)
                {
                    var vehInFront = World.GetClosestVehicle(car.GetOffsetInWorldCoords(new Vector3(6.5f, sensor, 0)), 2f);
                    if (vehInFront?.DisplayName != null)
                    {
                        isCarInFront = true;
                        car.RightIndicatorLightOn = true;
                        break;
                    }
                    sensor += 2f;
                }
                if (!isCarInFront)
                {
                    car.TrimColor = VehicleColor.BrushedGold;
                }
            }
        }

        private static void ManageFakeCar2()
        {
            for (var i = 0; i < carListFake.Count; i++)
            {
                var testCar = carListFake[i];
                var currentIndex = indxArrFake[i];

                currentIndex += 1;
                indxArrFake[i] = currentIndex;

                if ((currentIndex) > (currentFakePath.PathList.Count - 150))
                {
                    CheckIfCarIsToDel(testCar, i, true);
                }
                else
                {
                    if (currentIndex % 60 == 0 || (testCar.Position.DistanceTo(veh.Position) < 150 && currentIndex % 2 == 0) || (testCar.Position.DistanceTo(veh.Position) < 220 && currentIndex % 6 == 0))
                    {
                        var itm = currentFakePath.PathList[currentIndex];
                        testCar.Position = itm.Position;
                        if (testCar.DashboardColor == VehicleColor.MetallicMidnightSilver)
                        {
                            testCar.Position = testCar.GetOffsetInWorldCoords(new Vector3(-6.5f, 0, 0));
                        }
                        testCar.Heading = itm.Direction;
                    }
                }
            }
        }

        private static float defSpeed = 31f;

        private static Random columnPosition = new Random();
        private static Random isLeftLaneRnd = new Random();
        private static Random forcedLeftLaneRnd = new Random();
        private static Random specialSlowInFrontRnd = new Random();

        private static int SpawnCar(int dist = 0, bool isLeftLane = false, bool isTest = false)
        {
            if (carList.Count < maxCars && ((carList.Count + carListFake.Count) < maxTotalCars || isCity))
            {
                PathModel path = null;
                var indx = 0;
                var fakeIndx = currentVehIndex;
                var specialSlowInFront = isLowSpeed && carList.Where(c => c.DashboardColor == VehicleColor.MatteBlack).ToList().Count <= 1 && specialSlowInFrontRnd.Next(0, 10) > 4;

                if (!isTest)
                {
                    if (dist > 0)
                    {
                        dist += columnPosition.Next(35, 58);
                        indx = dist;
                        if (indx < currentPath.PathList.Count)
                        {
                            path = currentPath.PathList[dist];
                        }
                    }
                    while (true)
                    {
                        if (fakeIndx < (currentPath.PathList.Count - 1))
                        {
                            var p = currentPath.PathList[fakeIndx];
                            if (p.Position.DistanceTo(veh.Position) > 160)
                            {
                                var carAround = World.GetNearbyVehicles(p.Position, 12f);
                                if (carAround.Length <= 0)
                                {
                                    path = p;
                                    indx = fakeIndx;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }

                        if ((!isLowSpeed || currentVehIndex < 2000 || specialSlowInFront) || !isTwoLaneRoad)
                        {
                            fakeIndx += 200;
                        }
                        else
                        {
                            fakeIndx -= 200;
                        }

                    }
                }
                else
                {
                    indx = fakeIndx + 70;
                    path = currentPath.PathList[indx];
                }
                if (path != null)
                {
                    CarModel model = CarsSingleRoad[GenerateRandomNumberBetween(0, (CarsSingleRoad.Count - 1))];
                    var car = World.CreateVehicle(new Model(model.Model.Hash), path.Position, path.Direction);
                    indxArr[carList.Count] = indx;
                    car.Heading = path.Direction;
                    car.LodDistance = isNight ? 2100 : 330;
                    //  car.Speed = defSpeed;
                    car.HasCollision = false;
                    car.FreezePosition = true;
                    car.NumberPlate = id.ToString();
                    overtakeArr[id] = 0f;
                    //  car.CurrentRPM = 0f;
                    if (specialSlowInFront)
                    {
                        car.DashboardColor = VehicleColor.MatteBlack;
                        car.TrimColor = VehicleColor.MatteBlue;
                    }
                    else
                    {
                        if (isLeftLane && car.ClassType != VehicleClass.Commercial)
                        {
                            car.DashboardColor = VehicleColor.MetallicMidnightSilver;
                            car.TrimColor = VehicleColor.MatteBlue;
                        }
                        else
                        {
                            car.DashboardColor = isLeftLaneRnd.Next(0, 5) > 2 && (carList.Count % 2 == 0 || isCity)
                                && car.ClassType != VehicleClass.Commercial
                                && carList.Where(c => c.DashboardColor == VehicleColor.MetallicMidnightSilver).ToList().Count <= (isCity && (h < 20 && h > 5) ? 4 : 1) ? VehicleColor.MetallicMidnightSilver : VehicleColor.MatteBlack;
                            //car.DashboardColor = VehicleColor.MatteBlack;
                            car.TrimColor = VehicleColor.MatteBlue;
                        }
                    }


                    if (car.DashboardColor == VehicleColor.MetallicMidnightSilver)
                    {
                        overtakeArr[id] = 6.5f;
                        //  var rn = forcedLeftLaneRnd.Next(0, 3);
                        //   if (rn == 2)
                        //  {
                        //    overtakeArr[id] = 6.5f;
                        // }
                    }

                    // Function.Call((Hash)0x9EBC85ED0FFFE51C, car, false, false);
                    carList.Add(car);
                    id++;
                }
                return indx;
            }
            return 0;
        }

        private static Random columnPositionFake = new Random();
        private static Random spawnFakeDistRnd = new Random();
        private static Random spawnFakeModelRnd = new Random();
        private static Random isLeftLaneFakeRnd = new Random();
        private static int SpawnCarFake(int dist = 0)
        {

            if (carListFake.Count < maxFakeCars && ((carList.Count + carListFake.Count) < maxTotalCars || isCity))
            {
                try
                {
                    float lastDist = 999999f;
                    PathModel path = null;
                    var indx = 0;
                    var fakeIndx = 0;
                    if (dist > 0)
                    {
                        dist += columnPositionFake.Next(35, 58);
                        indx = dist;
                        if (indx < (currentFakePath.PathList.Count - 1))
                        {
                            path = currentFakePath.PathList[indx];
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (fakeIndx < (currentFakePath.PathList.Count - 1))
                            {
                                var p = currentFakePath.PathList[fakeIndx];
                                if (p.Position.DistanceTo(veh.Position) < lastDist)
                                {
                                    lastDist = p.Position.DistanceTo(veh.Position);
                                    if (lastDist < (isTwoLaneRoad ? spawnFakeDistRnd.Next(290, 330) : spawnFakeDistRnd.Next(600, 900)))
                                    {
                                        path = p;
                                        indx = fakeIndx;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                            fakeIndx += isTwoLaneRoad ? 100 : 300;
                        }
                    }

                    if (path != null)
                    {
                        CarModel model = CarsSingleRoad[spawnFakeModelRnd.Next(0, (CarsSingleRoad.Count - 1))];
                        var car = World.CreateVehicle(new Model(model.Model.Hash), path.Position, path.Direction);

                        try
                        {
                            indxArrFake[carListFake.Count] = indx;
                        }
                        catch (Exception e)
                        {
                            UI.Notify("wrong assigment to indxArrFake mess: " + e.Message);
                        }

                        car.Heading = path.Direction;
                        car.LodDistance = isNight ? 2100 : 390;
                        car.HasCollision = false;
                        car.FreezePosition = true;
                        car.DashboardColor = isLeftLaneRnd.Next(0, 5) > 2
                         && car.ClassType != VehicleClass.Commercial
                         && carListFake.Where(c => c.DashboardColor == VehicleColor.MetallicMidnightSilver).ToList().Count <= 1 ? VehicleColor.MetallicMidnightSilver : VehicleColor.MatteBlack;
                        // Function.Call((Hash)0x9EBC85ED0FFFE51C, car, false, false);
                        carListFake.Add(car);

                    }
                    return indx;
                }
                catch (Exception e)
                {
                    UI.ShowSubtitle("Special error in spawnFakeCar, fake count: " + carListFake.Count + " mess: " + e.Message);
                }

            }
            return 0;
        }

        public static void GetTraffic()
        {
            h = int.Parse(System.IO.File.ReadAllText(@"D:\Steam\steamapps\common\Grand Theft Auto V\scripts\current_time.txt"));

            if (h >= 0 && h <= 5)
            {
                maxCars = 2;
                maxFakeCars = 2;
                minTime = 40000;
                maxTime = 90000;

                minTimeFake = 27000;
                maxTimeFake = 70000;
                if (isTwoLaneRoad)
                {
                    maxCars = 4;
                    maxFakeCars = 4;
                    minTime = 15000;
                    maxTime = 23000;

                    minTimeFake = 4000;
                    maxTimeFake = 12000;

                    if (isCity)
                    {
                        maxCars = 5;
                        maxFakeCars = 6;
                        minTime = 3000;
                        maxTime = 12000;
                        minTimeFake = 4000;
                        maxTimeFake = 9001;
                    }
                }
            }
            if (h == 6)
            {
                maxCars = 3;
                maxFakeCars = 4;
                minTime = 10000;
                maxTime = 23000;

                minTimeFake = 4000;
                maxTimeFake = 13000;

                if (isTwoLaneRoad)
                {
                    maxCars = 5;
                    maxFakeCars = 7;
                    minTime = 4000;
                    maxTime = 13000;
                    minTimeFake = 1000;
                    maxTimeFake = 8001;
                    if (isCity)
                    {
                        maxCars = 12;
                        maxFakeCars = 11;
                        minTime = 100;
                        maxTime = 300;
                        minTimeFake = 100;
                        maxTimeFake = 101;
                    }
                }
            }
            if (h >= 7 && h < 18)
            {
                maxCars = 5;
                maxFakeCars = 7;
                minTime = 4000;
                maxTime = 13000;

                minTimeFake = 1000;
                maxTimeFake = 9000;
                if (isTwoLaneRoad)
                {
                    minTime = 2000;
                    maxTime = 3000;
                    minTimeFake = 100;
                    maxTimeFake = 200;
                    if (isCity)
                    {
                        maxCars = 12;
                        maxFakeCars = 11;
                        minTime = 1000;
                        maxTime = 5000;
                        minTimeFake = 100;
                        maxTimeFake = 101;
                    }
                }
            }
            if (h >= 18 && h < 20)
            {
                maxCars = 4;
                maxFakeCars = 4;
                minTime = 10000;
                maxTime = 23000;

                minTimeFake = 4000;
                maxTimeFake = 16000;

                if (isTwoLaneRoad)
                {
                    maxCars = 5;
                    maxFakeCars = 7;
                    minTime = 4000;
                    maxTime = 13000;

                    minTimeFake = 100;
                    maxTimeFake = 200;

                    if (isCity)
                    {
                        maxCars = 9;
                        maxFakeCars = 11;
                        minTime = 2000;
                        maxTime = 6000;
                        minTimeFake = 100;
                        maxTimeFake = 101;
                    }
                }
            }
            if (h >= 20 && h <= 23)
            {
                maxCars = 3;
                maxFakeCars = 3;
                minTime = 11000;
                maxTime = 39000;

                minTimeFake = 12000;
                maxTimeFake = 27000;
                if (isTwoLaneRoad)
                {
                    maxCars = 5;
                    maxFakeCars = 7;
                    minTime = 4000;
                    maxTime = 13000;

                    minTimeFake = 1000;
                    maxTimeFake = 2000;
                    if (isCity)
                    {
                        maxCars = 5;
                        maxFakeCars = 7;
                        minTime = 4000;
                        maxTime = 13000;
                        minTimeFake = 1000;
                        maxTimeFake = 2001;
                    }
                }
            }
            isNight = h > 21 || h < 6;
            isEvening = h > 18 && h < 22;
            if (currentVehIndex > 22000)
            {
                // maxCars = 2;
                //  maxFakeCars = 2;
            }
        }

        private static bool isWinterLoaded = false;
        private static void NorthYankton()
        {
            if (!isWinterLoaded && isInWinterZone)
            {
                for (var i = 0; i < nyList.Count; ++i)
                {
                    Function.Call((Hash)0x41B4893843BBDB74, nyList[i]);
                }
                isWinterLoaded = true;
            }
            if (!isInWinterZone && isWinterLoaded)
            {
                for (var i = 0; i < nyList.Count; ++i)
                {
                    Function.Call((Hash)0xEE6C5AD3ECE0A82D, nyList[i]);
                }
                isWinterLoaded = false;
            }
        }

        public static List<string> nyList = new List<string>() { "plg_01", "prologue01",
"prologue01_lod",
"prologue01c",
"prologue01c_lod",
"prologue01d",
"prologue01d_lod",
"prologue01e",
"prologue01e_lod",
"prologue01f",
"prologue01f_lod",
"prologue01g",
"prologue01h",
"prologue01h_lod",
"prologue01i",
"prologue01i_lod",
"prologue01j",
"prologue01j_lod",
"prologue01k",
"prologue01k_lod",
"prologue01z",
"prologue01z_lod",
"plg_02",
"prologue02",
"rologue02_lod",
"plg_03",
"prologue03",
"prologue03_lod",
"prologue03b",
"prologue03b_lod",
"prologue03_grv_dug",
"prologue03_grv_dug_lod",
"prologue_grv_torch",
"plg_04",
"prologue04",
"prologue04_lod",
"prologue04b",
"prologue04b_lod",
"prologue04_cover",
"des_protree_end",
"des_protree_start",
"des_protree_start_lod",
"plg_05",
"prologue05",
"prologue05_lod",
"prologue05b",
"prologue05b_lod",
"plg_06",
"prologue06",
"prologue06_lod",
"prologue06b",
"prologue06b_lod",
"prologue06_int",
"prologue06_int_lod",
"prologue06_pannel",
"prologue06_pannel_lod",
"prologue_m2_door",
"prologue_m2_door_lod",
"plg_occl_00",
"prologue_occl",
"plg_rd",
"prologuerd",
"prologuerdb",
"prologuerd_lod",};
    }
}
