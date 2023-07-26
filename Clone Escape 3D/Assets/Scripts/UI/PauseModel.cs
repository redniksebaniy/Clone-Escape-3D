using UnityEngine;

namespace CloneEscape.UI.Model
{
    public class PauseModel : SoundModel
    {
        private bool isPaused = false;
        public bool IsPaused
        {
            get { return isPaused; }
            set 
            { 
                isPaused = value;
                Time.timeScale = value ? 0f : 1f;
            }

        }
    }
}
