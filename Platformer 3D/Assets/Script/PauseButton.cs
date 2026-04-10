using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ShowPausePanel);
    }

    private void ShowPausePanel()
    {
        AudioManager.Instance.PlayUISfx();
        pausePanel.SetActive(true);
    }
}
