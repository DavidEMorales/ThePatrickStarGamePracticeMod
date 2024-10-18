using MelonLoader;

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
}