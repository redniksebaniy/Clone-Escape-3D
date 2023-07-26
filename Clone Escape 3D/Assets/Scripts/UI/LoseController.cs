using UnityEngine;
using UnityEngine.SceneManagement;
using CloneEscape.UI.Panel;
using CloneEscape.Audio;
using CloneEscape.LevelBase;

namespace CloneEscape.UI.Controller
{
    public class LoseController : AnimatedPanelController
    {
        [SerializeField]
        private AudioClip _loseClip;

        private void Awake()
        {
            LevelController.Instance.OnPlayerLose.AddListener(OpenPanel);
            OnOpenStateChange.AddListener((bool _) => AudioManager.Instance.AudioSource.PlayOneShot(_loseClip));
        }

        private void OnDestroy()
        {
            LevelController.Instance.OnPlayerLose.RemoveListener(OpenPanel);
            OnOpenStateChange?.RemoveListener((bool _) => AudioManager.Instance.AudioSource.PlayOneShot(_loseClip));
        }

        public void Restart()
        {
            Scene newActiveScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(newActiveScene.buildIndex);
        }

        public void Exit() => SceneManager.LoadScene(0);
    }
}
