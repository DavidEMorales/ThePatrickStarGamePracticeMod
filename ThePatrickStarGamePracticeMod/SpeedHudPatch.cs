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
        static Actor_PlayerCC playerCC;

        private static void Prefix()
        {
            
        }

        private static void Postfix(ref LocalizedTextMeshPro ____sandDollarsText)
        {
            if (playerCC == null) 
            { 
                playerCC = GameObject.FindObjectOfType<Actor_PlayerCC>();
            }

            ____sandDollarsText.SetTextDirectly(____sandDollarsText.textMeshPro.text + "\n" + (int)(playerCC.velocity.magnitude*100));
        }
    }
}
