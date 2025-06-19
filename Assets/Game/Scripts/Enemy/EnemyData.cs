using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data", order = 1)]
public class  EnemyData : ScriptableObject
{
    public GameObject Prefab;
    public float Speed;
    public int MaxHealth;
    public int DeadMoney;
}

// 새로운 웨이브 데이터
[System.Serializable]
public class SpawnInfo
{
    public EnemyData EnemyData;  // 어떤 몬스터를
    public int SpawnCount;           // 몇 마리 스폰할지
    public float SpawnDelay;         // 각 몬스터 사이의 딜레이
}
