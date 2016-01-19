using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Colony.UI
{
    public class EndGamePanelController : MonoBehaviour
    {
        private Text messageBox;

        void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.name == "Message")
                {
                    messageBox = child.GetComponent<Text>();
                }
            }
            Time.timeScale = 0.0f;
        }

        public void SetMessage(string message)
        {
            messageBox.text = message;
        }

        public void BackToMenu()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("menu");
        }
    }
}