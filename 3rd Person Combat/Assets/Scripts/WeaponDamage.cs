using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{   
    [SerializeField] private Collider playerCollider;
    private float damage;
    private float knockback;
    private List<Collider> alreadyCollidedWith =  new List<Collider>(); 
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider) { return; }

        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - playerCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }
    public void SetAttack(float damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
