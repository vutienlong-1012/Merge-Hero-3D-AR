using BreakInfinity;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace MergeAR.UI.PopupOnlineReward
{
    public class PopupOnlineReward : PopupBase
    {
        [SerializeField] Image lidImage;
        [SerializeField] Image fxHaloImage;
        [SerializeField] WatchAdAndClaimButton watchAdAndClaimButton;
        [SerializeField] NormalClaimButton normalClaimButton;
        [SerializeField, ReadOnly] BigDouble value;

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);

            //           fxHaloImage.DOFade(0, 0);
            lidImage.rectTransform.anchoredPosition = new Vector3(0, 77.41f, 0);
            StartCoroutine(_DelayPlayOpenAnimation());

            IEnumerator _DelayPlayOpenAnimation()
            {
                yield return new WaitForSeconds(1f);
                lidImage.rectTransform.DOLocalMove(new Vector3(10, 1100, 0), 1).SetEase(Ease.InExpo);
                //                fxHaloImage.DOFade(0.7f, 0.5f).SetDelay(0.7f);
            }
            SetActiveClaimCoinButtons(true);
            value = (BigDouble)data;
            normalClaimButton.Init(value);
            watchAdAndClaimButton.Init(value * 5);
        }

        public override void Hide()
        {
            base.Hide();
        }



        public void SetActiveClaimCoinButtons(bool _value)
        {
            watchAdAndClaimButton.ThisButton.interactable = _value;
            normalClaimButton.ThisButton.interactable = _value;
        }
    }
}