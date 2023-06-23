using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;
using VTLTools.UIAnimation;

namespace MergeAR.UI
{
    public class SetupMapPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] protected Button placeMapButton;

        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            placeMapButton.onClick.AddListener(OnClickPlaceMapButton);
            closeButton.onClick.AddListener(OnClickCloseButton);
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            placeMapButton.onClick.RemoveListener(OnClickPlaceMapButton);
            closeButton.onClick.RemoveListener(OnClickCloseButton);
        }

        void OnClickPlaceMapButton()
        {
            if (CursorControl.Instance.IsCursorPlacementValid)
            {
                EnvironmentManager.Instance.SetActiveEnvironment(true);
                EnvironmentManager.Instance.SetPosEnvironment(CursorControl.Instance.PlacementPose);

                MenuItem _closeMenuItem = closeButton.GetComponent<MenuItem>();
                if (_closeMenuItem.ThisMenuItemState != MenuItemState.Showing
                    && _closeMenuItem.ThisMenuItemState != MenuItemState.Showed)
                    _closeMenuItem.StartShow();
            }
        }

        void OnClickCloseButton()
        {
            closeButton.GetComponent<MenuItem>().StartHide();
        }
    }
}
