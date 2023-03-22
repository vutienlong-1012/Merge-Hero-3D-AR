using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class HomePopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] protected Button settingButton;
        [SerializeField, BoxGroup("Popup Reference")] protected Button setupMapButton;

        private void OnEnable()
        {

        }
        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            settingButton.onClick.AddListener(SettingButtonOnClick);
            setupMapButton.onClick.AddListener(SetupMapButtonOnClick);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            settingButton.onClick.RemoveListener(SettingButtonOnClick);
            setupMapButton.onClick.RemoveListener(SetupMapButtonOnClick);
        }

        void SettingButtonOnClick()
        {
            UIManager.Instance.ShowPopup
                (
                _popup: UIManager.Instance.settingPopup,
                _data: null,
                _delay: 0,
                _actionOnStartShow: () => UIManager.Instance.HidePopup(UIManager.Instance.homePopup),
                _actionOnCompleteShow: null,
                _actionOnStartHide: () => UIManager.Instance.ShowPopup(UIManager.Instance.homePopup),
                _actionOnCompleteHide: null
                );
        }
        void SetupMapButtonOnClick()
        {
            UIManager.Instance.ShowPopup
                (
                _popup: UIManager.Instance.setupMapPopup,
                _data: null,
                _delay: 0,
                _actionOnStartShow: () =>
                {
                    UIManager.Instance.HidePopup(UIManager.Instance.homePopup);
                    GameManager.Instance.State = GameState.WaitToPlaceEnvironment;
                },
                _actionOnCompleteShow: null,
                _actionOnStartHide: null,
                _actionOnCompleteHide: () =>
                {
                    GameManager.Instance.State = GameState.Idle;
                }
                );
        }
    }
}