using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using static HighBeam.HighwayTrafficOld;
using static HighBeam.NewHighwayTraffic.Index;
using static HighBeam.HighwayTraffic;
using static HighBeam.ZoneCreatorOld;
using GTA.Math;
using HighBeam;
using static HighBeam.AutobahnPropStreamer;
using XInputDotNetPure;
using static HighBeam.NewHighwayTraffic.SingleRoadRadar;

namespace HighBeam
{
    public class Main : Script
    {
        public static Vehicle veh;

        public static bool init2 = false;
        public static List<Prop> trees = new List<Prop>();
        public static List<int> treesHashes = new List<int>() { -947490680, -2114297789, 1768206104, 1958725070, -1605097644, 1380551480, 1827343468 };
        public static Random rnd = new Random();
        public static Stopwatch treeLoaderStopwatch = new Stopwatch();
        public static Vehicle truckTrailer = null;
        public static bool isTruckMode = false;
        public static bool isInLC = false;
        public static bool veryFirstInit = false;
        public static bool veryFirstInitDelay = false;
        private static Stopwatch veryFirstInitStopwatch = new Stopwatch();
        public Main()
        {
            this.Tick += onTick;
        }

        private void onTick(object sender, EventArgs e)
        {
            if (!veryFirstInitStopwatch.IsRunning)
                veryFirstInitStopwatch.Start();
            if (!veryFirstInitDelay && veryFirstInitStopwatch.ElapsedMilliseconds > 7000)
            {
                veryFirstInitDelay = true;
                ToggleSF();
                ToggleLV();
            }
            if (veryFirstInitDelay)
            {
                try
                {
                    veh = Game.Player.LastVehicle;
                    //   Viewer();
                    RunSingleRoad();
                    // var dis = Game.Player.Character.Position.DistanceTo(new Vector3(-1602f, 5247.6f, 14.7f));

                 //   UI.ShowSubtitle(Game.Player.Character.Position.X + "  " + Game.Player.Character.Position.Y + " " + Game.Player.Character.Position.Z + " " + Game.Player.Character.Heading);

                    if (Game.Player.Character.IsInVehicle(veh))
                    {
                        RunAutobahnPropStreamer();
                    }
                    RunNewHighwayTraffic();

                    //   RunZoneCreator();

                    if (isInLC && !NewHighwayTraffic.Index.isOnHighway)
                    {
                        LibertyCityPedControl();
                    }
                    else
                    {
                        if (pedList.Count > 0)
                        {
                            for (var i = 0; i < pedList.Count; ++i)
                            {
                                pedList[i].Delete();
                            }
                            pedList = new List<Ped>();
                        }
                    }

                    if (veryFirstInit)
                        LibertyCity();
                    else
                        veryFirstInit = true;
                    if (isInSFLVZone)
                        LasVegas();
                    else
                    {
                        if (isLvLoaded)
                            ToggleLV();
                        if (isSfLoaded)
                            ToggleSF();
                    }
                  
                    if (currentPath != null)
                    {
                        if (isNoTraffic)
                        {
                            Function.Call((Hash)0xB3B3359379FE77D3, 0f);
                            Function.Call((Hash)0x245A6883D966D537, 0f);
                        }
                        if (isLvLoaded || isSfLoaded)
                        {
                            Function.Call((Hash)0xB3B3359379FE77D3, 0f);
                            Function.Call((Hash)0x245A6883D966D537, 0f);
                        }
                    }
                    else if (isLvLoaded || isSfLoaded)
                    {
                        Function.Call((Hash)0xB3B3359379FE77D3, 0.25f);
                        Function.Call((Hash)0x245A6883D966D537, 0.25f);
                    }
                

                }
                catch (Exception ex)
                {
                    UI.ShowSubtitle(ex.Message.Substring(0, 100));
                }
               
            }
          //  Function.Call((Hash)0xB3B3359379FE77D3, 0f);
          //  Function.Call((Hash)0x245A6883D966D537, 0f);
          //  isInSFLVZone = true;

        }

        private int indexx = 0;
        private bool delete = true;
        private string curr = "";
        private void Viewer()
        {
            if (Game.IsControlJustPressed(0, GTA.Control.VehicleSelectNextWeapon))
            {
                var props = lvPropList;
                delete = !delete;
                Function.Call((Hash)(!delete ? 0x41B4893843BBDB74 : 0xEE6C5AD3ECE0A82D), props[indexx]);
                curr = props[indexx];
                if (delete)
                    indexx += 1;
                if (indexx >= (props.Count - 1))
                    indexx = 0;

            }

            UI.ShowSubtitle(curr);
        }

