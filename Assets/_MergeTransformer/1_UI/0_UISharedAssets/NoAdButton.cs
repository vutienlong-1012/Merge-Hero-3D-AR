using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI
{
    public class NoAdButton : MonoBehaviour
    {
        Button button;
        [ShowInInspector]
        Button ThisButton
        {
            get
            {
                if (button == null)
                {
                    button = GetComponent<Button>();
                }
                return button;
            }
        }

        [SerializeField] string wherePurchase;
        private void OnEnable()
        {
            ThisButton.onClick.AddListener(StartPurchase);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(StartPurchase);
        }

        private void StartPurchase()
        {
            UIManager.Instance.ShowPopup(UIManager.Instance.purchaseLoadingPopup, wherePurchase, 0, null, () =>
            {
                //CC_Interface.current.PurchaseItem(IAP_Product.hr3d_removeads);
            });
        }
    }
}