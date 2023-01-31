using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighBeam
{
    public static class AutobahnZones
    {
        public static List<HighwayZoneModel> Zones = new List<HighwayZoneModel>()
        {
            //////    A4 HWY     ///////
            ///


             ////////////////////////////////  SECTION -1  ///////////////////////////////////////////////////////////////
            // NORTH //
            // 0 
           /*    new HighwayZoneModel()
               {
                 Name = "A4_N_-1_0",
                 Index = 0,
                 HeadingDirection = 331.375f,
                 ZoneBoundary = new ZoneBoundaryModel()
                 {
                         StartLeftX = 3055,
                         StartLeftY = -362,
                         StartRightX = 3067,
                         StartRightY = -368,
                         FinishLeftX = 11874,
                         FinishLeftY = 15810,
                         FinishRightX = 11885,
                         FinishRightY = 15805,
                         ZCoord = 7,
                 }
                },


               // SOUTH //
                // 1 
               new HighwayZoneModel()
               {
                 Name = "A4_S_-1_0",
                 Index = 0,
                 HeadingDirection = 151.375f,
                 ZoneBoundary = new ZoneBoundaryModel()
                 {
                         StartLeftX = 11868,
                         StartLeftY = 15813,
                         StartRightX = 11857,
                         StartRightY = 15819,
                         FinishLeftX = 3046,
                         FinishLeftY = -366,
                         FinishRightX = 3035,
                         FinishRightY = -360,
                         ZCoord = 7,
                 }
                },*/

            ////////////////////////////////  SECTION 0  ///////////////////////////////////////////////////////////////
            // NORTH //
            // 0 
               new HighwayZoneModel()
               {
                 Name = "A4_N_0_0",
                 Index = 0,
                 HeadingDirection = 185.4f,
                 ZoneBoundary = new ZoneBoundaryModel()
                 {
                         StartLeftX = 2977,
                         StartLeftY = -420,
                         StartRightX = 2964,
                         StartRightY = -421,
                         FinishLeftX = 3125,
                         FinishLeftY = -2002,
                         FinishRightX = 3113,
                         FinishRightY = -2003,
                         ZCoord = 7,
                 }
                },
            // 1
                new HighwayZoneModel()
                {
                Name = "A4_N_0_1",
                 Index = 0,
                HeadingDirection = 140.4f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = 3010,
                StartLeftY = -2320,
                 StartRightX = 3000,
                 StartRightY = -2312,
                FinishLeftX = 1934,
                FinishLeftY = -3621,
                 FinishRightX = 1924,
                 FinishRightY = -3613,
                 ZCoord = 7,
                }
                },
            // 2
                 new HighwayZoneModel()
                {
                Name = "A4_N_0_2",
                 Index = 0,
                HeadingDirection = 93.3f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = 1810,
                StartLeftY = -3688,
                 StartRightX = 1809,
                 StartRightY = -3675,
                FinishLeftX = -729,
                FinishLeftY = -3831,
                 FinishRightX = -729,
                 FinishRightY = -3819,
                  ZCoord = 7,
                }
                },
            // SOUTH //
            // 0
            new HighwayZoneModel()
            {
            Name = "A4_S_0_0",
             Index = 0,
            HeadingDirection = 320.4f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 1950,
            StartLeftY = -3614,
             StartRightX = 1960,
             StartRightY = -3622,
            FinishLeftX = 3015,
            FinishLeftY = -2326,
             FinishRightX = 3025,
             FinishRightY = -2334,
             ZCoord = 7
            }
            },
            // 1
            new HighwayZoneModel()
            {
            Name = "A4_S_0_1",
             Index = 0,
            HeadingDirection = 5.4f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
                StartLeftX = 3133,
                StartLeftY = -2003,
                StartRightX = 3145,
                StartRightY = -2002,
                FinishLeftX = 2984,
                FinishLeftY = -427,
                FinishRightX = 2997,
                FinishRightY = -425,
                ZCoord = 7,
            }
            },



            ////////////////////////////////  SECTION 1  ///////////////////////////////////////////////////////////////
            // NORTH //
                new HighwayZoneModel()
                {
                Name = "A4_N_1_0",
                 Index = 0,
                HeadingDirection = 65.2f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -981,
                StartLeftY = -3780,
                 StartRightX = -975,
                 StartRightY = -3769,
                FinishLeftX = -1846,
                FinishLeftY = -3381,
                 FinishRightX = -1841,
                 FinishRightY = -3369,
                  ZCoord = 7,
                }
                },
                new HighwayZoneModel()
                {
                Name = "A4_N_1_1",
                 Index = 0,
                HeadingDirection = 327.2f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -2034,
                StartLeftY = -2708,
                 StartRightX = -2023,
                 StartRightY = -2715,
                FinishLeftX = -1690,
                FinishLeftY = -2174,
                 FinishRightX = -1679,
                 FinishRightY = -2181,
                  ZCoord = 7,
                }
                },
                new HighwayZoneModel()
                {
                Name = "A4_N_1_2",
                 Index = 0,
                HeadingDirection = 45.3f,
                EnableTrafficOnStreets = true,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -1768,
                StartLeftY = -1544,
                 StartRightX = -1759,
                 StartRightY = -1535,
                FinishLeftX = -2914,
                FinishLeftY = -407,
                 FinishRightX = -2905,
                 FinishRightY = -398,
                  ZCoord = 7,
                }
                },


            // SOUTH //
                        new HighwayZoneModel()
                {
                Name = "A4_S_1_0",
                 Index = 0,
                HeadingDirection = 225.3f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -2944,
                StartLeftY = -387,
                 StartRightX = -2953,
                 StartRightY = -396,
                FinishLeftX = -1778,
                FinishLeftY = -1544,
                 FinishRightX = -1786,
                 FinishRightY = -1553,
                 ZCoord = 7
                }
                },
                 new HighwayZoneModel()
                {
                Name = "A4_S_1_1",
                 Index = 0,
                HeadingDirection = 147.2f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -1694,
                StartLeftY = -2170,
                 StartRightX = -1705,
                 StartRightY = -2163,
                FinishLeftX = -2038,
                FinishLeftY = -2703,
                 FinishRightX = -2049,
                 FinishRightY = -2697,
                 ZCoord = 7,
                }
                },
                 new HighwayZoneModel()
                {
                Name = "A4_S_1_2",
                 Index = 0,
                HeadingDirection = 245f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -1843,
                StartLeftY = -3389,
                 StartRightX = -1848,
                 StartRightY = -3401,
                FinishLeftX = -978,
                FinishLeftY = -3788,
                 FinishRightX = -983,
                 FinishRightY = -3800,
                  ZCoord = 7,
                }
                },
                new HighwayZoneModel()
                {
                Name = "A4_S_1_3",
                 Index = 0,
                HeadingDirection = 273.2f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = -729,
                StartLeftY = -3837,
                 StartRightX = -728,
                 StartRightY = -3850,
                FinishLeftX = 1811,
                FinishLeftY = -3694,
                 FinishRightX = 1811,
                 FinishRightY = -3707,
                 ZCoord = 7,
                }
                },



            ////////////////////////////////  SECTION 2  ///////////////////////////////////////////////////////////////
            // NORTH //
            /* new HighwayZoneModel()
            {
            Name = "A4_N_2_0",
             Index = 0,
            HeadingDirection = 90f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
           StartLeftX = -3044,
            StartLeftY = -352,
             StartRightX = -3042,
             StartRightY = -340,
            FinishLeftX = -3519,
            FinishLeftY = -353,
             FinishRightX = -3519,
             FinishRightY = -340,
             ZCoord = 6,
            },
            SpeedLimitLeftLane = 110,
            SpeedLimitRightLane = 102,
            },*/ 
             new HighwayZoneModel()
            {
            Name = "A4_N_2_1",
             Index = 0,
            HeadingDirection = 346f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -3961,
            StartLeftY = 269,
             StartRightX = -3949,
             StartRightY = 266,
            FinishLeftX = -3192,
            FinishLeftY = 3353,
             FinishRightX = -3180,
             FinishRightY = 3350,
             ZCoord = 6,
            },
                 Beacons = new BeaconModel()
            {
                ColumnCount = 12,
                RowCount = 4,
                Offset = new GTA.Math.Vector3(-330f, -180f, 12f),
                OffsetInside = new GTA.Math.Vector3(77f, 0, 13f)
            }
            },

           // SOUTH
             new HighwayZoneModel()
            {
            Name = "A4_S_2_0",
             Index = 0,
            HeadingDirection = 166f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -3200,
            StartLeftY = 3355,
             StartRightX = -3212,
             StartRightY = 3358,
            FinishLeftX = -3968,
            FinishLeftY = 272,
             FinishRightX = -3981,
             FinishRightY = 275,
             ZCoord = 6,
            }
            },
          /*    new HighwayZoneModel()
            {
            Name = "A4_S_2_0",
             Index = 0,
            HeadingDirection = 269.9f,
            TrafficPaths = FakeTraffic,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -3514,
            StartLeftY = -360,
             StartRightX = -3514,
             StartRightY = -373,
            FinishLeftX = -3044,
            FinishLeftY = -360,
             FinishRightX = -3044,
             FinishRightY = -373,
             ZCoord = 6,
            },
            SpeedLimitLeftLane = 124,
            SpeedLimitRightLane = 114,
            },*/


            ////////////////////////////////  SECTION 3  ///////////////////////////////////////////////////////////////
            // NORTH //

            new HighwayZoneModel()
            {
            Name = "A4_N_3_0",
             Index = 0,
            HeadingDirection = 320.6f,
            TrafficPaths = FakeTraffic,
            EnableTrafficOnStreets = true,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -3105,
            StartLeftY = 3534,
             StartRightX = -3095,
             StartRightY = 3526,
            FinishLeftX = -223,
            FinishLeftY = 7045,
             FinishRightX = -214,
             FinishRightY = 7037,
             ZCoord = 6,
            },
            Beacons = new BeaconModel()
            {
                ColumnCount = 4,
                RowCount = 4,
                Offset = new GTA.Math.Vector3(-500f, -500f, 15f),
                OffsetInside = new GTA.Math.Vector3(55f, 0, 13f)
            }
            },
             new HighwayZoneModel()
            {
            Name = "A4_N_3_1",
             Index = 0,
            HeadingDirection = 283.0f,
            TrafficPaths = FakeTraffic,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 1259,
            StartLeftY = 6624,
             StartRightX = 1262,
             StartRightY = 6612,
            FinishLeftX = 2032,
            FinishLeftY = 6803,
             FinishRightX = 2035,
             FinishRightY = 6790,
             ZCoord = 6,
            }
            },
             new HighwayZoneModel()
            {
            Name = "A4_N_3_2",
             Index = 0,
            HeadingDirection = 245.9f,
            TrafficPaths = FakeTraffic,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 2393,
            StartLeftY = 6772,
             StartRightX = 2388,
             StartRightY = 6760,
            FinishLeftX = 3180,
            FinishLeftY = 6420,
             FinishRightX = 3174,
             FinishRightY = 6409,
             ZCoord = 6
            }
            },
               new HighwayZoneModel()
            {
            Name = "A4_N_3_3",
             Index = 0,
            HeadingDirection = 196.0f,
            TrafficPaths = FakeTraffic,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3504,
            StartLeftY = 6030,
             StartRightX = 3491,
             StartRightY = 6027,
            FinishLeftX = 3903,
            FinishLeftY = 4635,
             FinishRightX = 3890,
             FinishRightY = 4632,
             ZCoord = 7,
            }
            },
                new HighwayZoneModel()
            {
            Name = "A4_N_3_4",
             Index = 0,
            HeadingDirection = 179.8f,
            TrafficPaths = FakeTraffic,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 4022,
            StartLeftY = 4245,
             StartRightX = 4009,
             StartRightY = 4245,
            FinishLeftX = 4021,
            FinishLeftY = 3619,
             FinishRightX = 4008,
             FinishRightY = 3619,
             ZCoord = 7,
            }
            },
                 new HighwayZoneModel()
            {
            Name = "A4_N_3_5",
             Index = 0,
            HeadingDirection = 164.7f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3998,
            StartLeftY = 3464,
             StartRightX = 3985,
             StartRightY = 3467,
            FinishLeftX = 2993,
            FinishLeftY = -216,
             FinishRightX = 2980,
             FinishRightY = -213,
             ZCoord = 7,
            }
            },

            // SOUTH
             new HighwayZoneModel()
            {
            Name = "A4_S_3_0",
             Index = 0,
            HeadingDirection = 344.7f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3000,
            StartLeftY = -218,
             StartRightX = 3012,
             StartRightY = -222,
            FinishLeftX = 4003,
            FinishLeftY = 3456,
             FinishRightX = 4015,
             FinishRightY = 3453,
             ZCoord = 6,
            }
            },
                new HighwayZoneModel()
            {
            Name = "A4_S_3_1",
             Index = 0,
            HeadingDirection = 359.9f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 4027,
            StartLeftY = 3619,
             StartRightX = 4040,
             StartRightY = 3619,
            FinishLeftX = 4028,
            FinishLeftY = 4433,
             FinishRightX = 4041,
             FinishRightY = 4433,
             ZCoord = 7,
            }
            },
                 new HighwayZoneModel()
            {
            Name = "A4_S_3_2",
             Index = 0,
            HeadingDirection = 16f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3819,
            StartLeftY = 4953,
             StartRightX = 3832,
             StartRightY = 4956,
            FinishLeftX = 3504,
            FinishLeftY = 6055,
             FinishRightX = 3517,
             FinishRightY = 6059,
              ZCoord = 7,
            }
            },
             new HighwayZoneModel()
            {
            Name = "A4_S_3_3",
             Index = 0,
            HeadingDirection = 66f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3183,
            StartLeftY = 6427,
             StartRightX = 3188,
             StartRightY = 6439,
            FinishLeftX = 2396,
            FinishLeftY = 6778,
             FinishRightX = 2401,
             FinishRightY = 6790,
             ZCoord = 6,
            }
            },
               new HighwayZoneModel()
            {
            Name = "A4_S_3_4",
             Index = 0,
            HeadingDirection = 103f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 2031,
            StartLeftY = 6810,
             StartRightX = 2029,
             StartRightY = 6822,
            FinishLeftX = 1281,
            FinishLeftY = 6636,
             FinishRightX = 1277,
             FinishRightY = 6649,
              ZCoord = 6,
            }
            },
                 new HighwayZoneModel()
            {
            Name = "A4_S_3_5",
             Index = 0,
            HeadingDirection = 140.6f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -228,
            StartLeftY = 7051,
             StartRightX = -238,
             StartRightY = 7059,
            FinishLeftX = -3107,
            FinishLeftY = 3543,
             FinishRightX = -3117,
             FinishRightY = 3551,
             ZCoord = 7,
            },
               Beacons = new BeaconModel()
            {
                ColumnCount = 20,
                RowCount = 4,
                Offset = new GTA.Math.Vector3(100f, -180f, 15f),
                OffsetInside = new GTA.Math.Vector3(55f, 0, 13f)
            }
            },


            ////////////////////////////////  SECTION 4  ///////////////////////////////////////////////////////////////
            // NORTH //

            new HighwayZoneModel()
                {
                Name = "A4_N_4_0",
                 Index = 0,
                HeadingDirection = 267.3f,
                ZoneBoundary = new ZoneBoundaryModel()
                {
                StartLeftX = 3494,
                StartLeftY = -901,
                 StartRightX = 3494,
                 StartRightY = -914,
                FinishLeftX = 6192,
                FinishLeftY = -1029,
                 FinishRightX = 6191,
                 FinishRightY = -1041,
                 ZCoord = 7,
                }
                },
            new HighwayZoneModel()
            {
            Name = "A4_N_4_1",
             Index = 0,
            HeadingDirection = 229.33f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 6679,
            StartLeftY = -1184,
             StartRightX = 6670,
             StartRightY = -1193,
             FinishLeftX = 8004,
            FinishLeftY = -2323,
             FinishRightX = 7996,
             FinishRightY = -2332,
             ZCoord = 7
            }
            },
            new HighwayZoneModel()
            {
            Name = "A4_N_4_2",
             Index = 0,
            HeadingDirection = 138.54f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
           StartLeftX = 8058,
            StartLeftY = -3140,
             StartRightX = 8049,
             StartRightY = -3131,
            FinishLeftX = 7007,
            FinishLeftY = -4331,
             FinishRightX = 6997,
             FinishRightY = -4322,
             ZCoord = 7,
            }
            },
            new HighwayZoneModel()
            {
            Name = "A4_N_4_3",
             Index = 0,
            HeadingDirection = 60f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 6316,
            StartLeftY = -4462,
             StartRightX = 6323,
             StartRightY = -4451,
            FinishLeftX = 5645,
            FinishLeftY = -4075,
             FinishRightX = 5652,
             FinishRightY = -4064,
             ZCoord = 7
            },
            SpeedLimitLeftLane = 88,
            SpeedLimitRightLane = 77,
            },
            new HighwayZoneModel()
            {
            Name = "A4_N_4_4",
             Index = 0,
            HeadingDirection = 87.2f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
           StartLeftX = 6557,
        StartLeftY = -4530,
         StartRightX = 6557,
         StartRightY = -4517,
        FinishLeftX = 2474,
        FinishLeftY = -4334,
         FinishRightX = 2476,
         FinishRightY = -4321,
             ZCoord = 7,
            },
            HasHill = true,
            },
            new HighwayZoneModel()
            {
             Name = "A4_N_4_5",
             Index = 0,
            HeadingDirection = 1f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 2807,
            StartLeftY = -4239,
             StartRightX = 2819,
             StartRightY = -4238,
            FinishLeftX = 2793,
            FinishLeftY = -3604,
             FinishRightX = 2805,
             FinishRightY = -3603,
             ZCoord = 30,

            },
            SpeedLimitLeftLane = 110,
            SpeedLimitRightLane = 90,
            },
            new HighwayZoneModel()
            {
             Name = "A4_N_4_6",
             Index = 0,
            HeadingDirection = 321.3f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 2915,
            StartLeftY = -3223,
             StartRightX = 2925,
             StartRightY = -3231,
            FinishLeftX = 3353,
            FinishLeftY = -2677,
             FinishRightX = 3363,
             FinishRightY = -2685,
             ZCoord = 30
            },
            SpeedLimitLeftLane = 130,
            SpeedLimitRightLane = 100,
            },
                          

            // SOUTH

            new HighwayZoneModel()
            {
            Name = "A4_S_4_0",
             Index = 0,
            HeadingDirection = 141.3f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3405,
            StartLeftY = -2600,
             StartRightX = 3395,
             StartRightY = -2592,
            FinishLeftX = 2909,
            FinishLeftY = -3218,
             FinishRightX = 2898,
             FinishRightY = -3211,
             ZCoord = 30
            },
             SpeedLimitLeftLane = 100,
            SpeedLimitRightLane = 80,
            },
            new HighwayZoneModel()
            {
            Name = "A4_S_4_1",
             Index = 0,
            HeadingDirection = 181f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 2787,
            StartLeftY = -3603,
             StartRightX = 2774,
             StartRightY = -3603,
            FinishLeftX = 2800,
            FinishLeftY = -4226,
             FinishRightX = 2787,
             FinishRightY = -4226,
             ZCoord = 30
            },
            SpeedLimitLeftLane = 100,
            SpeedLimitRightLane = 80,
            },
            new HighwayZoneModel()
            {
            Name = "A4_S_4_2",
             Index = 0,
            HeadingDirection = 267.3f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 2474,
            StartLeftY = -4340,
             StartRightX = 2473,
             StartRightY = -4352,
            FinishLeftX = 6552,
            FinishLeftY = -4535,
             FinishRightX = 6552,
             FinishRightY = -4548,
             ZCoord = 7,
            },
            HasHill = true,
            },
            new HighwayZoneModel()
            {
            Name = "A4_S_4_3",
             Index = 0,
            HeadingDirection = 318.54f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 7012,
            StartLeftY = -4335,
             StartRightX = 7021,
             StartRightY = -4344,
            FinishLeftX = 8064,
            FinishLeftY = -3145,
             FinishRightX = 8073,
             FinishRightY = -3153,
             ZCoord = 7,
            }
            },
            new HighwayZoneModel()
            {
            Name = "A4_S_4_4",
             Index = 0,
            HeadingDirection = 49.33f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
         StartLeftX = 8010,
        StartLeftY = -2318,
         StartRightX = 8018,
         StartRightY = -2308,
            FinishLeftX = 6685,
            FinishLeftY = -1179,
             FinishRightX = 6693,
             FinishRightY = -1169,
             ZCoord = 7,
            }
            },
            new HighwayZoneModel()
            {
            Name = "A4_S_4_5",
             Index = 0,
            HeadingDirection = 87.3f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            FinishLeftX = 3495,
            FinishLeftY = -894,
             FinishRightX = 3496,
             FinishRightY = -881,
            StartLeftX = 6194,
            StartLeftY = -1021,
             StartRightX = 6194,
             StartRightY = -1008,
             ZCoord = 7,
            }
            },
            new HighwayZoneModel()
            {
            Name = "A4_S_4_6",
             Index = 0,
            HeadingDirection = 270.8f,
            SpeedLimitLeftLane = 110,
            SpeedLimitRightLane = 85,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 6239,
            StartLeftY = -2000,
             StartRightX = 6238,
             StartRightY = -2012,
            FinishLeftX = 7391,
            FinishLeftY = -1982,
             FinishRightX = 7391,
             FinishRightY = -1994,
             ZCoord = 7,
            }
            },

            new HighwayZoneModel()
            {
            Name = "zone",
             Index = 0,
            HeadingDirection = 265.2f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 188,
            StartLeftY = 7221,
             StartRightX = 186,
             StartRightY = 7209,
            FinishLeftX = 4303,
            FinishLeftY = 6878,
             FinishRightX = 4302,
             FinishRightY = 6865,
             ZCoord = 7
            }
            },

            new HighwayZoneModel()
            {
            Name = "zone",
             Index = 0,
            HeadingDirection = 85.2f,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 4298,
            StartLeftY = 6885,
             StartRightX = 4299,
             StartRightY = 6897,
            FinishLeftX = 190,
            FinishLeftY = 7228,
             FinishRightX = 191,
             FinishRightY = 7241,
             ZCoord = 7
            }
            },

            new HighwayZoneModel()
            {
            Name = "zone",
             Index = 0,
            HeadingDirection = 171.0f,
             IsThreeLaneRoad = true,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 4816,
            StartLeftY = 6216,
             StartRightX = 4803,
             StartRightY = 6218,
            FinishLeftX = 3780,
            FinishLeftY = -379,
             FinishRightX = 3767,
             FinishRightY = -377,
             ZCoord = 7
            }
            },

            new HighwayZoneModel()
            {
            Name = "zone",
             Index = 0,
            HeadingDirection = 351f,
            IsThreeLaneRoad = true,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = 3800,
            StartLeftY = -383,
             StartRightX = 3812,
             StartRightY = -385,
            FinishLeftX = 4836,
            FinishLeftY = 6212,
             FinishRightX = 4848,
             FinishRightY = 6210,
             ZCoord = 7
            }
            },
                          
                          
                          
                          
                          




























            /* NORTH ROAD START */
           
           
            
          /*

            new HighwayZoneModel()
            {
            Name = "a4 5",
             Index = 0,
            HeadingDirection = 51.1f,
            TrafficPaths = FakeTraffic,
            EnableTrafficOnStreets = true,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -2737,
            StartLeftY = 2911,
             StartRightX = -2729,
             StartRightY = 2921,
             FinishLeftX = -3107,
             FinishLeftY = 3210,
             FinishRightX = -3099,
             FinishRightY = 3220,
             ZCoord = 7
            }
            },
           




            new HighwayZoneModel()
{
Name = "a4 20",
 Index = 0,
HeadingDirection = 245f,
ZoneBoundary = new ZoneBoundaryModel()
{
StartLeftX = -1843,
StartLeftY = -3389,
 StartRightX = -1848,
 StartRightY = -3401,
FinishLeftX = -978,
FinishLeftY = -3788,
 FinishRightX = -983,
 FinishRightY = -3800,
  ZCoord = 7,
}
},



           
         
           
           
       
          
            new HighwayZoneModel()
            {
            Name = "a4 29",
             Index = 0,
            HeadingDirection = 231.1f,
            EnableTrafficOnStreets = true,
            ZoneBoundary = new ZoneBoundaryModel()
            {
            StartLeftX = -3112,
            StartLeftY = 3205,
             StartRightX = -3120,
             StartRightY = 3195,
            FinishLeftX = -2742,
            FinishLeftY = 2905,
             FinishRightX = -2750,
             FinishRightY = 2895,
             ZCoord = 7,
            }
            },
          */
        };

        public static FakeTrafficModel FakeTraffic = new FakeTrafficModel()
        {
            LeftLaneOffset = 30,
            RightLaneOffset = 40,
            Id = 1,
        };

        /*   public static List<FakeTrafficModel> FakeTraffic = new List<FakeTrafficModel>()
             {
                  new FakeTrafficModel()
                 {
                     LeftLaneOffset = 10,
                     RightLaneOffset = 17,
                     Id = 1,
                  },
           };*/

        //        public static List<FakeTrafficModel> FakeTraffic = new List<FakeTrafficModel>()
        //        {
        //            /* NRS north road start from Los Santos */
        //            // 1
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 19,
        //                RightLaneOffset = 25,
        //                Id = 1,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 19,
        //                RightLaneOffset = 25,
        //                Id = 2,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 19,
        //                RightLaneOffset = 25, 
        //                Id = 3,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 17,
        //                RightLaneOffset = 23,
        //                Id = 4,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 13, 
        //                RightLaneOffset = 18,
        //                Id = 5,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 20,
        //                RightLaneOffset = 26,
        //                Id = 6,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 35,
        //                RightLaneOffset = 41,
        //                Id = 7,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 41, 
        //                RightLaneOffset = 47,
        //                Id = 8,
        //            },
        //             new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 35,
        //                RightLaneOffset = 41,
        //                Id = 9,
        //            },
        //              new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 26,
        //                RightLaneOffset = 32,
        //                Id = 10, 
        //            },
        //             new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 35,
        //                RightLaneOffset = 41,
        //                Id = 12,
        //            },

        //             // SRS 
        //             new FakeTrafficModel()
        //            { 
        //                LeftLaneOffset = 20,
        //                RightLaneOffset = 27,
        //                Id = 13,
        //            },
        //             new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 19,
        //                RightLaneOffset = 26,
        //                Id = 14,
        //            },
        //             new FakeTrafficModel()
        //            { 
        //                LeftLaneOffset = 19,
        //                RightLaneOffset = 26,
        //                Id = 15,
        //            },
        //             new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 17,
        //                RightLaneOffset = 24,
        //                Id = 16,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 35,
        //                RightLaneOffset = 42,
        //                Id = 17,
        //            },
        //            new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 20,
        //                RightLaneOffset = 28,
        //                Id = 18,
        //            },
        //             new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 18,
        //                RightLaneOffset = 26,
        //                Id = 19,
        //            },
        //               new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 18,
        //                RightLaneOffset = 25,
        //                Id = 20,
        //            },
        //                 new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 18, 
        //                RightLaneOffset = 25,
        //                Id = 21,
        //            },
        //                 new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 20,
        //                RightLaneOffset = 28,
        //                Id = 22,
        //            },
        //                 new FakeTrafficModel()
        //            {
        //                LeftLaneOffset = 13,
        //                RightLaneOffset = 18,
        //                Id = 23,
        //            },
        //        };

        //        public static List<HighwayZoneModel> Zones = new List<HighwayZoneModel>()
        //        {
        //            new HighwayZoneModel()
        //            {
        //                Name = "First after toll",
        //                Index = 0,
        //                CompassDirection = 0,
        //                HeadingDirection = 38.95f,
        //                TrafficPaths = FakeTraffic[0],
        //                ZoneBoundary = new ZoneBoundaryModel()
        //                {
        //                    StartLeftX = -1413,
        //                    StartLeftY = -1580,
        //                    StartRightX = -1402,
        //                    StartRightY = -1571,
        //                    FinishLeftX = -1911,
        //                    FinishLeftY = -963,
        //                    FinishRightX = -1901,
        //                    FinishRightY = -954,
        //                    ZCoord = 27,
        //                }
        //            },
        //            new HighwayZoneModel()
        //                                 {
        //                                 Name = "nrs 2",
        //                                 Index = 1,
        //                                 HeadingDirection = 55.7f,
        //                                 TrafficPaths = FakeTraffic[1],
        //                                 ZoneBoundary = new ZoneBoundaryModel()
        //                                 {
        //                                 StartLeftX = -2025,
        //                                 StartLeftY = -861,
        //                                 StartRightX = -2018,
        //                                 StartRightY = -850,
        //                                 FinishLeftX = -2945,
        //                                 FinishLeftY = -235,
        //                                 FinishRightX = -2937,
        //                                 FinishRightY = -224,
        //                                 ZCoord = 27,
        //                                 }
        //                                 },
        //            new HighwayZoneModel()
        //            {
        //                Name = "nrs 3",
        //                 Index = 2,
        //                HeadingDirection = 12.8f,
        //                TrafficPaths = FakeTraffic[2],
        //                ZoneBoundary = new ZoneBoundaryModel()
        //                {
        //                StartLeftX = -3249,
        //                StartLeftY = 140,
        //                 StartRightX = -3237,
        //                 StartRightY = 143,
        //                FinishLeftX = -3388,
        //                FinishLeftY = 760,
        //                 FinishRightX = -3375,
        //                 FinishRightY = 763,
        //                 ZCoord = 27,
        //                }
        //            },
        //           /* new HighwayZoneModel()
        //            {
        //            Name = "zone",
        //             Index = 3,
        //            HeadingDirection = 11.25f,
        //            ZoneBoundary = new ZoneBoundaryModel()
        //            {
        //            StartLeftX = -3389,
        //            StartLeftY = 760,
        //             StartRightX = -3375,
        //             StartRightY = 763,
        //            FinishLeftX = -3441,
        //            FinishLeftY = 1018,
        //             FinishRightX = -3428,
        //             FinishRightY = 1021,
        //             ZCoord = 22,
        //            }
        //            },*/

        //            new HighwayZoneModel()
        //            {
        //                Name = "nrs 4",
        //                Index = 4,
        //                CompassDirection = 0,
        //                HeadingDirection = 348.9f,
        //                TrafficPaths = FakeTraffic[3],
        //                ZoneBoundary = new ZoneBoundaryModel()
        //                {
        //                    StartLeftX = -3502,
        //                    StartLeftY = 1594,
        //                    StartRightX = -3489,
        //                    StartRightY = 1590,
        //                    FinishLeftX = -2981,
        //                    FinishLeftY = 4243,
        //                    FinishRightX = -2966,
        //                    FinishRightY = 4241,
        //                    ZCoord = 23,
        //                }
        //            },
        //            new HighwayZoneModel()
        //            {
        //            Name = "nrs 5",
        //             Index = 5,
        //            HeadingDirection = 323.2f,
        //            TrafficPaths = FakeTraffic[4],
        //            ZoneBoundary = new ZoneBoundaryModel()
        //            {
        //            StartLeftX = -2897,
        //            StartLeftY = 4435,
        //             StartRightX = -2887,
        //             StartRightY = 4428,
        //            FinishLeftX = -614,
        //            FinishLeftY = 7486,
        //             FinishRightX = -604,
        //             FinishRightY = 7477,
        //             ZCoord = 23,
        //            }
        //            },
        //            new HighwayZoneModel()
        //            {
        //            Name = "nrs 6",
        //             Index = 0,
        //            HeadingDirection = 298.0f,
        //            TrafficPaths = FakeTraffic[5],
        //            ZoneBoundary = new ZoneBoundaryModel()
        //            {
        //            StartLeftX = -488,
        //            StartLeftY = 7581,
        //             StartRightX = -482,
        //             StartRightY = 7569,
        //            FinishLeftX = 1043,
        //            FinishLeftY = 8401,
        //             FinishRightX = 1049,
        //             FinishRightY = 8390,
        //             ZCoord = 23,
        //            }
        //            },
        //            new HighwayZoneModel()
        //            {
        //            Name = "nrs 7",
        //             Index = 0,
        //            HeadingDirection = 226.4f,
        //            TrafficPaths = FakeTraffic[6],
        //            ZoneBoundary = new ZoneBoundaryModel()
        //            {
        //            StartLeftX = 1574,
        //            StartLeftY = 8338,
        //             StartRightX = 1565,
        //             StartRightY = 8329,
        //            FinishLeftX = 3528,
        //            FinishLeftY = 6474,
        //             FinishRightX = 3519,
        //             FinishRightY = 6465,
        //             ZCoord = 23,
        //            }
        //            },
        //            new HighwayZoneModel()
        //{
        //Name = "nrs 8",
        // Index = 0,
        //HeadingDirection = 201.6f,
        //TrafficPaths = FakeTraffic[7],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3641,
        //StartLeftY = 6298,
        // StartRightX = 3629,
        // StartRightY = 6293,
        //FinishLeftX = 4284,
        //FinishLeftY = 4674,
        // FinishRightX = 4272,
        // FinishRightY = 4669,
        // ZCoord = 23,
        //},
        //StaticProps = new List<StaticPropsModel>()
        //    {
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4046.5f, 5205.7f, 0),
        //            Heading = 199.6f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4048.4f, 5199.8f, 0),
        //            Heading = 199.6f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4040.4f, 5205.4f, 0),
        //            Heading = 199.6f
        //        },
        //          new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4037.6f, 5213.8f, 0),
        //            Heading = 199.6f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4033.9f, 5185.6f, 0),
        //            Heading = 199.6f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4041.8f, 5174.1f, 0),
        //            Heading = 201.0f,
        //            IsElectric = true,
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4048.8f, 5153.3f, 0),
        //            Heading = 65.3f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4047.2f, 5154.2f, 0),
        //            Heading = 222.4f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4046.4f, 5152.5f, 0),
        //            Heading = 283.7f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4063.7f, 5152.4f, 0),
        //            Heading = 328.5f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4063.7f, 5154.0f, 0),
        //            Heading = 189.4f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4043.6f, 5207.0f, 0),
        //            Heading = 270.0f, 
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4045.7f, 5201.4f, 0),
        //            Heading = 270.0f,
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //          new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4041.9f, 5206.3f, 0),
        //            Heading = 147.0f,
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //           new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4045.2f, 5186.9f, 0),
        //            Heading = 43.9f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4043.5f, 5186.9f, 0),
        //            Heading = 250.9f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4045.3f, 5188.5f, 0),
        //            Heading = 162.9f,
        //        },
        //}
        //},
        //            new HighwayZoneModel()
        //{
        //Name = "nrs 9",
        // Index = 0,
        //HeadingDirection = 179.1f,
        //TrafficPaths = FakeTraffic[8],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4317,
        //StartLeftY = 4520,
        // StartRightX = 4304,
        // StartRightY = 4520,
        //FinishLeftX = 4304,
        //FinishLeftY = 3726,
        // FinishRightX = 4291,
        // FinishRightY = 3726,
        // ZCoord = 23,
        //}
        //},
        //            new HighwayZoneModel()
        //{
        //Name = "nrs 10",
        // Index = 0,
        //HeadingDirection = 146.6f,
        //TrafficPaths = FakeTraffic[9],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4210,
        //StartLeftY = 3431,
        // StartRightX = 4198,
        // StartRightY = 3438,
        //FinishLeftX = 3336,
        //FinishLeftY = 2106,
        // FinishRightX = 3325,
        // FinishRightY = 2113,
        // ZCoord = 23,
        //}
        //},

        //new HighwayZoneModel()
        //{
        //Name = "nrs 12",
        // Index = 0,
        //HeadingDirection = 177.8f,
        //TrafficPaths = FakeTraffic[10],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3175,
        //StartLeftY = -85,
        // StartRightX = 3161,
        // StartRightY = -85,
        //FinishLeftX = 3132,
        //FinishLeftY = -1188,
        // FinishRightX = 3118,
        // FinishRightY = -1186,
        // ZCoord = 67,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 12 o",
        // Index = 0,
        //HeadingDirection = 357.9f,
        //TrafficPaths = FakeTraffic[10],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3150,
        //StartLeftY = -1289,
        // StartRightX = 3163,
        // StartRightY = -1290,
        //FinishLeftX = 3197,
        //FinishLeftY = -105,
        // FinishRightX = 3210,
        // FinishRightY = -106,
        // ZCoord = 67,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 10 o",
        // Index = 0,
        //HeadingDirection = 326.5f,
        //TrafficPaths = FakeTraffic[9],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3348,
        //StartLeftY = 2097,
        // StartRightX = 3359,
        // StartRightY = 2089,
        //FinishLeftX = 4214,
        //FinishLeftY = 3410,
        // FinishRightX = 4226,
        // FinishRightY = 3403,
        // ZCoord = 23,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 9 o",
        // Index = 0,
        //HeadingDirection = 359.2f,
        //TrafficPaths = FakeTraffic[8],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4328,
        //StartLeftY = 3746,
        // StartRightX = 4342,
        // StartRightY = 3745,
        //FinishLeftX = 4341,
        //FinishLeftY = 4539,
        // FinishRightX = 4354,
        // FinishRightY = 4539,
        // ZCoord = 23,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 8 o",
        // Index = 0,
        //HeadingDirection = 21.6f,
        //TrafficPaths = FakeTraffic[7],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4311,
        //StartLeftY = 4688,
        // StartRightX = 4324,
        // StartRightY = 4693,
        //FinishLeftX = 3726,
        //FinishLeftY = 6164,
        // FinishRightX = 3739,
        // FinishRightY = 6170,
        // ZCoord = 23,
        //},
        //StaticProps = new List<StaticPropsModel>()
        //    {
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4130.6f, 5216.7f, 0),
        //            Heading = 19.2f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4136.4f, 5217.4f, 0),
        //            Heading = 19.2f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4140.2f, 5219.9f, 0),
        //            Heading = 19.2f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4136.0f, 5256.0f, 0),
        //            Heading = 20.2f,
        //            IsElectric = true,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4133.5f, 5254.9f, 0),
        //            Heading = 20.2f,
        //            IsElectric = true,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(4127.6f, 5252.7f, 0),
        //            Heading = 20.2f, 
        //            IsElectric = true,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4130.9f, 5221.9f, 0),
        //            Heading = 104.5f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4132.5f, 5223.1f, 0),
        //            Heading = 285.0f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4139.9f, 5225.0f, 0),
        //            Heading = 102.8f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4138.1f, 5242.4f, 0),
        //            Heading = 180.9f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(4138.2f, 5240.7f, 0),
        //            Heading = 8.5f,
        //        },
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 7 o",
        // Index = 0,
        //HeadingDirection = 46.4f,
        //TrafficPaths = FakeTraffic[6],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3554,
        //StartLeftY = 6482,
        // StartRightX = 3563,
        // StartRightY = 6491,
        //FinishLeftX = 1567,
        //FinishLeftY = 8378,
        // FinishRightX = 1575,
        // FinishRightY = 8388,
        // ZCoord = 23,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 6 o",
        // Index = 0,
        //HeadingDirection = 118.2f,
        //TrafficPaths = FakeTraffic[5],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 1045,
        //StartLeftY = 8416,
        // StartRightX = 1037,
        // StartRightY = 8427,
        //FinishLeftX = -489,
        //FinishLeftY = 7595,
        // FinishRightX = -496,
        // FinishRightY = 7606,
        // ZCoord = 23,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 5 o",
        // Index = 0,
        //HeadingDirection = 143.2f,
        //TrafficPaths = FakeTraffic[4],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = -617,
        //StartLeftY = 7490,
        // StartRightX = -627,
        // StartRightY = 7499,
        //FinishLeftX = -2895,
        //FinishLeftY = 4447,
        // FinishRightX = -2906,
        // FinishRightY = 4456,
        // ZCoord = 23,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 168.9f,
        //TrafficPaths = FakeTraffic[3],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = -2986,
        //StartLeftY = 4249,
        // StartRightX = -2999,
        // StartRightY = 4251,
        //FinishLeftX = -3507,
        //FinishLeftY = 1596,
        // FinishRightX = -3520,
        // FinishRightY = 1600,
        // ZCoord = 23,
        //}
        //},
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 191.7f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = -3453,
        //StartLeftY = 1023,
        // StartRightX = -3466,
        // StartRightY = 1020,
        //FinishLeftX = -3398,
        //FinishLeftY = 754,
        // FinishRightX = -3411,
        // FinishRightY = 751,
        // ZCoord = 23,
        //}
        //},*/
        //new HighwayZoneModel()
        //{
        //Name = "nrs 3 o",
        // Index = 0,
        //HeadingDirection = 192.5f,
        //TrafficPaths = FakeTraffic[2],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = -3398,
        //StartLeftY = 754,
        // StartRightX = -3411,
        // StartRightY = 751,
        //FinishLeftX = -3259,
        //FinishLeftY = 134,
        // FinishRightX = -3272,
        // FinishRightY = 131,
        // ZCoord = 27,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 2 o",
        // Index = 0,
        //HeadingDirection = 235.8f,
        //TrafficPaths = FakeTraffic[1],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = -2949,
        //StartLeftY = -242,
        // StartRightX = -2956,
        // StartRightY = -253,
        //FinishLeftX = -2026,
        //FinishLeftY = -870,
        // FinishRightX = -2035,
        // FinishRightY = -881,
        // ZCoord = 27,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "nrs 1 o",
        // Index = 0,
        //HeadingDirection = 219f,
        //TrafficPaths = FakeTraffic[0],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = -1919,
        //StartLeftY = -968,
        // StartRightX = -1929,
        // StartRightY = -976,
        //FinishLeftX = -1554,
        //FinishLeftY = -1420,
        // FinishRightX = -1564,
        // FinishRightY = -1428,
        // ZCoord = 27,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 1",
        // Index = 0,
        //HeadingDirection = 358.3f,
        //TrafficPaths = FakeTraffic[11],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3324,
        //StartLeftY = -1935,
        // StartRightX = 3337,
        // StartRightY = -1936,
        //FinishLeftX = 3399,
        //FinishLeftY = 551,
        // FinishRightX = 3412,
        // FinishRightY = 550,
        // ZCoord = 69
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 1 o",
        // Index = 0,
        //HeadingDirection = 178.2f,
        //TrafficPaths = FakeTraffic[11],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3383,
        //StartLeftY = 403,
        // StartRightX = 3370,
        // StartRightY = 403,
        //FinishLeftX = 3313,
        //FinishLeftY = -1935,
        // FinishRightX = 3300,
        // FinishRightY = -1934,
        // ZCoord = 69
        //}
        //},
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 178.3f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3234,
        //StartLeftY = 1819,
        // StartRightX = 3221,
        // StartRightY = 1820,
        //FinishLeftX = 3185,
        //FinishLeftY = 234,
        // FinishRightX = 3172,
        // FinishRightY = 233,
        // ZCoord = 27,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 322.2f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 8611,
        //StartLeftY = -2846,
        // StartRightX = 8622,
        // StartRightY = -2854,
        //FinishLeftX = 9389,
        //FinishLeftY = -1841,
        // FinishRightX = 9400,
        // FinishRightY = -1849,
        // ZCoord = 117,
        //}
        //},*/
        //new HighwayZoneModel()
        //{
        //Name = "srs 5 o",
        // Index = 0,
        //HeadingDirection = 11f,
        //TrafficPaths = FakeTraffic[15],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9487,
        //StartLeftY = -1458,
        // StartRightX = 9500,
        // StartRightY = -1455,
        //FinishLeftX = 8760,
        //FinishLeftY = 2284,
        // FinishRightX = 8773,
        // FinishRightY = 2287,
        // ZCoord = 117,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 4 o",
        // Index = 0,
        //HeadingDirection = 57.7f,
        //TrafficPaths = FakeTraffic[14],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 8557,
        //StartLeftY = 2776,
        // StartRightX = 8564,
        // StartRightY = 2786,
        //FinishLeftX = 8155,
        //FinishLeftY = 3029,
        // FinishRightX = 8162,
        // FinishRightY = 3040,
        // ZCoord = 117,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 3 o",
        // Index = 0,
        //HeadingDirection = 117.4f,
        //TrafficPaths = FakeTraffic[13],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7618,
        //StartLeftY = 3058,
        // StartRightX = 7612,
        // StartRightY = 3070,
        //FinishLeftX = 4895,
        //FinishLeftY = 1642,
        // FinishRightX = 4888,
        // FinishRightY = 1654,
        // ZCoord = 117,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 2 o",
        // Index = 0,
        //HeadingDirection = 118.1f,
        //TrafficPaths = FakeTraffic[12],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4053,
        //StartLeftY = 1196,
        // StartRightX = 4047,
        // StartRightY = 1208,
        //FinishLeftX = 3633,
        //FinishLeftY = 972,
        // FinishRightX = 3627,
        // FinishRightY = 984,
        // ZCoord = 97,
        //}
        //},
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 172.1f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3382,
        //StartLeftY = 608,
        // StartRightX = 3368,
        // StartRightY = 610,
        //FinishLeftX = 3164,
        //FinishLeftY = -963,
        // FinishRightX = 3151,
        // FinishRightY = -961,
        // ZCoord = 97,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 352f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3271,
        //StartLeftY = -347,
        // StartRightX = 3284,
        // StartRightY = -349,
        //FinishLeftX = 3403,
        //FinishLeftY = 603,
        // FinishRightX = 3416,
        // FinishRightY = 601,
        // ZCoord = 97,
        //}
        //},*/
        //new HighwayZoneModel()
        //{
        //Name = "srs 2",
        // Index = 0,
        //HeadingDirection = 298f,
        //TrafficPaths = FakeTraffic[12],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3661,
        //StartLeftY = 977,
        // StartRightX = 3667,
        // StartRightY = 965,
        //FinishLeftX = 4501,
        //FinishLeftY = 1423,
        // FinishRightX = 4507,
        // FinishRightY = 1412,
        // ZCoord = 97,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 3",
        // Index = 0,
        //HeadingDirection = 297.5f,
        //TrafficPaths = FakeTraffic[13],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4640,
        //StartLeftY = 1498,
        // StartRightX = 4647,
        // StartRightY = 1486,
        //FinishLeftX = 7593,
        //FinishLeftY = 3033,
        // FinishRightX = 7599,
        // FinishRightY = 3022,
        // ZCoord = 117,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 4",
        // Index = 0,
        //HeadingDirection = 237.7f,
        //TrafficPaths = FakeTraffic[14],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 8145,
        //StartLeftY = 3026,
        // StartRightX = 8137,
        // StartRightY = 3014,
        //FinishLeftX = 8547,
        //FinishLeftY = 2772,
        // FinishRightX = 8539,
        // FinishRightY = 2761,
        // ZCoord = 117,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 5",
        // Index = 0,
        //HeadingDirection = 191.0f,
        //TrafficPaths = FakeTraffic[15],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 8731,
        //StartLeftY = 2280,
        // StartRightX = 8718,
        // StartRightY = 2278,
        //FinishLeftX = 9457,
        //FinishLeftY = -1453,
        // FinishRightX = 9443,
        // FinishRightY = -1454,
        // ZCoord = 117,
        //}
        //},
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 142.3f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9357,
        //StartLeftY = -1854,
        // StartRightX = 9347,
        // StartRightY = -1845,
        //FinishLeftX = 8610,
        //FinishLeftY = -2820,
        // FinishRightX = 8599,
        // FinishRightY = -2812,
        // ZCoord = 117,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 79.4f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 8429,
        //StartLeftY = -2891,
        // StartRightX = 8430,
        // StartRightY = -2878,
        //FinishLeftX = 7866,
        //FinishLeftY = -2790,
        // FinishRightX = 7868,
        // FinishRightY = -2773,
        // ZCoord = 117,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 46f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9126,
        //StartLeftY = -765,
        // StartRightX = 9135,
        // StartRightY = -755,
        //FinishLeftX = 4349,
        //FinishLeftY = 3848,
        // FinishRightX = 4358,
        // FinishRightY = 3858,
        // ZCoord = 37,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 226f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4319,
        //StartLeftY = 3864,
        // StartRightX = 4310,
        // StartRightY = 3855,
        //FinishLeftX = 8425,
        //FinishLeftY = -101,
        // FinishRightX = 8416,
        // FinishRightY = -111,
        // ZCoord = 37,
        //}
        //},*/
        //// express road zones start

        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 347.1f,
        //LeftLaneDirection = 167.2f,
        //IsOneLaneRoad = true,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 1718,
        //StartLeftY = -4166,
        // StartRightX = 1731,
        // StartRightY = -4169,
        //FinishLeftX = 1953,
        //FinishLeftY = -3135,
        // FinishRightX = 1967,
        // FinishRightY = -3138,
        // ZCoord = 66
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 336.7f,
        //LeftLaneDirection = 156.6f,
        //IsOneLaneRoad = true,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 1988,
        //StartLeftY = -3037,
        // StartRightX = 2000,
        // StartRightY = -3042,
        //FinishLeftX = 2367,
        //FinishLeftY = -2161,
        // FinishRightX = 2379,
        // FinishRightY = -2167,
        // ZCoord = 66,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 345.6f,
        //LeftLaneDirection = 165.8f,
        //IsOneLaneRoad = true,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 2470,
        //StartLeftY = -1816,
        // StartRightX = 2484,
        // StartRightY = -1819,
        //FinishLeftX = 2822,
        //FinishLeftY = -429,
        // FinishRightX = 2835,
        // FinishRightY = -433,
        // ZCoord = 66,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 358.7f,
        //LeftLaneDirection = 178.5f,
        //IsOneLaneRoad = true,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 2839,
        //StartLeftY = -296,
        // StartRightX = 2852,
        // StartRightY = -296,
        //FinishLeftX = 2878,
        //FinishLeftY = 1199,
        // FinishRightX = 2892,
        // FinishRightY = 1199,
        // ZCoord = 66,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 77.0f,
        //LeftLaneDirection = 257.0f,
        //IsOneLaneRoad = true,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 6832,
        //StartLeftY = -2599,
        // StartRightX = 6837,
        // StartRightY = -2586,
        //FinishLeftX = 2861,
        //FinishLeftY = -1682,
        // FinishRightX = 2864,
        // FinishRightY = -1669,
        // ZCoord = 66,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 143.7f,
        //LeftLaneDirection = 323.8f,
        //IsOneLaneRoad = true,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7980,
        //StartLeftY = -3521,
        // StartRightX = 7968,
        // StartRightY = -3513,
        //FinishLeftX = 6190,
        //FinishLeftY = -5961,
        // FinishRightX = 6180,
        // FinishRightY = -5953,
        // ZCoord = 119,
        //}
        //},*/
        //new HighwayZoneModel()
        //{
        //Name = "srs 6",
        // Index = 0,
        //HeadingDirection = 177.8f,
        //TrafficPaths = FakeTraffic[16],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9426,
        //StartLeftY = -1515,
        // StartRightX = 9413,
        // StartRightY = -1514,
        //FinishLeftX = 9326,
        //FinishLeftY = -4052,
        // FinishRightX = 9313,
        // FinishRightY = -4052,
        // ZCoord = 129,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 7",
        // Index = 0,
        //HeadingDirection = 178.0f,
        //TrafficPaths = FakeTraffic[17],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9326,
        //StartLeftY = -4052,
        // StartRightX = 9314,
        // StartRightY = -4052,
        //FinishLeftX = 9288,
        //FinishLeftY = -5163,
        // FinishRightX = 9275,
        // FinishRightY = -5162,
        // ZCoord = 165,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 143.3f,
        //TrafficPaths = FakeTraffic[18],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9168,
        //StartLeftY = -5505,
        // StartRightX = 9158,
        // StartRightY = -5497,
        //FinishLeftX = 7552,
        //FinishLeftY = -7669,
        // FinishRightX = 7542,
        // FinishRightY = -7661,
        // ZCoord = 165,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 87.3f,
        //TrafficPaths = FakeTraffic[19],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7054,
        //StartLeftY = -7913,
        // StartRightX = 7054,
        // StartRightY = -7900,
        //FinishLeftX = 4356,
        //FinishLeftY = -7783,
        // FinishRightX = 4356,
        // FinishRightY = -7771,
        // ZCoord = 165,
        //}
        //},
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 353.3f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3852,
        //StartLeftY = -6526,
        // StartRightX = 3864,
        // StartRightY = -6527,
        //FinishLeftX = 3942,
        //FinishLeftY = -5749,
        // FinishRightX = 3955,
        // FinishRightY = -5751,
        // ZCoord = 144,
        //}
        //},*/
        ///*new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 173.5f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 3911,
        //StartLeftY = -5921,
        // StartRightX = 3898,
        // StartRightY = -5920,
        //FinishLeftX = 3769,
        //FinishLeftY = -7146,
        // FinishRightX = 3756,
        // FinishRightY = -7144,
        // ZCoord = 129,
        //}
        //},*/
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 267.3f,
        //TrafficPaths = FakeTraffic[19],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 4365,
        //StartLeftY = -7791,
        // StartRightX = 4364,
        // StartRightY = -7804,
        //FinishLeftX = 7064,
        //FinishLeftY = -7921,
        // FinishRightX = 7063,
        // FinishRightY = -7934,
        // ZCoord = 164,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 323.3f,
        //TrafficPaths = FakeTraffic[18],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7552,
        //StartLeftY = -7680,
        // StartRightX = 7563,
        // StartRightY = -7687,
        //FinishLeftX = 9175,
        //FinishLeftY = -5507,
        // FinishRightX = 9186,
        // FinishRightY = -5514,
        // ZCoord = 164,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 7 o",
        // Index = 0,
        //HeadingDirection = 358.0f,
        //TrafficPaths = FakeTraffic[17],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9297,
        //StartLeftY = -5165,
        // StartRightX = 9310,
        // StartRightY = -5165,
        //FinishLeftX = 9340,
        //FinishLeftY = -3964,
        // FinishRightX = 9352,
        // FinishRightY = -3965,
        // ZCoord = 164,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "srs 6",
        // Index = 0,
        //HeadingDirection = 357.7f,
        //TrafficPaths = FakeTraffic[16],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9361,
        //StartLeftY = -3418,
        // StartRightX = 9374,
        // StartRightY = -3417,
        //FinishLeftX = 9444,
        //FinishLeftY = -1316,
        // FinishRightX = 9456,
        // FinishRightY = -1318,
        // ZCoord = 128,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "tny 1",
        // Index = 0,
        //HeadingDirection = 142.5f,
        //TrafficPaths = FakeTraffic[20],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 9357,
        //StartLeftY = -1854,
        // StartRightX = 9347,
        // StartRightY = -1846,
        //FinishLeftX = 7226,
        //FinishLeftY = -4626,
        // FinishRightX = 7216,
        // FinishRightY = -4618,
        // ZCoord = 117
        //}
        //},
        //new HighwayZoneModel()
        //{
        //    Name = "tny 2",
        //    Index = 0,
        //    HeadingDirection = 82.9f,
        //    TrafficPaths = FakeTraffic[21],
        //    ZoneBoundary = new ZoneBoundaryModel()
        //    {
        //     StartLeftX = 6686,
        //     StartLeftY = -4773,
        //     StartRightX = 6688,
        //     StartRightY = -4760,
        //     FinishLeftX = 2271,
        //     FinishLeftY = -4219,
        //     FinishRightX = 2273,
        //     FinishRightY = -4207,
        //     ZCoord = 129
        //    },
        //    StaticProps = new List<StaticPropsModel>()
        //    {
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6344, -4687, 0),
        //            Heading = 83.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle", 
        //            Postion = new GTA.Math.Vector3(6343, -4696, 0),
        //            Heading = 83.9f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle", 
        //            Postion = new GTA.Math.Vector3(6334, -4694, 0),
        //            Heading = 83.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6333, -4689, 0),
        //            Heading = 83.9f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6333, -4685, 0),
        //            Heading = 83.9f
        //        },
        //          new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6345, -4682, 0),
        //            Heading = 83.9f
        //        },
        //           new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6321, -4695, 0),
        //            Heading = 83.9f
        //        },
        //             new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6322, -4692, 0),
        //            Heading = 83.9f
        //        },
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6323.7f, -4677.7f, 0),
        //            Heading = 83.9f
        //        },
        //         // side parking 
        //         new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6289.6f, -4684.6f, 0),
        //            Heading = 148.5f
        //        },
        //          new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6286.2f, -4684.3f, 0),
        //            Heading = 148.5f
        //        },
        //           new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6282.9f, -4683.5f, 0),
        //            Heading = 148.5f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6279.1f, -4683.4f, 0),
        //            Heading = 148.5f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6275.7f, -4682.8f, 0),
        //            Heading = 148.5f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6268.0f, -4681.7f, 0),
        //            Heading = 148.5f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            IsElectric = true,
        //            Postion = new GTA.Math.Vector3(6309.8f, -4674.3f, 0),
        //            Heading = 82.2f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            IsElectric = true,
        //            Postion = new GTA.Math.Vector3(6309.1f, -4683.9f, 0),
        //            Heading = 82.2f
        //        },
        //        new StaticPropsModel()
        //        { 
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6284.1f, -4692.0f, 0),
        //            Heading = 265.5f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6288.2f, -4692.5f, 0),
        //            Heading = 74.7f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6279.5f, -4688.9f, 0),
        //            Heading = 5.1f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6268.5f, -4687.4f, 0),
        //            Heading = 289.9f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6270.9f, -4685.9f, 0),
        //            Heading = 143.4f,
        //        }, 
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6271.2f, -4687.8f, 0),
        //            Heading = 60.5f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6279.6f, -4687.2f, 0),
        //            Heading = 187.7f,
        //        },
        //        //gas 
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6335.2f, -4692.2f, 0),
        //            Heading = 184.0f,
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6334.3f, -4690.6f, 0),
        //            Heading = 3.4f,
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6334.5f, -4683.5f, 0),
        //            Heading = 173.5f, 
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6345.9f, -4685.1f, 0),
        //            Heading = 166.2f,
        //            ForcedAnimation = "world_human_gardener_leaf_blower"
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6320.3f, -4681.5f, 0),
        //            Heading = 291.0f,
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6321.6f, -4680.7f, 0),
        //            Heading = 118.2f,
        //        },
        //        new StaticPropsModel() 
        //        { 
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6321.5f, -4682.3f, 0),
        //            Heading = 64.4f,
        //        },
        //    }
        //},
        //new HighwayZoneModel()
        //{
        //Name = "tny 3",
        // Index = 0,
        //HeadingDirection = 5.8f,
        //TrafficPaths = FakeTraffic[21],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 1880,
        //StartLeftY = -4056,
        // StartRightX = 1893,
        // StartRightY = -4056,
        //FinishLeftX = 1788,
        //FinishLeftY = -3141,
        // FinishRightX = 1800,
        // FinishRightY = -3140,
        // ZCoord = 129,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "tny 4",
        // Index = 0,
        //HeadingDirection = 185.8f,
        //TrafficPaths = FakeTraffic[21],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 1779,
        //StartLeftY = -3142,
        // StartRightX = 1766,
        // StartRightY = -3143,
        //FinishLeftX = 1875,
        //FinishLeftY = -4090,
        // FinishRightX = 1862,
        // FinishRightY = -4091,
        // ZCoord = 99,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "tny 5",
        // Index = 0,
        //HeadingDirection = 262.9f,
        //TrafficPaths = FakeTraffic[21],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 2270,
        //StartLeftY = -4229,
        // StartRightX = 2268,
        // StartRightY = -4242,
        //FinishLeftX = 6674,
        //FinishLeftY = -4782,
        // FinishRightX = 6671,
        // FinishRightY = -4794,
        // ZCoord = 129,
        //},
        // StaticProps = new List<StaticPropsModel>()
        //    {
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6273.4f, -4759.6f, 0),
        //            Heading = 263.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6282.9f, -4765.6f, 0),
        //            Heading = 263.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6282.1f, -4768.9f, 0),
        //            Heading = 263.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6271.7f, -4767.5f, 0),
        //            Heading = 263.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6270.7f, -4775.3f, 0),
        //            Heading = 263.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6292.0f, -4781.7f, 0),
        //            Heading = 263.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6305.8f, -4783.6f, 0),
        //            Heading = 262.7f,
        //            IsElectric = true
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6324.9f, -4776.1f, 0),
        //            Heading = 329.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6328.5f, -4776.4f, 0),
        //            Heading = 329.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6335.4f, -4777.5f, 0),
        //            Heading = 329.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "vehicle",
        //            Postion = new GTA.Math.Vector3(6342.4f, -4778.5f, 0),
        //            Heading = 329.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6330.9f, -4773.1f, 0),
        //            Heading = 53.7f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6329.1f, -4772.3f, 0),
        //            Heading = 279.1f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6330.5f, -4771.1f, 0),
        //            Heading = 178.9f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6340.9f, -4769.5f, 0),
        //            Heading = 94.8f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6338.9f, -4769.3f, 0),
        //            Heading = 259.4f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6347.4f, -4774.7f, 0), 
        //            Heading = 77.2f
        //        },
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6297.0f, -4767.3f, 0),
        //            Heading = 35.2f
        //        }, 
        //        new StaticPropsModel()
        //        {
        //            Type = "ped",
        //            Postion = new GTA.Math.Vector3(6296.0f, -4766.1f, 0),
        //            Heading = 222.4f
        //        },
        // },
        //},
        //new HighwayZoneModel()
        //{
        //Name = "tny 6",
        // Index = 0,
        //HeadingDirection = 322.6f,
        //TrafficPaths = FakeTraffic[20],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7271,
        //StartLeftY = -4597,
        // StartRightX = 7280,
        // StartRightY = -4605,
        //FinishLeftX = 9389,
        //FinishLeftY = -1841,
        // FinishRightX = 9399,
        // FinishRightY = -1849,
        // ZCoord = 129,
        //}
        //},
        //// nurgurging authoban 
        //new HighwayZoneModel()
        //{
        //Name = "nur 1",
        // Index = 0,
        // TrafficPaths = FakeTraffic[3],
        //HeadingDirection = 355.1f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 5696,
        //StartLeftY = -4383,
        // StartRightX = 5708,
        // StartRightY = -4384,
        //FinishLeftX = 5867,
        //FinishLeftY = -2331,
        // FinishRightX = 5880,
        // FinishRightY = -2332,
        // ZCoord = 129,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        // TrafficPaths = FakeTraffic[3],
        //HeadingDirection = 327f,
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 5949,
        //StartLeftY = -2091,
        // StartRightX = 5960,
        // StartRightY = -2097,
        //FinishLeftX = 7215,
        //FinishLeftY = -141,
        // FinishRightX = 7225,
        // FinishRightY = -148,
        // ZCoord = 234,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        // TrafficPaths = FakeTraffic[3],
        //HeadingDirection = 353f, 
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7880,
        //StartLeftY = 387,
        // StartRightX = 7893,
        // StartRightY = 385,
        //FinishLeftX = 8777,
        //FinishLeftY = 7698,
        // FinishRightX = 8790,
        // FinishRightY = 7696,
        // ZCoord = 354,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0, 
        //HeadingDirection = 173.0f,
        //TrafficPaths = FakeTraffic[3],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 8786,
        //StartLeftY = 7850,
        // StartRightX = 8773,
        // StartRightY = 7851,
        //FinishLeftX = 7872,
        //FinishLeftY = 398,
        // FinishRightX = 7859,
        // FinishRightY = 400,
        // ZCoord = 354,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 147f,
        //TrafficPaths = FakeTraffic[3],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 7178,
        //StartLeftY = -182,
        // StartRightX = 7167,
        // StartRightY = -175,
        //FinishLeftX = 5940,
        //FinishLeftY = -2088,
        // FinishRightX = 5929,
        // FinishRightY = -2081,
        // ZCoord = 360,
        //}
        //},
        //new HighwayZoneModel()
        //{
        //Name = "zone",
        // Index = 0,
        //HeadingDirection = 175.1f,
        //TrafficPaths = FakeTraffic[3],
        //ZoneBoundary = new ZoneBoundaryModel()
        //{
        //StartLeftX = 5857,
        //StartLeftY = -2328,
        // StartRightX = 5844,
        // StartRightY = -2327,
        //FinishLeftX = 5671,
        //FinishLeftY = -4540,
        // FinishRightX = 5659,
        // FinishRightY = -4539,
        // ZCoord = 234,
        //}
        //},





        //        };
    }
}
