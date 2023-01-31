using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HighBeam.NewHighwayTraffic.CarSpawner;
using static HighBeam.NewHighwayTraffic.Zone;
using static HighBeam.NewHighwayTraffic.Helpers;
using static HighBeam.NewHighwayTraffic.Radar;
using XInputDotNetPure;
using System.Diagnostics;

namespace HighBeam.NewHighwayTraffic
{
    public static class Brother
    {
        public static BrotherModel bro = null;
        public class BrotherModel
        {
            public bool IsSpawned { get; set; }
            public GeneralCar CarModel { get; set; }
            public int CarModelHash { get; set; }
            public VehicleColor CarColor { get; set; }
            public VehicleColor CarColor2 { get; set; }
        }

        public static Stopwatch brotherSelectStopwatch = new Stopwatch();
        public static void SelectBrother()
        {
            if (brotherSelectStopwatch.ElapsedMilliseconds > 2000)
            {
                CreateBrother();
                brotherSelectStopwatch = new Stopwatch();
                UI.ShowSubtitle("Joining ride with: " + Index.veh.DisplayName);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Guide == ButtonState.Pressed)
            {
                if (!brotherSelectStopwatch.IsRunning)
                {
                    brotherSelectStopwatch.Start();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Guide == ButtonState.Released)
            {
                brotherSelectStopwatch = new Stopwatch();
            }
        }

        public static void ResetBrother()
        {
            bro.CarModel.Vehicle.Delete();
            bro = null;
        }

        public static void CreateBrother()
        {
            var carStats = GetRandomCar();
            var speed = (float)((150) / 3.6);
            carStats.Speed = speed;
            carStats.isLeftLane = true;
            carStats.forcedLeftLane = true;
            carStats.DefaultSpeed = speed;
            carStats.Class = "family";
            int hash = Index.veh.Model.Hash;
            bro = new BrotherModel()
            {
                IsSpawned = false,
                CarModel = new GeneralCar() { Stats = carStats },
                CarModelHash = hash,
                CarColor = Index.veh.PrimaryColor,
                CarColor2 = Index.veh.SecondaryColor
            };
        }

        public static void ManageSpeed()
        {
            var desiredSpeed = Index.veh.Speed;
            var broSpeed = bro.CarModel.Stats.DefaultSpeed;
            var speedDiff = (broSpeed - desiredSpeed) * 3.6;
            if (speedDiff > 40 || GamePad.GetState(PlayerIndex.One).Triggers.Left > 0.9f)
            {
                bro.CarModel.Stats.deccelerate = true;
                bro.CarModel.Stats.adjustSpeedTo = desiredSpeed;
                bro.CarModel.Stats.DefaultSpeed = desiredSpeed;
            }

            if (Game.IsControlJustPressed(0, Control.VehicleSelectNextWeapon))
            {
                var speedUp = desiredSpeed + 8f;
                bro.CarModel.Stats.accelerate = true;
                bro.CarModel.Stats.adjustSpeedTo = speedUp;
                bro.CarModel.Stats.DefaultSpeed = speedUp;
            }
            var speedInKmh = (int)(Math.Round(broSpeed * 3.6));
            // var fakeSpeed = (int)Math.Round(speedInKmh / (1.28));

        }

        public static Stopwatch laneChangeReactionStopwatch = new Stopwatch();
        public static Stopwatch noReactionStopwatch = new Stopwatch();
        public static Random laneChangeReactionRnd = new Random();
        public static bool toLeftLane = false;
        public static int reactionTime = 0;
        public static void ChangeLane()
        {

            if (bro != null && bro?.IsSpawned != false)
            {
                //  UI.ShowSubtitle("is overtaking: " + bro.CarModel.Stats.IsOvertaking + " is left lane: " + bro.CarModel.Stats.isLeftLane);
                var l = GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed;
                var r = GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed;
                if ((l || r) && !noReactionStopwatch.IsRunning && !bro.CarModel.Stats.IsOvertaking)
                {
                    var isBroLeftLane = BrotherLaneCheck();
                    if ((r && !isBroLeftLane) || (l && isBroLeftLane))
                    {
                        // UI.ShowSubtitle("cant turn already in lane");
                    }
                    else
                    {
                        noReactionStopwatch.Start();

                        reactionTime = laneChangeReactionRnd.Next(1000, 3600);
                        toLeftLane = l;
                        //  UI.ShowSubtitle("will turn in: " + reactionTime + "");
                    }

                }
                if (noReactionStopwatch.ElapsedMilliseconds > reactionTime && noReactionStopwatch.IsRunning)
                {
                    var isBroLeftLane = BrotherLaneCheck();
                  //  UI.ShowSubtitle("now turning! to left lane: " + toLeftLane);
                    noReactionStopwatch = new Stopwatch();
                    if (toLeftLane && !isBroLeftLane && !bro.CarModel.Stats.IsOvertaking)
                    {
                        bro.CarModel.Stats.IsOvertaking = true;
                        bro.CarModel.Stats.SteerPercentage = 0f;
                        bro.CarModel.Vehicle.LeftIndicatorLightOn = true;
                    }

                    if (!toLeftLane && isBroLeftLane && !bro.CarModel.Stats.IsOvertaking)
                    {
                        bro.CarModel.Stats.IsOvertaking = true;
                        bro.CarModel.Stats.SteerPercentage = 0.9f;
                        bro.CarModel.Vehicle.RightIndicatorLightOn = true;
                    }
                    toLeftLane = false;
                    reactionTime = 0;
                }
            }
        }

        public static Stopwatch lastBroSpawn = new Stopwatch();
        public static void ManageBrother()
        {
            if (bro != null)
            {
                if (CurrentZone.Name != null)
                {
                    Vector3 zoneStart = new Vector3() { X = CurrentZone.ZoneBoundary.StartLeftX, Y = CurrentZone.ZoneBoundary.StartLeftY, Z = CurrentZone.ZoneBoundary.ZCoord };
                    Vector3 zoneEnd = new Vector3() { X = CurrentZone.ZoneBoundary.FinishLeftX, Y = CurrentZone.ZoneBoundary.FinishLeftY, Z = CurrentZone.ZoneBoundary.ZCoord };
                    var isAtStart = zoneStart.DistanceTo(Index.veh.Position) < 200;
                    var isAtEnd = zoneEnd.DistanceTo(Index.veh.Position) < 100;
                    if (isAtStart && !bro.IsSpawned)
                    {
                     //   UI.ShowSubtitle("spawning bro");
                        SpawnBrother();
                    }
                    ManageSpeed();
                    if ((Game.IsControlJustPressed(0, Control.VehicleHorn)) && !lastBroSpawn.IsRunning && !isAtEnd)
                    {
                      //  UI.ShowSubtitle("respawning bro");
                        if (!bro.IsSpawned)
                        {
                            SpawnBrother();
                        }
                        else
                        {
                            if (bro.CarModel.Vehicle.Position.DistanceTo(Index.veh.Position) > 600)
                            {
                                SpawnBrother();
                            }
                            RespawnBrother();
                        }
                        lastBroSpawn.Start();
                    }
                    ChangeLane();
                }
                else if (bro.IsSpawned)
                {
                    bro.IsSpawned = false;
                    if (bro?.CarModel?.Vehicle?.DisplayName != null)
                    {
                        bro.CarModel.Vehicle.Delete();
                        bro.CarModel.Vehicle = null;
                    }
                }
            }

            if (lastBroSpawn.ElapsedMilliseconds > 3000)
            {
                lastBroSpawn = new Stopwatch();
            }
        }

        public static void SpawnBrother()
        {
            var metersBeforePlayer = 38;
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
            Vector3 leftLane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = playerCords.Z };
            var head = (float)CurrentZone.HeadingDirection;
            var car = World.CreateVehicle(new Model(bro.CarModelHash), leftLane, head);
            car.Speed = Index.veh.Speed;
            bro.CarModel.Stats.Speed = Index.veh.Speed;
            bro.CarModel.Stats.DefaultSpeed = Index.veh.Speed;
            car.PlaceOnGround();
            car.LightsMultiplier = 6f;
            car.LightsOn = true;
            car.HighBeamsOn = true;
            car.PrimaryColor = bro.CarColor;
            car.SecondaryColor = bro.CarColor2;
            car.SetNoCollision(Index.veh, true);
            car.Heading = CurrentZone.HeadingDirection;
            bro.CarModel.Stats.Heading = head;
            bro.CarModel.Stats.isLeftLane = true;
            bro.CarModel.Stats.IsOvertaking = false;
            bro.CarModel.Stats.HeadingPercentage = 0f;
            bro.CarModel.Stats.SteerPercentage = 0f;
            bro.CarModel.Stats.Deleted = false;
            carList.Add(new GeneralCar() { Vehicle = car, Stats = bro.CarModel.Stats });
            bro.CarModel.Vehicle = car;
            bro.IsSpawned = true;
        }

        public static void RespawnBrother()
        {
            var metersBeforePlayer = 38;
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
            Vector3 leftLane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = playerCords.Z };
            var head = (float)CurrentZone.HeadingDirection;
            var car = bro.CarModel.Vehicle;
            car.Position = leftLane;
            car.Speed = Index.veh.Speed;
            car.Heading = head;
            bro.CarModel.Stats.Speed = Index.veh.Speed;
            bro.CarModel.Stats.DefaultSpeed = Index.veh.Speed;
            car.PlaceOnGround();
            car.Repair();
            car.SetNoCollision(Index.veh, true);
            bro.CarModel.Stats.isLeftLane = true;
            bro.CarModel.Stats.IsOvertaking = false;
            bro.CarModel.Stats.HeadingPercentage = 0f;
            bro.CarModel.Stats.SteerPercentage = 0f;
            bro.CarModel.Stats.Deleted = false;
            bro.CarModel.Vehicle = car;
            bro.IsSpawned = true;
        }
    }
}
