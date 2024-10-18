using System;
using System.Linq;
using System.Collections.Generic;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using PHL.Common.Utility;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.SlowDownSliderPatch), "SlowDownSliderPatch", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod
{
    [HarmonyPatch(typeof(SettingFunctionality_Audio_VOVolume), "OnValueApplied", new Type[] { typeof(float) })]
    static class SlowDownSliderPatch
    {
        public static float TimeScale = 1.0f;

        private static void Postfix(float value)
        {
            TimeScale = value / 10.0f;
        }
    }

    [HarmonyPatch(typeof(UI_SandDollars), "Update", new Type[] { })]
    static class SlowDownApplierPatch
    {
        private static void Postfix(ref LocalizedTextMeshPro ____sandDollarsText)
        {
            Time.timeScale = SlowDownSliderPatch.TimeScale;

            ____sandDollarsText.SetTextDirectly(____sandDollarsText.textMeshPro.text + "\n" + Time.timeScale);
        }
    }
}
