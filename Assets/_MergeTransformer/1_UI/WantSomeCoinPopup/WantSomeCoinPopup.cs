using BreakInfinity;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class WantSomeCoinPopup : PopupBase
    {
        [SerializeField] Text valueText;
        [SerializeField, ReadOnly] BigDouble value;

        [SerializeField] Button WatchAdsAndGetCoinButton;



        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            WatchAdsAndGetCoinButton.onClick.AddListener(ShowAds);
            closeButton.onClick.AddListener(ContinueSuggestReinforcement);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            WatchAdsAndGetCoinButton.onClick.RemoveListener(ShowAds);
            closeButton.onClick.RemoveListener(ContinueSuggestReinforcement);
        }

        private void ShowAds()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            //CC_Interface.current.ShowRewardAds(0, "Reward_ShipCoin", StaticVariables.CurrentLevel, _CheckIsWatchAdSuccess);
            //void _CheckIsWatchAdSuccess(bool _isWatched)
            //{
            //    if (_isWatched)
            //    {
            //        UIManager.Instance.ShowPopup(UIManager.Instance.youEarnCoinPopup, value, 0.3f);
            //        Reinforcement.Instance.ImmediatelyDeActive();
            //        Hide();
            //    }
            //}      
        }

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            value = (BigDouble)_data;

            valueText.text = BigDouble.ToText(value);
        }

        private void ContinueSuggestReinforcement()
        {
            Reinforcement.Instance.Play();
        }
    }
}