using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Laserbean.CustomGUI.ModalWindow;

public class TestShowModalWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        new ModalWindowBuilder(GenericUIController.Instance.ModalWindow)
        .SetTitle("Fish")

        .SetDeclineButtonClose()


        .Show();


        
    }

    
}
