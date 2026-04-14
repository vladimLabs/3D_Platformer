using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private ReplayManager replayManager;
    public void StartRecording()
    {
        replayManager.StartRecording();
    }

    public void StopRecording()
    {
        replayManager.StopRecording();
    }
}
