using UnityEngine;

public class DamageButton : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out Health health))
            {
                health.GetDamage(damage);
            }
        }
    }
}