using BreakInfinity;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.BattleResultPopup
{
    public class WatchAdsAndResetButton : MonoBehaviour
    {
        Button button;

        [ShowInInspector]
        Button ThisButton
        {
            get
            {
                if (button == null)
                    button = this.GetComponent<Button>();
                return button;
            }
        }

        [SerializeField] Text bonusCoinText;
        [SerializeField] BonusRewardSlider bonusRewardSlider;

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(ShowAds);
            bonusRewardSlider.OnSliderChangedValue.AddListener(UpdateCoinText);
            UpdateCoinText();
        }


        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(ShowAds);
            bonusRewardSlider.OnSliderChangedValue.RemoveListener(UpdateCoinText);
        }


        private void ShowAds()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();

            bonusRewardSlider.PauseSlider();
            //CC_Interface.current.ShowRewardAds(0, "Reward_EndBattleBonusReward", StaticVariables.CurrentLevel, _CheckIsWatchAdSuccess);
            //void _CheckIsWatchAdSuccess(bool _isWatched)
            //{
            //    if (_isWatched)
            //        UIManager.Instance.battleResultPopup.StopSliderAndGiveCoin(this.transform, UIManager.Instance.battleResultPopup.RewardCoinWithBonus, ResetRound);
            //    else
            //        bonusRewardSlider.PlaySlider();
            //}
        }

        void ResetRound()
        {
            UIManager.Instance.HidePopup(UIManager.Instance.battleResultPopup);
            StartCoroutine(_DelayResetRound());

            IEnumerator _DelayResetRound()
            {
                yield return new WaitForSeconds(0.3f);
                GameManager.Instance.State = VTLTools.GameState.ResetRound;

            }
        }
        public void SetInteractableButton(bool _value)
        {
            ThisButton.interactable = _value;
        }

        private void UpdateCoinText(int arg0 = 0)
        {
            bonusCoinText.text = BigDouble.ToText(UIManager.Instance.battleResultPopup.RewardCoinWithBonus);
        }
    }
}