        // to del: lv_props2, moreprops, vegasw_houses, sflv_trees_new, LASVENTURE


        private Stopwatch sflvPosCheck = new Stopwatch();
        private Vector3 lvPos = new Vector3(-195.9f, 6635.4f, 1.4f);
        private Vector3 sfPos = new Vector3(-7439.3f, 1558.5f, 15.2f);
        private bool isLvLoaded = true;
        private bool isSfLoaded = true;
        public static bool isInSFLVZone = false;

        private List<string> sfPropList = new List<string>()
        {
             "san_fierro", "sf_distantlights", "sf_lights", "sf_lodlights","sflv_trees_new", "sf_props"
        };

        private List<string> lvPropList = new List<string>()
        {
              "barnnstuff", "LASVENTURE", "las_venturas", "lv_desert", "lv_distantlights", "lv_lodlights", "lv_props", "lv_props2", "lv_trafficlights",
              "moreprops", "vegasw_houses", "sflv_traffic_lights", "sflv_trees_new" ,"sflv_remastered", "sf_props"
        };

        public void LasVegas()
        {
            var ch = Game.Player.Character.Position;

            if (sflvPosCheck.IsRunning && sflvPosCheck.ElapsedMilliseconds > 5000)
            {
                sflvPosCheck = new Stopwatch();
                if (ch.DistanceTo(sfPos) < 5230 && !isSfLoaded)
                {
                    ToggleSF();
                }
                if (ch.DistanceTo(sfPos) > 5400 && isSfLoaded)
                {
                    ToggleSF();
                }

                // if (ch.DistanceTo(lvPos) < 6400 && !isLvLoaded && isSfLoaded) TODO
                if (ch.DistanceTo(lvPos) < 6400 && !isLvLoaded)
                {
                    ToggleLV();
                }
                if (ch.DistanceTo(lvPos) > 6600 && isLvLoaded)
                {
                    ToggleLV();
                }
            }
            if (!sflvPosCheck.IsRunning)
            {
                sflvPosCheck.Start();
            }
        }

        private void ToggleSF()
        {
            for (var i = 0; i < sfPropList.Count; i++)
            {
                if (isLvLoaded && isSfLoaded && sfPropList[i] == "sf_props")
                {

                }
                else
                {
                    Function.Call((Hash)(!isSfLoaded ? 0x41B4893843BBDB74 : 0xEE6C5AD3ECE0A82D), sfPropList[i]);
                }

            }
            isSfLoaded = !isSfLoaded;
        }

        private void ToggleLV()
        {
            for (var i = 0; i < lvPropList.Count; i++)
            {
                Function.Call((Hash)(!isLvLoaded ? 0x41B4893843BBDB74 : 0xEE6C5AD3ECE0A82D), lvPropList[i]);
            }
            isLvLoaded = !isLvLoaded;
        }

        public void LibertyCity()
        {
            isInLC = Game.Player.Character.Position.DistanceTo(new Vector3(5413.6f, -2737.34f, 15.1f)) < 2745f || (isLvLoaded && isInSFLVZone);
        }

        private int pedCountThisFrame = 4;
        private Stopwatch pedControlStopwatch = new Stopwatch();
        private List<Ped> pedList = new List<Ped>();
        private Vector3 playerLastPos = new Vector3();
        private List<PedHash> pedHashes = new List<PedHash>()
        {
            PedHash.Bevhills01AFM, PedHash.Bevhills01AFY, PedHash.Bevhills01AMM,PedHash.Bevhills01AMY,PedHash.Bevhills02AFM,
            PedHash.Bevhills02AFY, PedHash.Bevhills02AMM, PedHash.Bevhills04AFY, PedHash.Busicas01AMY, PedHash.Business02AFM,
            PedHash.Business04AFY, PedHash.Eastsa01AFM, PedHash.Eastsa01AFY, PedHash.Eastsa01AMY, PedHash.Eastsa02AFY,
            PedHash.Eastsa02AMM, PedHash.FatWhite01AFM, PedHash.Fitness01AFY, PedHash.Fitness02AFY, PedHash.Gay01AMY,
            PedHash.Genfat02AMM, PedHash.Genstreet01AMO, PedHash.Hooker01SFY, PedHash.Hooker03SFY, PedHash.Indian01AMY,
            PedHash.Indian01AFY, PedHash.Korean01GMY, PedHash.Korean02GMY, PedHash.Ktown02AMY, PedHash.Ktown01AMM,
            PedHash.Business02AFM, PedHash.Lost01GMY, PedHash.MexGang01GMY, PedHash.MexGoon02GMY, PedHash.MexGoon03GMY,
            PedHash.Paparazzi01AMM, PedHash.PoloGoon01GMY, PedHash.Polynesian01AMM, PedHash.Postal02SMM,
            PedHash.Ranger01SMY, PedHash.Runner01AFY,PedHash.Rurmeth01AFY, PedHash.Rurmeth01AMM,
            PedHash.Salton01AMM, PedHash.Salton01AMY, PedHash.Salton03AMM, PedHash.SalvaBoss01GMY,
            PedHash.SalvaGoon02GMY, PedHash.Sheriff01SFY, PedHash.Business01AFY, PedHash.Skater01AFY,
            PedHash.Skater01AMM, PedHash.Skidrow01AFM, PedHash.Skidrow01AMM, PedHash.Soucent01AFO,
            PedHash.Soucent01AMM, PedHash.Soucent02AFM, PedHash.Soucent02AMM, PedHash.Soucent03AMM,
            PedHash.Tourist01AFM, PedHash.Tourist01AMM, PedHash.Tourist02AFY, PedHash.Trucker01SMM,
            PedHash.Vindouche01AMY, PedHash.Vinewood02AFY, PedHash.Vinewood03AMY, PedHash.Vinewood04AMY
        };

