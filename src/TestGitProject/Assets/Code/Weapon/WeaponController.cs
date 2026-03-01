using System;
using UnityEngine;

namespace Code
{
    public class WeaponController : MonoBehaviour
    {
        private WeaponSelector _weaponSelector;

        private void Start()
        {
            Weapon[] weapons = GetComponentsInChildren<Weapon>(true);
            _weaponSelector = new WeaponSelector(weapons);
            _weaponSelector.Select(0);

        }

        private void Update()
        {
            SelectWeapon();

            if (Input.GetMouseButton(0))
            {
                _weaponSelector.Fire();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _weaponSelector.Recharge();
            }
        }

        private void SelectWeapon()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            if (scrollWheel >= 0.1f)
            {
                _weaponSelector.Next();
            }

            if (scrollWheel <= -0.1f)
            {
                _weaponSelector.Preview();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _weaponSelector.Select(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _weaponSelector.Select(1);
            }
        }
    }
}
