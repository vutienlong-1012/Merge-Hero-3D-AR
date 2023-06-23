using BreakInfinity;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.HomePopup
{
    public class OnlineRewardButton : MonoBehaviour
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

        DOTweenAnimation dOTweenAnimation;
        [ShowInInspector]
        public DOTweenAnimation ThisDoTweenAnimation
        {
            get
            {
                if (dOTweenAnimation == null)
                    dOTweenAnimation = GetComponent<DOTweenAnimation>();
                return dOTweenAnimation;
            }
        }

        OnlineRewardButtonState onlineRewardButtonState;
        [ShowInInspector]
        public OnlineRewardButtonState OnlineRewardButtonState
        {
            get => onlineRewardButtonState;
            set
            {
                onlineRewardButtonState = value;
                SetButton();
            }
        }


        [SerializeField] Image signImage;
        [SerializeField] Image countdownImage;

        [SerializeField] Text countdownText;

        [SerializeField] List<OnlineRewardData> onlineRewardDatas;



        private void Start()
        {
            if (StaticVariables.OnlineRewardClaimedCount >= onlineRewardDatas.Count)
                OnlineRewardButtonState = OnlineRewardButtonState.OutOfOrder;
            else
            {
                if (StaticVariables.RemainTimeCountDownOnlineReward == 0)
                    OnlineRewardButtonState = OnlineRewardButtonState.Waiting;
                else
                {
                    OnlineRewardButtonState = OnlineRewardButtonState.Counting;
                    TimeManager.Instance.StartCountdown(StaticVariables.RemainTimeCountDownOnlineReward, countdownText, OnCountDownUpdate, OnCountdownComplete);

                }
            }
        }

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(ShowPopupOnlineReward);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(ShowPopupOnlineReward);
        }

        private void ShowPopupOnlineReward()
        {
            BigDouble _reward = LevelReward.Instance.GetCurrentLevelWinReward() * onlineRewardDatas[StaticVariables.OnlineRewardClaimedCount].multiValue;
            UIManager.Instance.ShowPopup(UIManager.Instance.popupOnlineReward, _reward, 0.3f, null, null, StartCountDownAgain);
            UIManager.Instance.HidePopup(UIManager.Instance.homePopup);
        }

        void OnCountDownUpdate(float _value)
        {
            StaticVariables.RemainTimeCountDownOnlineReward = _value;
        }

        void OnCountdownComplete()
        {
            OnlineRewardButtonState = OnlineRewardButtonState.Waiting;
        }

        void SetButton()
        {
            switch (OnlineRewardButtonState)
            {
                case OnlineRewardButtonState.Counting:
                    _SetActiveComponent(false, false, true);

                    ThisDoTweenAnimation.DOPause();
                    break;
                case OnlineRewardButtonState.Waiting:
                    _SetActiveComponent(true, true, true);

                    ThisDoTweenAnimation.DOPlay();
                    countdownText.text = "00:00";
                    break;
                case OnlineRewardButtonState.OutOfOrder:
                    _SetActiveComponent(false, false, false);

                    ThisDoTweenAnimation.DOPause();
                    break;
            }

            void _SetActiveComponent(bool _buttonInteractable, bool _signActive, bool _cdImageActive)
            {
                ThisButton.interactable = _buttonInteractable;
                signImage.gameObject.SetActive(_signActive);
                countdownImage.gameObject.SetActive(_cdImageActive);
                ThisDoTweenAnimation.DORestart();
            }
        }

        void StartCountDownAgain()
        {
            StaticVariables.OnlineRewardClaimedCount++;
            if (StaticVariables.OnlineRewardClaimedCount >= onlineRewardDatas.Count)
                OnlineRewardButtonState = OnlineRewardButtonState.OutOfOrder;
            else
            {
                OnlineRewardButtonState = OnlineRewardButtonState.Counting;
                TimeManager.Instance.StartCountdown(onlineRewardDatas[StaticVariables.OnlineRewardClaimedCount].second, countdownText, OnCountDownUpdate, OnCountdownComplete);
            }
        }

        [Serializable]
        struct OnlineRewardData
        {
            [HorizontalGroup, LabelWidth(50)] public int second;
            [HorizontalGroup, LabelWidth(70)] public int multiValue;
        }
    }
}