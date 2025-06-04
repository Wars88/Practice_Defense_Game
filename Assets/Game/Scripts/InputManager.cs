using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GUIManager _guiManager;
    private TowerManager _towerManager;

    private void Awake()
    {
        _guiManager = FindAnyObjectByType<GUIManager>();
        _towerManager = FindAnyObjectByType<TowerManager>();
    }

    public void OnClick(GameObject tower)
    {
        _towerManager.TowerPlacement(tower);
    }


}