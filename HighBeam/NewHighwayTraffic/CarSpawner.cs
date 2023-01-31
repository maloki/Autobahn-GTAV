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
using System.Diagnostics;

namespace HighBeam.NewHighwayTraffic
{
    public static class CarSpawner
    {
        private static int test = 0;
        public static List<GeneralCar> carList = new List<GeneralCar>();
        public static List<GeneralCar> carListOnRightLane = new List<GeneralCar>();
        public static GeneralCar firstInColumn;
        public static GeneralCar lastInColumn;
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
        public static void ManageTrafficForZone()
        {
            try
            {
                //UI.ShowSubtitle("L: " + carsLeftLaneCount + " R: " + carsRightLaneCount);
                Vector3 zoneEnd = new Vector3() { X = CurrentZone.ZoneBoundary.FinishLeftX, Y = CurrentZone.ZoneBoundary.FinishLeftY, Z = CurrentZone.ZoneBoundary.ZCoord };
                if (zoneEnd.DistanceTo(Index.veh.Position) < 50)
                {
                    for (var i = 0; i < carList.Count; i++)
                    {
                        var car = carList[i];
                        Vector3 del = new Vector3() { X = 0f, Y = 0f, Z = 0 };
                        car.Vehicle.Speed = 0f;
                        car.Vehicle.MarkAsNoLongerNeeded();
                        car.Vehicle.Position = del;
                        car.Stats.Deleted = true;
                    }
                    carsLeftLaneCount = 0;
                    carsRightLaneCount = 0;
                    carList = new List<GeneralCar>();
                }
                else
                {
                    Vector3 zoneStart = new Vector3() { X = CurrentZone.ZoneBoundary.StartLeftX, Y = CurrentZone.ZoneBoundary.StartLeftY, Z = CurrentZone.ZoneBoundary.ZCoord };
                    bool isSlow = (Index.veh.Speed * 3.6) < 250;
                    var isAtStart = zoneStart.DistanceTo(Index.veh.Position) < 200;
                    if (!spawnFasterCarsStopwatch.IsRunning)
                    {
                        spawnFasterCarsStopwatch.Start();
                        spawnFasterCarDelay = GenerateRandomNumberBetween(isAtStart ? 3000 : 6000, 23000);
                        if (isNight)
                        {
                            spawnFasterCarDelay = GenerateRandomNumberBetween(isAtStart ? 22000 : 29000, 60000);
                        }
                        else
                        {
                            spawnFasterCarDelay = GenerateRandomNumberBetween(isAtStart ? 3000 : 6000, 23000);
                        }
                    }

                    if (spawnFasterCarsStopwatch.ElapsedMilliseconds > spawnFasterCarDelay)
                    {
                        if (isSlow)
                        {
                            var count = GenerateRandomNumberBetween(1, ((carList.Count % 4 == 0 || !isNight) ? 3 : 1));
                            for (var i = 0; i < count; ++i)
                            {
                                SpawnCar(rightLane: false, isAtStart: isAtStart, isSlow: isSlow, bypassLimit: true);
                            }
                        }
                        spawnFasterCarsStopwatch = new Stopwatch();

                    }
                    if (!delayBeetwenSpawnsStopwatch.IsRunning)
                    {
                        delayBeetwenSpawnsStopwatch.Start();
                        delayAt = GenerateRandomNumberBetween(10000, 15000);
                        delayLength = GenerateRandomNumberBetween(18000, isNight ? 40000 : 23000);
                    }

                    if (delayBeetwenSpawnsStopwatch.ElapsedMilliseconds > delayAt && delayBeetwenSpawnsStopwatch.ElapsedMilliseconds < (delayAt + delayLength) && !isAtStart)
                    {
                        // dont spawn, delay in spawns 

                    }
                    else if (delayBeetwenSpawnsStopwatch.ElapsedMilliseconds > (delayAt + delayLength))
                    {
                        delayBeetwenSpawnsStopwatch = new Stopwatch();
                    }
                    else
                    {
                        var tickBetweenSpawn = 500;
                        if (isAtStart && carList.Count < 10)
                            tickBetweenSpawn = 80;
                        if (spawnTrafficStopwatch.ElapsedMilliseconds > tickBetweenSpawn && carList.Count < maxNumOfSpawnedCars)
                        {

                            if (carList.Count % 4 == 0)
                            {
                                SpawnCar(rightLane: false, isAtStart: isAtStart);
                            }
                            SpawnCar(rightLane: true, isAtStart: isAtStart);

                            spawnTrafficStopwatch = new Stopwatch();
                        }
                        if (!spawnTrafficStopwatch.IsRunning)
                        {
                            spawnTrafficStopwatch = new Stopwatch();
                            spawnTrafficStopwatch.Start();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                UI.ShowSubtitle(e.Message);
            }

        }

        public static void SpawnCar(bool rightLane = true, bool isAtStart = false, bool isSlow = false, bool bypassLimit = false)
        {
            if (maxNumOfSpawnedCars > carList.Count || (bypassLimit && ((maxNumOfSpawnedCars + 4) > carList.Count)))
            {
                bool isCurrentCarVerySlow = (Index.veh.Speed * 3.6) < 145;
                Vector3 zoneStart = new Vector3() { X = CurrentZone.ZoneBoundary.StartLeftX, Y = CurrentZone.ZoneBoundary.StartLeftY, Z = CurrentZone.ZoneBoundary.ZCoord };
                int metersBefore = GenerateRandomNumberBetween(240, 310);
                if (isSlow)
                    metersBefore = -GenerateRandomNumberBetween(80, 160);
                if (zoneStart.DistanceTo(Index.veh.Position) < 450 && isSlow)
                {
                    metersBefore = -GenerateRandomNumberBetween(30, 70);
                }
                if (isAtStart)
                    metersBefore = GenerateRandomNumberBetween(320, 490);
                if (isAtStart && isCurrentCarVerySlow)
                {
                    metersBefore = GenerateRandomNumberBetween(100, 140);
                }
                Vector3 lane = new Vector3(0, 0, 0);
                bool leftLane = rightLane ? false : true;
                bool dontSpawn = false;
                for (var i = 0; i < 18; i++)
                {
                    var possiblePlacement = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, metersBefore, 0));
                    var leftBorder = GetClosestPoint(
                        new Vector2(CurrentZone.ZoneBoundary.StartLeftX, CurrentZone.ZoneBoundary.StartLeftY),
                        new Vector2(CurrentZone.ZoneBoundary.FinishLeftX, CurrentZone.ZoneBoundary.FinishLeftY),
                        new Vector2(possiblePlacement.X, possiblePlacement.Y)
                        );
                    var rightBorder = GetClosestPoint(
                        new Vector2(CurrentZone.ZoneBoundary.StartRightX, CurrentZone.ZoneBoundary.StartRightY),
                        new Vector2(CurrentZone.ZoneBoundary.FinishRightX, CurrentZone.ZoneBoundary.FinishRightY),
                        new Vector2(possiblePlacement.X, possiblePlacement.Y)
                        );
                    Vector3 centerPoint = new Vector3() { X = ((leftBorder.X + rightBorder.X) / 2), Y = ((leftBorder.Y + rightBorder.Y) / 2), Z = CurrentZone.ZoneBoundary.ZCoord };
                    if (rightLane)
                    {
                        lane = new Vector3() { X = ((centerPoint.X + rightBorder.X) / 2), Y = ((centerPoint.Y + rightBorder.Y) / 2), Z = CurrentZone.ZoneBoundary.ZCoord };
                    }
                    else
                    {
                        lane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = CurrentZone.ZoneBoundary.ZCoord };
                    }
                    var carAtPos = World.GetClosestVehicle(lane, 20f);
                    if (carAtPos == null)
                    {
                        break;
                    }
                    metersBefore += GenerateRandomNumberBetween(63, 100);
                    if (isSlow)
                        metersBefore -= GenerateRandomNumberBetween(63, 100);
                    if (i == 17)
                        dontSpawn = true;
                }
                if (!dontSpawn)
                {
                    CarModel model = rightLane ? Cars[GenerateRandomNumberBetween(0, Cars.Count)] : CarsLeftLane[GenerateRandomNumberBetween(0, CarsLeftLane.Count)];
                    TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                    int secondsSinceEpoch = (int)t.TotalSeconds + carList.Count;
                    var speedForCar = GetSpeedForCar(rightLane, isForced: bypassLimit);
                    var carStats = new CarStatsModel()
                    {
                        Model = model.Model,
                        Class = model.Class,
                        Speed = speedForCar,
                        Heading = (float)CurrentZone.HeadingDirection,
                        Id = secondsSinceEpoch.ToString(),
                        DefaultSpeed = speedForCar,
                    };
                    carStats.isLeftLane = leftLane;
                    var car = World.CreateVehicle(carStats.Model, lane, carStats.Heading);
                    car.Speed = carStats.Speed;
                    car.PlaceOnGround();
                    car.LightsMultiplier = 6f;
                    car.EngineRunning = true;
                    car.TaxiLightOn = leftLane ? true : false;
                    car.LodDistance = isNight ? 1000 : 410;
                    if (bypassLimit)
                        carStats.forcedLeftLane = true;
                    for (var i = 0; i < carList.Count; ++i)
                    {
                        car.SetNoCollision(carList[i].Vehicle, true);
                        car.SetNoCollision(Index.veh, true);
                        if (isTruckMode)
                        {
                            car.SetNoCollision(truckTrailer, true);
                        }
                    }
                    carList.Add(new GeneralCar() { Vehicle = car, Stats = carStats });
                    if (rightLane)
                        carsRightLaneCount++;
                    else
                        carsLeftLaneCount++;
                }
            }
        }

