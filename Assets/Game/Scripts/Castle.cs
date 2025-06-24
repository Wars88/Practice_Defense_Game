using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour
{
    public int MaxHealth = 9;

    public UnityAction _onTakeDamaged;

    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;

        _onTakeDamaged += HpUIUpdate;
        HpUIUpdate();
    }

    private void OnDestroy()
    {
        _onTakeDamaged -= HpUIUpdate;
    }

    public void TakeDamage()
    {
        CurrentHealth--;
        _onTakeDamaged?.Invoke();
        AudioManager.Instance.PlaySoundEffect("CastleHit");

        if (CurrentHealth <= 0)
        {
            CastleDestroy();
        }
    }

    private void CastleDestroy()
    {
        GameManager.Instance.IsGameOver = true;
        GameManager.Instance.OnGameOver?.Invoke();

        AudioManager.Instance.PlaySoundEffect("GameOver");
        Destroy(gameObject);
    }

    private void HpUIUpdate()
    {
        GUIManager.Instance.HpText.text = $"HP: {CurrentHealth}";
    }
}