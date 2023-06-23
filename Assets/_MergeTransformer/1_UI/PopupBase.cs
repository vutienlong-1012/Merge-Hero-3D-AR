using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools.UIAnimation;
using VTLTools;

namespace MergeAR.UI
{
    public class PopupBase : MonoBehaviour
    {
        [SerializeField, BoxGroup("Popup Reference")] protected Button closeButton;
        private MenuAnimationControl menuAnimationControl;
        protected MenuAnimationControl ThisMenuAnimationControl
        {
            get
            {
                if (menuAnimationControl == null)
                    menuAnimationControl = GetComponent<MenuAnimationControl>();
                return menuAnimationControl;
            }
        }
        protected System.Action actionOnStartShow, actionOnCompleteShow, actionOnStartHide, actionOnCompleteHide;
        protected object data;

        [ShowInInspector, ReadOnly]
        public bool IsShow
        {
            get
            {
                return ThisMenuAnimationControl.ThisMenuItemState == MenuItemState.Showed || ThisMenuAnimationControl.ThisMenuItemState == MenuItemState.Showing;
            }
            private set { }
        }

        #region SHOW
        public virtual void Show(object _data = null, float _delay = 0f, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            this.data = _data;
            this.actionOnStartShow = _actionOnStartShow;
            this.actionOnCompleteShow = _actionOnCompleteShow;
            this.actionOnStartHide = _actionOnStartHide;
            this.actionOnCompleteHide = _actionOnCompleteHide;
            this.Init();

            ButtonAddListener();
            if (ThisMenuAnimationControl == null)
            {
                this.gameObject.SetActive(true);
                OnShowStarted();
                OnShowCompleted();
            }
            else
            {
                this.gameObject.SetActive(true);
                ThisMenuAnimationControl.StartShow(_delay, _onShowStarted: OnShowStarted, _onShowCompleted: OnShowCompleted);
            }
        }
        protected virtual void OnShowStarted()
        {
            this.actionOnStartShow?.Invoke();
        }
        protected virtual void OnShowCompleted()
        {
            this.actionOnCompleteShow?.Invoke();
        }
        [Button, BoxGroup("UI preview")]
        public void PreviewShow()
        {
            foreach (var _item in ThisMenuAnimationControl.menuItems)
            {
                _item.PreviewShow();
            }
        }

        #endregion

        #region HIDE
        public virtual void Hide()
        {
            if (!IsShow)
                return;
            ButtonRemoveListener();
            if (ThisMenuAnimationControl == null)
            {
                OnHideStarted();
                OnHideCompleted();
            }
            else
            {
                ThisMenuAnimationControl.StartHide(0f, _onHideStarted: OnHideStarted, _onHideCompleted: OnHideCompleted);
            }
        }
        protected virtual void OnHideStarted()
        {
            this.actionOnStartHide?.Invoke();
        }
        protected virtual void OnHideCompleted()
        {
            this.actionOnCompleteHide?.Invoke();
            this.gameObject.SetActive(false);
        }
        [Button, BoxGroup("UI preview")]
        public void PreviewHide()
        {
            foreach (var _item in ThisMenuAnimationControl.menuItems)
            {
                _item.PreviewHide();
            }
        }
        #endregion

        public virtual void Init()
        {

        }
        protected virtual void ButtonAddListener()
        {
            closeButton?.onClick.AddListener(OnCloseClick);
        }
        protected virtual void ButtonRemoveListener()
        {
            closeButton?.onClick.RemoveListener(OnCloseClick);
        }
        protected virtual void OnCloseClick()
        {
            if (!IsShow)
                return;
            this.Hide();
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }



        //TEST TRUOC LUC NGU TRUA 
    }
}