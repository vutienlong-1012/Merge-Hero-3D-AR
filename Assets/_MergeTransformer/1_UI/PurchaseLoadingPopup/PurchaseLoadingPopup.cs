using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR.UI
{
    public class PurchaseLoadingPopup : PopupBase
    {
        [SerializeField, ReadOnly] string purchaseFrom;
        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            purchaseFrom = (string)_data;
        }

        public string GetPurchaseFrom()
        {
            return purchaseFrom;
        }
    }
}