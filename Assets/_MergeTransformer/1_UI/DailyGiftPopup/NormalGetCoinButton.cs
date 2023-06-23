using BreakInfinity;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.DailyGiftPopup
{
    public class NormalGetCoinButton : MonoBehaviour
    {
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

        [SerializeField] Text valueText;

        [SerializeField, ReadOnly] BigDouble value;

        public void Init(BigDouble _value)
        {
            value = _value;
            valueText.text = BigDouble.ToText(_value);
        }

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(WatchAdAndGetCoin);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(WatchAdAndGetCoin);
        }

        private void WatchAdAndGetCoin()
        {
            UIManager.Instance.ShowPopup(UIManager.Instance.youEarnCoinPopup, value, 0.3f, null, null, null, () =>
            {
                UIManager.Instance.ShowPopup(UIManager.Instance.homePopup);
            });
            //LogManager.LogResource(FlowType.source, CurrencyType.Coin, (int)value.ToDouble(), "DailyGiftNormal", StaticVariables.CurrentLevel);


            UIManager.Instance.HidePopup(UIManager.Instance.dailyGiftPopup);
            UIManager.Instance.dailyGiftPopup.SetInteractableGetCoinButton(false);
            StaticVariables.IsAlreadyGetTodayDailyGift = true;
            StaticVariables.CurrentDailyGiftDay++;

            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }
    }
}