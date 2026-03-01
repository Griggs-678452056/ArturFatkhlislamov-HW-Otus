using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public sealed class Gun : Weapon
    {
        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private bool _useBurstMode = false;
        [SerializeField] private int _burstCount = 3;
        [SerializeField] private float _burstDelay = 0.3f;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shootSound;

        private Transform _bulletRoot;
        private Bullet[] _bullets;

        private bool _isBurstShooting;

        private void Start()
        {
            _bulletRoot = new GameObject("Bullet root").transform;
            Recharge();
        }

        public override void Fire()
        {
            if (CanShoot == false)
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
                if (TryGetBullet(out Bullet bullet))
                {
                    bullet.Run(_barrel.forward * _force, _barrel.position);
                    PlayShootSound();
                    LastShootTime = 0.0f;
                }
                                
                yield return new WaitForSeconds(_burstDelay);                
            }

            _isBurstShooting = false;
        }

        private void ShootSingle()
        {
            if (TryGetBullet(out Bullet bullet))
            {
                bullet.Run(_barrel.forward * _force, _barrel.position);
                PlayShootSound();
                LastShootTime = 0.0f;
            }
        }

        public override void Recharge()
        {
            if (IsAnyActiveBullet())
            {
                return;
            }
            _bullets = new Bullet[_countClip];
            for (int i = 0; i < _countClip; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bullets[i] = bullet;
            }
        }

        private bool IsAnyActiveBullet()
        {
            if (_bullets == null)
            {
                return false;
            }

            for (int i = 0; i < _countClip; i++)
            {
                Bullet bullet = _bullets[i];

                if (bullet == null)
                {
                    continue;
                }

                if (bullet.IsActive)
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryGetBullet(out Bullet result)
        {
            int candidate = -1;
            result = default;

            if (_bullets == null)
            {
                return false;
            }

            for (int i = 0; i < _bullets.Length; i++)
            {
                Bullet bullet = _bullets[i];
                if (bullet == null)
                {
                    continue;
                }

                if (bullet.IsActive)
                {
                    continue;
                }

                candidate = i;
                break;
            }

            if (candidate == -1)
            {
                return false;
            }

            result = _bullets[candidate];
            return true;
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