using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource uisfxAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource levelSfxAudioSource;
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayUISfx()
    {
        uisfxAudioSource.Play();
    }

    public void PlayPlayerSfx(AudioClip clip)
    {
        playerAudioSource.clip = clip;
        playerAudioSource.Play();
    }

    public void PlayLevelSfx(AudioClip clip)
    {
        levelSfxAudioSource.clip = clip;
        levelSfxAudioSource.Play();
    }

    public void PlayEnemySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