        private List<string> audioIds = new List<string>()
        {
            "CHAT_STATE",
            "CHAT_RESP",
            "GENERIC_THANKS",
            "GENERIC_HI",
            "GENERIC_HOWS_IT_GOING",
        };

        private List<string> scenarios = new List<string>() { "WORLD_HUMAN_DRINKING", "WORLD_HUMAN_COP_IDLES", "WORLD_HUMAN_DRUG_DEALER_HARD", "WORLD_HUMAN_TOURIST_MAP" };

        private bool stop = false;
        private Random rndPed = new Random();
        private Random rndPedHash = new Random();
        private Random rndPedOff = new Random();
        private Random rndPedTask = new Random();
        private Random rndPedAudio = new Random();
        private Random rndPedAudioId = new Random();
        private Random rndPedPos = new Random();

        private Stopwatch spawnPedStopwatch = new Stopwatch();
        private int spawnPedInt = 0;
        private Stopwatch deletePedStopwatch = new Stopwatch();
        private int deletePedInt = 0;
        //private int maxPedCount = 90; laggy
        private int maxPedCount = 35;

        private Entity initRef = Game.Player.Character;
        private float spawnInitialOff = 150;

        private void PlayPedAudio()
        {
            if (pedList.Count > 0)
            {
                var ped = pedList[rndPedAudio.Next(0, pedList.Count - 1)];
                Function.Call(Hash._PLAY_AMBIENT_SPEECH1, ped, audioIds[rndPedAudioId.Next(0, audioIds.Count - 1)], "SPEECH_PARAMS_STANDARD");
            }
        }
        private void LibertyCityPedControl()
        {
            if (pedControlStopwatch.ElapsedMilliseconds > 1000 && !stop)
            {
                pedControlStopwatch = new Stopwatch();
                spawnPedInt = 0;
                deletePedInt = 0;
                spawnInitialOff = 85;
                if (Game.Player.Character.LastVehicle.Speed > 25)
                {
                    spawnInitialOff = 95;
                }

                deletePedStopwatch = new Stopwatch();
                deletePedStopwatch.Start();

                initRef = Game.Player.Character;

                spawnPedStopwatch = new Stopwatch();
                spawnPedStopwatch.Start();

            }

            if (pedControlStopwatch.ElapsedMilliseconds % 100 == 0)
            {
                PlayPedAudio();
            }

            if (deletePedStopwatch.ElapsedMilliseconds > 30 && deletePedInt < (pedList.Count))
            {
                var aheadOff = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 500f, 0));
                var isBehindPlayer = Game.Player.Character.Position.DistanceTo(aheadOff) < pedList[deletePedInt].Position.DistanceTo(aheadOff) - 20f;
                if (pedList[deletePedInt].Position.DistanceTo(Game.Player.Character.Position) > 250 || isBehindPlayer)
                {
                    pedList[deletePedInt].Delete();
                    pedList.RemoveAt(deletePedInt);
                }
                deletePedStopwatch = new Stopwatch();
                deletePedStopwatch.Start();
                deletePedInt++;

            }
            var heading = Game.Player.Character.Heading;

