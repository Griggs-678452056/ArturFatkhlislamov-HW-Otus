using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public sealed class Gun : Weapon
    {
        [Header("Bullet")]
        [SerializeField] private Bullet _bulletPrefab;

        [Header("Burst Mode")]
        [SerializeField] private bool _useBurstMode = false;
        [SerializeField] private int _burstCount = 3;
        [SerializeField] private float _burstDelay = 0.3f;

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shootSound;

        private Transform _bulletRoot;
        private Bullet[] _bullets;

        private bool _isBurstShooting;

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

        private int ClipSize
        {
            get
            {
                return _weaponConfig.ClipSize;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _bulletRoot = new GameObject("Bullet root").transform;

            _currentAmmo = ClipSize;
            _reserveAmmo = ClipSize * 4;

            RechargePool();

            NotifyAmmoChanged(_currentAmmo, _reserveAmmo);
        }

        public override void Fire()
        {
            if (CanShoot == false || _currentAmmo <= 0)
            {
                return;
            }

            if (_useBurstMode)
            {
                if (_isBurstShooting == false)
                {
                    StartCoroutine(BurstCoroutine());
                }
            }
            else
            {
                ShootSingle();
            }
        }

        private IEnumerator BurstCoroutine()
        {
            _isBurstShooting = true;

            for (int i = 0; i < _burstCount; i++)
            {
                if (_currentAmmo <= 0)
                {
                    yield break;
                }

                if (TryGetBullet(out Bullet bullet))
                {
                    _currentAmmo--;

                    bullet.Run(_barrel.forward * _force, _barrel.position);
                    PlayShootSound();
                    LastShootTime = 0.0f;

                    NotifyAmmoChanged(_currentAmmo, _reserveAmmo);
                }

                yield return new WaitForSeconds(_burstDelay);
            }

            _isBurstShooting = false;
        }

        private void ShootSingle()
        {
            if (_currentAmmo <= 0)
            {
                return;
            }

            if (TryGetBullet(out Bullet bullet))
            {
                _currentAmmo--;

                bullet.Run(_barrel.forward * _force, _barrel.position);
                PlayShootSound();
                LastShootTime = 0.0f;

                NotifyAmmoChanged(_currentAmmo, _reserveAmmo);
            }
        }

        public override void Recharge()
        {
            if (_reserveAmmo <= 0)
            {
                return;
            }

            int neededAmmo = ClipSize - _currentAmmo;
            int ammoToLoad = Mathf.Min(neededAmmo, _reserveAmmo);

            _currentAmmo += ammoToLoad;
            _reserveAmmo -= ammoToLoad;

            for (int i = 0; i < _bullets.Length; i++)
            {
                if (_bullets[i] != null)
                {
                    _bullets[i].gameObject.SetActive(true);
                    _bullets[i].Sleep();
                }
            }

            NotifyAmmoChanged(_currentAmmo, _reserveAmmo);
        }

        private void RechargePool()
        {
            _bullets = new Bullet[ClipSize * 2];
            for (int i = 0; i < ClipSize; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bullets[i] = bullet;
            }
        }

        private bool TryGetBullet(out Bullet result)
        {
            result = null;

            if (_bullets == null)
            {
                return false;
            }

            for (int i = 0; i < _bullets.Length; i++)
            {
                Bullet bullet = _bullets[i];
                if (bullet == null)
                    continue;

                if (!bullet.IsActive)
                {
                    result = bullet;
                    return true;
                }
            }

            return false;
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