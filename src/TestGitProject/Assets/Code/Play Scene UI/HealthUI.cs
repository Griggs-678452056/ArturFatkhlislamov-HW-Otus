using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged += UpdateHealthBar;
        }
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }

    private void Start()
    {
        if (_playerHealth != null)
        {
            UpdateHealthBar(_playerHealth.CurrentHP, _playerHealth.MaxHP);
        }
    }

    private void UpdateHealthBar(float current, float max)
    {
        if (_healthBar == null)
        {
            return;
        }
        _healthBar.maxValue = max;
        _healthBar.value = current;
    }
}
