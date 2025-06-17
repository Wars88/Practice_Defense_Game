using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("�������� ��ȣ")]
    public int stageNumber; // �������� ��ȣ

    [Header("���̺� ������")]
    public EnemyData[] enemyData; // �� ������ �迭
}