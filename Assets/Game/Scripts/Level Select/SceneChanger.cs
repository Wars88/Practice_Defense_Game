using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Button NewGameButton;
    public Button ContinueButton;
    public string _sceneName;

    private void Start()
    {
        NewGameButton.onClick += ChangeToSelectScene;
        ContinueButton.onClick += ChangeToSelectScene;
    }

    private void OnDestroy()
    {
        NewGameButton.onClick -= ChangeToSelectScene;
        ContinueButton.onClick -= ChangeToSelectScene;
    }

    // 1. 기본적인 씬 전환 (씬 이름으로)
    private void ChangeToSelectScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}