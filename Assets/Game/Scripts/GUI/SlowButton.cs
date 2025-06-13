using UnityEngine;
using UnityEngine.Events;

public class SlowButton : Button
{
    public UnityAction<GameObject> OnClick;
    public GameObject TowerPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnSlowButtonClicked()
    {

        if (OnClick != null)
        {
            Debug.Log($"OnClick 리스너 개수: {OnClick.GetInvocationList().Length}");
            //OnClick.Invoke(TowerPrefab);
        }
        else
            Debug.LogWarning("OnClick 이벤트가 설정되지 않았습니다.");
    }
}