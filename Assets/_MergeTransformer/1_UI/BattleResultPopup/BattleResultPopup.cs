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
    public class BattleResultPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] Text rewardBaseText;
        [SerializeField, BoxGroup("Popup Reference")] Image resultImage;
        [SerializeField, BoxGroup("Popup Reference")] Image resultBackgroundImage;
        [SerializeField, BoxGroup("Popup Reference")] Text resultText;
        [SerializeField, BoxGroup("Popup Reference")] Outline[] resultTextOutlines;

        [SerializeField, BoxGroup("Popup Reference")] BonusRewardSlider bonusRewardSlider;
        [SerializeField, BoxGroup("Popup Reference")] WatchAdsAndResetButton watchAdsButton;
        [SerializeField, BoxGroup("Popup Reference")] NoThanksButton noThankButton;

        [SerializeField, BoxGroup("Popup Reference")] List<UIXReward> uIXRewards;

        [SerializeField, BoxGroup("Asset")] Sprite victorySprite;
        [SerializeField, BoxGroup("Asset")] Sprite defeatSprite;

        [SerializeField, BoxGroup("Asset")] Sprite victoryBackgroundSprite;
        [SerializeField, BoxGroup("Asset")] Sprite defeatBackgroundSprite;

        [SerializeField, BoxGroup("Asset")] string victoryString = "VICTORY";
        [SerializeField, BoxGroup("Asset")] string defeatString = "DEFEAT";

        [SerializeField, BoxGroup("Asset")] Color victoryOutlineColor;
        [SerializeField, BoxGroup("Asset")] Color defeatOutlineColor;


        [ShowInInspector, ReadOnly]
        public BigDouble RewardCoin
        {
            get;
            private set;
        }

        [ShowInInspector, ReadOnly]
        public BigDouble RewardCoinWithBonus
        {
            get;
            private set;
        }


        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            bonusRewardSlider.OnSliderChangedValue.AddListener(OnSliderValueChangeListener);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            bonusRewardSlider.OnSliderChangedValue.RemoveListener(OnSliderValueChangeListener);
        }

        public override void Show(object _data, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            RewardCoin = (BigDouble)_data;
            rewardBaseText.text = BigDouble.ToText(RewardCoin);
            if (GameManager.Instance.State == GameState.VictoryBattle)
                _SetPanel(victorySprite, victoryBackgroundSprite, victoryString, victoryOutlineColor);
            else
                _SetPanel(defeatSprite, defeatBackgroundSprite, defeatString, defeatOutlineColor);

            void _SetPanel(Sprite _resultSprite, Sprite _resultBackgroundSprite, string _resultText, Color _resultTextOutlineColor)
            {
                resultImage.sprite = _resultSprite;
                resultBackgroundImage.sprite = _resultBackgroundSprite;
                resultText.text = _resultText;
                foreach (var item in resultTextOutlines)
                {
                    item.effectColor = _resultTextOutlineColor;
                }
            }

            watchAdsButton.SetInteractableButton(true);
            noThankButton.SetInteractableButton(true);
        }

        public void StopSliderAndGiveCoin(Transform _spawnArea, BigDouble _plusCoinValue, Action _OnCompleteAction)
        {
            UIManager.Instance.coinInforPopup.SpawnAndPlusCoins(_spawnArea, _plusCoinValue, _OnCompleteAction);
            //LogManager.LogResource(FlowType.source, CurrencyType.Coin, (int)_plusCoinValue.ToDouble(), "EndBattle", StaticVariables.CurrentLevel);

            watchAdsButton.SetInteractableButton(false);
            noThankButton.SetInteractableButton(false);
            bonusRewardSlider.PauseSlider();
        }


        private void OnSliderValueChangeListener(int _value)
        {
            RewardCoinWithBonus = RewardCoin * uIXRewards[_value].XValue;
        }
    }
}