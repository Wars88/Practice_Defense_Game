using UnityEngine.SceneManagement;

public class LevelSelectButton : Button
{
    public int LevelIndex;
    protected override void Awake()
    {
        base.Awake();
        onClick += OnLevelSelected;
    }

    private void OnDestroy()
    {
        onClick -= OnLevelSelected;
    }

    private void OnLevelSelected()
    {
        StageManager.Instance.CurrentStageIndex = LevelIndex;

        SceneManager.LoadScene("Stage");
    }
}