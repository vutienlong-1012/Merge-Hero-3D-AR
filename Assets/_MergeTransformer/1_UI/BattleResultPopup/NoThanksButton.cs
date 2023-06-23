using BreakInfinity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.BattleResultPopup
{
    public class NoThanksButton : MonoBehaviour
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

        [SerializeField] Transform coinIcon;

        [SerializeField] BonusRewardSlider bonusRewardSlider;

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(PlusCoinAndResetRound);
        }


        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(PlusCoinAndResetRound);
        }


        private void PlusCoinAndResetRound()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.battleResultPopup.StopSliderAndGiveCoin(coinIcon, UIManager.Instance.battleResultPopup.RewardCoin, ResetRound);
        }

        public void SetInteractableButton(bool _value)
        {
            ThisButton.interactable = _value;
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
    }
}