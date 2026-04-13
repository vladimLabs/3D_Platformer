using UnityEngine;

public class ActivationButton : MonoBehaviour
{
    [SerializeField] private Animator target;
    [SerializeField] private bool enable;
    [SerializeField] private AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.SetTrigger("Open");
            AudioManager.Instance.PlayLevelSfx(sound);
        }
    }
}
