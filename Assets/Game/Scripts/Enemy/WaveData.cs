// ���ο� ���̺� ������
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Game/Wave Data")]
public class WaveData : ScriptableObject
{
    public SpawnInfo[] EnemiesToSpawn;  // ���� ���� ���� �迭
    public float WaveStartDelay;         // ���̺� ���� �� ���ð�
}



