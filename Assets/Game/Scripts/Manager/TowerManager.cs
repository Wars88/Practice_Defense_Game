using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] LayerMask _placementLayer;
    private bool _isPlacing = false;
    private Dictionary<Vector3, GameObject> _gridTowers = new Dictionary<Vector3, GameObject>();

    private GameObject _currentTower;
    private Vector3 _location;

    private int _gridSize = 2;

    private void Update()
    {
        // 타워버튼을 누른 미리보기 상태일 때
        if (_isPlacing)
        {
            // 현재 타일에 타워가 있는지 확인.
            if (_gridTowers.TryGetValue(GetGridPosition(), out GameObject tower))
                return;
            else
                _currentTower.transform.position = GetGridPosition();

            if (Input.GetMouseButtonDown(0))
            {
                _isPlacing = false;

                var towerClass = _currentTower.GetComponent<Tower>();
                towerClass.enabled = true;
                towerClass.PlaceTower();

                _gridTowers.Add(GetGridPosition(), _currentTower);

                var slowComponent = _currentTower.GetComponent<Slow>();
                if (slowComponent == null)
                    _currentTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(false);
            }
        }
    }

    public Vector3 GetGridPosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200f, _placementLayer))
        {
            _location = hit.point;
        }

        _location.x = MakeOdd(_location.x);
        _location.y = 0f;
        _location.z = MakeOdd(_location.z);

        return _location;
    }    

    private int MakeOdd(float value)
    {
        int result = Mathf.RoundToInt(value);

        if (result % _gridSize == 0)
        {
            if (result > value)
                result -= 1; // Make it odd
            else
                result += 1; // Make it odd
        }
        return result;
    }

    public void TowerPlacement(GameObject tower)
    {
        if (tower.GetComponent<Tower>().PossibleToTower())
        {
            _isPlacing = true;

            _currentTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
            _currentTower.GetComponent<Tower>().enabled = false;
            _currentTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
        
    }
}