using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private float multiplior = 1.0f;
    [SerializeField] private float cooldownSeconds = 0.05f;
    [SerializeField] private AudioClip saund;

    private float _cooldown;

    private void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_cooldown <= 0)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController player))
            //if (collision.gameObject.GetComponent<PlayerController>() != null) 
            {
                Vector3 vRel = collision.relativeVelocity;
                vRel.y = 0f;
                Vector3 deltaV = (-2f * vRel) * multiplior;
                player.AddKnockbackDeltaV(deltaV);
                _cooldown = cooldownSeconds;
                AudioManager.Instance.PlayLevelSfx(saund);
            }
        }
    }



    //[SerializeField] private float bounceMultiplier;
    //[SerializeField] private float minHitSpeed;

    //private Rigidbody _rb;

    //private void Awake()
    //{
    //    _rb = GetComponent<Rigidbody>();
    //}

    //private void OnCollisionEnter(Collision other)
    //{
    //    PerformBounce(other);
    //}

    //private void PerformBounce(Collision other)
    //{
    //    Rigidbody target = other.rigidbody;
    //    if (target == null) return;

    //    ContactPoint cp = other.GetContact(0);
    //    Vector3 normal = cp.normal.normalized;
    //    Vector3 normalSpeed = target.linearVelocity;
    //    float speedIntoSurface = Vector3.Dot(normalSpeed, -normal);
    //    if (speedIntoSurface < minHitSpeed) return;
    //    float e = bounceMultiplier;
    //    Vector3 deltaV = (1 + e) * speedIntoSurface * normal;
    //    Vector3 impulse = target.mass * deltaV;
    //    Debug.Log(impulse);
    //    target.AddForce(impulse, ForceMode.Impulse);
    //}
}
