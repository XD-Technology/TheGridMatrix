using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGridMatrix
{
    public class Menu : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}