using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR.UI.PurchaseFailPopup
{
    public class TryAgainButton : MonoBehaviour
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

        private void OnEnable()
        {
            ThisButton.onClick.AddListener(TryPurchaseAgain);
        }
        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(TryPurchaseAgain);
        }

        private void TryPurchaseAgain()
        {
            UIManager.Instance.HidePopup(UIManager.Instance.purchaseFailPopup);

            string _wherePurchase = UIManager.Instance.purchaseLoadingPopup.GetPurchaseFrom();
            //UIManager.Instance.ShowPopup(UIManager.Instance.purchaseLoadingPopup, _wherePurchase, 0, null, () =>
            //{
            //    CC_Interface.current.PurchaseItem(IAP_Product.hr3d_removeads);
            //});

            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
        }
    }
}