using TMPro;
using UnityEngine;

namespace Code
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammo;
        [SerializeField] private Gun _gun;
        [SerializeField] private Bazooka _bazooka;

        private void OnEnable()
        {
            if (_gun != null)
            {
                _gun.OnAmmoChanged += UpdateAmmo;
            }

            if (_bazooka != null)
            {
                _bazooka.OnAmmoChanged += UpdateAmmo;
            }
        }

        private void OnDisable()
        {
            if (_gun != null)
            {
                _gun.OnAmmoChanged -= UpdateAmmo;
            }

            if (_bazooka != null)
            {
                _bazooka.OnAmmoChanged -= UpdateAmmo;
            }
        }

        public void UpdateAmmo(int currentAmmo, int reserveAmmo)
        {
            _ammo.text = $"{currentAmmo} / {reserveAmmo}";
        }
    }
}
