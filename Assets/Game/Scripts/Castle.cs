using UnityEngine;
using UnityEngine.Events;

public class Castle : MonoBehaviour
{
    public int MaxHealth = 30;

    public System.Action _onDestroyed;
    public UnityAction _onTakeDamaged;

    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        _onDestroyed += CastleDestroy;
        _onTakeDamaged += HpUIUpdate;
    }

    private void OnDestroy()
    {
        _onDestroyed -= CastleDestroy;
        _onTakeDamaged -= HpUIUpdate;
    }

    public void TakeDamage()
    {
        CurrentHealth--;
        _onTakeDamaged?.Invoke();

        if (CurrentHealth <= 0)
        {
            _onDestroyed?.Invoke();
        }
    }

    private void CastleDestroy()
    {
        Destroy(gameObject);
    }

    private void HpUIUpdate()
    {
        GUIManager.Instance.HpText.text = $"HP: {CurrentHealth}";
    }
}