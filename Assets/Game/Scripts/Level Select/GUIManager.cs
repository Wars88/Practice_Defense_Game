using UnityEngine;
using UnityEngine.SceneManagement;

namespace Select
{
    public class GUIManager : MonoBehaviour
    {
        public Button HomeButton;

        public void OnHomeButtonClicked()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}