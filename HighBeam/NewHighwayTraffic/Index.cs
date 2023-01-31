using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HighBeam.NewHighwayTraffic.Radar;
using static HighBeam.NewHighwayTraffic.CarSpawner;
using static HighBeam.NewHighwayTraffic.Zone;
using static HighBeam.NewHighwayTraffic.MoveCar;
using static HighBeam.NewHighwayTraffic.Helpers;
using static HighBeam.NewHighwayTraffic.FakeTraffic;
using static HighBeam.NewHighwayTraffic.Brother;
using static HighBeam.AutobahnPropStreamer;
using GTA;
using GTA.Math;
using GTA.Native;

namespace HighBeam.NewHighwayTraffic
{
    public static class Index
    {
        public static bool isOnHighway = false;
        public static Vehicle veh;
        public static bool isPlayerOnLeftLane;
        public static bool isNight;
        public static bool isEvening;
        public static bool isRaining;
        private static Stopwatch RadarStopWatch = new Stopwatch();
        public static bool isForcingCarInFrontToChangeLane = false;
        private static Stopwatch ForcingCarInFrontToChangeLaneReaction = new Stopwatch();
        public static int forceReaction = 0;
        private static bool showProximityMeter = false;
        private static bool showFasterCarBehind = false;
        private static Random speedForZoneRnd = new Random();

        public static void RunNewHighwayTraffic()
        {
            try
            {
                veh = Game.Player.LastVehicle;
                // UI.ShowSubtitle(Game.Player.Character.Position.X + "  " + Game.Player.Character.Position.Y + " " + Game.Player.Character.Position.Z + " " + veh.Heading);
                if (isOnHighway && SingleRoadRadar.currentPath == null)
                {
                    UpdateCoords();
                    GetCurrentZone();
                    GetTraffic();
                    Beacons();
                    SelectBrother();
                    if (CurrentZone.Name != null)
                    {
                        if (speedForZone < 0)
                        {
                            SingleRoadRadar.RemoveAllCarsFromAutobahn();
                            if (isEvening)
                            {
                                speedForZone = (float)(speedForZoneRnd.Next(180, 190) / 3.6);
                            }
                            else if (isNight)
                            {
                                speedForZone = (float)(speedForZoneRnd.Next(188, 230) / 3.6);
                            }
                            else
                            {
                                speedForZone = (float)(speedForZoneRnd.Next(160, 180) / 3.6);
                            }
                        }
                        //  UI.ShowSubtitle(CurrentZone.Name);
                        if (!CurrentZone.EnableTrafficOnStreets)
                        {
                            Function.Call((Hash)0xB3B3359379FE77D3, 0f);
                            Function.Call((Hash)0x245A6883D966D537, 0f);
                        }
                        PlayerLaneCheck();
                        ManageTrafficForZone();

                        if (showProximityMeter)
                            RenderProximityMeter();
                        if (showFasterCarBehind)
                            RenderFasterCarBehindIcon();

                        if (RadarStopWatch.ElapsedMilliseconds > 400)
                        {
                            int carsNearPlayer = 0;
                            int carsFasterThanPlayer = 0;
                            for (var i = 0; i < carList.Count; ++i)
                            {
                                GeneralCar currentCar = carList[i];
                                /*  if (ForcingCarInFrontToChangeLaneReaction.ElapsedMilliseconds > forceReaction && currentCar.Vehicle.Position.DistanceTo(Index.veh.Position) < 30 && currentCar.Stats.isLeftLane)
                                  {
                                      ForceCarInFrontToChangeLane(currentCar);
                                      ForcingCarInFrontToChangeLaneReaction = new Stopwatch();
                                      isForcingCarInFrontToChangeLane = false;
                                  }*/
                                NewRadar(currentCar);
                                //    CheckMirrors(currentCar); 
                                SetCarLight(currentCar);
                                CheckIfCarIsToDelete(currentCar);
                                RunMoveCar(currentCar);
                                // AdjustCarSpeed(currentCar);
                                if (!isPlayerOnLeftLane && currentCar.Stats.isLeftLane)
                                    carsNearPlayer += ProximityMeter(currentCar);
                                if (isPlayerOnLeftLane && currentCar.Stats.isLeftLane)
                                    carsFasterThanPlayer += FasterCarBehind(currentCar);
                                if (carsNearPlayer > 0)
                                    showProximityMeter = true;
                                else
                                    showProximityMeter = false;
                                if (carsFasterThanPlayer > 0)
                                    showFasterCarBehind = true;
                                else
                                    showFasterCarBehind = false;

                            }
                            RadarStopWatch = new Stopwatch();
                        }
                        for (var i = 0; i < carList.Count; ++i)
                        {
                            GeneralCar currentCar = carList[i];
                            Overtake(currentCar);
                            AdjustCarSpeed(currentCar);
                        }
                        if (Game.IsControlJustReleased(0, GTA.Control.VehicleSelectNextWeapon))
                        {
                            //   SpawnCarTest();
                            /* isForcingCarInFrontToChangeLane = true;
                             ForcingCarInFrontToChangeLaneReaction = new Stopwatch();
                             ForcingCarInFrontToChangeLaneReaction.Start();
                             forceReaction = GenerateRandomNumberBetween(1300, 4000);*/
                        }
                        if (!RadarStopWatch.IsRunning)
                        {
                            RadarStopWatch = new Stopwatch();
                            RadarStopWatch.Start();
                        }
                        RunFakeTraffic();
                    }
                    else
                    {
                        speedForZone = -10;
                    }
                    ManageBrother();
                }
                else if (bro != null)
                {
                    ResetBrother();
                }
            }
            catch (Exception e)
            {
                UI.Notify(e.Message);
            }
        }
    }
}
