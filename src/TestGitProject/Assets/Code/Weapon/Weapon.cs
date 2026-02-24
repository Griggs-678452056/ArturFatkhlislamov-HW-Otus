using UnityEngine;
using System.Collections;

namespace Code
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _countClip;
        [SerializeField] private float _force;
        [SerializeField] private float _shotDelay;

        private Transform _bulletRoot;
        private Bullet[] _bullets;

        private bool _canShoot;
        private float _lastShootTime;


        private void Start()
        {
            _bulletRoot = new GameObject("Bullet root").transform;
            Recharge();
        }

        private void Update()
        {
            _canShoot = _shotDelay <= _lastShootTime;

            if (_canShoot)
            {
                return;
            }

            _lastShootTime += Time.deltaTime;
        }

        public void Fire()
        {
            if (_canShoot == false)
            {
                return;
            }

            if (TryGetBullet(out Bullet bullet))
            {
                bullet.Run(_barrel.forward * _force, _barrel.position);
                _lastShootTime = 0.0f;                
            }
        }

        public void Recharge()
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
    }
}
