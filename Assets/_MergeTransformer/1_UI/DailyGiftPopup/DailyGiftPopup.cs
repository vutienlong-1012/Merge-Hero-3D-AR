using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.DailyGiftPopup
{
    public class DailyGiftPopup : PopupBase
    {
        [SerializeField] List<DailyGift> dailyGifts;
        [SerializeField] List<DailyGiftData> dailyGiftDatas;

        [SerializeField] WatchAdAndGetCoinButton watchAdAndGetCoinButton;
        [SerializeField] NormalGetCoinButton normalGetCoinButton;
        public override void Init()
        {
            base.Init();
            for (int i = 0; i < dailyGifts.Count; i++)
            {
                dailyGifts[i].Init(dailyGiftDatas[i]);
            }
            SetInteractableGetCoinButton(!StaticVariables.IsAlreadyGetTodayDailyGift);
            normalGetCoinButton.Init(dailyGiftDatas[StaticVariables.CurrentDailyGiftDay - 1].value);
            watchAdAndGetCoinButton.Init(dailyGiftDatas[StaticVariables.CurrentDailyGiftDay - 1].value * 5);
        }

        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            closeButton.onClick.AddListener(ShowHomePopup);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            closeButton.onClick.RemoveListener(ShowHomePopup);
        }

        private void ShowHomePopup()
        {
            UIManager.Instance.ShowPopup(UIManager.Instance.homePopup, null, 0.3f);
        }

        public void SetInteractableGetCoinButton(bool _value)
        {
            watchAdAndGetCoinButton.gameObject.SetActive(_value);
            normalGetCoinButton.gameObject.SetActive(_value);
        }
    }
}