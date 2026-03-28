using System;
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

        private const int MaxReserve = 3;
        private int ClipSize
        {
            get
            {
                return _weaponConfig.ClipSize;
            }
        }

        private int _currentAmmo;
        private int _reserveAmmo;

        public override int CurrentAmmo
        {
            get
            {
                return _currentAmmo;
            }
        }

        public override int ReserveAmmo
        {
            get
            {
                return _reserveAmmo;
            }
        }

        private void Start()
        {
            _currentAmmo = ClipSize;
            _reserveAmmo = MaxReserve - 1;

            Recharge();
            NotifyAmmoChanged(_currentAmmo, _reserveAmmo);
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
                yield break;
            }

            _currentAmmo--;

            _instantiateRocket.Run(_barrel.forward * _force);
            PlayShootSound();

            _instantiateRocket = null;
            NotifyAmmoChanged(_currentAmmo, _reserveAmmo);

            StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            if (_reserveAmmo <= 0)
            {
                yield break;
            }

            _isReloading = true;
            yield return new WaitForSeconds(_reloadTime);

            int neededAmmo = ClipSize - _currentAmmo;
            int ammoToLoad = Mathf.Min(neededAmmo, _reserveAmmo);

            _currentAmmo += ammoToLoad;
            _reserveAmmo -= ammoToLoad;

            Recharge();
            NotifyAmmoChanged(_currentAmmo, _reserveAmmo);

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