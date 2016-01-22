using UnityEngine;
using UnityEngine.UI;

namespace Colony.UI
{
    public class ForceCreatePanelController : MonoBehaviour
    {
        public GameObject counterPanel;

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

        public void OnButtonClick()
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }
}