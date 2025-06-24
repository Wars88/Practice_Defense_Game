using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] LayerMask _placementLayer;
    private bool _isPlacing = false;
    private Dictionary<Vector3, GameObject> _gridTowers = new Dictionary<Vector3, GameObject>();

    public GameObject _selectedTower { get; private set; }

    public GameObject CurrentTower;
    [SerializeField] GameObject _priviousTower;

    private Vector3 _location;

    private int _gridSize = 2;
    public bool IsClicked = false;

    private void Update()
    {
        // 타워버튼을 누른 미리보기 상태일 때
        if (_isPlacing)
        {
            // 현재 타일에 타워가 있는지 확인.
            if (_gridTowers.TryGetValue(GetGridPosition(), out GameObject tower))
                return;
            else
                _selectedTower.transform.position = GetGridPosition();

            if (Input.GetMouseButtonDown(0))
            {
                _isPlacing = false;

                var towerClass = _selectedTower.GetComponent<Tower>();
                towerClass.enabled = true;
                towerClass.PlaceTower();

                _gridTowers.Add(GetGridPosition(), _selectedTower);
                Debug.Log($"타워 배치: {GetGridPosition()}");
                _selectedTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(false);
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

            _selectedTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
            _selectedTower.GetComponent<Tower>().enabled = false;
            _selectedTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
        
    }

    public void TowerSelect(Vector3 key)
    {
        if (_gridTowers.TryGetValue(key, out GameObject tower))
        {
            GUIManager.Instance.ShowUpgradePannel();

            if (CurrentTower != null)
            {
                _priviousTower = CurrentTower;
                _priviousTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(false);

                CurrentTower = tower;
                CurrentTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(true);
            }
            else
            {
                CurrentTower = tower;
                CurrentTower.GetComponentInChildren<TowerRing>(true).gameObject.SetActive(true);
            }

            var towerClass = CurrentTower.GetComponent<Tower>();

            GUIManager.Instance.UpgradeText.text = $"Up: {towerClass.Cost}";
            GUIManager.Instance.SellText.text = $"Sell: {towerClass.Cost / 2}";
        }
    }

    public void TowerUpgrade()
    {
        if (CurrentTower != null)
        {
            var tower = CurrentTower.GetComponent<Tower>();
            if (tower.PossibleToTower())
            {
                bool canUpgrade = tower.Upgrade();

                if (canUpgrade)
                {
                    GameManager.Instance.MoneyManager.SpendMoney(tower.TowerName);
                    AudioManager.Instance.PlaySoundEffect("TowerUpgrade");
                }
                else
                {
                    AudioManager.Instance.PlaySoundEffect("CastleHit");
                }
            }
            else
            {
                AudioManager.Instance.PlaySoundEffect("CastleHit");
            }
        }
    }

    public void TowerDelete()
    {
        if (CurrentTower != null)
        {
            _gridTowers.Remove(CurrentTower.transform.position);

            int resellCost = CurrentTower.GetComponent<Tower>().Cost / 2;
            GameManager.Instance.MoneyManager.GetMoney(resellCost);
            AudioManager.Instance.PlaySoundEffect("TowerSell");

            Destroy(CurrentTower);
        }
    }
}