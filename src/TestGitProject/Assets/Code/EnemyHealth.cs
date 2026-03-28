using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHP = 100f;
        private float _currentHP;
        private bool _isDead;

        public float CurrentHP
        {
            get
            {
                return _currentHP;
            }
        }

        public float MaxHP
        {
            get
            {
                return _maxHP;
            }
        }

        public event Action<float, float> OnHealthChanged;

        public event Action OnDeath;

        private void Awake()
        {
            _currentHP = _maxHP;
            NotifyHealthChanged();
        }

        public bool IsDead()
        {
            return _isDead;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead())
            {
                return;
            }

            _currentHP -= damage;

            if (_currentHP <= 0f)
            {
                _currentHP = 0f;
                _isDead = true;

                OnDeath?.Invoke();
                Destroy(gameObject);
            }

            NotifyHealthChanged();
        }

        private void NotifyHealthChanged()
        {
            OnHealthChanged?.Invoke(_currentHP, _maxHP);
        }
    }
}