using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject currentPanel;
    [SerializeField] private GameObject targetPanel;
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        AudioManager.Instance.PlayUISfx();
        currentPanel.SetActive(false);
        targetPanel.SetActive(true);
    }
}
