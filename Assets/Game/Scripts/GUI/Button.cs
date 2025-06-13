using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityAction onClick; // UnityAction을 사용하여 클릭 이벤트를 처리합니다.

    protected virtual void Awake()
    {
        var button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnButtonClicked); // 버튼 클릭 시 onClick 이벤트를 호출합니다.
    }

    private void OnButtonClicked()
    {
        onClick?.Invoke();
    }

}