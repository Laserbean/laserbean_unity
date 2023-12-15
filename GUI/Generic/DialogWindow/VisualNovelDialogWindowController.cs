using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Laserbean.CustomGUI.DialogBoxes
{
    public class VisualNovelDialogWindowController : MonoBehaviour
    {

        // [Header("Picture")]
        [SerializeField] internal Transform headerTransform;

        [SerializeField] internal Image speakerImage;
        [SerializeField] internal TextMeshProUGUI speakerName;
        [SerializeField] internal TextMeshProUGUI dialogField;

        public event Action OnContinueCallback;

        internal bool isPlaying = false;

        [SerializeField] float charTime = 0.2f;
        [SerializeField] float defaultSpeed = 1f;
        [SerializeField] float fastSpeed = 1.5f;
        [SerializeField] float slowSpeed = 0.5f;

        const char customFormatMarker = '*';

        internal string dialogText = "";
        internal string dialogTextRaw = "";

        enum CharState
        {
            Normal,
            RichFormat,
            RichFormatEnd,
            CustomFormat
        }

        void GenerateText()
        {
            dialogText = "";
            CharState state = CharState.Normal;
            foreach (var ccc in dialogTextRaw) {

                switch (state) {
                    case CharState.Normal:
                        if (ccc == customFormatMarker) {
                            state = CharState.CustomFormat;
                            continue;
                        }
                        dialogText += ccc;
                        break;

                    case CharState.CustomFormat:
                        if (ccc == customFormatMarker) {
                            state = CharState.Normal;
                            continue;
                        }
                        break;
                }
            }
        }

        IEnumerator DisplaySentence()
        {
            isPlaying = true;
            CharState state = CharState.Normal;
            dialogField.gameObject.SetActive(true);

            float curspeed = charTime / defaultSpeed;



            int index = 0;
            int endOfRichFormat = 0;

            int richWordStart = 0;
            int richWordEnd = 0;

            string tempText = "";
            string curRichPrefix = "";
            string curRichSuffix = "";
            string curRichText = "";

            foreach (var ccc in dialogTextRaw) {

                switch (state) {
                    case CharState.Normal:

                        state = ccc switch {
                            customFormatMarker => CharState.CustomFormat,
                            '<' => CharState.RichFormat,
                            _ => state
                        };

                        if (state != CharState.Normal) {
                            endOfRichFormat = 0;
                            richWordStart = 0;
                            richWordEnd = 0;
                        }
                        break;

                    case CharState.CustomFormat:
                        if (ccc == customFormatMarker) {
                            state = CharState.Normal;

                            index++;
                            continue;
                        }
                        break;

                    case CharState.RichFormat:
                        if (index >= endOfRichFormat) {
                            state = CharState.Normal;
                            tempText += curRichPrefix + curRichText + curRichSuffix;
                        }

                        break;
                }



                switch (state) {
                    case CharState.Normal:
                        tempText += ccc;
                        dialogField.text += ccc;
                        yield return new WaitForSeconds(curspeed);
                        break;

                    case CharState.RichFormat:
                        if (endOfRichFormat == 0) {

                            endOfRichFormat = dialogTextRaw.IndexOf('>', index);
                            richWordStart = ++endOfRichFormat;
                            curRichPrefix = dialogTextRaw[index..endOfRichFormat];

                            richWordEnd = dialogTextRaw.IndexOf('<', endOfRichFormat);
                            endOfRichFormat = dialogTextRaw.IndexOf('>', endOfRichFormat) + 1;
                            curRichSuffix = dialogTextRaw[richWordEnd..endOfRichFormat];
                            curRichText = "";
                        }



                        if (index >= richWordStart && index < richWordEnd) {
                            curRichText += ccc;
                            dialogField.text = tempText + curRichPrefix + curRichText + curRichSuffix;
                            yield return new WaitForSeconds(curspeed);
                        }


                        break;
                    case CharState.CustomFormat:
                        curspeed = ccc switch {
                            's' => charTime / slowSpeed,
                            'f' => charTime / fastSpeed,
                            _ => curspeed,
                        };
                        break;
                }

                index++;
                if (!isPlaying) {
                    break;
                }
            }
            isPlaying = false; 
            yield break;
        }


        public void DisableStuff()
        {
            OnContinueCallback = null;
            dialogField.SetText("");
            dialogText = "";
            dialogTextRaw = "";

            speakerImage.gameObject.SetActive(false);
            speakerName.gameObject.SetActive(false);
            dialogField.gameObject.SetActive(false);
        }

        public void Skip()
        {
            isPlaying = false;
            GenerateText();
            dialogField.text = dialogText;
        }

        public void OnContinue()
        {
            if (isPlaying) {
                Skip();
                return;
            }
            OnContinueCallback.Invoke();
        }

        internal void Play()
        {
            StartCoroutine(DisplaySentence());

        }
    }


    public class VN_Dialog_Builder
    {
        VisualNovelDialogWindowController window;
        public VN_Dialog_Builder(VisualNovelDialogWindowController target)
        {
            window = target;
            window.DisableStuff();

        }


        public VN_Dialog_Builder SetSpeakerName(string text)
        {
            bool hasText = !string.IsNullOrEmpty(text);
            window.speakerName.text = text;
            window.speakerName.gameObject.SetActive(hasText);
            return this;
        }

        public VN_Dialog_Builder SetSpeakerImage(Sprite val)
        {
            window.speakerImage.sprite = val;
            window.speakerImage.gameObject.SetActive(val != null);
            return this;
        }

        public VN_Dialog_Builder SetDialogRaw(string text)
        {
            // bool hasText = !string.IsNullOrEmpty(text);
            window.dialogTextRaw = text;
            // window.dialogField.gameObject.SetActive(hasText);
            return this;
        }

        public VN_Dialog_Builder SetDialogInstant(string text)
        {
            bool hasText = !string.IsNullOrEmpty(text);
            window.dialogText = text;
            window.dialogField.text = text;
            window.dialogField.gameObject.SetActive(hasText);
            return this;
        }

        public VN_Dialog_Builder SetContinueAction(Action action)
        {
            // window.cont.GetComponentInChildren<TextMeshProUGUI>().text = text;
            window.OnContinueCallback += action;
            // window.declineButton.gameObject.SetActive(true);
            return this;
        }



        public VN_Dialog_Builder SetContinueClose()
        {
            // window.cont.GetComponentInChildren<TextMeshProUGUI>().text = text;
            window.OnContinueCallback += delegate { window.gameObject.SetActive(false); };
            // window.declineButton.gameObject.SetActive(true);
            return this;
        }


        public VisualNovelDialogWindowController Show()
        {
            window.gameObject.SetActive(true);
            window.Play();
            return window;
        }



    }
}
