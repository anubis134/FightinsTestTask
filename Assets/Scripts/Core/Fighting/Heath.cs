using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heath : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private int _health;
    public int Health { get => _health; }
    private int _startHealth;


    void Awake() 
    {
        _health = GetComponent<Fighter>()._fighterData.Health;
        _startHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.fillAmount -= (1f / _startHealth) * damage;
        if (_health <= 0) 
        {
            _healthBar.transform.parent.gameObject.SetActive(false);
        }
    }
}
