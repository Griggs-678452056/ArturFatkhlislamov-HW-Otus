using UnityEngine;
using System.Collections;

namespace Code
{
    public class HealBoxSystem : MonoBehaviour
    {
        [Header("Aptechka prefab")]
        [SerializeField] private GameObject healBoxPrefab;

        [Header("Spawn settings")]
        [SerializeField] private int numberOfBoxes = 5;
        [SerializeField] private float minX = -10f;
        [SerializeField] private float maxX = 10f;
        [SerializeField] private float minZ = -10f;
        [SerializeField] private float maxZ = 10f;
        [SerializeField] private float yPos = 0.5f;

        private void Start()
        {
            SpawnHealBoxes();
        }

        private void SpawnHealBoxes()
        {
            for (int i = 0; i < numberOfBoxes; i++)
            {
                // Случайная позиция
                float x = Random.Range(minX, maxX);
                float z = Random.Range(minZ, maxZ);
                Vector3 spawnPos = new Vector3(x, yPos, z);

                // Создаём аптечку и добавляем компонент для триггера
                GameObject box = Instantiate(healBoxPrefab, spawnPos, Quaternion.identity);

                // Убедимся, что у аптечки есть коллайдер с IsTrigger
                BoxCollider col = box.GetComponent<BoxCollider>();
                if (col == null) col = box.AddComponent<BoxCollider>();
                col.isTrigger = true;

                // Добавляем скрипт под подбор
                box.AddComponent<HealBoxPickup>();
            }
        }
    }

    // Скрипт для обработки подбора аптечки
    public class HealBoxPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Аптечка подобрана. Здоровье восстановлено!");
                Destroy(gameObject);
            }
        }
    }
}