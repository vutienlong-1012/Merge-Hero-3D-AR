using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class SettingPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] Button cheatConfirm1Button;
        [SerializeField, BoxGroup("Popup Reference")] Button cheatConfirm2Button;
        [SerializeField, BoxGroup("Popup Reference")] RateUsButton rateUsButton;
        [SerializeField, BoxGroup("Popup Reference")] Image bigBackGroundImage;
        [SerializeField, ReadOnly] int step1Count;
        [SerializeField, ReadOnly] int step2Count;

        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            cheatConfirm1Button.onClick.AddListener(ConfirmStep1);
            cheatConfirm2Button.onClick.AddListener(ConfirmStep2);
        }
        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            cheatConfirm1Button.onClick.RemoveListener(ConfirmStep1);
            cheatConfirm2Button.onClick.RemoveListener(ConfirmStep2);
        }

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            rateUsButton.gameObject.SetActive(!StaticVariables.IsRated);
            ResizePopup();
        }

        public override void Hide()
        {
            base.Hide();
            step1Count = step2Count = 0;
        }

        private void ConfirmStep1()
        {

            step1Count++;
        }

        private void ConfirmStep2()
        {
            step2Count++;

            if (step1Count == 3 && step2Count == 3)
                UIManager.Instance.ShowPopup(UIManager.Instance.cheatPopup);
        }

        public void ResizePopup()
        {
            if (StaticVariables.IsRated)
            {
                bigBackGroundImage.rectTransform.anchoredPosition = new Vector3(0, 74, 0);
                bigBackGroundImage.rectTransform.sizeDelta = new Vector2(519.17f, 722.74f);
            }
        }
    }
}