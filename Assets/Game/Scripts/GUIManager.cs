using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public TowerButton towerButton;

    private void Awake()
    {
        towerButton = GetComponentInChildren<TowerButton>();
    }

}