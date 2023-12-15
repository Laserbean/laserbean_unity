using System.Collections;
using System.Collections.Generic;
using Laserbean.CustomGUI.DialogBoxes;
using Laserbean.CustomGUI.ModalWindow;
using UnityEngine;

public class GenericUIController : Singleton<GenericUIController>
{
    [field: SerializeField] public ModalWindowController ModalWindow { get; internal set; }
    [field: SerializeField] public VisualNovelDialogWindowController DialogWindow { get; internal set; }
}
