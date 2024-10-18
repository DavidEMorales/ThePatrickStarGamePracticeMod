using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(ThePatrickStarGamePracticeMod.Core), "ThePatrickStarGamePracticeMod", "1.0.0", "Sleepyhead08", null)]
[assembly: MelonGame("PHL", "Patrick")]
namespace ThePatrickStarGamePracticeMod
{
    public class Core : MelonMod
    {
        private static Actor_PlayerCC _playerCC;
        public static Actor_PlayerCC PlayerCC 
        {
            get 
            {
                if (_playerCC == null) 
                {
                    _playerCC = PlayerManager.player.GetBehaviour<Actor_PlayerCC>();
                }
                return _playerCC;
            }
        }

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}