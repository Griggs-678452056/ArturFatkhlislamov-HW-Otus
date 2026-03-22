using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHP = 100f;
    private float _currentHP;

    public float CurrentHP
    {
        get
        {
            return _currentHP;
        }
    }

    public float MaxHP
    {
        get
        {
            return _maxHP;
        }
    }

    public event Action<float, float> OnHealthChanged;

    private void Awake()
    {
        _currentHP = _maxHP;
        NotifyHealthChanged();
    }

    public bool CanTakeDamage(float damage)
    {
        return _currentHP - damage <= 0f;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0f)
        {
            _currentHP = 0f;
        }

        NotifyHealthChanged();
    }

    public void Heal(float heal)
    {
        _currentHP += heal;

        if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }

        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }
}
