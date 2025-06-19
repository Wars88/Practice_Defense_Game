using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("�������� ��ȣ")]
    public int StageNumber; // �������� ��ȣ

    [Header("���̺� ������")]
    public WaveData[] WaveData; // �� ������ �迭
}