using GTA;
using GTA.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static HighBeam.Main;
using static HighBeam.HighwayTrafficOld;
using static HighBeam.AutobahnZones;
using static HighBeam.NewHighwayTraffic.Index;
using static HighBeam.NewHighwayTraffic.Zone;
using GTA.Native;
using System.Drawing;
using HighBeam.NewHighwayTraffic;

namespace HighBeam
{
    public static class AutobahnPropStreamer
    {
        public static List<PropModel> AutobahnPropsToRender = new List<PropModel>();
        public static PropFamilyModel AutobahnRenderedProps = new PropFamilyModel();
        public static List<PropModel> ConnectPropsToRender = new List<PropModel>();
        public static PropFamilyModel ConnectRenderedProps = new PropFamilyModel();
        public static PropFamilyModel BaseRenderedProps = new PropFamilyModel();
        public static List<PropModel> NyAirportPropsToRender = new List<PropModel>();
        public static PropFamilyModel NyAirportRenderedProps = new PropFamilyModel();
        public static List<PropModel> PaletoOfficeLiftToRender = new List<PropModel>();
        public static PropFamilyModel PaletoOfficeRenderedProps = new PropFamilyModel();
        public static bool isXmlLoaded = false;
        public static bool isAutobahnRendered = false;
        public static bool isPaletoOfficeRenderd = false;
        public static Stopwatch RenderAutobahnStopWatch = new Stopwatch();
        public static bool isTruckBaseRendered = false;
        public static bool isNyAirportRenedered = false;
        public static Stopwatch isInLiftStopWatch = new Stopwatch();
        public static Stopwatch afterLiftExitStopwatch = new Stopwatch();
        public static bool isLiftRunning = false;
        public static StepModel currentStep = null;
        public static float currentLiftZ = 0;
        public static Prop currentLiftProp;
        public static Prop beachBridgeLightRef = null;
        public static Prop beachSideWallLightRef = null;
        public static bool init = false;
        public static List<Color> colors = new List<Color>() {
            Color.FromArgb(131, 45, 206),
            Color.FromArgb(0, 32, 127),
            Color.FromArgb(233, 39, 91)
        };
        public static int colorRnd = 0;
        public static HwySection currentHwySection = null;
        private static Vector3 posToDelProps = new Vector3(6306.7f, -1732.9f, 24.9f);
        private static Vehicle vehLoc;
        public class StepModel
        {
            public int step;
            public float fromZ;
            public float toZ;

        }
        public static void RunAutobahnPropStreamer()
        {
            vehLoc = Game.Player.LastVehicle;
            // UI.ShowSubtitle(Game.Player.Character.Heading.ToString());
            CheckIfPlayerIsCloseToHwy();
            if (NewHighwayTraffic.Index.isOnHighway)
            {
                ManageHwy();
                //  if (vehLoc.Position.DistanceTo(posToDelProps) < 600)
                //   RemovePropsAtLC();
            }
        }

        public static List<int> propsToRemoveHashes = new List<int>()
        {
            1048501890, -73333162, -2113539824, 136645433, 1430257647, 1962326206
        };

        public static void RemovePropsAtLC()
        {
            for (var ii = 0; ii < propsToRemoveHashes.Count; ++ii)
            {
                var pos = new Vector3(6306.7f, -1732.9f, 24.9f);
                var props = World.GetNearbyProps(vehLoc.Position, 360f, new Model(propsToRemoveHashes[ii]));
                for (var i = 0; i < props.Length; ++i)
                {
                    props[i].Position = new Vector3(0, 0, 0);
                    props[i].Delete();
                }
            }
        }



        public class HwySection
        {
            public int Id { get; set; }
            public int refId { get; set; }
            public Vector3 InitialLoadPoint { get; set; }
            public Vector3 InitialLoadPointAlt { get; set; }
            public Vector3 StartConnectPoint { get; set; }
            public Vector3 EndConnectPoint { get; set; }
            // for faking previous zone to load proper in front, for example if zone is totally outside like s1 (vegas)
            public Vector3 EndConnectPointFakeForce { get; set; }
            public string EndConnectFileNameForce { get; set; }
            public List<string> Chunks { get; set; }
            public string ActiveZoneAtLoad { get; set; }
        }

        private static List<Prop> beaconList = new List<Prop>();
        private static List<Prop> visibleBeaconList = new List<Prop>();
        private static Stopwatch getVisibleBeaconListStopwatch = new Stopwatch();
        private static Stopwatch beaconFlashStopwatch = new Stopwatch();
        private static bool isBeaconOn = false;
        private static string currentZoneName = "";

        public static void Beacons()
        {
            if (beaconList.Count > 0)
            {
                bool isBadWeather = false && Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Rain")
             || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Thunder")
             || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Clearing")
              || Function.Call<bool>((Hash)0x2FAA3A30BEC0F25D, "Foggy");

                if (isEvening || isNight)
                {
                    if (!beaconFlashStopwatch.IsRunning)
                        beaconFlashStopwatch.Start();
                    if (beaconFlashStopwatch.ElapsedMilliseconds > 2000)
                    {
                        isBeaconOn = !isBeaconOn;
                    }
                    if (beaconFlashStopwatch.ElapsedMilliseconds > 2000)
                    {
                        beaconFlashStopwatch = new Stopwatch();
                    }
                    if (isBeaconOn)
                    {
                        RenderBeacons();
                    }
                    if (getVisibleBeaconListStopwatch.ElapsedMilliseconds > 5000)
                    {
                        getVisibleBeaconListStopwatch = new Stopwatch();
                    }
                    if (!getVisibleBeaconListStopwatch.IsRunning)
                    {
                        visibleBeaconList = new List<Prop>();
                        for (var i = 0; i < beaconList.Count; i++)
                        {
                            var beacon = beaconList[i];
                            var front = beacon.Position.DistanceTo(Main.veh.GetOffsetInWorldCoords(new Vector3(0, 50f, 0)));
                            var back = beacon.Position.DistanceTo(Main.veh.GetOffsetInWorldCoords(new Vector3(0, -50f, 0)));
                            // UI.ShowSubtitle("front: " + front + " back: " + back + " isBehind: " + (front > back) + " dis: " + front);

                            if (front < back && front < 1550f && visibleBeaconList.Count < 31)
                            {
                                visibleBeaconList.Add(beacon);
                            }
                        }
                        getVisibleBeaconListStopwatch.Start();
                    }
                }
            }
        }

