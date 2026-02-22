using UnityEngine;
using System.Collections;

namespace Code
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Magazine _magazine;

        [SerializeField] private int _countClip;
        [SerializeField] private float _force;
        [SerializeField] private float _shotDelay;

        private bool _canShoot;
        private float _lastShootTime;


        private void Start()
        {
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

            if (_lastShootTime >= _shotDelay)
            {
                _canShoot = true;
            }
        }

        public void Fire()
        {
            if (!_canShoot)
            {
                return;
            }

            if (TryGetBullet(out Bullet bullet))
            {
                bullet.Run(_barrel.forward * _force, _barrel.position);
                _lastShootTime = 0.0f;
                _canShoot = false;
            }
        }
                
        public void Recharge()
        {
            if (IsAnyActiveBullet())
            {
                return;
            }
            _magazine.Bullets = new Bullet[_countClip];
            for (int i = 0; i < _countClip; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _magazine.transform);
                bullet.gameObject.SetActive(false);
                bullet.Sleep();
                _magazine.Bullets[i] = bullet;
            }
        }

        private bool IsAnyActiveBullet()
        {
            if(_magazine.Bullets == null)
            {
                return false;
            }

            for (int i = 0; i < _countClip; i++)
            {
                Bullet bullet = _magazine.Bullets[i];

                if (bullet == null)
                {
                    continue;
                }

                if (bullet.gameObject.activeSelf)
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

            if (_magazine.Bullets == null)
            {
                return false;
            }

            for (int i = 0; i < _magazine.Bullets.Length; i++)
            {
                Bullet bullet = _magazine.Bullets[i];
                if (bullet == null)
                {
                    continue;
                }

                if (bullet.gameObject.activeSelf)
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

            result = _magazine.Bullets[candidate];
            return true;
        }
    }
}
