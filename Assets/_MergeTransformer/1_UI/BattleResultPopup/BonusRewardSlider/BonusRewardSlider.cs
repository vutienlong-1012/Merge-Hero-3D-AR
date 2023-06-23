using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VTLTools.UIAnimation;

namespace MergeAR.UI.BattleResultPopup
{
    public class BonusRewardSlider : MonoBehaviour
    {
        Slider rewardSlider;
        [ShowInInspector]
        Slider RewardSlider
        {
            get
            {
                if (rewardSlider == null)
                    rewardSlider = GetComponent<Slider>();
                return rewardSlider;
            }
        }

        MenuItemScale menuItemScale;
        [ShowInInspector]
        MenuItemScale MenuItemScale
        {
            get
            {
                if (menuItemScale == null)
                    menuItemScale = GetComponent<MenuItemScale>();
                return menuItemScale;
            }
        }

        [ShowInInspector]
        int CurrentValueMulti => (int)RewardSlider.value;
        int previousValueMulti;

        public UnityEvent<int> OnSliderChangedValue;

        void OnEnable()
        {
            RewardSlider.value = 0;
            RewardSlider.DOValue(RewardSlider.maxValue, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            previousValueMulti = CurrentValueMulti;
        }

        void OnDisable()
        {
            RewardSlider.value = 0;
            RewardSlider.DOKill();
        }

        private void Update()
        {
            if (CurrentValueMulti != previousValueMulti)
            {
                previousValueMulti = CurrentValueMulti;
                OnSliderChangedValue.Invoke(CurrentValueMulti);
                if (MenuItemScale.ThisMenuItemState == VTLTools.MenuItemState.Showed)
                    VibrationSystem.Instance.PlayVibration();
            }
        }

        public void PauseSlider()
        {
            RewardSlider.DOPause();
        }

        public void PlaySlider()
        {
            RewardSlider.DOPlay();
        }
    }
}