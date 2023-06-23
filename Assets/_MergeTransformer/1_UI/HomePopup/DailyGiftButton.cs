using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.HomePopup
{
    public class DailyGiftButton : MonoBehaviour
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
        public DOTweenAnimation ThisDOTweenAnimation
        {
            get
            {
                if (dOTweenAnimation == null)
                    dOTweenAnimation = GetComponent<DOTweenAnimation>();
                return dOTweenAnimation;
            }
        }

        [SerializeField] Image signImage;

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(DailyGiftButtonOnClick);

            if (StaticVariables.CurrentLevel < 3)
            {
                ThisButton.interactable = false;
            }
            else
            {
                ThisButton.interactable = true;
                if (StaticVariables.IsAlreadyGetTodayDailyGift)
                {
                    ThisDOTweenAnimation.DOPause();
                    signImage.gameObject.SetActive(false);
                }
                else
                {
                    ThisDOTweenAnimation.DOPlay();
                    signImage.gameObject.SetActive(true);
                }
            }
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(DailyGiftButtonOnClick);
        }

        void DailyGiftButtonOnClick()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.ShowPopup
                (
                _popup: UIManager.Instance.dailyGiftPopup,
                _data: null,
                _delay: 0.3f,
                _actionOnStartShow: () => UIManager.Instance.HidePopup(UIManager.Instance.homePopup),
                _actionOnCompleteShow: null,
                _actionOnStartHide: null,
                _actionOnCompleteHide: null
                ); ;
        }
    }
}