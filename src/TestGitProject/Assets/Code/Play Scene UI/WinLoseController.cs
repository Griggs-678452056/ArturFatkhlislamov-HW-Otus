using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class WinLoseController : MonoBehaviour
    {
        [SerializeField] private NPCHealth _npcHealth;

        private int _aliveEnemies;

        private void Start()
        {
            if (_npcHealth != null)
            {
                _npcHealth.OnDeath += OnNPCDead;
            }
        }

        public void RegisterEnemy(EnemyHealth enemy)
        {
            _aliveEnemies++;
            enemy.OnDeath += OnEnemyDead;
        }

        private void OnEnemyDead()
        {
            _aliveEnemies--;

            if (_aliveEnemies <= 0)
            {
                Win();
            }
        }

        private void Win()
        {
            Time.timeScale = 0.0f;

            SceneManager.LoadScene("YouWin", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnNPCDead()
        {
            Time.timeScale = 0.0f;

            SceneManager.LoadScene("YouLose", LoadSceneMode.Additive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDestroy()
        {
            if (_npcHealth != null)
            {
                _npcHealth.OnDeath -= OnNPCDead;
            }
        }
    }
}