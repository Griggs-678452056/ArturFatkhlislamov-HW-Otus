using UnityEngine;

namespace Code
{
    public class EnemySpawnerService : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _countForSpawn = 10;
        [SerializeField] private float _timeBetweenSpawns;
        [SerializeField] private int _enemyCountThresholdForWave = 10;
        private float _currentTime;

        private void Start()
        {
            StartWave();
            SetSpawnTimerForMaxTime();
        }

        private void Update()
        {
            CurrentTimerTick();

            if (_currentTime <= 0 && GetAliveEnemiesCount() < _enemyCountThresholdForWave)
            {
                StartWave();
                SetSpawnTimerForMaxTime();
            }
        }

        private void CurrentTimerTick()
        {
            _currentTime -= Time.deltaTime;
        }

        private void StartWave()
        {
            for (int i = 0; i < _countForSpawn; i++)
            {
                CreateEnemy();
            }
        }

        private void CreateEnemy()
        {
            Instantiate(
                _enemyPrefab,
                new Vector3(
                    RandomFloat(_spawnPoint.position.x, 10),
                    _spawnPoint.position.y,
                    RandomFloat(_spawnPoint.position.z, 10)),
                Quaternion.identity);
        }

        private void SetSpawnTimerForMaxTime()
        {
            _currentTime = _timeBetweenSpawns;
        }

        private float RandomFloat(float positionAxis, float offset)
        {
            return Random.Range(positionAxis, positionAxis + offset);
        }

        private int GetAliveEnemiesCount()
        {
            return FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None).Length;
        }
    }
}