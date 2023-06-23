using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.HomePopup
{
    public class HomePopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] protected Button settingButton;
        [SerializeField, BoxGroup("Popup Reference")] Text currentLevelText;

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            UpdateText();           
        }
        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            settingButton.onClick.AddListener(SettingButtonOnClick);           
            EventDispatcher.Instance.AddListener(EventName.OnLevelChange, UpdateText);

        }
        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            settingButton.onClick.RemoveListener(SettingButtonOnClick);
            EventDispatcher.Instance.RemoveListener(EventName.OnLevelChange, UpdateText);
        }

        void SettingButtonOnClick()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            UIManager.Instance.ShowPopup
                (
                _popup: UIManager.Instance.settingPopup,
                _data: null,
                _delay: 0.3f,
                _actionOnStartShow: () => UIManager.Instance.HidePopup(UIManager.Instance.homePopup),
                _actionOnCompleteShow: null,
                _actionOnStartHide: null,
                _actionOnCompleteHide: () => UIManager.Instance.ShowPopup(UIManager.Instance.homePopup)
                ); ;
        }

       

        private void UpdateText(EventName key = EventName.NONE, object data = null)
        {
            currentLevelText.text = (I2.Loc.ScriptLocalization.LEVEL + " " + StaticVariables.CurrentLevel).ToUpper();
        }
    }
}