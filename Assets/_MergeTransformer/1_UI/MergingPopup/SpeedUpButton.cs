using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.MergingPopup
{
    public class SpeedUpButton : MonoBehaviour
    {
        [SerializeField] Image iconBackgroundImage;
        [SerializeField] Image iconImage;
        [SerializeField] Image tickBackgroundImage;
        [SerializeField] Image tickImage;
        [SerializeField] Text speedText;
        [SerializeField] Outline speedTextOutline;

        [SerializeField] Sprite iconBackgroundX1SpeedSprite;
        [SerializeField] Sprite iconBackgroundX2SpeedSprite;

        [SerializeField] Sprite iconX1SpeedSprite;
        [SerializeField] Sprite iconX2SpeedSprite;

        [SerializeField] Color tickBackgroundX1SpeedColor;
        [SerializeField] Color tickBackgroundX2SpeedColor;

        [SerializeField] Color textOutlineX1SpeedColor;
        [SerializeField] Color textOutlineX2SpeedColor;

        bool isSpeedUp;
        [ShowInInspector]
        public bool IsSpeedUp
        {
            get
            {
                return isSpeedUp;
            }
            set
            {
                isSpeedUp = value;
                SetButtonState();
            }
        }

        Button button;
        [ShowInInspector]
        public Button ThisButton
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();
                return button;
            }
        }

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(SpeedUp);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(SpeedUp);
        }

        private void SpeedUp()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            IsSpeedUp = !IsSpeedUp;
        }

        void SetButtonState()
        {
            switch (IsSpeedUp)
            {
                case true:
                    iconBackgroundImage.sprite = iconBackgroundX2SpeedSprite;
                    iconImage.sprite = iconX2SpeedSprite;
                    tickBackgroundImage.color = tickBackgroundX2SpeedColor;
                    tickImage.gameObject.SetActive(true);
                    speedText.text = ("X2" + " " + I2.Loc.ScriptLocalization.SPEED).ToUpper();
                    speedText.color = tickBackgroundX2SpeedColor;
                    speedTextOutline.effectColor = textOutlineX2SpeedColor;

                    Time.timeScale = 2;
                    break;
                case false:
                    iconBackgroundImage.sprite = iconBackgroundX1SpeedSprite;
                    iconImage.sprite = iconX1SpeedSprite;
                    tickBackgroundImage.color = tickBackgroundX1SpeedColor;
                    tickImage.gameObject.SetActive(false);
                    speedText.text = ("X1" + " " + I2.Loc.ScriptLocalization.SPEED).ToUpper();
                    speedText.color = tickBackgroundX1SpeedColor;
                    speedTextOutline.effectColor = textOutlineX1SpeedColor;

                    Time.timeScale = 1;
                    break;
            }
        }

        public void SetInteractable(bool _value)
        {
            ThisButton.interactable = _value;
        }
    }
}