        private static void RenderBeacons()
        {
            for (var i = 0; i < visibleBeaconList.Count; ++i)
            {
                var beacon = visibleBeaconList[i];

                var pos1 = beacon.GetOffsetInWorldCoords(new Vector3(-0.4f, 0f, 0.4f));
                var pos2 = beacon.GetOffsetInWorldCoords(new Vector3(0.4f, 0f, 0.4f));
                var off = 0.02;
                for (var ii = 0; ii < 1; ++ii)
                {
                    Function.Call((Hash)0x6B7256074AE34680, pos1.X, pos1.Y, pos1.Z + off, pos2.X, pos2.Y, pos2.Z + off, 255, 5, 5, 255);
                    //   off += 0.02;
                }
            }
        }

        private static Stopwatch manageHwyStopwatch = new Stopwatch();
        public static List<HwySection> hwySections = new List<HwySection>()
        {
            // start is end in north dir
                   new HwySection()
            {
                Id = -7,
                refId = 0,
                Chunks = new List<string>()
                {
                    "a4_-6_wro_base.xml"
                },
                EndConnectPoint =  new Vector3(24432.1f, 18712.2f, 4f),
                StartConnectPoint = new Vector3(0, 0, 0),
            },
                new HwySection()
            {
                Id = -6,
                refId = 1,
                Chunks = new List<string>()
                {
                    "a4_-5_wro_base.xml"
                },
                EndConnectPoint =  new Vector3(20249.7f, 19976.8f, 4f),
                StartConnectPoint = new Vector3(24432.1f, 18712.2f, 4f),
            },
                new HwySection()
            {
                Id = -5,
                refId = 2,
                Chunks = new List<string>()
                {
                    "a4_-4_wro_base_new.xml"
                },
                EndConnectPoint =  new Vector3(19019.8f,24283.2f, 6.3f),
                StartConnectPoint = new Vector3(20249.7f, 19976.8f, 4f),
            },
              new HwySection()
            {
                Id = -4,
                refId = 3,
                Chunks = new List<string>()
                {
                    "a4_-3_wro_base_new.xml"
                },
                EndConnectPoint =  new Vector3(12225.5f,15250.4f,6.4f),
                StartConnectPoint = new Vector3(19019.8f,24283.2f, 6.3f),
            },
             new HwySection()
            {
                Id = -3,
                refId = 4,
                Chunks = new List<string>()
                {
                    "a4_-2_wro_base.xml"
                },
                EndConnectPoint =  new Vector3(8324.6f,9307.3f,6.6f),
                StartConnectPoint = new Vector3(12225.5f,15250.4f,6.4f),
            },
               new HwySection()
            {
                Id = -2,
                refId = 5,
                Chunks = new List<string>()
                {
                    "a4_-2_wro_new_base.xml"
                },
                EndConnectPoint =  new Vector3(6899.7f, 6091.7f, 6.6f),
                StartConnectPoint = new Vector3(8324.6f,9307.3f,6.6f),
            },
             new HwySection()
            {
                Id = -1,
                refId = 6,
                Chunks = new List<string>()
                {
                    "a4_-1_wro_new_base.xml"
                },
                EndConnectPoint =  new Vector3(3275.072f,25.8f,6.6f),
                StartConnectPoint = new Vector3(6899.7f, 6091.7f, 6.6f),
            },
            new HwySection()
            {
                Id = 0,
                refId = 7,
                Chunks = new List<string>()
                {
                    "a4_0_wro_base.xml", "a4_0_wro_addons.xml", "a4_0_wro_alt_entrance.xml"
                },
               // InitialLoadPoint = new Vector3(729.7f, -2424.9f, 20.0f),
               InitialLoadPoint = new Vector3(253.3f, -2663.2f, 17.9f),
                InitialLoadPointAlt = new Vector3(2455.337f, -496.14f, 70.1f),
                StartConnectPoint = new Vector3(3275.072f,25.8f,6.6f),
                EndConnectPoint = new Vector3(260.39f, -3769.27f, 6.03f),
             //   ActiveZoneAtLoad = "A4_N_0_0"
            },
              new HwySection()
            {
                Id = 1,
                refId = 8,
                Chunks = new List<string>()
                {
                    "a4_1_wro_base.xml", "a4_1_wro_addons3.xml",
                    "a4_1_wro_new_entrance.xml",
                    "a4_2_wro_base.xml", "a4_2_wro_addons3.xml"
                },
                InitialLoadPoint = new Vector3(-1663.55f, -696.26f, 11.25f),
                StartConnectPoint = new Vector3(260.39f, -3769.27f, 6.03f),
                EndConnectPoint = new Vector3(-3106.81f, 3521.51f, 6.4f),
                EndConnectPointFakeForce = new Vector3(-1602f, 5247.6f, 14.7f),
                EndConnectFileNameForce = "a4_87_wro_connect.xml",
                ActiveZoneAtLoad = "A4_N_1_2",
            },
             /*      new HwySection()
            {
                Id = 1,
                Chunks = new List<string>()
                {
                    "a4_1_wro_base.xml", "a4_1_wro_exit_addons.xml", "a4_1_wro_exit_base.xml", "a4_1_wro_addons3.xml"
                },
                InitialLoadPoint = new Vector3(-2205.33f, -360.35f, 12.57f),
                StartConnectPoint = new Vector3(260.39f, -3769.27f, 6.03f),
                EndConnectPoint = new Vector3(-2664.9f, -652.48f, 6.3f),
            },
               new HwySection()
            {
                Id = 2,
                Chunks = new List<string>()
                {
                    "a4_2_wro_base.xml", "a4_2_wro_addons3.xml"
                },
                InitialLoadPoint = new Vector3(0f, 0f, -99f),
                StartConnectPoint = new Vector3(-2664.9f, -652.48f, 6.3f),
                EndConnectPoint = new Vector3(-3106.81f, 3521.51f, 6.4f),
            },*/
               new HwySection()
    {
        Id = 2,
                refId = 9,
                Chunks = new List<string>()
                {
                    "a4_3_wro_base.xml" , "a4_3_wro_rondabout.xml"
                },
                InitialLoadPoint = new Vector3(-693.0f, 5841.9f, 16.1f),
                StartConnectPoint = new Vector3(-3106.81f, 3521.51f, 6.4f),
                EndConnectPoint = new Vector3(601.3f, 7191.1f, 6.0f),
                ActiveZoneAtLoad = "A4_N_3_0",
                 // old new Vector3(3856.48f, -921.1f, 6.0f)
            },
               /*CONNECT ID -1 FROM THIS POINT BECAUSE WRONG ID ASSIGNED*/
                new HwySection()
    {
        Id = 3,
                refId = 10,
                Chunks = new List<string>()
                {
                    "a4_4_wro_base.xml"
                },
              //  InitialLoadPoint = new Vector3(-693.0f, 5841.9f, 16.1f),
                StartConnectPoint = new Vector3(601.3f, 7191.1f, 6.0f),
                EndConnectPoint = new Vector3(4939.3f, -966.3f, 6.0f),
              //  ActiveZoneAtLoad = "A4_N_3_0",
                 // old new Vector3(3856.48f, -921.1f, 6.0f)
            },
                     new HwySection()
    {
        Id = 4,
                refId = 11,
                Chunks = new List<string>()
                {
                    "a4_5_wro_base.xml"
                },
                StartConnectPoint = new Vector3(4499.4f, -932.0f, 5.7f),
                EndConnectPoint = new Vector3(4434.7f, -3991.5f, 126.2f),
            },
                          new HwySection()
    {
                 Id = 5,
                refId = 12,
                Chunks = new List<string>()
                {
                    "a4_6_wro_base.xml"
                },
                InitialLoadPoint = new Vector3(5434.9f, -5127.3f, 78f),
                StartConnectPoint = new Vector3(4434.7f, -3991.5f, 126.2f),
                EndConnectPoint = new Vector3(708.9f, -4458.3f, 133.0f),
            },
             // Single road DW794 train tracks 

              new HwySection() // fake section for an offset
    {
        Id = 87,
                refId = 13,
                Chunks = new List<string>()
                {
                    "a4_88_wro_connect.xml"
                },
                EndConnectPoint = new Vector3(-1602f, 5247.6f, 14.7f),
                StartConnectPoint = new Vector3(0, 0, 0),
    },
               new HwySection()
    {
        Id = 88,
                refId = 14,
                Chunks = new List<string>()
                {
                    "s1_0_base.xml"
                },
                EndConnectPoint = new Vector3(-4846.8f, 2831.4f, 28.2f),
                StartConnectPoint = new Vector3(0, 0, 0),
               // InitialLoadPoint = new Vector3(-3024.8f, 1920.8f, 28.4f),
              //  InitialLoadPointAlt = new Vector3(2129.1f, 4747.65f, 41.2f)
    },
                    new HwySection()
    {
        Id = 89,
                refId = 15,
                Chunks = new List<string>()
                {
                    "s1_1_base.xml"
                },
                EndConnectPoint = new Vector3(-6401.6f, 4314.5f, 38.2f),
                StartConnectPoint = new Vector3(-4846.8f, 2831.4f, 28.2f),
                InitialLoadPoint = new Vector3(-6311.9f, 4021f, 44.3f),
    },
          new HwySection()
        {
            Id = 90,
                    refId = 16,
                    Chunks = new List<string>()
                    {
                        "s1_2_base.xml"
                    },
                    EndConnectPoint = new Vector3(-3563.8f, 5614.3f, 10.8f),
                    StartConnectPoint = new Vector3(-6401.6f, 4314.5f, 38.2f),
        },
              new HwySection()
        {
            Id = 91,
                    refId = 17,
                    Chunks = new List<string>()
                    {
                        "s1_3_base.xml"
                    },
                    EndConnectPoint = new Vector3(0,0,0),
                    StartConnectPoint =new Vector3(-3563.8f, 5614.3f, 10.8f),
        },
            new HwySection()
            {
                Id = 100,
                refId = 18,
                Chunks = new List<string>()
                {
                    "s7_addons.xml"
                },
                InitialLoadPoint = new Vector3(-448.2f, 5924.4f, 32.5f),
                StartConnectPoint = new Vector3(2663.9f, 6458.4f, 76.1f),
                EndConnectPoint = new Vector3(-2701.4f, -26.8f, 15.6f),
            },
             new HwySection()
            {
                Id = 101,
                refId = 19,
                Chunks = new List<string>()
                {
                    "dk94_olk.xml"
                },
                StartConnectPoint = new Vector3(-2701.4f, -26.8f, 15.6f),
                EndConnectPoint = new Vector3(-6676.3f, -1578f, 19.4f),
            },
              new HwySection()
              {
                Id = 102,
                refId = 20,
                Chunks = new List<string>()
                {
                    "dk94_sla.xml"
                },
                StartConnectPoint = new Vector3(-6676.3f, -1578f, 19.4f),
                EndConnectPoint = new Vector3(-9632.2f, -5033.5f, 10.9f),
            },
               new HwySection()
              {
                Id = 103,
                refId = 21,
                Chunks = new List<string>()
                {
                    "dk94_dg.xml"
                },
                StartConnectPoint = new Vector3(-9632.2f, -5033.5f, 10.9f),
                EndConnectPoint = new Vector3(-11463f, -9726.3f, 15f),
            },
                 new HwySection()
              {
                Id = 104,
                refId = 22,
                Chunks = new List<string>()
                {
                    "dk94_ktw_a4.xml"
                },
                StartConnectPoint = new Vector3(-11463f, -9726.3f, 15f),
                EndConnectPoint = new Vector3(-12728.4f, -13143.4f, 6.3f),
            },
                     new HwySection()    {
                Id = 105,
                refId = 23,
                Chunks = new List<string>()
                {
                    "a4_sf_base.xml"
                },
                    InitialLoadPoint = new Vector3(-7043f, 4550f, 25.6f),
                StartConnectPoint = new Vector3(-10515.8f, -5665.2f, 0.7f),
                EndConnectPoint = new Vector3(-6399.2f, 4797.8f, 38.2f),
            },
                       new HwySection()
        {
            Id = 90,
                    refId = 24,
                    Chunks = new List<string>()
                    {
                        "s1_2_base.xml"
                    },
                    EndConnectPoint = new Vector3(-3563.8f, 5614.3f, 10.8f),
                    StartConnectPoint = new Vector3(-6399.2f, 4797.8f, 38.2f),
        },
              new HwySection()
        {
            Id = 91,
                    refId = 25,
                    Chunks = new List<string>()
                    {
                        "s1_3_base.xml"
                    },
                    EndConnectPoint = new Vector3(0,0,0),
                    StartConnectPoint =new Vector3(-3563.8f, 5614.3f, 10.8f),
        },
                   new HwySection()
              {
                Id = 10500,
                refId = 26,
                Chunks = new List<string>()
                {
                    "dk94_eur.xml"
                },
                StartConnectPoint = new Vector3(-12728.4f, -13143.4f, 6.3f),
                EndConnectPoint = new Vector3(-12014.9f, -15248.6f, 10.2f),
            },
                            new HwySection()
              {
                Id = 130,
                refId = 27,
                Chunks = new List<string>()
                {
                    "a18_base.xml"
                },
                StartConnectPoint = new Vector3(-12728.4f, -13143.4f, 6.3f),
                EndConnectPoint = new Vector3(-12014.9f, -15248.6f, 10.2f),
            },

        };

