using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Actor_PlayerReset), "ResetPlayerPosition", [])]
internal static class ResetPlayerPositionPatch
{
    private static void Prefix() { }

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
                    CustomValues.bools[objectByID2] = false;
                }
            }
        }

        CustomValues.AllValuesUpdated();

        WorldZoneDatabase.instance.RefreshDatabase();

        SavingManager.instance.gameFile.PopulateFromGame();
        SavingManager.instance.gameFile.ApplyToGame();

        var playerCC = Object.FindObjectOfType<Actor_PlayerCC>();
        playerCC.Teleport(new Vector3(65f, 19f, 138f), 0, true);

        var waterSprayer = Object.FindObjectOfType<Equipment_WaterSprayer>(true);
        var patrickEquipment = PlayerManager.player.GetBehaviour<Actor_Equipment>();
        patrickEquipment.Equip(waterSprayer);
    }
}
