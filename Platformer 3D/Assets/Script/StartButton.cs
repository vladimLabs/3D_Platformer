using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    //[SerializeField] private AudioSource audioSource;
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        AudioManager.Instance.PlayUISfx();
        //audioSource.Play();
        SceneManager.LoadScene(sceneToLoad);
    }
}
