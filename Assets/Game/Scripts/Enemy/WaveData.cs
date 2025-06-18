// 새로운 웨이브 데이터
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Game/Wave Data")]
public class WaveData : ScriptableObject
{
    public SpawnInfo[] EnemiesToSpawn;  // 여러 몬스터 정보 배열
    public float WaveStartDelay;         // 웨이브 시작 전 대기시간
}



