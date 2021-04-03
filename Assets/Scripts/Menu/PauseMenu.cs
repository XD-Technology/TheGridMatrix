using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheGridMatrix
{
    public class PauseMenu : Menu
    {
        public Text header;

        public void Menu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }

        public void EndGame()
        {
            header.text = "Final Score: " + GameManager.Score;
        }


        public void Pause(bool value)
        {
            GameManager.Pause = value;
        }
    }
}