using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("스테이지 번호")]
    public int StageNumber; // 스테이지 번호

    [Header("웨이브 데이터")]
    public WaveData[] WaveData; // 적 데이터 배열
}