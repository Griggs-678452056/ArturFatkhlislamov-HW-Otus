using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class WinLoseController : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private NPCHealth _npcHealth;

        private void Start()
        {
            if (_enemyHealth != null)
            {
                _enemyHealth.OnDeath += OnEnemyDead;
            }

            if (_npcHealth != null)
            {
                _npcHealth.OnDeath += OnNPCDead;
            }
        }

        
        private void OnEnemyDead()
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
            if (_enemyHealth != null)
            {
                _enemyHealth.OnDeath -= OnEnemyDead;
            }

            if (_npcHealth != null)
            {
                _npcHealth.OnDeath -= OnNPCDead;
            }
        }
    }
}