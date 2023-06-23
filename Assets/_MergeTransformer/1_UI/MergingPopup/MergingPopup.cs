using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.MergingPopup
{
    public class MergingPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] Button myCardButton;
        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            myCardButton.onClick.AddListener(ShowMyCardPopup);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            myCardButton.onClick.RemoveListener(ShowMyCardPopup);
        }

        private void ShowMyCardPopup()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.ShowPopup(UIManager.Instance.myCardPopup);
        }

       
    }
}