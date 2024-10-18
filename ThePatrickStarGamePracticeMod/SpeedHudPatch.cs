using System;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.SpeedHudPatch), "SpeedHudPatch", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod
{
    [HarmonyPatch(typeof(UI_SandDollars), "Update", new Type[] { })]
    static class SpeedHudPatch
    {
        private static void Postfix(ref LocalizedTextMeshPro ____sandDollarsText)
        {
            ____sandDollarsText.SetTextDirectly(____sandDollarsText.textMeshPro.text + "\n" + (int)(Core.PlayerCC.velocity.magnitude*100));
        }
    }
}
