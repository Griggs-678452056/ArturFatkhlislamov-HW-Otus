using UnityEngine;

namespace Code
{
    public class AllyOrEnemySpawned : MonoBehaviour
    {
        [SerializeField] private int objectCount = 10;

        [SerializeField] private float spawnRangeX = 5f;
        [SerializeField] private float spawnRangeZ = 5f;
        [SerializeField] private float spawnY = 0.5f;

        private void Start()
        {
            for (int i = 0; i < objectCount; i++)
            {
                PrimitiveType type = (Random.value > 0.5f) ? PrimitiveType.Cube : PrimitiveType.Sphere;
                GameObject gameObject = GameObject.CreatePrimitive(type);

                float posX = Random.Range(-spawnRangeX, spawnRangeX);
                float posZ = Random.Range(-spawnRangeZ, spawnRangeZ);

                gameObject.transform.position = new Vector3(posX, spawnY, posZ);
                MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
                renderer.material.color = Color.gray;

                if (type == PrimitiveType.Cube)
                {
                    gameObject.name = "Бро";
                    renderer.material.color = Color.blue;
                }
                else if (type == PrimitiveType.Sphere)
                {
                    gameObject.name = "Не бро";
                    renderer.material.color = Color.red;
                }
            }
        }
    }
}