        public static void SpawnFastCar(int metersBeforePlayer)
        {
            if (maxNumOfSpawnedCars > carList.Count)
            {
                CarModel model = SuperCars[GenerateRandomNumberBetween(0, SuperCars.Count)];
                TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                int secondsSinceEpoch = (int)t.TotalSeconds + carList.Count;
                var carStats = new CarStatsModel()
                {
                    Model = model.Model,
                    Class = model.Class,
                    Speed = GetSpeedForCar(true),
                    Heading = (float)CurrentZone.HeadingDirection,
                    Id = secondsSinceEpoch.ToString(),
                    DefaultSpeed = GetSpeedForCar(true),
                };
                var playerCords = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, metersBeforePlayer, 0));
                var leftBorder = GetClosestPoint(
                    new Vector2(CurrentZone.ZoneBoundary.StartLeftX, CurrentZone.ZoneBoundary.StartLeftY),
                    new Vector2(CurrentZone.ZoneBoundary.FinishLeftX, CurrentZone.ZoneBoundary.FinishLeftY),
                    new Vector2(playerCords.X, playerCords.Y)
                    );
                var rightBorder = GetClosestPoint(
                    new Vector2(CurrentZone.ZoneBoundary.StartRightX, CurrentZone.ZoneBoundary.StartRightY),
                    new Vector2(CurrentZone.ZoneBoundary.FinishRightX, CurrentZone.ZoneBoundary.FinishRightY),
                    new Vector2(playerCords.X, playerCords.Y)
                    );
                Vector3 centerPoint = new Vector3() { X = ((leftBorder.X + rightBorder.X) / 2), Y = ((leftBorder.Y + rightBorder.Y) / 2), Z = CurrentZone.ZoneBoundary.ZCoord };
                Vector3 lane;
                var leftLane = false;
                lane = new Vector3() { X = ((centerPoint.X + rightBorder.X) / 2), Y = ((centerPoint.Y + rightBorder.Y) / 2), Z = CurrentZone.ZoneBoundary.ZCoord };
                if (isTruckMode)
                {
                    lane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = playerCords.Z };
                    leftLane = true;
                }
                carStats.isLeftLane = leftLane;
                var car = World.CreateVehicle(carStats.Model, lane, carStats.Heading);
                car.Speed = carStats.Speed;
                car.PlaceOnGround();
                car.LightsMultiplier = 6f;
                car.EngineRunning = true;
                car.FuelLevel = leftLane ? 1 : 0;
                carList.Add(new GeneralCar() { Vehicle = car, Stats = carStats });
            }
        }

        public static GeneralCar CreatePlayerCarModel(bool isTruck = false)
        {
            var vehicleFake = World.CreateVehicle(VehicleHash.Panto, Main.veh.Position, Main.veh.Heading);
            vehicleFake.Alpha = 0;
            vehicleFake.HasCollision = false;
            return new GeneralCar()
            {
                Vehicle = vehicleFake,
                Stats = new CarStatsModel()
                {
                    isLeftLane = isPlayerOnLeftLane,
                    isPlayerCar = true
                }
            };
        }

        public static CarStatsModel GetRandomCar(bool isTruckMode = false, bool isNightSuperFast = false)
        {
            CarModel model = Cars[GenerateRandomNumberBetween(0, Cars.Count)];
            if (isNightSuperFast)
            {
                model = SuperCars[GenerateRandomNumberBetween(0, SuperCars.Count)];
            }
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds + carList.Count;
            var car = new CarStatsModel()
            {
                Model = model.Model,
                Class = model.Class,
                Speed = GetSpeedForCar(true),
                Heading = (float)CurrentZone.HeadingDirection,
                Id = secondsSinceEpoch.ToString(),
                OvertakenCarsIds = new List<string>(),
                DefaultSpeed = GetSpeedForCar(true)
            };
            return car;
        }


        public class GeneralCar
        {
            public Vehicle Vehicle { get; set; }
            public CarStatsModel Stats { get; set; }
        }

        public static void SpawnCarTest()
        {
            if (CurrentZone.Name != null)
            {
                var metersBeforePlayer = GenerateRandomNumberBetween(30, 30);
                var playerCords = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, metersBeforePlayer, 0));
                var leftBorder = GetClosestPoint(
                    new Vector2(CurrentZone.ZoneBoundary.StartLeftX, CurrentZone.ZoneBoundary.StartLeftY),
                    new Vector2(CurrentZone.ZoneBoundary.FinishLeftX, CurrentZone.ZoneBoundary.FinishLeftY),
                    new Vector2(playerCords.X, playerCords.Y)
                    );
                var rightBorder = GetClosestPoint(
                    new Vector2(CurrentZone.ZoneBoundary.StartRightX, CurrentZone.ZoneBoundary.StartRightY),
                    new Vector2(CurrentZone.ZoneBoundary.FinishRightX, CurrentZone.ZoneBoundary.FinishRightY),
                    new Vector2(playerCords.X, playerCords.Y)
                    );
                Vector3 centerPoint = new Vector3() { X = ((leftBorder.X + rightBorder.X) / 2), Y = ((leftBorder.Y + rightBorder.Y) / 2), Z = playerCords.Z };
                Vector3 rightLane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = playerCords.Z };
                Vector3 leftLane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = playerCords.Z };
                var carStats = GetRandomCar();
                var car = World.CreateVehicle(VehicleHash.Elegy2, leftLane, CurrentZone.HeadingDirection);
                switch (test)
                {
                    case 22:
                        carStats.Speed = (float)(100 / 3.6);
                        carStats.isLeftLane = true;
                        car.TaxiLightOn = true;
                        carStats.forcedLeftLane = true;
                        break;
                    case 21:
                        carStats.Speed = (float)(100 / 3.6);
                        carStats.isLeftLane = true;
                        car.TaxiLightOn = false;
                        carStats.forcedLeftLane = true;
                        break;
                    case 0:
                        carStats.Speed = (float)(((Index.veh.Speed * 3.6) + 27) / 3.6);
                        carStats.isLeftLane = true;
                        carStats.forcedLeftLane = false;
                        car.TaxiLightOn = true;
                        carStats.forcedLeftLane = true;
                        break;
                }
                car.Speed = carStats.Speed;
                carStats.DefaultSpeed = car.Speed;
                car.PlaceOnGround();
                carStats.Class = "family";
                car.LightsMultiplier = 6f;
                car.LightsOn = true;
                car.HighBeamsOn = true;
                //Function.Call<int>(Hash.SET_ENTITY_COLLISION, car, false, true);
                carList.Add(new GeneralCar() { Vehicle = car, Stats = carStats });
                test++;
                if (test == 1)
                    test = 0;
            }

        }

        public static float GetSpeedForCar(bool rightLane, bool isForced = false)
        {
            if (CurrentZone.SpeedLimitLeftLane > 0 && CurrentZone.SpeedLimitRightLane > 0)
            {
                return (float)(rightLane ? (CurrentZone.SpeedLimitRightLane / 3.6) : (CurrentZone.SpeedLimitLeftLane / 3.6));
            }
            if (rightLane)
            {
                if (isTruckMode)
                    return (float)(97 / 3.6);
                else if (isRaining)
                    return (float)(109 / 3.6);
                return (float)(118 / 3.6);
            }
            else
            {
                if (isRaining)
                    return (float)(159 / 3.6);
                Vector3 zoneEnd = new Vector3() { X = CurrentZone.ZoneBoundary.FinishLeftX, Y = CurrentZone.ZoneBoundary.FinishLeftY, Z = CurrentZone.ZoneBoundary.ZCoord };
                if ((isEvening || isNight) && isForced)
                {
                    var possibility = GenerateRandomNumberBetween(0, 10);
                    if (possibility == 7)
                    {
                        return (float)(220 / 3.6);
                    }
                }
                return speedForZone;
            }
        }

        private class TrafficHoursModel
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }
    }
}
