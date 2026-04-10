using UnityEngine;

public class ActivationButton : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool enable;
    [SerializeField] private AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.SetActive(enable);
            AudioManager.Instance.PlayLevelSfx(sound);
        }
    }
}
