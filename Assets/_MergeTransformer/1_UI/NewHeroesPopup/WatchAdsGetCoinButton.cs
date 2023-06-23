using BreakInfinity;
using MergeAR.UI.BattleResultPopup;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.NewHeroesPopup
{
    public class WatchAdsGetCoinButton : MonoBehaviour
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

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(ShowAds);
            UpdateCoinText();
        }


        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(ShowAds);

        }


        private void ShowAds()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();


            //CC_Interface.current.ShowRewardAds(0, "Reward_NewHero", StaticVariables.CurrentLevel, _CheckIsWatchAdSuccess);
            //void _CheckIsWatchAdSuccess(bool _isWatched)
            //{
            //    if (_isWatched)
            //    {
            //        BigDouble _value = LevelManager.Instance.levelReward.GetCurrentLevelWinReward() * 5;
            //        UIManager.Instance.newHeroesPopup.SetInteractablePopup(false);
            //        UIManager.Instance.coinInforPopup.SpawnAndPlusCoins(this.transform, _value, () =>
            //        {
            //            UIManager.Instance.HidePopup(UIManager.Instance.newHeroesPopup);
            //        });
            //        LogManager.LogResource(FlowType.source, CurrencyType.Coin, (int)_value.ToDouble(), "NewHero", StaticVariables.CurrentLevel);
            //    }
            //}
        }

        private void UpdateCoinText(int arg0 = 0)
        {
            bonusCoinText.text = BigDouble.ToText(LevelReward.Instance.GetCurrentLevelWinReward() * 5);
        }
    }
}