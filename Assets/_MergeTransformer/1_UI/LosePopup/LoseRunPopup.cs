using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class LoseRunPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] protected Button retryLevelButton;

        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            retryLevelButton.onClick.AddListener(StartNextLevelButtonOnClick);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            retryLevelButton.onClick.RemoveListener(StartNextLevelButtonOnClick);
        }

        void StartNextLevelButtonOnClick()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.HidePopup(this);
            GameManager.Instance.State = GameState.ResetRound;
        }
    }
}