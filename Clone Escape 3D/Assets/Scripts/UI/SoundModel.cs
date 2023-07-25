using UnityEngine;

namespace CloneEscape.UI.Model
{
    public class SoundModel
    {
        private const string IS_MUTE = AudioManager.IS_MUTE;

        public bool IsMuted
        {
            get
            {
                return PlayerPrefs.GetInt(IS_MUTE, 0) > 0;
            }

            set
            {
                PlayerPrefs.SetInt(IS_MUTE, value ? 1 : 0);
            }
        }
    }
}