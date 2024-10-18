using HarmonyLib;

namespace ThePatrickStarGamePracticeMod;

[HarmonyPatch(typeof(SaveFile_Game), "ApplyToGame", [])]
static class LoadGamePatch
{
    private static void Prefix() { }

    private static void Postfix()
    {
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
    }
}
