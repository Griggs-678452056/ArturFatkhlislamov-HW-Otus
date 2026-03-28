using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class PauseMenuController : MonoBehaviour
    {
        private bool _isPaused;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_isPaused)
                {
                    PauseGame();
                }
                else
                {
                    ContinueGame();
                }
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0.0f;

            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _isPaused = true;
        }

        internal void ContinueGame()
        {
            Time.timeScale = 1.0f;

            SceneManager.UnloadSceneAsync("PauseMenu");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _isPaused = false;
        }
    }
}
