using BreakInfinity;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI
{
    public class YouEarnCoinPopup : PopupBase
    {
        [SerializeField] Text textValue;
        [SerializeField, ReadOnly] BigDouble value;

        [SerializeField] Button tapToContinueButton;
        [SerializeField] Image iconCoinImage;

        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            tapToContinueButton.onClick.AddListener(PlusCoin);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            tapToContinueButton.onClick.RemoveListener(PlusCoin);
        }

        private void PlusCoin()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            tapToContinueButton.interactable = false;
            UIManager.Instance.coinInforPopup.SpawnAndPlusCoins(tapToContinueButton.transform, value, () =>
            {
                this.Hide();
            });
        }

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            value = (BigDouble)_data;
            textValue.text = BigDouble.ToText(value);
            tapToContinueButton.interactable = true;
        }


    }
}