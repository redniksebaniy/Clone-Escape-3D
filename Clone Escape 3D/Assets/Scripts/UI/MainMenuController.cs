using UnityEngine;
using UnityEngine.SceneManagement;
using CloneEscape.UI.Model;

namespace CloneEscape.UI.Controller
{
    public class MainMenuController : MonoBehaviour
    {
        private readonly MainMenuModel mainMenuModel = new();

        public void ContinueGame()
        {
            SceneManager.LoadScene(mainMenuModel.CurrentLevel);
        }

        public void StartGame()
        {
            mainMenuModel.CurrentLevel = 1;
            ContinueGame();
        }

        public void ChangeMute()
        {
            bool value = !mainMenuModel.IsMuted;
            mainMenuModel.IsMuted = value;
            AudioManager.Instance.IsMuted = value;
        }

        public void Exit() => Application.Quit();
    }
}