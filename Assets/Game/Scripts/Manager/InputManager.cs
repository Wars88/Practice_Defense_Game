using UnityEngine;

public class InputManager : MonoBehaviour
{
    private TowerManager _towerManager;

    private void Awake()
    {
        _towerManager = FindAnyObjectByType<TowerManager>();
    }

}