using System;
using System.Linq;
using System.Collections.Generic;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using PHL.Common.Utility;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.TeleportToWaypointPatch), "TeleportToWaypointPatch", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod
{
    [HarmonyPatch(typeof(UI_MapPoint_Custom), "Pressed", new Type[] { })]
    static class TeleportToWaypointPatch
    {
        private static void Postfix(ref UI_MapPoint_Custom __instance)
        {
            Core.PlayerCC.Teleport(__instance.mapPoint.position);
        }
    }
}
