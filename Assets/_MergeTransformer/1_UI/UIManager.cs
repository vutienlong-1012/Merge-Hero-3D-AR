using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public SetupMapPopup setupMapPopup;
        public HomePopup.HomePopup homePopup;
        public SettingPopup settingPopup;
        public MergingPopup.MergingPopup mergingPopup;
        public FightPopUp fightPopup;
        public BattleResultPopup.BattleResultPopup battleResultPopup;
        public LoseRunPopup loseRunPopup;
        public CoinInforPopup coinInforPopup;
        public NewHeroesPopup.NewHeroesPopup newHeroesPopup;
        public MyCardPopup.MyCardPopup myCardPopup;
        public ReinforcementPopup.ReinforcementPopup reinforcementPopup;
        public WantSomeCoinPopup wantSomeCoinPopup;
        public YouEarnCoinPopup youEarnCoinPopup;
        public PushGamePopup pushGamePopup;
        public RateGamePopup.RateGamePopup rateGamePopup;
        public CheatPopup.CheatPopup cheatPopup;
        public DailyGiftPopup.DailyGiftPopup dailyGiftPopup;
        public PopupOnlineReward.PopupOnlineReward popupOnlineReward;
        public PurchaseLoadingPopup purchaseLoadingPopup;
        public PurchaseSuccessPopup purchaseSuccessPopup;
        public PurchaseFailPopup.PurchaseFailPopup purchaseFailPopup;
        public NoInternetPopup noInternetPopup;

        [SerializeField] List<GameObject> allNoAdButtons;


        void Start()
        {
            setupMapPopup.PreviewHide();
            homePopup.PreviewHide();
            settingPopup.PreviewHide();
            mergingPopup.PreviewHide();
            fightPopup.PreviewHide();
            battleResultPopup.PreviewHide();
            loseRunPopup.PreviewHide();
            coinInforPopup.PreviewHide();
            newHeroesPopup.PreviewHide();
            myCardPopup.PreviewHide();
            reinforcementPopup.PreviewHide();
            wantSomeCoinPopup.PreviewHide();
            youEarnCoinPopup.PreviewHide();
            rateGamePopup.PreviewHide();
            cheatPopup.PreviewHide();
            dailyGiftPopup.PreviewHide();
            popupOnlineReward.PreviewHide();
            purchaseLoadingPopup.PreviewHide();
            purchaseSuccessPopup.PreviewHide();
            purchaseFailPopup.PreviewHide();
            noInternetPopup.PreviewHide();
            pushGamePopup.PreviewHide();
            HideAllAdButtons();
        }

        [Button]
        public void ShowPopup(PopupBase _popup, object _data = null, float _delay = 0f, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            _popup.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
        }

        [Button]
        public void HidePopup(PopupBase _popup)
        {
            _popup.Hide();
        }

        public void HideAllAdButtons()
        {
            if (StaticVariables.IsRemovedAd)
                foreach (var item in allNoAdButtons)
                {
                    item.SetActive(false);
                }
        }
    }
}