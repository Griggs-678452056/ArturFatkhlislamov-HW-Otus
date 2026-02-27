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

        protected bool CanShoot { get; private set; }
        public float LastShootTime { get; protected set; }

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
    }
}
