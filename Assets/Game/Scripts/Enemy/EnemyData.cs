using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data", order = 1)]
public class  EnemyData : ScriptableObject
{
    public GameObject Prefab;
    public float Speed;
    public int MaxHealth;
    public int SpawnCount;
    public int DeadMoney;
    public float SpawnTime = 2.5f; // 소환 시간, 필요시 조정 가능
}
