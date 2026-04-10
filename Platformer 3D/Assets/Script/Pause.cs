using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnEnable()
    {
        player.SetPauseState(true);
    }

    private void OnDisable()
    {
        player.SetPauseState(false);
    }
}
