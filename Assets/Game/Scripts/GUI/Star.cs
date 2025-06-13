using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image[] stars;

    private void Awake()
    {
        stars = GetComponentsInChildren<Image>().OrderBy(star => star.transform.GetSiblingIndex()).ToArray();
    }

    public void SetStar(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].gameObject.SetActive(true);
        }

        for (int i = starCount; i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(false); // 남은 별은 비활성화합니다.
        }
    }

}