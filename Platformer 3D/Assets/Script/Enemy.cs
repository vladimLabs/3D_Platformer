using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float coolDown;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float damage;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource audioSource;

    private bool _isPlayerInRange;
    private float _coolDownTimer;

    private void Shoot()
    {
        firePoint.LookAt(player);
        var newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        AudioManager.Instance.PlayEnemySound(audioSource, shootSound);
        if (newBullet.TryGetComponent(out Bullet bullet))
        {
            bullet.Init(damage);
        }
    }

    private void Update()
    {
        if (_isPlayerInRange)
        {
            if (_coolDownTimer > 0)
            {
                _coolDownTimer -= Time.deltaTime;
            }
            else
            {
                Shoot();
                _coolDownTimer = coolDown;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            _coolDownTimer = coolDown;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }
}
