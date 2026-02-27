using System.Collections;
using UnityEngine;

namespace Code
{
    public sealed class Bazooka : Weapon
    {
        [SerializeField] private Rocket _rocketPrefab;

        [SerializeField] private float _reloadTime = 3f;
        [SerializeField] private float _launchDelay = 0.4f;

        private Rocket _instantiateRocket;
        private bool _isReloading;

        private void Start()
        {
            Recharge();
        }

        public override void Fire()
        {
            if (_instantiateRocket == null || _isReloading)
            {
                return;
            }

            StartCoroutine(FireCoroutine());
        }
        private IEnumerator FireCoroutine()
        {
            yield return new WaitForSeconds(_launchDelay);

            _instantiateRocket.Run(_barrel.forward * _force);
            _instantiateRocket = null;

            StartCoroutine(ReloadCoroutine());

        }

        private IEnumerator ReloadCoroutine()
        {
            _isReloading = true;

            yield return new WaitForSeconds(_reloadTime);

            Recharge();
            _isReloading = false;
        }

        public override void Recharge()
        {
            if (_instantiateRocket != null)
            {
                return;
            }
            _instantiateRocket = Instantiate(_rocketPrefab, _barrel);
            _instantiateRocket.Sleep(_barrel.position);
        }
    }
}