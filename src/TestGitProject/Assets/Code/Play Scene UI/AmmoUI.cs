using System;
using TMPro;
using UnityEngine;

namespace Code
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammo;
        [SerializeField] private WeaponController _weaponController;

        private Weapon _currentWeapon;

        private void OnEnable()
        {
            _weaponController.OnWeaponChanged += HandleWeaponChanged;
        }

        private void OnDisable()
        {
            _weaponController.OnWeaponChanged -= HandleWeaponChanged;

            UnsubscribeFromWeapon();
        }

        private void HandleWeaponChanged(Weapon weapon)
        {
            UnsubscribeFromWeapon();

            _currentWeapon = weapon;

            if (_currentWeapon != null)
            {
                _currentWeapon.OnAmmoChanged += UpdateAmmo;

                UpdateAmmoImmediately();
            }
        }               

        private void UnsubscribeFromWeapon()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.OnAmmoChanged -= UpdateAmmo;

                _currentWeapon = null;
            }
        }

        private void UpdateAmmoImmediately()
        {
            UpdateAmmo(_currentWeapon.CurrentAmmo, _currentWeapon.ReserveAmmo);
        }
        
        public void UpdateAmmo(int currentAmmo, int reserveAmmo)
        {
            _ammo.text = $"{currentAmmo} / {reserveAmmo}";
        }
    }
}
