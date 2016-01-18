using UnityEngine;
using UnityEngine.UI;

public class MissionPanelController : MonoBehaviour
{
    public GameObject titleObject;
    public GameObject descriptionObject;

    private Text titleText;
    private Text descriptionText;

    void Awake()
    {
        titleText = titleObject.transform.GetChild(1).GetComponent<Text>();
        descriptionText = descriptionObject.GetComponentInChildren<Text>();
    }

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }
}