using System;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.ResetPlayerPositionPatch), "ResetPlayerPositionPatch", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod
{
    [HarmonyPatch(typeof(Actor_PlayerReset), "ResetPlayerPosition", new Type[] { })]
    static class ResetPlayerPositionPatch
    {
        private static void Prefix() { }

        private static void Postfix()
        {
            // Reset sand dollars / missions / feats / collectables.
            SandDollarManager.instance.SetSandDollarsAmount(0);
            PHL_MissionManager.instance.missionLog.Clear();
            PHL_MissionManager.instance.MissionsChanged();
            FeatsManager.instance.completedFeats.Clear();
            FeatsManager.instance.FeatsLoaded();
            CollectableObjectManager.instance.ClearAll();
            CollectableObjectManager.instance.ApplyCollectableStates();
            SavingManager.instance.gameFile.PopulateFromGame();
            SavingManager.instance.gameFile.ApplyToGame();

            // Teleport player to magnet.
            var playerCC = GameObject.FindObjectOfType<Actor_PlayerCC>();
            playerCC.Teleport(new Vector3(65f, 19f, 138f), 0, true);

            // Give player a water sprayer.
            var waterSprayer = GameObject.FindObjectOfType<Equipment_WaterSprayer>(true);
            var patrickEquipment = PlayerManager.player.GetBehaviour<Actor_Equipment>();
            patrickEquipment.Equip(waterSprayer);
        }
    }
}
