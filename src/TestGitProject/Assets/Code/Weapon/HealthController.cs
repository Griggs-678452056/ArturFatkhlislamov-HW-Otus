using UnityEngine;
using System.Collections;
using System;

namespace Code
{
	public class HealthController: MonoBehaviour
	{
		[SerializeField] private float _health = 5.0f;
		[SerializeField] private float _lifetime = 10.0f;

        private float _maxHp;
        private bool _isAlive = true;

        private void Start()
        {
            _maxHp = _health;
        }

        public bool CanTakeDamage(float damage)
        {
            if( _isAlive == false)
            {
                return false;
            }

            _health -= damage;

            if ( _health <= 0 )
            {
                StartCoroutine(Die());
                _isAlive = false;
                return false;
            }

            return true;
        }

        private IEnumerator Die()
        {
            Renderer component = GetComponent<Renderer>();

            component.material.color = Color.red;
            yield return new WaitForSeconds(1.0f);
            component.material.color = Color.green;
            yield return new WaitForSeconds(1.0f);
            component.material.color = Color.red;
            yield return new WaitForSeconds(1.0f);
            component.material.color = Color.magenta;

            yield return new WaitForSeconds(_lifetime);

            StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                Color color = renderer.material.color;

                for (float alpha = 1.0f; alpha >= 0.0f; alpha -= 0.01f)
                {
                    color.a = alpha;
                    renderer.material.color = color;
                    yield return new WaitForSeconds(0.01f);
                }
            }

            Destroy(gameObject);
        }
    }
}