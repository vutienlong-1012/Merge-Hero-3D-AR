using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] public HomePopup homePopup;
        [SerializeField] public SettingPopup settingPopup;
        [SerializeField] public SetupMapPopup setupMapPopup;

        private void Start()
        {
            homePopup.PreviewHide();
            settingPopup.PreviewHide();
            setupMapPopup.PreviewHide();
        }

        [Button]
        public void ShowPopup(PopupBase _popup, object _data = null, float _delay = 0f, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            _popup.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
        }

        [Button]
        public void HidePopup(PopupBase _popup)
        {
            _popup.Hide();
        }
    }
}