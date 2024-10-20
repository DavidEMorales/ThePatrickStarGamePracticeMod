using System;
using System.Linq;
using System.Collections.Generic;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using PHL.Common.Utility;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.TimerDisplayPatch), "TimerDisplayPatch", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod
{
    [HarmonyPatch(typeof(PauseMenuManager), "OpenPauseMenu", new Type[] { typeof(int) })]
    static class TimerOpenPatch
    {
        public static float StartTime = 0.0f;
        public static float StopTime = 0.0f;
        public static bool TimerIsRunning = false;

        private static void Postfix()
        {
            TimerIsRunning = false;
        }
    }

    [HarmonyPatch(typeof(PauseMenuManager), "ClosePauseMenu", new Type[] { })]
    static class TimerClosePatch
    {
        private static void Postfix()
        {
            TimerOpenPatch.StartTime = Time.time;
            TimerOpenPatch.TimerIsRunning = true;

        }
    }

    [HarmonyPatch(typeof(UI_SandDollars), "Update", new Type[] { })]
    static class TimerDisplayPatch
    {
        private static void Postfix(ref LocalizedTextMeshPro ____sandDollarsText)
        {
            string displayTime;
            if (TimerOpenPatch.TimerIsRunning)
            {
                displayTime = "" + (Time.time - TimerOpenPatch.StartTime);
            }
            else
            {
                if (TimerOpenPatch.StopTime < TimerOpenPatch.StartTime)
                {
                    TimerOpenPatch.StopTime = Time.time;
                }

                displayTime = "" + (TimerOpenPatch.StopTime - TimerOpenPatch.StartTime);
            }

            displayTime = displayTime.Substring(0, displayTime.IndexOf('.') + 3);

            ____sandDollarsText.SetTextDirectly(____sandDollarsText.textMeshPro.text + "\n" + displayTime + "\n----------");
        }
    }
}
