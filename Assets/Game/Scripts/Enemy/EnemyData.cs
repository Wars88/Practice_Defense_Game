﻿using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data", order = 1)]
public class  EnemyData : ScriptableObject
{
    public GameObject Prefab;
    public float Speed;
    public int MaxHealth;
    public int Damage;
    public int SpawnCount;
}
