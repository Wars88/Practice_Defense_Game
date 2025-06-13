using UnityEngine;
using UnityEngine.UI;

public class ScrollerController : MonoBehaviour
{
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] float _scrollSpeed;

    private void Update()
    {
        MoveScroll();
    }

    private void MoveScroll()
    {
        if (_scrollRect == null)
            return;

        float value = Input.mousePosition.x / Screen.width;
        float currentPosition = _scrollRect.horizontalNormalizedPosition;
        float nextPosition = currentPosition;

        if (0.9f < value)
            nextPosition = _scrollRect.horizontalNormalizedPosition + (Time.deltaTime * _scrollSpeed);
        else if (0.75f < value)
            nextPosition = _scrollRect.horizontalNormalizedPosition + (Time.deltaTime * _scrollSpeed / 2);
        else if (value < 0.1f)
            nextPosition = _scrollRect.horizontalNormalizedPosition - (Time.deltaTime * _scrollSpeed);
        else if (value < 0.35f)
            nextPosition = _scrollRect.horizontalNormalizedPosition - (Time.deltaTime * _scrollSpeed / 2);
        
       
        Mathf.Clamp01(nextPosition);
        _scrollRect.horizontalNormalizedPosition = nextPosition;
    }
}