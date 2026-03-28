using System;
using UnityEngine;

namespace Code
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected WeaponConfig _weaponConfig;

        protected int _countClip;
        protected float _force;
        private float _shotDelay;

        public event Action<int, int> OnAmmoChanged;

        protected bool CanShoot { get; private set; }
        public float LastShootTime { get; protected set; }

        public abstract int CurrentAmmo { get; }
        public abstract int ReserveAmmo { get; }

        protected virtual void Awake()
        {
            _countClip = _weaponConfig.ClipSize;
            _force = _weaponConfig.Force;
            _shotDelay = _weaponConfig.ShotDelay;
        }

        private void Update()
        {
            CanShoot = _shotDelay <= LastShootTime;

            if (CanShoot)
            {
                return;
            }

            LastShootTime += Time.deltaTime;
        }

        public abstract void Fire();

        public abstract void Recharge();

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        protected void NotifyAmmoChanged(int currentAmmo, int reserveAmmo)
        {
            OnAmmoChanged?.Invoke(currentAmmo, reserveAmmo);
        }
    }
}