            if (spawnPedStopwatch.ElapsedMilliseconds > 10)
            {

                var nort = heading > 359.8f || heading < 0.2f;
                var south = heading > 179.8f && heading < 180.2f;
                var east = heading > 89.8f && heading < 90.2f;
                var west = heading > 269.8f && heading < 270.2f;

                west = true;

                if ((pedList.Count < maxPedCount && (nort || south || east || west)) || (pedList.Count < 1))
                {
                    var isEven = pedList.Count % 2 == 0;
                    var pedHeading = heading;
                    if (nort)
                        pedHeading = isEven ? 360 : 0;
                    if (south)
                        pedHeading = isEven ? 0 : 360;
                    if (east)
                        pedHeading = isEven ? 90 : 270;
                    if (west)
                        pedHeading = isEven ? 270 : 90;


                    var pedPossiblePos = initRef.GetOffsetInWorldCoords(new Vector3(isEven ? 7f : -12f, spawnInitialOff, 0f));
                    if (pedPossiblePos.DistanceTo(Game.Player.Character.Position) < 80f)
                    {
                        spawnInitialOff = 100f;
                        pedPossiblePos = initRef.GetOffsetInWorldCoords(new Vector3(isEven ? 7f : -12f, spawnInitialOff, 0f));
                    }
                    OutputArgument pavForPed = new OutputArgument();
                    Function.Call(Hash.GET_SAFE_COORD_FOR_PED, pedPossiblePos.X, pedPossiblePos.Y, pedPossiblePos.Z, true, pavForPed, 16);

                    Vector3 pavForPedPos = pavForPed.GetResult<Vector3>();
                    if (World.GetNearbyPeds(pavForPedPos, 5f).Length < 1)
                    {
                        var ped = World.CreatePed(new Model(pedHashes[rndPedHash.Next(0, pedHashes.Count - 1)]), pavForPedPos, pedHeading);

                        ped.LodDistance = 71;
                        var taskId = rndPedTask.Next(0, 4);


                        //    OutputArgument posToGoForPedOutput = new OutputArgument();
                        //    var posToGo = initRef.GetOffsetInWorldCoords(new Vector3(isEven ? 12f : -12f, spawnInitialOff + 2f, 0f));
                        //  Function.Call(Hash.GET_SAFE_COORD_FOR_PED, posToGo.X, posToGo.Y, posToGo.Z, true, posToGoForPedOutput, 16);

                        // var posToGoForPedPos = initRef.GetOffsetInWorldCoords(new Vector3(isEven ? 12f : -12f, spawnInitialOff + 2f, 0f));
                        var posGoToPed = ped.GetOffsetInWorldCoords(new Vector3(0, isEven ? -150f : 150f, 0));
                        // var seq1 = new TaskSequence();
                        if ((nort || south || east || west))
                        {
                            if (pedList.Count % 5 == 0)
                            {
                                ped.Task.UseMobilePhone();
                            }
                            var seq = new TaskSequence();
                            //  seq.AddTask.GoTo(posGoToPed, ignorePaths: false);
                            seq.AddTask.WanderAround();
                            seq.Close();
                            ped.Task.PerformSequence(seq);
                            seq.Dispose();
                        }
                        else
                        {
                            ped.Task.StartScenario(scenarios[rndPedPos.Next(0, scenarios.Count - 1)], ped.Position);
                        }

                        ped.SetNoCollision(veh, true);
                        ped.AlwaysKeepTask = true;
                        // ped.Task.PerformSequence(seq1);
                        //   seq1.Dispose();

                        pedList.Add(ped);
                        initRef = ped;
                        spawnInitialOff = (float)rndPedOff.Next(5, 19);
                    }

                }
                spawnPedStopwatch = new Stopwatch();
                spawnPedStopwatch.Start();
                spawnPedInt++;
            }

            if (!pedControlStopwatch.IsRunning)
                pedControlStopwatch.Start();

            var h = int.Parse(System.IO.File.ReadAllText(@"D:\Steam\steamapps\common\Grand Theft Auto V\scripts\current_time.txt")); ;
            if (h == 6)
            {
                maxPedCount = 10;
            }
            if (h >= 7 && h <= 12)
            {
                maxPedCount = 25;
            }
            if (h >= 13 && h <= 15)
            {
                maxPedCount = 27;
            }
            if (h == 17)
            {
                maxPedCount = 27;
            }
            if (h > 17 && h <= 21)
            {
                maxPedCount = 23;
            }
            if (h > 21 && h <= 23)
            {
                maxPedCount = 17;
            }
            if (h >= 0 && h <= 5)
            {
                maxPedCount = 6;
            }
        }
    }
}
