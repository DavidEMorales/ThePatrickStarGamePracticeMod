using MelonLoader;
using HarmonyLib;
using UnityEngine;
using System.Collections;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.Core), "ThePatrickStarGamePracticeMod", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]

namespace ThePatrickStarGamePracticeMod
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }

    [HarmonyPatch(typeof(Actor_PlayerReset), "ResetPlayerPosition", new Type[] { })]
    static class ResetPlayerPositionPatch
    {
        private static void Prefix()
        {

        }

        private static void Postfix()
        {
            SandDollarManager.instance.SetSandDollarsAmount(0);
            PHL_MissionManager.instance.missionLog.Clear();
            PHL_MissionManager.instance.MissionsChanged();
            FeatsManager.instance.completedFeats.Clear();
            FeatsManager.instance.FeatsLoaded();
            CollectableObjectManager.instance.ClearAll();
            CollectableObjectManager.instance.ApplyCollectableStates();



            foreach (string text in SavingManager.instance.gameFile.cvBools.Keys)
            {
                if (text.Contains("FlyoverSeen"))
                {
                    CustomValue_SO objectByID2 = Database<CustomValueDatabase, CustomValue_SO>.instance.GetObjectByID(text);
                    if (objectByID2 != null)
                    {
                        if (CustomValues.bools.Keys.Contains(objectByID2))
                            CustomValues.bools[objectByID2] = false;
                        else
                            CustomValues.bools.Add(objectByID2, false);
                    } 
                }
            }
            CustomValues.AllValuesUpdated();

            WorldZoneDatabase.instance.RefreshDatabase();

            SavingManager.instance.gameFile.PopulateFromGame();
            SavingManager.instance.gameFile.ApplyToGame();

            var playerCC = GameObject.FindObjectOfType<Actor_PlayerCC>();
            playerCC.Teleport(new Vector3(65f, 19f, 138f), 0, true);

            var waterSprayer = GameObject.FindObjectOfType<Equipment_WaterSprayer>(true);
            var patrickEquipment = PlayerManager.player.GetBehaviour<Actor_Equipment>();
            patrickEquipment.Equip(waterSprayer);
        }

    }

    [HarmonyPatch(typeof(SaveFile_Game), "ApplyToGame", new Type[] { })]
    static class LoadGamePatch
    {
        private static void Prefix()
        {

        }

        private static void Postfix()
        {
            foreach (string text in SavingManager.instance.gameFile.cvBools.Keys)
            {
                if (text.Contains("FlyoverSeen"))
                {
                    CustomValue_SO objectByID2 = Database<CustomValueDatabase, CustomValue_SO>.instance.GetObjectByID(text);
                    if (objectByID2 != null)
                    {
                        if (CustomValues.bools.Keys.Contains(objectByID2))
                            CustomValues.bools[objectByID2] = false;
                        else
                            CustomValues.bools.Add(objectByID2, false);
                    }
                }
            }
            CustomValues.AllValuesUpdated();
        }

    }

}