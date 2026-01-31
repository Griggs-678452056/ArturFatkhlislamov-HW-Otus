using UnityEngine;
namespace Code
{
    public class DeathIsComing : MonoBehaviour
    {
        private int hp = 100;
        private bool isDead = false;

        private void Start()
        {
            Debug.LogError($"Уровень здоровья: {hp}. В добрый путь!");
        }
        private void Update()
        {
            if (isDead)
                return;
            int damage = Random.Range(1, 11);
            damage = Mathf.Min(damage, hp);
            hp -= damage;
            Debug.LogError($"Персонажу нанесён урон {damage}. Уровень здоровья опустился до {hp}");

            if (hp == 0)
            {
                isDead = true;
                Debug.LogError("Персонаж пал смертью храбрых!");
                return;
            }

        }
    }
}