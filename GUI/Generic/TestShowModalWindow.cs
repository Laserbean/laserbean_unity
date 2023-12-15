using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Laserbean.CustomGUI.ModalWindow;
using Laserbean.CustomGUI.DialogBoxes;

public class TestShowModalWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowDialog();

    }
    void ShowFirstWindow()
    {
        new ModalWindowBuilder(GenericUIController.Instance.ModalWindow)
        .SetTitle("Fish")
        .SetHorizontalText("AAAAAAA1111")
        .SetOtherButton("Continue?", ShowNextWindow)
        .Show();

    }
    void ShowNextWindow()
    {
        new ModalWindowBuilder(GenericUIController.Instance.ModalWindow)
        .SetTitle("Fish")
        .SetHorizontalText("BBBB")
        .SetDeclineButtonClose()
        .AddDeclineAction(ShowDialog)
        .SetConfirmButton("Back?", ShowFirstWindow)
        .Show();
    }

    [TextArea(10, 10)]
    [SerializeField] string DialogText = "Hello. *s*I am a <b>slow boi</b>. *f*Now i should be fast.";

    [EasyButtons.Button]
    void ShowDialog()
    {
        new VN_Dialog_Builder(GenericUIController.Instance.DialogWindow)
        .SetSpeakerName("fish")
        .SetDialogRaw(DialogText)   
        .SetContinueClose()
        .Show();

    }

}
