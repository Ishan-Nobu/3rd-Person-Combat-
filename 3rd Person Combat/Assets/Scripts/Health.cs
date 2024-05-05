using System;
using UnityEngine;

public class Health : MonoBehaviour
{   
    [SerializeField] private float maxHealth;
    private float health;
    private bool isInvulnerable;
    public bool IsDead => health == 0f;
    public event Action OnTakeDamage;
    public event Action OnDie;
    private void Awake()
    {
        health = maxHealth;
    }
    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
    public void DealDamage(float damage)
    {   
        if (health <= 0f)   { return; }
        if (isInvulnerable) { return; }

        health = Mathf.Max(health - damage, 0);
        OnTakeDamage?.Invoke();
        
        if (health <= 0f) 
        {   
            OnDie?.Invoke();
        }
        print(health);
    }
}
