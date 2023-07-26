using UnityEngine;
using UnityEngine.SceneManagement;
using CloneEscape.UI.Panel;
using CloneEscape.Audio;
using CloneEscape.LevelBase;

namespace CloneEscape.UI.Controller
{
    public class WinController : AnimatedPanelController
    {
        [SerializeField]
        private AudioClip _winClip;

        private void Awake()
        {
            LevelController.Instance.OnPlayerWin.AddListener(OpenPanel);
            LevelController.Instance.OnPlayerWin.AddListener(SaveCurrentLevel);
            OnOpenStateChange.AddListener((bool _) => AudioManager.Instance.AudioSource.PlayOneShot(_winClip));
        }

        private void OnDestroy()
        {
            LevelController.Instance.OnPlayerWin.RemoveListener(OpenPanel);
            LevelController.Instance.OnPlayerWin.RemoveListener(SaveCurrentLevel);
            OnOpenStateChange?.RemoveListener((bool _) => AudioManager.Instance.AudioSource.PlayOneShot(_winClip));
        }

        private void SaveCurrentLevel()
        {
            string CURRENT_LEVEL = "Current Level";
            PlayerPrefs.SetInt(CURRENT_LEVEL, SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Next()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex + 1);
        }

        public void Exit() => SceneManager.LoadScene(0);
    }
}