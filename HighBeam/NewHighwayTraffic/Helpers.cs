using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HighBeam.NewHighwayTraffic.Zone;
using static HighBeam.NewHighwayTraffic.Index;
using static HighBeam.NewHighwayTraffic.CarSpawner;
using System.Drawing;

namespace HighBeam.NewHighwayTraffic
{
    public static class Helpers
    {
        private static Random r = new Random();
        private static Random rnd2 = new Random();
        public static int x = 0;
        public static int y = 0;
        public static int z = 0;

        public static int ProximityMeter(GeneralCar c)
        {
            var dist = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, -6f, 0)).DistanceTo(c.Vehicle.Position) < 45f;
            Vector3 zoneEnd = new Vector3() { X = CurrentZone.ZoneBoundary.FinishRightX, Y = CurrentZone.ZoneBoundary.FinishRightY, Z = CurrentZone.ZoneBoundary.ZCoord };
            var distToEnd = c.Vehicle.Position.DistanceTo(zoneEnd) > Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, -6f, 0)).DistanceTo(zoneEnd);
            if (dist && distToEnd)
                return 1;
            else
                return 0;
        }

        public static int FasterCarBehind(GeneralCar c)
        {
            var dist = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, -6f, 0)).DistanceTo(c.Vehicle.Position) < 85f;
            Vector3 zoneEnd = new Vector3() { X = CurrentZone.ZoneBoundary.FinishRightX, Y = CurrentZone.ZoneBoundary.FinishRightY, Z = CurrentZone.ZoneBoundary.ZCoord };
            var distToEnd = c.Vehicle.Position.DistanceTo(zoneEnd) > Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, -6f, 0)).DistanceTo(zoneEnd);
            if (dist && distToEnd && c.Stats.DefaultSpeed > veh.Speed)
                return 1;
            else
                return 0;
        }

        public static void RenderProximityMeter()
        {
            var cont = new UIContainer(new Point(5, UI.HEIGHT - 180), new Size(3, 15), Color.FromArgb(255, 251, 95, 21));
            cont.Enabled = true;
            cont.Draw();
        }

        public static void RenderFasterCarBehindIcon()
        {
            var cont = new UIContainer(new Point(5, UI.HEIGHT - 180), new Size(3, 15), Color.FromArgb(255, 0, 0, 247));
            var cont2 = new UIContainer(new Point(5, UI.HEIGHT - 180), new Size(11, 2), Color.FromArgb(255, 0, 0, 247));
            var cont3 = new UIContainer(new Point(5, (UI.HEIGHT - 180) + 7), new Size(11, 2), Color.FromArgb(255, 0, 0, 247));
            var cont4 = new UIContainer(new Point(5, (UI.HEIGHT - 180) + 15), new Size(11, 2), Color.FromArgb(255, 0, 0, 247));
            cont.Enabled = true;
            cont.Draw();
            cont2.Draw();
            cont3.Draw();
            cont4.Draw();
        }

        public static void SetCarLight(GeneralCar car)
        {
            var h = Function.Call<int>((Hash)0x25223CA6B4D20B7F);
            var m = Function.Call<int>((Hash)0x13D2B8ADD79640F2);
            var s = Function.Call<int>((Hash)0x494E97C2EF27C470);
            bool isBadWeather = Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Rain")
                   || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Thunder")
                   || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Clearing")
                    || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Foggy");
            if (isBadWeather || (h > 18 || h < 7))
            {
                car.Vehicle.LightsOn = true;
                car.Vehicle.HighBeamsOn = true;
            }
            else
            {
                car.Vehicle.LightsOn = false;
                car.Vehicle.HighBeamsOn = false;
            }
        }

        public static bool PlayerLaneCheck()
        {
            var leftBorder = GetClosestPoint(
                                  new Vector2(CurrentZone.ZoneBoundary.StartLeftX, CurrentZone.ZoneBoundary.StartLeftY),
                                  new Vector2(CurrentZone.ZoneBoundary.FinishLeftX, CurrentZone.ZoneBoundary.FinishLeftY),
                                  new Vector2(veh.Position.X, veh.Position.Y)
                              );
            var rightBorder = GetClosestPoint(
                new Vector2(CurrentZone.ZoneBoundary.StartRightX, CurrentZone.ZoneBoundary.StartRightY),
                new Vector2(CurrentZone.ZoneBoundary.FinishRightX, CurrentZone.ZoneBoundary.FinishRightY),
                new Vector2(veh.Position.X, veh.Position.Y)
            );
            Vector3 centerPoint = new Vector3() { X = ((leftBorder.X + rightBorder.X) / 2), Y = ((leftBorder.Y + rightBorder.Y) / 2), Z = veh.Position.Z };
            Vector3 leftLane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = veh.Position.Z };
            var dist = veh.Position.DistanceTo(leftLane);
            if (dist < 3.5f)
            {
                isPlayerOnLeftLane = true;
                veh.TaxiLightOn = true;
                return true;
            }
            else
            {
                isPlayerOnLeftLane = false;
                veh.TaxiLightOn = false;
                return false;
            }
        }

        public static bool BrotherLaneCheck()
        {
            var veh = Brother.bro?.CarModel?.Vehicle;
            var leftBorder = GetClosestPoint(
                                  new Vector2(CurrentZone.ZoneBoundary.StartLeftX, CurrentZone.ZoneBoundary.StartLeftY),
                                  new Vector2(CurrentZone.ZoneBoundary.FinishLeftX, CurrentZone.ZoneBoundary.FinishLeftY),
                                  new Vector2(veh.Position.X, veh.Position.Y)
                              );
            var rightBorder = GetClosestPoint(
                new Vector2(CurrentZone.ZoneBoundary.StartRightX, CurrentZone.ZoneBoundary.StartRightY),
                new Vector2(CurrentZone.ZoneBoundary.FinishRightX, CurrentZone.ZoneBoundary.FinishRightY),
                new Vector2(veh.Position.X, veh.Position.Y)
            );
            Vector3 centerPoint = new Vector3() { X = ((leftBorder.X + rightBorder.X) / 2), Y = ((leftBorder.Y + rightBorder.Y) / 2), Z = veh.Position.Z };
            Vector3 leftLane = new Vector3() { X = ((leftBorder.X + centerPoint.X) / 2), Y = ((leftBorder.Y + centerPoint.Y) / 2), Z = veh.Position.Z };
            var dist = veh.Position.DistanceTo(leftLane);
            if (dist < 3.5f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void UpdateCoords()
        {
            x = int.Parse(Math.Round((decimal)Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 0, 0)).X, 0).ToString());
            y = int.Parse(Math.Round((decimal)Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 0, 0)).Y, 0).ToString());
            z = int.Parse(Math.Round((decimal)Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 0, 0)).Z, 0).ToString());
            isRaining = Function.Call<float>((Hash)0x96695E368AD855F3) > 0.3f;
        }

        public static Vector2 GetClosestPoint(Vector2 v1, Vector2 v2, Vector2 p)
        {
            Vector2 AP = p - v1;
            Vector2 AB = v2 - v1;
            float ab2 = AB.X * AB.X + AB.Y * AB.Y;
            float ap_ab = AP.X * AB.X + AP.Y * AB.Y;
            float t = ap_ab / ab2;
            Vector2 closest = v1 + AB * t;
            return closest;
        }

        public static float dist2(Vector2 v1, Vector2 v2)
        {
            return (float)(Math.Sqrt(v1.X - v2.X) + Math.Sqrt(v1.Y - v2.Y));
        }

        public static double Sign(int p1x, int p1y, int p2x, int p2y, int p3x, int p3y)
        {
            return (p1x - p3x) * (p2y - p3y) - (p2x - p3x) * (p1y - p3y);
        }

        public static bool PointInTriangle(int pX, int pY, int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            bool b1;
            bool b2;
            bool b3;

            b1 = Sign(pX, pY, v1x, v1y, v2x, v2y) < 0.0;
            b2 = Sign(pX, pY, v2x, v2y, v3x, v3y) < 0.0;
            b3 = Sign(pX, pY, v3x, v3y, v1x, v1y) < 0.0;

            return ((b1 == b2) && (b2 == b3));
        }

        public static int GenerateRandomNumberBetween(int min, int max, bool isLookingRnd = false)
        {
            // using second random generator if function is called from the multiple threads at the same time, to prevent generating the same numbers
            return isLookingRnd ? rnd2.Next(min, max) : r.Next(min, max);
        }
    }
}
