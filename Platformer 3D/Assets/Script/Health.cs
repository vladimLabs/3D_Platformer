using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    //[SerializeField] private bool isPlayer;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private AudioClip hurtSound;
    private float _currHp;

    private void Awake()
    {
        _currHp = maxHp;
    }

    public void GetDamage(float damage)
    { 
        _currHp -= damage;
        DrawHp();
        TryDie();
        AudioManager.Instance.PlayPlayerSfx(hurtSound);
    }

    private void TryDie()
    {
        if (_currHp <= 0)
        {
            if (gameObject.CompareTag("Player")) //isPlayer
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //reload level;
            }
            else 
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void DrawHp()
    {
        hpSlider.value = _currHp / maxHp;
    }
}
