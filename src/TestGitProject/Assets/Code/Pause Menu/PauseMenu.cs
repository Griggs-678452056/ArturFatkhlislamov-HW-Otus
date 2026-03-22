using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private PauseMenuController _pauseMenuController;

        private void Awake()
        {
            _pauseMenuController = FindFirstObjectByType<PauseMenuController>();

            if (_pauseMenuController == null)
            {
                Debug.LogError("PauseMenuController не найден!");
            }
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueClicked);
            _exitButton.onClick.AddListener(ExitClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueClicked);
            _exitButton.onClick.RemoveListener(ExitClicked);
        }

        private void ContinueClicked()
        {
            _pauseMenuController.ContinueGame();
        }

        private void ExitClicked()
        {
            Application.Quit();
            Debug.LogWarning("Вы вышли из игры");
        }
    }
}
