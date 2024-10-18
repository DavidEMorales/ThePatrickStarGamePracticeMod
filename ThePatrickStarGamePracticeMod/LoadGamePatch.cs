using System;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.LoadGamePatch), "LoadGamePatch", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod 
{
    [HarmonyPatch(typeof(SaveFile_Game), "ApplyToGame", new Type[] { })]
    static class LoadGamePatch
    {
        private static void Prefix() { }

        private static void Postfix()
        {
            // Reset all FlyoverSeen bools to unsee all cutscenes.
            foreach (string text in SavingManager.instance.gameFile.cvBools.Keys)
            {
                if (text.Contains("FlyoverSeen"))
                {
                    CustomValue_SO objectByID2 = Database<CustomValueDatabase, CustomValue_SO>.instance.GetObjectByID(text);
                    if (objectByID2 != null)
                    {
                        CustomValues.bools[objectByID2] = false;
                    }
                }
            }

            // Apply values.
            CustomValues.AllValuesUpdated();
        }
    } 
}
