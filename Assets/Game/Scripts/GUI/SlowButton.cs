using UnityEngine;
using UnityEngine.Events;

public class SlowButton : Button
{
    public UnityAction<GameObject> OnClick;
    public GameObject TowerPrefab;

    protected override void Awake()
    {
        base.Awake();
        onClick = OnSlowButtonClicked;
    }

    private void OnDestroy()
    {
        onClick -= OnSlowButtonClicked;
    }

    private void OnSlowButtonClicked()
    {
        OnClick?.Invoke(TowerPrefab);
    }
}