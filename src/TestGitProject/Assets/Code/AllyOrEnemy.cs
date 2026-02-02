using UnityEngine;
namespace Code
{
    public class AllyOrEnemy : MonoBehaviour
    {
        [SerializeField] private bool isEnemy;
        [SerializeField] private bool isAlly;
        private MeshRenderer meshRenderer;
        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.gray;
            if (isEnemy)
            {
                meshRenderer.material.color = Color.red;
            }
            else if (isAlly)
            {
                meshRenderer.material.color = Color.blue;
            }
            else return;
        }
    }
}