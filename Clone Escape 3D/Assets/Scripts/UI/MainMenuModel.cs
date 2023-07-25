using UnityEngine;

namespace CloneEscape.UI.Model
{
    public class MainMenuModel : SoundModel
    {
        private const string CURRENT_LEVEL = "Current Level";

        public int CurrentLevel
        {
            get
            {
                return PlayerPrefs.GetInt(CURRENT_LEVEL, 1);
            }

            set
            {
                PlayerPrefs.SetInt(CURRENT_LEVEL, value);
            }
        }
    }
}