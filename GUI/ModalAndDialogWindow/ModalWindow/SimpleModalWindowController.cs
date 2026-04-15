using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleModalWindowController : Singleton<SimpleModalWindowController>
{
    [SerializeField] GameObject ModalWindow; 
    [SerializeField] Transform headerTransform;
    [SerializeField] TextMeshProUGUI headerText;
    // [SerializeField] Image headerIcon;

    [Space()]
    [Header("Content")]
    [SerializeField] Transform contentTransform;
    [Space()]
    [SerializeField] TextMeshProUGUI contentText;



    [Space()]
    [Header("Footer")]

    [SerializeField] Transform footerTransform;
    // [SerializeField] Button confirmButton;
    // [SerializeField] Button declineButton;
    // [SerializeField] Button otherButton;

    public void ShowModalWindow(string title, string content)
    {
        headerText.text = title; 
        contentText.text = content; 
        ModalWindow.SetActive(true); 
    }
}

