using UnityEngine;
using UnityEngine.SceneManagement;

namespace Select
{
    public class GUIManager : MonoBehaviour
    {
        public Button[] buttons;

        [SerializeField] string _sceneName;


        private void Start()
        {
            foreach (var button in buttons)
            {
                button.onClick += ChangeToSelectScene;
            }
        }

        private void OnDestroy()
        {
            foreach (var button in buttons)
            {
                button.onClick -= ChangeToSelectScene;
            }
        }

        // 1. 기본적인 씬 전환 (씬 이름으로)
        private void ChangeToSelectScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}