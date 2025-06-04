using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] LayerMask _placementLayer;
    private bool _isPlacing = false;
    private GameObject _currentTower;

    private int _gridSize = 2;

    private void Update()
    {
        if (_isPlacing)

    }

    public Vector3 GetGridPosition()
    {
        Vector3 location = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200f, _placementLayer))
        {
            location = hit.point;
        }

        location.x = MakeOdd(location.x);
        location.y = 0f;
        location.z = MakeOdd(location.z);

        return location;
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
        _isPlacing = true;
        _currentTower = tower;
    }
}