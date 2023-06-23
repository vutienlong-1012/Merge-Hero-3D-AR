using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MergeAR.UI.BattleResultPopup
{
    public class UIXReward : MonoBehaviour
    {

        bool isChosen;
        [ShowInInspector, ReadOnly]
        public bool IsChosen
        {
            get
            {
                return isChosen;
            }
            set
            {
                isChosen = value;
                MakeThisBigger();
            }
        }

        [ShowInInspector]
        int ID => this.transform.GetSiblingIndex();

        [SerializeField] BonusRewardSlider slider;
        public int XValue;
        private void OnEnable()
        {
            slider.OnSliderChangedValue.AddListener(OnSliderChangedListener);
        }

        private void OnDisable()
        {
            slider.OnSliderChangedValue.RemoveListener(OnSliderChangedListener);
        }

        void OnSliderChangedListener(int _value)
        {

            IsChosen = (_value == ID);
        }
        void MakeThisBigger()
        {
            if (IsChosen)
                this.transform.DOScale(1.3f, 0.2f);
            else
                this.transform.DOScale(1f, 0.2f);
        }
    }
}