        public static Vector3 lastPos = new Vector3();
        public static bool isConnectLoaded = false;
        public static bool toDeletePrevious = false;
        public static bool toLoadNext = false;
        public static bool toDeleteConnect = false;
        public static bool isCloseToConnect = false;
        public static bool isTimeAfterCrossConnect = false;
        public static bool forcedDir = false;
        public static bool forceDir = false;

        public static bool testing = false;
        public static bool render = false;
        public static void ManageHwy()
        {
            try
            {
                if (!manageHwyStopwatch.IsRunning)
                    manageHwyStopwatch.Start();
                int time = (isCloseToConnect ? 300 : (isTimeAfterCrossConnect ? 28000 : 1000));
                if (manageHwyStopwatch.ElapsedMilliseconds > time && !testing)
                {
                    isTimeAfterCrossConnect = false;
                    manageHwyStopwatch = new Stopwatch();
                    bool northDir = currentHwySection.EndConnectPoint.DistanceTo(Main.veh.Position) < currentHwySection.EndConnectPoint.DistanceTo(lastPos);
                    if (SingleRoadRadar.currentPathName == "s1_s")
                        northDir = false;
                    if (forceDir)
                        northDir = forcedDir;
                    lastPos = Main.veh.Position;

                    Vector3 connectLoadPoint = northDir ? currentHwySection.EndConnectPoint : currentHwySection.StartConnectPoint;
                    float distToConnect = connectLoadPoint.DistanceTo(Main.veh.Position);
                    var altConnect = false;
                    if (currentHwySection?.EndConnectPointFakeForce.X != 0 && currentHwySection.EndConnectPointFakeForce.Y != 0)
                    {
                        altConnect = currentHwySection.EndConnectPointFakeForce.DistanceTo(Main.veh.Position) < 280;
                    }
                    if (currentHwySection?.EndConnectPointFakeForce.X != 0 && currentHwySection.EndConnectPointFakeForce.Y != 0 && altConnect)
                    {
                        distToConnect = currentHwySection.EndConnectPointFakeForce.DistanceTo(Main.veh.Position);
                    }
                    //   UI.ShowSubtitle("is close to conn: " + isCloseToConnect.ToString() + " dis:" + connectLoadPoint.DistanceTo(Main.veh.Position) + " isnorth " + northDir + " " + currentHwySection.Id);
                    if (distToConnect < 280 && !toLoadNext && !toDeletePrevious && !toDeleteConnect && !isConnectLoaded)
                    {
                        isCloseToConnect = true;
                        isConnectLoaded = true;
                        // UI.Notify($"a4_{(northDir ? currentHwySection.Id : currentHwySection.Id - 1)}_wro_connect.xml" + " loading");
                        if (currentHwySection.EndConnectFileNameForce != null && altConnect)
                            LoadConnectChunk(currentHwySection.EndConnectFileNameForce);
                        else
                            LoadConnectChunk($"a4_{(northDir ? currentHwySection.Id : currentHwySection.Id - 1)}_wro_connect.xml");
                        forceDir = true;
                        forcedDir = northDir;
                    }
                    if (distToConnect > 480)
                    {
                        forceDir = false;
                    }
                    if (toDeleteConnect)
                    {
                        RemoveConnectChunks();
                        toDeleteConnect = false;
                        forceDir = false;
                    }
                    if (toLoadNext)
                    {
                        currentHwySection = hwySections[northDir || (altConnect) ? (currentHwySection.refId + 1) : (currentHwySection.refId - 1)];
                        LoadHwyChunk(currentHwySection.Chunks);
                        toLoadNext = false;
                        toDeleteConnect = true;
                        isTimeAfterCrossConnect = true;
                    }
                    if (toDeletePrevious)
                    {
                        RemoveHwyChunks();
                        toDeletePrevious = false;
                        toLoadNext = true;
                        isConnectLoaded = false;
                    }
                    if (distToConnect < 60 && !toLoadNext && !toDeletePrevious && !toDeleteConnect)
                    {
                        toDeletePrevious = true;
                        isCloseToConnect = false;
                    }
                }
                if(CurrentZone.Name != null) 
                {
                    if(CurrentZone.Name == "A4_N_0_2")
                    {
                        if(Game.Player.Character.Position.DistanceTo(new Vector3(530.8f, -3460.6f, 10.9f)) < 30f)
                        {
                            CurrentZone = new HighwayZoneModel();
                            UI.ShowSubtitle("exiting and enabling traffic");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                UI.ShowSubtitle(e.Message);
            }

        }

        private static void CheckIfPlayerIsCloseToHwy()
        {
            if (testing && false)
            {
                try
                {
                    testing = false;
                    currentHwySection = hwySections[1];
                    if (render)
                        LoadHwyChunk(currentHwySection.Chunks);
                    NewHighwayTraffic.Index.isOnHighway = true;
                }
                catch (Exception e)
                {
                    UI.ShowSubtitle(e.Message);
                }
            }
            else
            {
                if (Game.IsControlJustPressed(0, GTA.Control.VehicleCinCam))
                {
                    RenderAutobahnStopWatch = new Stopwatch();
                    RenderAutobahnStopWatch.Start();
                }
                if (Game.IsControlJustReleased(0, GTA.Control.VehicleCinCam) && RenderAutobahnStopWatch.ElapsedMilliseconds < 2000)
                {
                    RenderAutobahnStopWatch = new Stopwatch();
                }
                if (RenderAutobahnStopWatch.ElapsedMilliseconds > 2000)
                {
                    RenderAutobahnStopWatch = new Stopwatch();
                    if (!NewHighwayTraffic.Index.isOnHighway)
                    {
                        Vector3 pos = Main.veh.Position;
                        for (var i = 0; i < hwySections.Count; ++i)
                        {
                            if (hwySections[i].InitialLoadPoint.DistanceTo(pos) < 150f || hwySections[i].InitialLoadPointAlt.DistanceTo(pos) < 150f)
                            {
                                currentHwySection = hwySections[i];
                                if (currentHwySection.ActiveZoneAtLoad != null)
                                    NewHighwayTraffic.Zone.CurrentZone = Zones.Where(z => z.Name == currentHwySection.ActiveZoneAtLoad).FirstOrDefault();
                                LoadHwyChunk(currentHwySection.Chunks);
                                NewHighwayTraffic.Index.isOnHighway = true;

                                truckTrailer = World.GetNearbyVehicles(Main.veh.Position, 18f, new Model(-877478386))?.FirstOrDefault() ?? null;
                                isTruckMode = truckTrailer?.DisplayName?.Contains("TRAIL") ?? false;
                                break;
                            }
                        }
                        if (false)
                        {
                            currentHwySection = hwySections[25];
                            LoadHwyChunk(currentHwySection.Chunks);
                            NewHighwayTraffic.Index.isOnHighway = true;
                        }
                    }
                    else
                    {
                        RemoveHwyChunks();
                        currentHwySection = null;
                        NewHighwayTraffic.Index.isOnHighway = false;
                    }
                }
            }

        }

        private static void RemoveHwyChunks()
        {
            Delete(AutobahnRenderedProps);
            AutobahnPropsToRender = new List<PropModel>();
            for (var i = 0; i < beaconList.Count; ++i)
            {
                beaconList[i].Delete();
            }
            beaconList = new List<Prop>();
        }

        private static void RemoveConnectChunks()
        {
            Delete(ConnectRenderedProps);
            ConnectPropsToRender = new List<PropModel>();
        }

        private static void LoadHwyChunk(List<string> chunkList)
        {
            List<PropModel> l = new List<PropModel>();
            for (var i = 0; i < chunkList.Count; ++i)
            {
                l.AddRange(ReadXml(chunkList[i]));
            }
            AutobahnPropsToRender = l;
            Render(AutobahnPropsToRender, AutobahnRenderedProps);
        }

        private static void LoadConnectChunk(string chunk)
        {
            ConnectPropsToRender = ReadXml(chunk);
            if (ConnectPropsToRender.Count > 0)
                Render(ConnectPropsToRender, ConnectRenderedProps);
        }

        private static void LoadHwyConnect(string connectName)
        {

        }

        private static void RenderRefPointsLight(bool delete = false)
        {
            if (delete)
            {
                beachBridgeLightRef.Delete();
                beachSideWallLightRef.Delete();
                beachBridgeLightRef = null;
                beachSideWallLightRef = null;
            }
            else
            {
                beachBridgeLightRef = World.CreateProp(new Model(-1038739674), new Vector3(-1970.310f, -469.86f, 19.4f), new Vector3(0, 0, 50f), true, true);
                beachBridgeLightRef.Alpha = 0;
                beachSideWallLightRef = World.CreateProp(new Model(-1038739674), new Vector3(-1903.240f, -509.3f, 11.8f), new Vector3(0, 0, 50.7f), true, true);
                beachSideWallLightRef.Alpha = 0;
            }

        }

        private static void RenderAutobahmLights()
        {
            if (beachSideWallLightRef != null && beachBridgeLightRef != null)
            {
                if (isAutobahnRendered && beachBridgeLightRef.Position.DistanceTo(Main.veh.Position) < 600f)
                {
                    // ped bridge 
                    var color = colors[colorRnd];
                    int off = -35;
                    for (var i = 0; i < 13; i++)
                    {
                        var v = beachBridgeLightRef.GetOffsetInWorldCoords(new Vector3(off, 2, -2f));
                        var v2 = beachBridgeLightRef.GetOffsetInWorldCoords(new Vector3(off, -2, -2f));
                        Function.Call(Hash.DRAW_LIGHT_WITH_RANGE, v.X, v.Y, v.Z, color.R, color.G, color.B, 5f, 10f);
                        Function.Call(Hash.DRAW_LIGHT_WITH_RANGE, v2.X, v2.Y, v2.Z, color.R, color.G, color.B, 5f, 10f);
                        off += 5;
                    }
                    //  wall 
                    int off1 = 0;
                    for (var i = 0; i < 17; i++)
                    {
                        var v = beachSideWallLightRef.GetOffsetInWorldCoords(new Vector3(0f, off1, 0f));
                        var v2 = beachSideWallLightRef.GetOffsetInWorldCoords(new Vector3(-24.5f, off1, 0f));
                        Function.Call(Hash.DRAW_LIGHT_WITH_RANGE, v.X, v.Y, v.Z, color.R, color.G, color.B, 10f, 10f);
                        Function.Call(Hash.DRAW_LIGHT_WITH_RANGE, v2.X, v2.Y, v2.Z, color.R, color.G, color.B, 10f, 10f);
                        off1 += 10;
                    }
                }
            }
        }

        private static void CheckIfPlayerIsCloseToAutobahn()
        {
            //  var t1 = PointInTriangle(x, y, zone.ZoneBoundary.StartLeftX, zone.ZoneBoundary.StartLeftY, zone.ZoneBoundary.FinishLeftX, zone.ZoneBoundary.FinishLeftY, zone.ZoneBoundary.StartRightX, zone.ZoneBoundary.StartRightY);
            //  var t2 = PointInTriangle(x, y, zone.ZoneBoundary.FinishLeftX, zone.ZoneBoundary.FinishLeftY, zone.ZoneBoundary.FinishRightX, zone.ZoneBoundary.FinishRightY, zone.ZoneBoundary.StartLeftX, zone.ZoneBoundary.StartLeftY);
            //  var t3 = PointInTriangle(x, y, zone.ZoneBoundary.FinishRightX, zone.ZoneBoundary.FinishRightY, zone.ZoneBoundary.StartRightX, zone.ZoneBoundary.StartRightY, zone.ZoneBoundary.FinishLeftX, zone.ZoneBoundary.FinishLeftY);
            //  var t4 = PointInTriangle(x, y, zone.ZoneBoundary.StartRightX, zone.ZoneBoundary.StartRightY, zone.ZoneBoundary.StartLeftX, zone.ZoneBoundary.StartLeftY, zone.ZoneBoundary.FinishRightX, zone.ZoneBoundary.FinishRightY);
            //   if (t1 || t2 || t3 || t4)
            //   {

            // }
        }

        private static List<PropModel> ReadXml(string dir)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Directory.GetCurrentDirectory().ToString() + "/" + dir);
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("//Map/Objects/MapObject");
                var elements = new List<PropModel>();
                for (var i = 0; i < nodes.Count; ++i)
                {
                    XmlNode node = nodes[i];
                    if (node.Name == "MapObject")
                    {
                        PropModel el;
                        var dynamic = node["Rotation"].ChildNodes[0].InnerText.ToString();
                        el = new PropModel()
                        {
                            Type = node["Type"].InnerText,
                            Position = new Vector3(float.Parse(node["Position"].ChildNodes[0].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Position"].ChildNodes[1].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Position"].ChildNodes[2].InnerText.ToString(), CultureInfo.InvariantCulture)),
                            Rotation = new Vector3(float.Parse(node["Rotation"].ChildNodes[0].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Rotation"].ChildNodes[1].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Rotation"].ChildNodes[2].InnerText.ToString(), CultureInfo.InvariantCulture)),
                            Hash = int.Parse(node["Hash"].InnerText.ToString()),
                            Dynamic = dynamic == "true" ? true : false,
                            PedAction = node["Action"]?.ChildNodes[0]?.InnerText.ToString(),
                            Quaternion = new Quaternion(float.Parse(node["Quaternion"].ChildNodes[0].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Quaternion"].ChildNodes[1].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Quaternion"].ChildNodes[2].InnerText.ToString(), CultureInfo.InvariantCulture), float.Parse(node["Quaternion"].ChildNodes[3].InnerText.ToString(), CultureInfo.InvariantCulture)),
                        };
                        elements.Add(el);
                    }
                }
                return elements;
            }
            catch (Exception e)
            {
                UI.Notify(e.ToString());
                return new List<PropModel>();
            }

        }

        public static void RenderTruckBase()
        {
            var dist = HighBeam.Main.veh.Position.DistanceTo(new Vector3(-1091.893f, -2214.289f, 13.2f));
            if (dist < 30 && !isTruckBaseRendered)
            {
                var vehs = World.GetAllVehicles();
                Vector3 refpoint = new Vector3(-1102.828f, -2140.918f, 13.9f);
                foreach (var v in vehs)
                {
                    if (refpoint.DistanceTo(v.Position) <= 30)
                    {
                        v.MarkAsNoLongerNeeded();
                        v.Position = new Vector3(0, 0, 0);
                        v.Delete();
                    }
                }
                isTruckBaseRendered = true;
                Render(ReadXml("new_warehouse_addons_active.xml"), BaseRenderedProps);
                UI.Notify("rendered base");
            }
            if (dist > 150 && isTruckBaseRendered)
            {
                isTruckBaseRendered = false;
                Delete(BaseRenderedProps);
                UI.Notify("deleted base");
            }
        }

        public static void RenderPaletoOffice()
        {
            // UI.ShowSubtitle(Game.Player.Character.Position.X + "  " + Game.Player.Character.Position.Y + " " + Game.Player.Character.Position.Z + " " + Game.Player.Character.Heading.ToString());
            var distToOff = HighBeam.Main.veh.Position.DistanceTo(new Vector3(79.12f, 6538.77f, 13.2f));
            if (!isPaletoOfficeRenderd && distToOff < 300)
            {
                isPaletoOfficeRenderd = true;
                Render(ReadXml("lift.xml"), PaletoOfficeRenderedProps, isLift: true);
                UI.Notify("rendered office");
            }
            if (isPaletoOfficeRenderd && distToOff > 300)
            {
                isPaletoOfficeRenderd = false;
                Delete(PaletoOfficeRenderedProps);
                UI.Notify("deleted office");
            }
            if (isPaletoOfficeRenderd)
            {
                var dist = Game.Player.Character.Position.DistanceTo(new Vector3(79.12f, 6538.77f, Game.Player.Character.Position.Z));
                if (dist < 1f && !isInLiftStopWatch.IsRunning && !isLiftRunning)
                {
                    isInLiftStopWatch = new Stopwatch();
                    isInLiftStopWatch.Start();
                }
                if (afterLiftExitStopwatch.IsRunning)
                {
                    isInLiftStopWatch = new Stopwatch();
                }
                if (afterLiftExitStopwatch.IsRunning && afterLiftExitStopwatch.ElapsedMilliseconds > 9999)
                {
                    afterLiftExitStopwatch = new Stopwatch();
                }
                if (dist > 1f)
                {
                    isInLiftStopWatch = new Stopwatch();
                }
                if (isInLiftStopWatch.IsRunning && isInLiftStopWatch.ElapsedMilliseconds > 500)
                {
                    isLiftRunning = true;
                }
                if (isLiftRunning && currentStep == null)
                {
                    posToStep();
                    isInLiftStopWatch = new Stopwatch();
                }
                if (isLiftRunning && currentStep != null)
                {
                    runLift();
                }
            }
            // isTruckBaseRendered = false;
            // Delete(BaseRenderedProps);
            //  UI.Notify("deleted base");

            // UI.ShowSubtitle(dist.ToString());
        }

        private static void posToStep()
        {
            var z = Game.Player.Character.Position.Z;
            if (z > 32.1f && z < 32.6f)
            {
                currentStep = new StepModel() { step = -1, fromZ = 32.2f, toZ = 65.0f };
            }
            else if (z > 64.4f && z < 65.9f)
            {
                currentStep = new StepModel() { step = 8, fromZ = 65.0f, toZ = 32.2f };
            }
            else
            {
                currentStep = new StepModel() { step = 0, fromZ = 0f, toZ = 0f };
            }
        }

        private static void runLift()
        {
            var liftSpeed = 0.03f;
            if (currentLiftZ == 0f)
            {
                currentLiftZ = currentStep.fromZ - 1.8f;
            }
            currentLiftZ = (currentStep.fromZ < currentStep.toZ ? (currentLiftZ + liftSpeed) : (currentLiftZ - liftSpeed));
            if (currentStep.fromZ < currentStep.toZ ? (currentLiftZ >= currentStep.toZ) : (currentLiftZ <= (currentStep.toZ + 1.9f)) || currentLiftZ <= 30.0f)
            {
                isLiftRunning = false;
                afterLiftExitStopwatch = new Stopwatch();
                afterLiftExitStopwatch.Start();
                currentStep = null;
            }
            currentLiftProp.Position = new Vector3(currentLiftProp.Position.X, currentLiftProp.Position.Y, currentLiftZ);
        }

        private static void Delete(PropFamilyModel propFamily)
        {
            for (var i = 0; i < propFamily.Vehicles.Count; ++i)
            {
                var toDel = propFamily.Vehicles[i];
                toDel.Delete();
                toDel.MarkAsNoLongerNeeded();
                toDel.Position = new Vector3(0, 0, 0);
            }
            for (var i = 0; i < propFamily.Peds.Count; ++i)
            {
                var toDel = propFamily.Peds[i];
                toDel.Delete();
                toDel.MarkAsNoLongerNeeded();
                toDel.Position = new Vector3(0, 0, 0);
            }
            for (var i = 0; i < propFamily.Props.Count; ++i)
            {
                var toDel = propFamily.Props[i];
                toDel.Delete();
                toDel.MarkAsNoLongerNeeded();
                toDel.Position = new Vector3(0, 0, 0);
            }
            propFamily = new PropFamilyModel();
        }

        public static double RadianToDegree(double angle) { return 180.0 * angle / Math.PI; }

        private static void Render(List<PropModel> toRender, PropFamilyModel propFamily, bool isLift = false)
        {
            try
            {
                propFamily.Props = new List<Prop>();
                propFamily.Vehicles = new List<Vehicle>();
                propFamily.Peds = new List<Ped>();
                for (var i = 0; i < toRender.Count; ++i)
                {

                    PropModel el = toRender[i];
                    if (el.Type.ToLower() == "vehicle")
                    {
                        var vehicle = World.CreateVehicle(new Model(el.Hash), el.Position, el.Rotation.Z);
                        vehicle.PlaceOnGround();
                        propFamily.Vehicles.Add(vehicle);
                    }
                    if (el.Type.ToLower() == "ped")
                    {
                        var ped = World.CreatePed(new Model(el.Hash), el.Position, el.Rotation.Z);
                        Function.Call(Hash.TASK_START_SCENARIO_IN_PLACE, ped, convertPedAction(el.PedAction.ToLower()), -1, false);
                        propFamily.Peds.Add(ped);
                    }
                    if (el.Type.ToLower() == "prop")
                    {
                        Prop prop = new Prop(Function.Call<int>(Hash.CREATE_OBJECT_NO_OFFSET, el.Hash, el.Position.X, el.Position.Y, el.Position.Z, true, true, false));
                        Function.Call(Hash.SET_ENTITY_QUATERNION, prop.Handle, el.Quaternion.X, el.Quaternion.Y, el.Quaternion.Z, el.Quaternion.W);
                        prop.LodDistance = 2000;
                        prop.FreezePosition = true;
                        propFamily.Props.Add(prop);
                        if (isLift)
                            currentLiftProp = prop;
                        /*   if (el.Hash == 1952396163)-772034186
                           {
                               beaconList.Add(prop);
                           }*/
                        if (el.Hash == -772034186)
                        {
                            beaconList.Add(prop);
                        }

                    }

                }
            }
            catch (Exception e)
            {
                UI.ShowSubtitle(e.Message);
            }

        }

        private static string convertPedAction(string a)
        {
            if (a == "leaf blower")
                return "WORLD_HUMAN_GARDENER_LEAF_BLOWER";
            if (a == "drinking")
                return "world_human_drinking";
            if (a == "tourist")
                return "world_human_tourist_map";
            if (a == "smoke" || a == "smoke 2")
                return "world_human_aa_smoke";
            if (a == "tourist")
                return "world_human_tourist_map";
            if (a == "drug dealer")
                return "WORLD_HUMAN_DRUG_DEALER";
            if (a == "clipboard")
                return "WORLD_HUMAN_CLIPBOARD";
            if (a == "drug dealer hard")
                return "world_human_drug_dealer_hard";
            if (a == "hammering")
                return "WORLD_HUMAN_HAMMERING";
            return "";
        }

        /*   "world_human_aa_coffee",
             "world_human_aa_smoke",
             "world_human_car_park_attendant",
             "world_human_drinking",
             "world_human_drug_dealer_hard",
             "world_human_guard_patrol",
             "world_human_hang_out_street",
             "world_human_hiker_standing",
             "world_human_jog_standing",
             "world_human_mobile_film_shocking",
             "world_human_stand_guard",
             "world_human_stand_impatient",
             "world_human_stand_mobile_fat", 
             "world_human_stand_mobile",
             "world_human_tourist_map",
             "world_human_tourist_map",
             "world_human_tourist_mobile",*/

        public class PropModel
        {
            public Vector3 Position { get; set; }
            public Vector3 Rotation { get; set; }
            public int Hash { get; set; }
            public bool Dynamic { get; set; }
            public Quaternion Quaternion { get; set; }
            public string Type { get; set; }
            public string PedAction { get; set; }
        }

        public class PropFamilyModel
        {
            public List<Vehicle> Vehicles { get; set; }
            public List<Ped> Peds { get; set; }
            public List<Prop> Props { get; set; }
        }

        private static double Sign(int p1x, int p1y, int p2x, int p2y, int p3x, int p3y)
        {
            return (p1x - p3x) * (p2y - p3y) - (p2x - p3x) * (p1y - p3y);
        }

        private static bool PointInTriangle(int pX, int pY, int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            bool b1;
            bool b2;
            bool b3;

            b1 = Sign(pX, pY, v1x, v1y, v2x, v2y) < 0.0;
            b2 = Sign(pX, pY, v2x, v2y, v3x, v3y) < 0.0;
            b3 = Sign(pX, pY, v3x, v3y, v1x, v1y) < 0.0;

            return ((b1 == b2) && (b2 == b3));
        }
    }
}
