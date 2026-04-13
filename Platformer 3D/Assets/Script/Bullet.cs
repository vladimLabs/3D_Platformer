using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    private Rigidbody _rb;
    private float _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, lifetime);
    }

    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                health.GetDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
