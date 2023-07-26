using UnityEngine;
using UnityEngine.SceneManagement;
using CloneEscape.UI.Panel;
using CloneEscape.UI.Model;
using CloneEscape.Audio;

namespace CloneEscape.UI.Controller
{
    public class PauseController : AnimatedPanelInputController
    {
        private readonly PauseModel pauseModel = new();

        private void Awake()
        {
            OnOpenStateChange.AddListener(TooglePause);
            OnOpenStateChange.AddListener(AudioManager.Instance.TooglePauseEffect);
        }

        private void OnDestroy()
        {
            OnOpenStateChange.RemoveListener(TooglePause);
            OnOpenStateChange.RemoveListener(AudioManager.Instance.TooglePauseEffect);

            AudioManager.Instance.TooglePauseEffect(false);
            TooglePause(false);
        }

        public void TooglePause(bool value)
        {
            pauseModel.IsPaused = value;
        }

        public void Restart()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        public void ChangeMute()
        {
            bool value = !pauseModel.IsMuted;
            pauseModel.IsMuted = value;
            AudioManager.Instance.IsMuted = value;
        }

        public void Exit() => SceneManager.LoadScene(0);
    }
}
