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

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shootSound;
        private void Start()
        {
            Recharge();
        }

        public override void Fire()
        {
            if (_instantiateRocket == null)
            {
                Debug.LogWarning("Bazooka: Нет готовой ракеты!");
                return;
            }

            if (_isReloading)
            {
                return;
            }

            StartCoroutine(FireCoroutine());
        }
        private IEnumerator FireCoroutine()
        {
            yield return new WaitForSeconds(_launchDelay);

            if (_instantiateRocket == null)
            {
                Debug.LogWarning("Bazooka: Rocket is null in FireCoroutine!");
                yield break;
            }

            _instantiateRocket.Run(_barrel.forward * _force);
            PlayShootSound();
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

            if (_rocketPrefab == null)
            {
                Debug.LogError("Префаб ракеты не назначен");
                return;
            }

            if (_barrel == null)
            {
                Debug.LogError("Ствол ракеты (Barrel) не назначен");
                return;
            }

            _instantiateRocket = Instantiate(_rocketPrefab, _barrel);
            _instantiateRocket.transform.localPosition = Vector3.zero;
            _instantiateRocket.transform.localRotation = Quaternion.identity;
            _instantiateRocket.Sleep(_barrel.position);
        }

        private void PlayShootSound()
        {
            if (_audioSource == null || _shootSound == null)
            {
                return;
            }

            _audioSource.PlayOneShot(_shootSound);
        }
    }
}