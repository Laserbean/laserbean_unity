using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.CustomGUI.DialogBoxes;
using Laserbean.CustomGUI.ModalWindow;
using UnityEngine;

using UnityEngine.InputSystem;

public class GenericUIController : Singleton<GenericUIController>
{
    [field: SerializeField] public ModalWindowController ModalWindow { get; internal set; }
    [field: SerializeField] public VisualNovelDialogWindowController DialogWindow { get; internal set; }

    [SerializeField] InputActionReference inputActionReference;

    private void OnEnable()
    {
        if (inputActionReference == null) return;
        inputActionReference.action.performed += OnInputAction;
        inputActionReference.action.Enable();
    }

    private void OnDisable()
    {
        if (inputActionReference == null) return;
        inputActionReference.action.performed -= OnInputAction;
        inputActionReference.action.Disable();
    }

    void OnInputAction(InputAction.CallbackContext context)
    {
        Debug.Log("Fish"); 
    }


    public ModalWindowBuilder ModalWindowBuilder()
    {
        return new ModalWindowBuilder(ModalWindow);
    }

    public VN_Dialog_Builder DialogWindowBuilder()
    {
        return new VN_Dialog_Builder(DialogWindow);
    }

}

