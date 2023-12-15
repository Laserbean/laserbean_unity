using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Laserbean.CustomGUI.ModalWindow
{

    public class ModalWindowController : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] internal Transform headerTransform;
        [SerializeField] internal TextMeshProUGUI headerText;
        [SerializeField] internal Image headerIcon;

        [Space()]
        [Header("Content")]
        [SerializeField] internal Transform contentTransform;
        [Space()]
        [SerializeField] internal Transform verticalLayoutTransform;
        [SerializeField] internal TextMeshProUGUI verticalContentText;
        [SerializeField] internal Image verticalContentImage;
        [Space()]
        [SerializeField] internal Transform horizontalLayoutTransform;
        [SerializeField] internal TextMeshProUGUI horizontalContentText;
        [SerializeField] internal Image horizontalContentImage;


        [Space()]
        [Header("Footer")]

        [SerializeField] internal Transform footerTransform;
        [SerializeField] internal Button confirmButton;
        [SerializeField] internal Button declineButton;
        [SerializeField] internal Button otherButton;


        public event Action OnConfirmCallback;
        public event Action OnDeclineCallback;
        public event Action OnOtherCallback;

        public void OnConfirm() => OnConfirmCallback?.Invoke();
        public void OnDecline() => OnDeclineCallback?.Invoke();
        public void OnOther() => OnOtherCallback?.Invoke();


        void OnDisable()
        {
            DisableStuff();
        }

        void DisableStuff()
        {
            headerText.gameObject.SetActive(false);
            headerIcon.gameObject.SetActive(false);
            verticalContentText.gameObject.SetActive(false);
            verticalContentImage.gameObject.SetActive(false);
            horizontalContentText.gameObject.SetActive(false);
            horizontalContentImage.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
            otherButton.gameObject.SetActive(false);
        }

        // public void Disable() {

        // }

    }


    public class ModalWindowBuilder
    {
        private ModalWindowController modalwindow;

        public ModalWindowBuilder(ModalWindowController target)
        {
            modalwindow = target;
        }

        public ModalWindowBuilder SetTitle(string title)
        {
            bool hasText = !string.IsNullOrEmpty(title);
            modalwindow.headerText.text = title;
            modalwindow.headerText.gameObject.SetActive(hasText);
            return this;
        }

        public ModalWindowBuilder SetTitleIcon(Sprite val)
        {
            modalwindow.headerIcon.sprite = val;
            modalwindow.headerIcon.gameObject.SetActive(val != null);
            return this;
        }


        public ModalWindowBuilder SetVerticalText(string text)
        {
            bool hasText = !string.IsNullOrEmpty(text);
            modalwindow.verticalContentText.text = text;
            modalwindow.verticalContentText.gameObject.SetActive(hasText);
            return this;
        }

        public ModalWindowBuilder SetVerticalImage(Sprite val)
        {
            modalwindow.verticalContentImage.sprite = val;
            modalwindow.verticalContentImage.gameObject.SetActive(val != null);
            return this;
        }

        public ModalWindowBuilder SetHorizontalText(string text)
        {
            bool hasText = !string.IsNullOrEmpty(text);
            modalwindow.horizontalContentText.text = text;
            modalwindow.horizontalContentText.gameObject.SetActive(hasText);
            return this;
        }

        public ModalWindowBuilder SetHorizontalImage(Sprite val)
        {
            modalwindow.horizontalContentImage.sprite = val;
            modalwindow.horizontalContentImage.gameObject.SetActive(val != null);
            return this;
        }


        // confirmButton;
        // declineButton;
        // otherButton;

        public ModalWindowBuilder SetConfirmButton(string text, Action OnButtonClick)
        {
            modalwindow.confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
            modalwindow.OnConfirmCallback += OnButtonClick;
            modalwindow.confirmButton.gameObject.SetActive(true);
            return this;
        }


        public ModalWindowBuilder SetDeclineButton(string text, Action OnButtonClick)
        {
            modalwindow.declineButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
            modalwindow.OnDeclineCallback += OnButtonClick;
            modalwindow.declineButton.gameObject.SetActive(true);
            return this;
        }

        public ModalWindowBuilder SetDeclineButtonClose(string text = "Close")
        {
            modalwindow.declineButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
            modalwindow.OnDeclineCallback += delegate { modalwindow.gameObject.SetActive(false); };
            modalwindow.declineButton.gameObject.SetActive(true);
            return this;
        }


        public ModalWindowBuilder SetOtherButton(string text, Action OnButtonClick)
        {
            modalwindow.otherButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
            modalwindow.OnOtherCallback += OnButtonClick;
            modalwindow.otherButton.gameObject.SetActive(true);
            return this;
        }


        public ModalWindowBuilder Show()
        {
            modalwindow.gameObject.SetActive(true);
            return this;
        }

    }
}