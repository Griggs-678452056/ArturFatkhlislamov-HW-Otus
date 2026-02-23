using UnityEngine;
using System.Collections;
using System;

namespace Assets.Code.Weapon
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
                _isAlive = false;
                return false;
            }

            return true;
        }
    }
}