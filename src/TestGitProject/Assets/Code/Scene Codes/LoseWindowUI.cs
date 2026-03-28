using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code
{
    public class LoseWindowUI : MonoBehaviour
    {
        [SerializeField] private Button _restartLevelButton;

        private WinLoseController _controller;

        private void Awake()
        {
            _controller = FindFirstObjectByType<WinLoseController>();

            if (_controller == null)
            {
                Debug.LogError("WinLoseController не найден!");
            }

            _restartLevelButton.onClick.AddListener(GoToNextLevel);
        }

        private void GoToNextLevel()
        {
            Time.timeScale = 1.0f;

            SceneManager.LoadScene("PlayScene");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}